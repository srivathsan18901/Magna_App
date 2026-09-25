using Magna_TestApplication.Models;
using Magna_TestApplication.services;
using System.ComponentModel;

namespace Magna_TestApplication
{
    public partial class Magna : Form
    {
        private TscPrinterService _printerService;

        private PlcService _plcService;
        private QrCodeService _qrCodeService;
        private QrDataService _qrDataService;
        private SampleDataService _sampleDataService;
        private QrDecoderService _qrDecoderService;
        private PlcConfigService _plcConfigService;
        private JsonLogService _jsonLogService;
        private System.Threading.Timer _plcCheckTimer;
        private bool _isCheckingPlc = false;
        private List<FunctionalTestLog> _ftLogsMaster = new();
        private List<TravelAndEnduranceLog> _teLogsMaster = new();
        private BindingList<FunctionalTestLog> _ftLogs;
        private BindingList<TravelAndEnduranceLog> _teLogs;
        private System.Threading.Timer _plcDataTimer;
        private bool _isReadingPlc = false;

        // NEW: Store the mapping from DB
        private List<PlcRegisterMap> _plcMappings = new();
        private bool _wasFtSequenceActive = false;
        private bool _wasTetSequenceActive = false;

        private static readonly Random _rng = new Random();

        private double Rand(double min, double max)
            => Math.Round(min + _rng.NextDouble() * (max - min), 2);
        public Magna()
        {
            InitializeComponent();

            _plcService = new PlcService();
            _qrCodeService = new QrCodeService();
            _qrDataService = new QrDataService();
            _jsonLogService = new JsonLogService();
            _sampleDataService = new SampleDataService();
            _plcConfigService = new PlcConfigService();
            _qrDecoderService = new QrDecoderService();
            _ftLogs = new BindingList<FunctionalTestLog>();
            _teLogs = new BindingList<TravelAndEnduranceLog>();

            // Set default IP and Port (Do NOT connect here)
            PLC_IP.Text = "192.168.3.111";
            PLC_Port.Text = "502";

            // Default date range = last 30 days
            FT_DTP_FROM.Value = TE_DTP_FROM.Value = DateTime.Today.AddDays(-30);
            FT_DTP_TO.Value = TE_DTP_TO.Value = DateTime.Today;

            //SetupFunctionalTestGrid();
            //SetupTravelEnduranceGrid();

            this.Load += Magna_Load;
            this.FormClosing += Magna_FormClosing;
        }
        private void Magna_Load(object sender, EventArgs e)
        {
            // 1. Load base mappings from code
            var baseMappings = PlcRegisterConfig.GetMappings();

            // 2. Apply user overrides (if any)
            ApplyConfigOverrides(baseMappings);

            _plcMappings = baseMappings;
            PopulatePlcConfigTab();

            if (_plcMappings.Count == 0)
            {
                MessageBox.Show("Warning: No PLC register mappings configured.");
                return;
            }

            // 3. Show log folder on the UI
            DB_Lbl.Text = "Logs: " + _jsonLogService.GetLogFolder();
            DB_Lbl.ForeColor = Color.Green;

            // 4. Initialize filter combos
            InitFilterCombos(FT_CMB_SHIFT, new[] { "A", "B", "C" });
            InitFilterCombos(FT_CMB_VARIANT, new[] { "MAGNA-X1", "MAGNA-X2" });
            InitFilterCombos(FT_CMB_RESULT, new[] { "PASS", "FAIL" });

            InitFilterCombos(TE_CMB_SHIFT, new[] { "A", "B", "C" });
            InitFilterCombos(TE_CMB_VARIANT, new[] { "MAGNA-X1", "MAGNA-X2" });
            InitFilterCombos(TE_CMB_RESULT, new[] { "PASS", "FAIL" });

            // 5. Populate report grids from JSON logs (independent of PLC)
            ApplyFunctionalTestFilter();
            ApplyTravelEnduranceFilter();

            // 6. Set Home page TextBoxes to a "waiting" state
            SetHomePageToWaiting();

            // 7. Connect PLC (async). Timer will be started from inside ConnectToPlc
            //    ONLY after a successful connection.
            Task.Run(() => ConnectToPlc());
        }

        private void PopulatePlcConfigTab()
        {
            foreach (var map in _plcMappings)
            {
                // Determine the TextBox name based on the same naming convention
                string textBoxName = GetConfigTextBoxName(map);
                if (string.IsNullOrWhiteSpace(textBoxName)) continue;

                Control[] found = this.Controls.Find(textBoxName, true);
                if (found.Length == 0 || found[0] is not TextBox txtBox) continue;

                txtBox.Text = map.RegisterAddress;
            }

            // Also populate special fields
            PLC_IP_Addr.Text = PLC_IP.Text;      // if you want the config tab to show it
            PLC_Port_Addr.Text = PLC_Port.Text;
        }

        private string GetConfigTextBoxName(PlcRegisterMap map)
        {
            // Meta (Result/Variant/Shift) → use LogPropertyName
            // Measurements → use LogPropertyName  
            // Everything else → use ParameterName
            string key = !string.IsNullOrWhiteSpace(map.LogPropertyName)
                ? map.LogPropertyName
                : map.ParameterName;

            if (string.IsNullOrWhiteSpace(key)) return null;

            // Sanitize: remove spaces, dashes
            key = key.Replace(" ", "").Replace("-", "_");

            return "TXT_" + key;
        }

        private void ApplyConfigOverrides(List<PlcRegisterMap> mappings)
        {
            var overrides = _plcConfigService.RegisterOverrides;
            if (overrides == null || overrides.Count == 0) return;

            foreach (var map in mappings)
            {
                // Key priority: LogPropertyName first, then ParameterName
                string key = !string.IsNullOrWhiteSpace(map.LogPropertyName)
                    ? map.LogPropertyName
                    : map.ParameterName;

                if (string.IsNullOrWhiteSpace(key)) continue;

                if (overrides.TryGetValue(key, out string newAddress)
                    && !string.IsNullOrWhiteSpace(newAddress))
                {
                    map.RegisterAddress = newAddress;
                }
            }
        }

        private void PlcDataTimerCallback(object state)
        {
            if (_isReadingPlc) return;
            _isReadingPlc = true;

            try
            {
                if (!_plcService.IsConnected || _plcMappings.Count == 0)
                    return;

                var addresses = _plcMappings
                    .Select(m => m.RegisterAddress)
                    .Distinct()
                    .ToList();

                var plcValues = _plcService.ReadMultipleRegisters(addresses);

                // --- FT edge detection ---
                bool ftComplete = plcValues.TryGetValue("D102", out string ft) && ft == "1";
                if (ftComplete && !_wasFtSequenceActive)
                {
                    SaveFtSnapshot(plcValues);
                    _wasFtSequenceActive = true;
                }
                else if (!ftComplete && _wasFtSequenceActive)
                {
                    _wasFtSequenceActive = false;
                }

                // --- TET edge detection ---
                bool tetComplete = plcValues.TryGetValue("D158", out string tet) && tet == "1";
                if (tetComplete && !_wasTetSequenceActive)
                {
                    SaveTetSnapshot(plcValues);
                    _wasTetSequenceActive = true;
                }
                else if (!tetComplete && _wasTetSequenceActive)
                {
                    _wasTetSequenceActive = false;
                }

                // --- Home page live update ---
                this.Invoke(new Action(() => UpdateUiFromPlc(plcValues)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("PLC Sync Error: " + ex.Message);
            }
            finally
            {
                _isReadingPlc = false;
            }
        }

        private void SaveFtSnapshot(Dictionary<string, string> plcValues)
        {
            try
            {
                var log = new FunctionalTestLogRecord
                {
                    LoggedAt = DateTime.Now,
                    SerialNumber = GenerateSerialNumber()
                };

                FillFromMappings(log, plcValues, "FT");
                _jsonLogService.AppendFtLog(log);

                this.Invoke(new Action(() => ApplyFunctionalTestFilter()));
                Console.WriteLine($"✔ FT saved @ {log.LoggedAt}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveFtSnapshot Error: " + ex.Message);
            }
        }

        private void SaveTetSnapshot(Dictionary<string, string> plcValues)
        {
            try
            {
                var log = new TravelEnduranceLogRecord
                {
                    LoggedAt = DateTime.Now,
                    SerialNumber = GenerateSerialNumber()
                };

                FillFromMappings(log, plcValues, "TET");
                _jsonLogService.AppendTetLog(log);

                this.Invoke(new Action(() => ApplyTravelEnduranceFilter()));
                Console.WriteLine($"✔ TET saved @ {log.LoggedAt}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveTetSnapshot Error: " + ex.Message);
            }
        }

        private void FillFromMappings<T>(T log, Dictionary<string, string> plcValues, string group) where T : class
        {
            var logType = typeof(T);

            foreach (var map in _plcMappings)
            {
                if (map.LogGroup != group) continue;
                if (string.IsNullOrWhiteSpace(map.LogPropertyName)) continue;
                if (!plcValues.TryGetValue(map.RegisterAddress, out string rawValue)) continue;

                var prop = logType.GetProperty(map.LogPropertyName);
                if (prop == null || !prop.CanWrite) continue;

                try
                {
                    if (prop.PropertyType == typeof(double))
                        prop.SetValue(log, ParseDouble(rawValue));
                    else if (prop.PropertyType == typeof(int))
                        prop.SetValue(log, int.TryParse(rawValue, out int i) ? i : 0);
                    else if (prop.PropertyType == typeof(string))
                        prop.SetValue(log, rawValue ?? "");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Map error {map.RegisterAddress} → {map.LogPropertyName}: {ex.Message}");
                }
            }
        }


        /// <summary>
        /// Safely retrieves a value from the PLC dictionary. Returns "" if not found.
        /// </summary>
        private string GetPlcValue(Dictionary<string, string> plcValues, string address)
        {
            return plcValues.TryGetValue(address, out string val) ? val : "";
        }

        /// <summary>
        /// Converts a PLC string to double. Returns 0 on failure.
        /// </summary>
        private double ParseDouble(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return 0;
            if (value.StartsWith("ERR")) return 0;
            return double.TryParse(value, out double result) ? result : 0;
        }

        /// <summary>
        /// Generates a unique serial number. Replace this with real PLC data if available.
        /// </summary>
        private string GenerateSerialNumber()
        {
            // Format: SN-YYYYMMDD-HHMMSS-XXX
            return $"SN-{DateTime.Now:yyyyMMdd-HHmmss}-{new Random().Next(100, 999)}";
        }

        // --- NEW: Dynamic UI Update Logic ---
        private void UpdateUiFromPlc(Dictionary<string, string> plcValues)
        {
            if (plcValues == null || plcValues.Count == 0)
                return;

            foreach (var map in _plcMappings)
            {
                // ---------------------------------------------
                // Validate mapping
                // ---------------------------------------------
                if (map == null)
                    continue;

                if (string.IsNullOrWhiteSpace(map.RegisterAddress))
                    continue;

                if (string.IsNullOrWhiteSpace(map.UiControlName))
                    continue;

                // ---------------------------------------------
                // Get PLC value
                // ---------------------------------------------
                if (!plcValues.TryGetValue(map.RegisterAddress, out string value))
                    continue;

                // ---------------------------------------------
                // Find UI control
                // ---------------------------------------------
                Control[] controls;

                try
                {
                    controls = this.Controls.Find(
                        map.UiControlName.Trim(),
                        true);
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine(
                        $"[UI ERROR] Invalid UiControlName for register {map.RegisterAddress}");

                    continue;
                }

                if (controls.Length == 0)
                {
                    Console.WriteLine(
                        $"[UI WARNING] Control '{map.UiControlName}' not found " +
                        $"for PLC register '{map.RegisterAddress}'");

                    continue;
                }

                // ---------------------------------------------
                // Update TextBox
                // ---------------------------------------------
                if (controls[0] is TextBox txtBox)
                {
                    txtBox.Text = value;

                    // Reset background to white to signal live data
                    if (txtBox.BackColor != Color.White)
                        txtBox.BackColor = Color.White;
                }
            }
        }


        private void Magna_FormClosing(object sender, FormClosingEventArgs e)
        {
            _plcCheckTimer?.Dispose();
            _plcService?.Disconnect();
        }

        private void PlcCheckTimerCallback(object state)
        {
            // Prevent overlapping checks if the previous one is still running
            if (_isCheckingPlc) return;
            _isCheckingPlc = true;

            try
            {
                UpdatePlcStatus();
            }
            catch (Exception ex)
            {
                Console.WriteLine("PLC Timer Error: " + ex.Message);
            }
            finally
            {
                _isCheckingPlc = false;
            }
        }

        private void ConnectToPlc()
        {
            const string PROBE_REGISTER = "D107";

            string ip = PLC_IP.Text.Trim();
            string portText = PLC_Port.Text.Trim();

            if (string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(portText))
            {
                UpdatePlcLabel("PLC Status: Invalid IP/Port", Color.Red);
                return;
            }

            if (!int.TryParse(portText, out int port))
            {
                UpdatePlcLabel("PLC Status: Invalid Port", Color.Red);
                return;
            }

            UpdatePlcLabelWithProbe("PLC Status: Connecting...", Color.Orange, PROBE_REGISTER, "");

            bool success = _plcService.Connect(ip, port);

            if (!success)
            {
                UpdatePlcLabelWithProbe("PLC Status: Connection Failed", Color.Red, PROBE_REGISTER, "");
                return;
            }

            // Connected — do an immediate probe read
            string probeValue = _plcService.ReadValue(PROBE_REGISTER);

            if (probeValue.StartsWith("ERR"))
            {
                // We connected but the probe failed → likely a protocol mismatch
                UpdatePlcLabelWithProbe("PLC Status: Connected (Probe Failed)", Color.DarkOrange, PROBE_REGISTER, "ERR");
            }
            else
            {
                UpdatePlcLabelWithProbe("PLC Status: Connected", Color.Green, PROBE_REGISTER, probeValue);
            }

            // Start the timer + do the initial read
            StartPlcDataTimer();
            PerformInitialRead();
        }

        private void StartPlcDataTimer()
        {
            // Dispose any previous timer (safety)
            _plcDataTimer?.Dispose();

            // dueTime = 0  → fires immediately once
            // period = 1000 → then fires every 1 second
            _plcDataTimer = new System.Threading.Timer(
                PlcDataTimerCallback,
                null,
                dueTime: 0,
                period: 1000);
        }

        private void PerformInitialRead()
        {
            try
            {
                if (!_plcService.IsConnected || _plcMappings.Count == 0) return;

                var addresses = _plcMappings
                    .Select(m => m.RegisterAddress)
                    .Distinct()
                    .ToList();

                var plcValues = _plcService.ReadMultipleRegisters(addresses);

                // Update Home page on the UI thread
                this.Invoke(new Action(() => UpdateUiFromPlc(plcValues)));

                Console.WriteLine($"[Initial Read] Read {plcValues.Count} values from PLC");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Initial read error: " + ex.Message);
            }
        }

        private void SetHomePageToWaiting()
        {
            foreach (var map in _plcMappings)
            {
                if (string.IsNullOrWhiteSpace(map.UiControlName)) continue;

                Control[] found = this.Controls.Find(map.UiControlName, true);
                if (found.Length == 0 || found[0] is not TextBox txtBox) continue;

                txtBox.Text = "";  // Or use "--" if you prefer
                txtBox.BackColor = Color.FromArgb(245, 245, 245);  // light grey
            }
        }

        private void UpdatePlcLabel(string text, Color color)
        {
            if (this.IsDisposed) return;

            this.Invoke(new Action(() =>
            {
                PLC_LBL.Text = text;
                PLC_LBL.ForeColor = color;
            }));
        }

        /// <summary>
        /// Overload: shows the PLC status and appends a probe register reading.
        /// Example output: "PLC Status: Connected  |  D107 = 25"
        /// </summary>
        private void UpdatePlcLabelWithProbe(string statusText, Color color, string register, string value)
        {
            if (this.IsDisposed) return;

            string display = string.IsNullOrWhiteSpace(value)
                ? $"{statusText}  |  {register} = --"
                : $"{statusText}  |  {register} = {value}";

            this.Invoke(new Action(() =>
            {
                PLC_LBL.Text = display;
                PLC_LBL.ForeColor = color;
            }));
        }

        private void UpdatePlcStatus()
        {
            const string PROBE_REGISTER = "D107";

            if (!_plcService.IsConnected)
            {
                UpdatePlcLabelWithProbe("PLC Status: Disconnected", Color.Red, PROBE_REGISTER, "");
                ConnectToPlc();
                return;
            }

            string probeValue = _plcService.ReadValue(PROBE_REGISTER);

            if (probeValue.StartsWith("ERR"))
            {
                // Show the actual error message in the console
                Console.WriteLine($"[Probe Error] {probeValue}");

                // Truncate the message for the label
                string shortMsg = probeValue.Length > 40
                    ? probeValue.Substring(0, 37) + "..."
                    : probeValue;

                UpdatePlcLabelWithProbe("PLC Status: Probe Failed", Color.DarkOrange, PROBE_REGISTER, shortMsg);
                // Do NOT disconnect — the connection itself is fine
            }
            else
            {
                UpdatePlcLabelWithProbe("PLC Status: Connected", Color.Green, PROBE_REGISTER, probeValue);
            }
        }

        private void InitFilterCombos(ComboBox cmb, string[] values)
        {
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb.Items.Clear();
            cmb.Items.Add("(All)");
            cmb.Items.AddRange(values);
            cmb.SelectedIndex = 0;
        }



        private void SampleDataTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // This will now try to reconnect if disconnected.
                // If the PLC is unreachable, it will block for 3 seconds.
                // Consider moving this to a background thread if the UI still feels laggy.
                UpdatePlcStatus();

                // --- SAMPLE DATA GENERATION IS NOW COMMENTED OUT AS REQUESTED ---
                //// 1. Generate Functional Test Log
                //FunctionalTestLog ftLog = _sampleDataService.GenerateNextLog();

                //// 2. Add to MASTER list
                //_ftLogsMaster.Insert(0, ftLog);
                //QTY_LBL.Text = ftLog.SNo.ToString();

                //// 3. Generate QR
                //string qrData = _qrDataService.GenerateQrData(
                //    ftLog.LoggedAt, ftLog.Shift, ftLog.Variant, ftLog.SNo);
                //DisplayQr(qrData);

                //// 4. Travel & Endurance
                //ProcessTravelAndEnduranceTest(qrData);

                //// 5. Re-apply filters (so grids stay up-to-date)
                //ApplyFunctionalTestFilter();
                //ApplyTravelEnduranceFilter();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void ProcessTravelAndEnduranceTest(string qrData)
        {
            var decoded = _qrDecoderService.DecodeQrData(qrData);

            string travelResult = _sampleDataService.GetSampleResult();
            string enduranceResult = _sampleDataService.GetSampleResult();

            var teLog = new TravelAndEnduranceLog
            {
                SNo = _teLogsMaster.Count + 1,
                LoggedAt = decoded.dateTime,
                Shift = decoded.shift,
                Variant = decoded.variant,
                SerialNumber = decoded.serialNumber,
                TravelResult = travelResult,
                EnduranceResult = enduranceResult
            };

            _teLogsMaster.Insert(0, teLog);
        }
        private void ApplyFunctionalTestFilter()
        {
            DateTime fromDate = FT_DTP_FROM.Value.Date;
            DateTime toDate = FT_DTP_TO.Value.Date.AddDays(1).AddSeconds(-1);

            var logs = _jsonLogService.GetFtLogs(fromDate, toDate);
            IEnumerable<FunctionalTestLogRecord> query = logs;

            if (!string.IsNullOrWhiteSpace(FT_TXT_TIME.Text))
                query = query.Where(x => x.Time.StartsWith(FT_TXT_TIME.Text.Trim()));

            // Only apply if a REAL value is selected (not empty, not "(All)")
            string variant = FT_CMB_VARIANT.Text?.Trim() ?? "";
            if (!string.IsNullOrEmpty(variant) && variant != "(All)")
                query = query.Where(x => x.Variant == variant);

            string shift = FT_CMB_SHIFT.Text?.Trim() ?? "";
            if (!string.IsNullOrEmpty(shift) && shift != "(All)")
                query = query.Where(x => x.Shift == shift);

            string result = FT_CMB_RESULT.Text?.Trim() ?? "";
            if (!string.IsNullOrEmpty(result) && result != "(All)")
                query = query.Where(x => x.Result == result);

            var list = query.ToList();
            Console.WriteLine($"[FT] from={fromDate:yyyy-MM-dd} to={toDate:yyyy-MM-dd} " +
                              $"totalInRange={logs.Count} afterFilter={list.Count}");

            FT_DGV.DataSource = new BindingList<FunctionalTestLogRecord>(list);
            FormatFTGrid();
        }

        private void ApplyTravelEnduranceFilter()
        {
            DateTime fromDate = TE_DTP_FROM.Value.Date;
            DateTime toDate = TE_DTP_TO.Value.Date.AddDays(1).AddSeconds(-1);

            var logs = _jsonLogService.GetTetLogs(fromDate, toDate);
            IEnumerable<TravelEnduranceLogRecord> query = logs;

            if (!string.IsNullOrWhiteSpace(TE_TXT_TIME.Text))
                query = query.Where(x => x.Time.StartsWith(TE_TXT_TIME.Text.Trim()));
            if (TE_CMB_VARIANT.Text != "(All)")
                query = query.Where(x => x.Variant == TE_CMB_VARIANT.Text);
            if (TE_CMB_SHIFT.Text != "(All)")
                query = query.Where(x => x.Shift == TE_CMB_SHIFT.Text);
            if (TE_CMB_RESULT.Text != "(All)")
                query = query.Where(x => x.Result == TE_CMB_RESULT.Text);

            TET_DGV.DataSource = new BindingList<TravelEnduranceLogRecord>(query.ToList());
            FormatTetGrid();
        }

        private void FormatFTGrid()
        {
            if (FT_DGV.Columns.Count == 0)
                return;

            var visibleProps = new HashSet<string>(
                StringComparer.OrdinalIgnoreCase)
    {
        "Date",
        "Time",
        "Shift",
        "Variant",
        "SerialNumber",
        "Result"
    };

            // Add PLC mapped properties
            foreach (var map in _plcMappings.Where(m =>
                     m.LogGroup == "FT" &&
                     m.ShowInReport &&
                     !string.IsNullOrEmpty(m.LogPropertyName)))
            {
                visibleProps.Add(map.LogPropertyName);
            }

            foreach (DataGridViewColumn col in FT_DGV.Columns)
            {
                if (col.Visible)
                {
                    col.HeaderCell.Style.WrapMode =
                        DataGridViewTriState.True;

                    col.MinimumWidth = 80;
                }
            }

            foreach (DataGridViewColumn col in FT_DGV.Columns)
            {
                // IMPORTANT:
                // Use DataPropertyName, NOT column Name
                string propertyName = col.DataPropertyName;

                if (string.IsNullOrWhiteSpace(propertyName))
                    propertyName = col.Name;

                col.Visible =
                    !string.Equals(propertyName, "Id",
                        StringComparison.OrdinalIgnoreCase)
                    && visibleProps.Contains(propertyName);
            }

            FT_DGV.RowPrePaint -= FT_DGV_RowPrePaint;
            FT_DGV.RowPrePaint += FT_DGV_RowPrePaint;
        }

        private void FormatTetGrid()
        {
            if (TET_DGV.Columns.Count == 0)
                return;

            var visibleProps = new HashSet<string>(
                StringComparer.OrdinalIgnoreCase)
    {
        "Date",
        "Time",
        "Shift",
        "Variant",
        "SerialNumber",
        "Result"
    };

            // Add PLC mapped properties
            foreach (var map in _plcMappings.Where(m =>
                     m.LogGroup == "TET" &&
                     m.ShowInReport &&
                     !string.IsNullOrEmpty(m.LogPropertyName)))
            {
                visibleProps.Add(map.LogPropertyName);
            }

            foreach (DataGridViewColumn col in TET_DGV.Columns)
            {
                if (col.Visible)
                {
                    col.HeaderCell.Style.WrapMode =
                        DataGridViewTriState.True;

                    col.MinimumWidth = 80;
                }
            }

            foreach (DataGridViewColumn col in TET_DGV.Columns)
            {
                // IMPORTANT:
                // Use DataPropertyName
                string propertyName = col.DataPropertyName;

                if (string.IsNullOrWhiteSpace(propertyName))
                    propertyName = col.Name;

                col.Visible =
                    !string.Equals(propertyName, "Id",
                        StringComparison.OrdinalIgnoreCase)
                    && visibleProps.Contains(propertyName);
            }

            TET_DGV.RowPrePaint -= TET_DGV_RowPrePaint;
            TET_DGV.RowPrePaint += TET_DGV_RowPrePaint;
        }

        private void FT_DGV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            PaintFailRow(FT_DGV, e);
        }

        private void TET_DGV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            PaintFailRow(TET_DGV, e);
        }

        private void PaintFailRow(DataGridView grid, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= grid.Rows.Count) return;
            if (!grid.Columns.Contains("Result")) return;

            try
            {
                var row = grid.Rows[e.RowIndex];
                var resultCell = row.Cells["Result"];
                if (resultCell?.Value == null) return;

                row.DefaultCellStyle.BackColor =
                    resultCell.Value.ToString() == "FAIL" ? Color.MistyRose : Color.White;
            }
            catch { }
        }

        // ---------------------------------------------------------------
        // TRAVEL & ENDURANCE FILTER
        // ---------------------------------------------------------------

        private void FT_BTN_FILTER_Click_1(object sender, EventArgs e) => ApplyFunctionalTestFilter();
        private void FT_BTN_CLEAR_Click_1(object sender, EventArgs e)
        {
            FT_DTP_FROM.Value = DateTime.Today.AddDays(-30);
            FT_DTP_TO.Value = DateTime.Today;
            FT_TXT_TIME.Text = "";
            FT_CMB_VARIANT.SelectedIndex = 0;
            FT_CMB_SHIFT.SelectedIndex = 0;
            FT_CMB_RESULT.SelectedIndex = 0;
            ApplyFunctionalTestFilter();
        }

        private void TE_BTN_FILTER_Click_1(object sender, EventArgs e) => ApplyTravelEnduranceFilter();
        private void TE_BTN_CLEAR_Click_1(object sender, EventArgs e)
        {
            TE_DTP_FROM.Value = DateTime.Today.AddDays(-30);
            TE_DTP_TO.Value = DateTime.Today;
            TE_TXT_TIME.Text = "";
            TE_CMB_VARIANT.SelectedIndex = 0;
            TE_CMB_SHIFT.SelectedIndex = 0;
            TE_CMB_RESULT.SelectedIndex = 0;
            ApplyTravelEnduranceFilter();
        }

        private void ExportBTN_Click(object sender, EventArgs e) { }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }

        private void FT_DGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }


        /// <summary>
        /// Builds one fake Functional Test record with random values inside spec,
        /// then 20% of the time forces a FAIL by pushing one value out of range.
        /// </summary>
        private FunctionalTestLogRecord BuildSampleFtRecord()
        {
            var log = new FunctionalTestLogRecord
            {
                LoggedAt = DateTime.Now,
                Shift = new[] { "A", "B", "C" }[_rng.Next(3)],
                Variant = new[] { "MAGNA-X1", "MAGNA-X2" }[_rng.Next(2)],
                SerialNumber = GenerateSerialNumber(),
                Result = "PASS"
            };

            // --- Seal Load ---
            log.SealLoad_Min = 10.5;
            log.SealLoad_Max = 25.0;
            log.SealLoad_Actual = Rand(12.0, 23.0);

            // --- Power Lock Current ---
            log.PowerLockCurrent_Min = 0.8;
            log.PowerLockCurrent_Max = 2.5;
            log.PowerLockCurrent_Actual = Rand(1.0, 2.2);

            // --- Power Unlock Current ---
            log.PowerUnlockCurrent_Min = 0.7;
            log.PowerUnlockCurrent_Max = 2.3;
            log.PowerUnlockCurrent_Actual = Rand(0.9, 2.0);

            // --- Key Lock ---
            log.KeyLockEffort_Min = 5.0;
            log.KeyLockEffort_Max = 15.0;
            log.KeyLockEffort_Actual = Rand(6.0, 14.0);

            log.KeyLockPreTravel_Min = 1.0;
            log.KeyLockPreTravel_Max = 4.0;
            log.KeyLockPreTravel_Actual = Rand(1.5, 3.5);

            log.KeyLockLockTravel_Min = 3.0;
            log.KeyLockLockTravel_Max = 8.0;
            log.KeyLockLockTravel_Actual = Rand(4.0, 7.0);

            log.KeyLockFullTravel_Min = 6.0;
            log.KeyLockFullTravel_Max = 12.0;
            log.KeyLockFullTravel_Actual = Rand(7.0, 11.0);

            // --- Key Unlock ---
            log.KeyUnlockEffort_Min = 5.0;
            log.KeyUnlockEffort_Max = 15.0;
            log.KeyUnlockEffort_Actual = Rand(6.0, 14.0);

            log.KeyUnlockPreTravel_Min = 1.0;
            log.KeyUnlockPreTravel_Max = 4.0;
            log.KeyUnlockPreTravel_Actual = Rand(1.5, 3.5);

            log.KeyUnlockLockTravel_Min = 3.0;
            log.KeyUnlockLockTravel_Max = 8.0;
            log.KeyUnlockLockTravel_Actual = Rand(4.0, 7.0);

            log.KeyUnlockFullTravel_Min = 6.0;
            log.KeyUnlockFullTravel_Max = 12.0;
            log.KeyUnlockFullTravel_Actual = Rand(7.0, 11.0);

            // --- Child Lock ---
            log.ChildLockEffort_Min = 4.0;
            log.ChildLockEffort_Max = 12.0;
            log.ChildLockEffort_Actual = Rand(5.0, 11.0);

            log.ChildLockTravel_Min = 2.0;
            log.ChildLockTravel_Max = 6.0;
            log.ChildLockTravel_Actual = Rand(2.5, 5.5);

            log.ChildUnlockEffort_Min = 4.0;
            log.ChildUnlockEffort_Max = 12.0;
            log.ChildUnlockEffort_Actual = Rand(5.0, 11.0);

            log.ChildUnlockTravel_Min = 2.0;
            log.ChildUnlockTravel_Max = 6.0;
            log.ChildUnlockTravel_Actual = Rand(2.5, 5.5);

            // --- EMG Lock ---
            log.EmgLockTorque_Min = 2.0;
            log.EmgLockTorque_Max = 8.0;
            log.EmgLockTorque_Actual = Rand(3.0, 7.0);

            log.EmgLockAngle_Min = 15.0;
            log.EmgLockAngle_Max = 45.0;
            log.EmgLockAngle_Actual = Rand(20.0, 40.0);

            // 20% chance to inject a FAIL
            if (_rng.Next(100) < 20)
            {
                log.Result = "FAIL";
                log.SealLoad_Actual = Rand(26.0, 30.0);   // above max
            }

            return log;
        }

        /// <summary>
        /// Builds one fake Travel & Endurance record.
        /// </summary>
        private TravelEnduranceLogRecord BuildSampleTetRecord()
        {
            var log = new TravelEnduranceLogRecord
            {
                LoggedAt = DateTime.Now,
                Shift = new[] { "A", "B", "C" }[_rng.Next(3)],
                Variant = new[] { "MAGNA-X1", "MAGNA-X2" }[_rng.Next(2)],
                SerialNumber = GenerateSerialNumber(),
                Result = "PASS"
            };

            log.InsideLockEffort_Min = 4.0; log.InsideLockEffort_Max = 12.0;
            log.InsideLockEffort_Actual = Rand(5.0, 11.0);

            log.InsideLockTravel_Min = 2.0; log.InsideLockTravel_Max = 6.0;
            log.InsideLockTravel_Actual = Rand(2.5, 5.5);

            log.InsideUnlockEffort_Min = 4.0; log.InsideUnlockEffort_Max = 12.0;
            log.InsideUnlockEffort_Actual = Rand(5.0, 11.0);

            log.InsideUnlockTravel_Min = 2.0; log.InsideUnlockTravel_Max = 6.0;
            log.InsideUnlockTravel_Actual = Rand(2.5, 5.5);

            log.InsideReleaseEffort_Min = 3.0; log.InsideReleaseEffort_Max = 10.0;
            log.InsideReleaseEffort_Actual = Rand(4.0, 9.0);

            log.InsideReleasePreTravel_Min = 1.0; log.InsideReleasePreTravel_Max = 3.5;
            log.InsideReleasePreTravel_Actual = Rand(1.2, 3.2);

            log.InsideReleaseReleaseTravel_Min = 2.5; log.InsideReleaseReleaseTravel_Max = 7.0;
            log.InsideReleaseReleaseTravel_Actual = Rand(3.0, 6.5);

            log.InsideReleaseFullTravel_Min = 5.0; log.InsideReleaseFullTravel_Max = 11.0;
            log.InsideReleaseFullTravel_Actual = Rand(6.0, 10.0);

            log.OutsideReleaseEffort_Min = 3.0; log.OutsideReleaseEffort_Max = 10.0;
            log.OutsideReleaseEffort_Actual = Rand(4.0, 9.0);

            log.OutsideReleasePreTravel_Min = 1.0; log.OutsideReleasePreTravel_Max = 3.5;
            log.OutsideReleasePreTravel_Actual = Rand(1.2, 3.2);

            log.OutsideReleaseReleaseTravel_Min = 2.5; log.OutsideReleaseReleaseTravel_Max = 7.0;
            log.OutsideReleaseReleaseTravel_Actual = Rand(3.0, 6.5);

            log.OutsideReleaseFullTravel_Min = 5.0; log.OutsideReleaseFullTravel_Max = 11.0;
            log.OutsideReleaseFullTravel_Actual = Rand(6.0, 10.0);

            // 20% chance to inject a FAIL
            if (_rng.Next(100) < 20)
            {
                log.Result = "FAIL";
                log.InsideLockEffort_Actual = Rand(13.0, 15.0);  // above max
            }

            return log;
        }

        private void SampleFtBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var log = BuildSampleFtRecord();

                // 1. Persist to JSON
                _jsonLogService.AppendFtLog(log);

                // 2. Refresh the FT grid
                ApplyFunctionalTestFilter();

                // 3. NEW: Push the record to Home page TextBoxes
                PushFtToHomePage(log);

                Console.WriteLine($"✔ Sample FT inserted: {log.SerialNumber} [{log.Result}]");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sample FT insert failed: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SampleTetBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var log = BuildSampleTetRecord();

                _jsonLogService.AppendTetLog(log);
                ApplyTravelEnduranceFilter();

                // NEW: Push to Home page
                PushTetToHomePage(log);

                Console.WriteLine($"✔ Sample TET inserted: {log.SerialNumber} [{log.Result}]");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sample TET insert failed: " + ex.Message,
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Pushes a FunctionalTestLogRecord's values into the Home page TextBoxes.
        /// Uses the same _plcMappings to find the right TextBox by LogPropertyName.
        /// </summary>
        private void PushFtToHomePage(FunctionalTestLogRecord log)
        {
            var logType = typeof(FunctionalTestLogRecord);

            foreach (var map in _plcMappings)
            {
                // Only handle FT registers
                if (map.LogGroup != "FT") continue;
                if (string.IsNullOrWhiteSpace(map.LogPropertyName)) continue;
                if (string.IsNullOrWhiteSpace(map.UiControlName)) continue;

                var prop = logType.GetProperty(map.LogPropertyName);
                if (prop == null) continue;

                object value = prop.GetValue(log);
                if (value == null) continue;

                // Find the TextBox on the Home page
                Control[] controls = this.Controls.Find(map.UiControlName, true);
                if (controls.Length == 0 || controls[0] is not TextBox txtBox) continue;

                // Format the value appropriately
                string text = value switch
                {
                    double d => d.ToString("0.##"),
                    int i => i.ToString(),
                    string s => s,
                    _ => value.ToString() ?? ""
                };

                txtBox.Text = text;
            }

            // Also push the meta (Shift / Variant / Result) into any Home-page textboxes
            // that share those UiControlNames in the config (if you have them)
            foreach (var map in _plcMappings)
            {
                if (map.LogGroup != "FT") continue;
                if (string.IsNullOrWhiteSpace(map.UiControlName)) continue;

                string? text = map.LogPropertyName switch
                {
                    "Shift" => log.Shift,
                    "Variant" => log.Variant,
                    "Result" => log.Result,
                    _ => null
                };

                if (text == null) continue;

                Control[] controls = this.Controls.Find(map.UiControlName, true);
                if (controls.Length > 0 && controls[0] is TextBox txtBox)
                    txtBox.Text = text;
            }
        }

        /// <summary>
        /// Same as above but for TravelEnduranceLogRecord.
        /// </summary>
        private void PushTetToHomePage(TravelEnduranceLogRecord log)
        {
            var logType = typeof(TravelEnduranceLogRecord);

            foreach (var map in _plcMappings)
            {
                if (map.LogGroup != "TET") continue;
                if (string.IsNullOrWhiteSpace(map.LogPropertyName)) continue;
                if (string.IsNullOrWhiteSpace(map.UiControlName)) continue;

                var prop = logType.GetProperty(map.LogPropertyName);
                if (prop == null) continue;

                object value = prop.GetValue(log);
                if (value == null) continue;

                Control[] controls = this.Controls.Find(map.UiControlName, true);
                if (controls.Length == 0 || controls[0] is not TextBox txtBox) continue;

                string text = value switch
                {
                    double d => d.ToString("0.##"),
                    int i => i.ToString(),
                    string s => s,
                    _ => value.ToString() ?? ""
                };

                txtBox.Text = text;
            }

            // Meta push
            foreach (var map in _plcMappings)
            {
                if (map.LogGroup != "TET") continue;
                if (string.IsNullOrWhiteSpace(map.UiControlName)) continue;

                string? text = map.LogPropertyName switch
                {
                    "Shift" => log.Shift,
                    "Variant" => log.Variant,
                    "Result" => log.Result,
                    _ => null
                };

                if (text == null) continue;

                Control[] controls = this.Controls.Find(map.UiControlName, true);
                if (controls.Length > 0 && controls[0] is TextBox txtBox)
                    txtBox.Text = text;
            }
        }

        private void SavePLC_BTN_Click(object sender, EventArgs e)
        {
            try
            {
                var overrides = new Dictionary<string, string>();

                foreach (var map in _plcMappings)
                {
                    string textBoxName = GetConfigTextBoxName(map);
                    if (string.IsNullOrWhiteSpace(textBoxName)) continue;

                    Control[] found = this.Controls.Find(textBoxName, true);
                    if (found.Length == 0 || found[0] is not TextBox txtBox) continue;

                    string newAddress = txtBox.Text.Trim().ToUpper();

                    // Validate address format (D100, M50, X0, Y0, etc.)
                    if (!IsValidPlcAddress(newAddress))
                    {
                        MessageBox.Show($"Invalid PLC address: '{newAddress}' for {map.ParameterName}",
                                        "Validation Error",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Save only if the address has changed OR always save everything
                    string key = !string.IsNullOrWhiteSpace(map.LogPropertyName)
                        ? map.LogPropertyName
                        : map.ParameterName;

                    if (!string.IsNullOrWhiteSpace(key))
                        overrides[key] = newAddress;
                }

                // Persist to JSON
                _plcConfigService.Save(overrides);

                // Re-apply to in-memory mappings
                ApplyConfigOverrides(_plcMappings);

                // In SavePLC_BTN_Click, after saving register overrides:
                string newIp = PLC_IP.Text.Trim();
                string newPort = PLC_Port.Text.Trim();

                if (!string.IsNullOrWhiteSpace(newIp))
                {
                    PLC_IP.Text = newIp;
                    overrides["PLC_IP"] = newIp;
                }

                if (!string.IsNullOrWhiteSpace(newPort) && int.TryParse(newPort, out _))
                {
                    PLC_Port.Text = newPort;
                    overrides["PLC_Port"] = newPort;
                }

                MessageBox.Show($"PLC config saved successfully.\n\nFile: {_plcConfigService.GetConfigPath()}",
                                "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                Console.WriteLine($"✔ Saved {overrides.Count} register overrides");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to save PLC config:\n" + ex.Message,
                                "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidPlcAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return false;

            // Accept D100, M50, X0, Y0, W100 — with optional bit suffix (e.g., D100.1)
            return System.Text.RegularExpressions.Regex.IsMatch(
                address,
                @"^[DWMXY]\d+(\.\d+)?$");
        }

        private void RestoreDefaults_BTN_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "This will reset all register addresses to their default values.\n\nContinue?",
                "Confirm Reset",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes) return;

            try
            {
                // Clear overrides and save empty
                _plcConfigService.Save(new Dictionary<string, string>());

                // Reload base mappings from code
                _plcMappings = PlcRegisterConfig.GetMappings();

                // Repopulate TextBoxes
                PopulatePlcConfigTab();

                MessageBox.Show("Register addresses restored to defaults.",
                                "Restored",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Restore failed:\n" + ex.Message,
                                "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Reload_BTN_Click(object sender, EventArgs e)
        {
            _plcConfigService.Load();                 // Re-read JSON from disk
            _plcMappings = PlcRegisterConfig.GetMappings();
            ApplyConfigOverrides(_plcMappings);
            PopulatePlcConfigTab();
        }
    }
}