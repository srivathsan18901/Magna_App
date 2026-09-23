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

        // NEW: QR Decoder Service
        private QrDecoderService _qrDecoderService;

        private DatabaseService _dbService;

        private System.Threading.Timer _plcCheckTimer;
        private bool _isCheckingPlc = false; // Prevents overlapping checks

        // Master data (never filtered)
        private List<FunctionalTestLog> _ftLogsMaster = new();
        private List<TravelAndEnduranceLog> _teLogsMaster = new();

        // Bound data (filtered view shown in grid)
        private BindingList<FunctionalTestLog> _ftLogs;
        private BindingList<TravelAndEnduranceLog> _teLogs;

        private System.Threading.Timer _plcDataTimer;
        private bool _isReadingPlc = false;


        // NEW: Store the mapping from DB
        private List<PlcRegisterMap> _plcMappings = new();
        private bool _wasSequenceActive = false;
        public Magna()
        {
            InitializeComponent();

            _plcService = new PlcService();
            _qrCodeService = new QrCodeService();
            _qrDataService = new QrDataService();
            _dbService = new DatabaseService();
            _sampleDataService = new SampleDataService();
            _qrDecoderService = new QrDecoderService(); // Initialize decoder

            _ftLogs = new BindingList<FunctionalTestLog>();
            _teLogs = new BindingList<TravelAndEnduranceLog>();

            // Set default IP and Port (Do NOT connect here)
            PLC_IP.Text = "192.168.1.10";
            PLC_Port.Text = "5000";

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
            TestAndShowDbStatus();
            // 1. Load Mappings from DB
            try
            {
                _plcMappings = _dbService.GetPlcMappings();
                if (_plcMappings.Count == 0)
                {
                    MessageBox.Show("Warning: No PLC register mappings found in Database.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("DB Error: " + ex.Message);
            }

            // 2. Initial PLC Connection
            Task.Run(() => ConnectToPlc());

            // 3. Start the PLC Data Sync Timer (every 1 second for live feel)
            _plcDataTimer = new System.Threading.Timer(PlcDataTimerCallback, null, 2000, 1000);
        }

        private void TestAndShowDbStatus()
        {
            var result = _dbService.TestDatabaseConnection();

            // Update the DB_LBL safely (since this might be called from a background thread in the future)
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => UpdateDbLabel(result.Success, result.Message)));
            }
            else
            {
                UpdateDbLabel(result.Success, result.Message);
            }
        }

        private void UpdateDbLabel(bool success, string message)
        {
            if (DB_Lbl == null) return; // Safety check

            DB_Lbl.Text = message;
            DB_Lbl.ForeColor = success ? Color.Green : Color.Red;
        }

        // --- NEW: Background Worker for reading PLC and updating UI ---
        private void PlcDataTimerCallback(object state)
        {
            if (_isReadingPlc) return;
            _isReadingPlc = true;

            try
            {
                // 1. Periodically verify DB is still up
                TestAndShowDbStatus();

                if (_plcService.IsConnected && _plcMappings.Count > 0)
                {
                    // 2. Read all mapped registers from PLC
                    var addresses = _plcMappings.Select(m => m.RegisterAddress).Distinct().ToList();
                    var plcValues = _plcService.ReadMultipleRegisters(addresses);

                    // =====================================================
                    // 3. EDGE DETECTION on D102 (Sequence Start Acknowledgement)
                    // =====================================================
                    bool isSequenceComplete =
                        plcValues.TryGetValue("D102", out string seqAck) && seqAck == "1";

                    if (isSequenceComplete && !_wasSequenceActive)
                    {
                        // Rising edge: 0 → 1. A test just completed.
                        SaveSnapshot(plcValues);
                        _wasSequenceActive = true;
                    }
                    else if (!isSequenceComplete && _wasSequenceActive)
                    {
                        // Falling edge: 1 → 0. Test cycle finished, ready for next.
                        _wasSequenceActive = false;
                    }

                    // =====================================================
                    // 4. Update Home page with LIVE values
                    // =====================================================
                    this.Invoke(new Action(() =>
                    {
                        UpdateUiFromPlc(plcValues);
                    }));
                }
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

        private void SaveSnapshot(Dictionary<string, string> plcValues)
        {
            try
            {
                // Build the log object
                var log = new TestLog
                {
                    // --- Meta ---
                    LoggedAt = DateTime.Now,  // Local PC time as requested
                    Shift = GetPlcValue(plcValues, "D105"),
                    Variant = GetPlcValue(plcValues, "D104"),
                    Result = GetPlcValue(plcValues, "D103"),
                    SerialNumber = GenerateSerialNumber(),

                    // =========================================
                    // FUNCTIONAL TEST
                    // =========================================
                    SealLoad_Min = ParseDouble(GetPlcValue(plcValues, "D106")),
                    SealLoad_Max = ParseDouble(GetPlcValue(plcValues, "D107")),
                    SealLoad_Actual = ParseDouble(GetPlcValue(plcValues, "D108")),

                    PowerLockCurrent_Min = ParseDouble(GetPlcValue(plcValues, "D109")),
                    PowerLockCurrent_Max = ParseDouble(GetPlcValue(plcValues, "D110")),
                    PowerLockCurrent_Actual = ParseDouble(GetPlcValue(plcValues, "D111")),

                    PowerUnlockCurrent_Min = ParseDouble(GetPlcValue(plcValues, "D112")),
                    PowerUnlockCurrent_Max = ParseDouble(GetPlcValue(plcValues, "D113")),
                    PowerUnlockCurrent_Actual = ParseDouble(GetPlcValue(plcValues, "D114")),

                    KeyLockEffort_Min = ParseDouble(GetPlcValue(plcValues, "D115")),
                    KeyLockEffort_Max = ParseDouble(GetPlcValue(plcValues, "D116")),
                    KeyLockEffort_Actual = ParseDouble(GetPlcValue(plcValues, "D117")),

                    KeyLockPreTravel_Min = ParseDouble(GetPlcValue(plcValues, "D118")),
                    KeyLockPreTravel_Max = ParseDouble(GetPlcValue(plcValues, "D119")),
                    KeyLockPreTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D120")),

                    KeyLockLockTravel_Min = ParseDouble(GetPlcValue(plcValues, "D121")),
                    KeyLockLockTravel_Max = ParseDouble(GetPlcValue(plcValues, "D122")),
                    KeyLockLockTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D123")),

                    KeyLockFullTravel_Min = ParseDouble(GetPlcValue(plcValues, "D124")),
                    KeyLockFullTravel_Max = ParseDouble(GetPlcValue(plcValues, "D125")),
                    KeyLockFullTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D126")),

                    KeyUnlockEffort_Min = ParseDouble(GetPlcValue(plcValues, "D127")),
                    KeyUnlockEffort_Max = ParseDouble(GetPlcValue(plcValues, "D128")),
                    KeyUnlockEffort_Actual = ParseDouble(GetPlcValue(plcValues, "D129")),

                    KeyUnlockPreTravel_Min = ParseDouble(GetPlcValue(plcValues, "D130")),
                    KeyUnlockPreTravel_Max = ParseDouble(GetPlcValue(plcValues, "D131")),
                    KeyUnlockPreTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D132")),

                    KeyUnlockLockTravel_Min = ParseDouble(GetPlcValue(plcValues, "D133")),
                    KeyUnlockLockTravel_Max = ParseDouble(GetPlcValue(plcValues, "D134")),
                    KeyUnlockLockTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D135")),

                    KeyUnlockFullTravel_Min = ParseDouble(GetPlcValue(plcValues, "D136")),
                    KeyUnlockFullTravel_Max = ParseDouble(GetPlcValue(plcValues, "D137")),
                    KeyUnlockFullTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D138")),

                    ChildLockEffort_Min = ParseDouble(GetPlcValue(plcValues, "D139")),
                    ChildLockEffort_Max = ParseDouble(GetPlcValue(plcValues, "D140")),
                    ChildLockEffort_Actual = ParseDouble(GetPlcValue(plcValues, "D141")),

                    ChildLockTravel_Min = ParseDouble(GetPlcValue(plcValues, "D142")),
                    ChildLockTravel_Max = ParseDouble(GetPlcValue(plcValues, "D143")),
                    ChildLockTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D144")),

                    ChildUnlockEffort_Min = ParseDouble(GetPlcValue(plcValues, "D145")),
                    ChildUnlockEffort_Max = ParseDouble(GetPlcValue(plcValues, "D146")),
                    ChildUnlockEffort_Actual = ParseDouble(GetPlcValue(plcValues, "D147")),

                    ChildUnlockTravel_Min = ParseDouble(GetPlcValue(plcValues, "D148")),
                    ChildUnlockTravel_Max = ParseDouble(GetPlcValue(plcValues, "D149")),
                    ChildUnlockTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D150")),

                    EmgLockTorque_Min = ParseDouble(GetPlcValue(plcValues, "D151")),
                    EmgLockTorque_Max = ParseDouble(GetPlcValue(plcValues, "D152")),
                    EmgLockTorque_Actual = ParseDouble(GetPlcValue(plcValues, "D153")),

                    EmgLockAngle_Min = ParseDouble(GetPlcValue(plcValues, "D154")),
                    EmgLockAngle_Max = ParseDouble(GetPlcValue(plcValues, "D155")),
                    EmgLockAngle_Actual = ParseDouble(GetPlcValue(plcValues, "D156")),

                    // =========================================
                    // TRAVEL & ENDURANCE TEST
                    // =========================================
                    InsideLockEffort_Min = ParseDouble(GetPlcValue(plcValues, "D157")),
                    InsideLockEffort_Max = ParseDouble(GetPlcValue(plcValues, "D158")),
                    InsideLockEffort_Actual = ParseDouble(GetPlcValue(plcValues, "D159")),

                    InsideLockTravel_Min = ParseDouble(GetPlcValue(plcValues, "D160")),
                    InsideLockTravel_Max = ParseDouble(GetPlcValue(plcValues, "D161")),
                    InsideLockTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D162")),

                    InsideUnlockEffort_Min = ParseDouble(GetPlcValue(plcValues, "D163")),
                    InsideUnlockEffort_Max = ParseDouble(GetPlcValue(plcValues, "D164")),
                    InsideUnlockEffort_Actual = ParseDouble(GetPlcValue(plcValues, "D165")),

                    InsideUnlockTravel_Min = ParseDouble(GetPlcValue(plcValues, "D166")),
                    InsideUnlockTravel_Max = ParseDouble(GetPlcValue(plcValues, "D167")),
                    InsideUnlockTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D168")),

                    InsideReleaseEffort_Min = ParseDouble(GetPlcValue(plcValues, "D169")),
                    InsideReleaseEffort_Max = ParseDouble(GetPlcValue(plcValues, "D170")),
                    InsideReleaseEffort_Actual = ParseDouble(GetPlcValue(plcValues, "D171")),

                    InsideReleasePreTravel_Min = ParseDouble(GetPlcValue(plcValues, "D172")),
                    InsideReleasePreTravel_Max = ParseDouble(GetPlcValue(plcValues, "D173")),
                    InsideReleasePreTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D174")),

                    InsideReleaseReleaseTravel_Min = ParseDouble(GetPlcValue(plcValues, "D175")),
                    InsideReleaseReleaseTravel_Max = ParseDouble(GetPlcValue(plcValues, "D176")),
                    InsideReleaseReleaseTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D177")),

                    InsideReleaseFullTravel_Min = ParseDouble(GetPlcValue(plcValues, "D178")),
                    InsideReleaseFullTravel_Max = ParseDouble(GetPlcValue(plcValues, "D179")),
                    InsideReleaseFullTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D180")),

                    OutsideReleaseEffort_Min = ParseDouble(GetPlcValue(plcValues, "D181")),
                    OutsideReleaseEffort_Max = ParseDouble(GetPlcValue(plcValues, "D182")),
                    OutsideReleaseEffort_Actual = ParseDouble(GetPlcValue(plcValues, "D183")),

                    OutsideReleasePreTravel_Min = ParseDouble(GetPlcValue(plcValues, "D184")),
                    OutsideReleasePreTravel_Max = ParseDouble(GetPlcValue(plcValues, "D185")),
                    OutsideReleasePreTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D186")),

                    OutsideReleaseReleaseTravel_Min = ParseDouble(GetPlcValue(plcValues, "D187")),
                    OutsideReleaseReleaseTravel_Max = ParseDouble(GetPlcValue(plcValues, "D188")),
                    OutsideReleaseReleaseTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D189")),

                    OutsideReleaseFullTravel_Min = ParseDouble(GetPlcValue(plcValues, "D190")),
                    OutsideReleaseFullTravel_Max = ParseDouble(GetPlcValue(plcValues, "D191")),
                    OutsideReleaseFullTravel_Actual = ParseDouble(GetPlcValue(plcValues, "D192"))
                };

                // Persist to DB
                _dbService.SaveTestLog(log);

                // Refresh Report page so the new row is visible
                this.Invoke(new Action(() =>
                {
                    ApplyFunctionalTestFilter();     // Reloads from DB & rebinds grid
                                                     // ApplyTravelEnduranceFilter(); // If you have a separate TE grid
                }));

                // Optional: Log to console for debugging
                Console.WriteLine($"✔ Test saved: {log.LoggedAt}  Variant={log.Variant}  Result={log.Result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("SaveSnapshot Error: " + ex.Message);
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Failed to save test log:\n" + ex.Message);
                }));
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
            foreach (var map in _plcMappings)
            {
                if (plcValues.TryGetValue(map.RegisterAddress, out string value))
                {
                    // Find the control on the form by its Name (from DB)
                    Control[] controls = this.Controls.Find(map.UiControlName, true);

                    if (controls.Length > 0 && controls[0] is TextBox txtBox)
                    {
                        // Update the textbox
                        txtBox.Text = value;

                        // Optional: Color code based on Min/Max (if you have limits in DB)
                        // if (map.ValueType == "Actual") { ... compare with min/max ... }
                    }
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

            UpdatePlcLabel("PLC Status: Connecting...", Color.Orange);

            // This call blocks the background thread, NOT the UI
            bool success = _plcService.Connect(ip, port);

            if (success)
            {
                UpdatePlcLabel("PLC Status: Connected", Color.Green);
            }
            else
            {
                UpdatePlcLabel("PLC Status: Connection Failed", Color.Red);
            }
        }

        private void UpdatePlcLabel(string text, Color color)
        {
            if (this.IsDisposed) return;

            // Use Invoke to marshal the call to the UI thread
            this.Invoke(new Action(() =>
            {
                PLC_LBL.Text = text;
                PLC_LBL.ForeColor = color;
            }));
        }

        private void UpdatePlcStatus()
        {
            // If we are not connected, try to reconnect
            if (!_plcService.IsConnected)
            {
                ConnectToPlc();
                return;
            }

            // If we are connected, do a quick read to verify it's still alive
            string testRead = _plcService.ReadValue("D0");

            if (testRead.StartsWith("ERR"))
            {
                UpdatePlcLabel("PLC Status: Connection Lost", Color.Red);
                _plcService.Disconnect();
            }
            else
            {
                UpdatePlcLabel("PLC Status: Connected", Color.Green);
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

        // ---------------------------------------------------------------
        // FUNCTIONAL TEST FILTER
        // ---------------------------------------------------------------
        private void ApplyFunctionalTestFilter()
        {
            DateTime fromDate = FT_DTP_FROM.Value.Date;
            DateTime toDate = FT_DTP_TO.Value.Date.AddDays(1).AddSeconds(-1);

            string timeFilter = FT_TXT_TIME.Text.Trim();
            string variantFilter = FT_CMB_VARIANT.Text;
            string shiftFilter = FT_CMB_SHIFT.Text;
            string resultFilter = FT_CMB_RESULT.Text;

            // 1. Query the DB (not the old in-memory list)
            List<TestLog> logs;
            try
            {
                logs = _dbService.GetTestLogs(fromDate, toDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load logs:\n" + ex.Message);
                return;
            }

            // 2. Apply additional filters in memory
            IEnumerable<TestLog> query = logs;

            if (!string.IsNullOrWhiteSpace(timeFilter))
                query = query.Where(x => x.Time.StartsWith(timeFilter));

            if (!string.IsNullOrWhiteSpace(variantFilter) && variantFilter != "(All)")
                query = query.Where(x => x.Variant == variantFilter);

            if (!string.IsNullOrWhiteSpace(shiftFilter) && shiftFilter != "(All)")
                query = query.Where(x => x.Shift == shiftFilter);

            if (!string.IsNullOrWhiteSpace(resultFilter) && resultFilter != "(All)")
                query = query.Where(x => x.Result == resultFilter);

            // 3. Bind the results to the DataGridView
            var bindingList = new BindingList<TestLog>(query.ToList());
            FT_DGV.DataSource = bindingList;

            // 4. Optional: format columns after binding
            FormatFTGrid();
        }

        private void FormatFTGrid()
        {
            if (FT_DGV.Columns.Count == 0) return;

            // Hide noisy columns
            if (FT_DGV.Columns.Contains("Id"))
                FT_DGV.Columns["Id"].Visible = false;

            // Format date / time columns
            if (FT_DGV.Columns.Contains("Date"))
                FT_DGV.Columns["Date"].Width = 90;

            if (FT_DGV.Columns.Contains("Time"))
                FT_DGV.Columns["Time"].Width = 80;

            // Right-align all numeric columns and set 2-decimal format
            foreach (DataGridViewColumn col in FT_DGV.Columns)
            {
                if (col.ValueType == typeof(double))
                {
                    //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    //col.DefaultCellStyle.Format = "N2";
                    //col.Width = 80;
                }
            }

            // Subscribe to RowPrePaint exactly ONCE (prevent duplicates)
            FT_DGV.RowPrePaint -= FT_DGV_RowPrePaint;
            FT_DGV.RowPrePaint += FT_DGV_RowPrePaint;
        }

        private void FT_DGV_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            // 1. Skip invalid row indices (header row is -1, or grid is empty)
            if (e.RowIndex < 0 || e.RowIndex >= FT_DGV.Rows.Count)
                return;

            // 2. Make sure the columns exist before accessing them
            if (!FT_DGV.Columns.Contains("Result"))
                return;

            try
            {
                var row = FT_DGV.Rows[e.RowIndex];
                var resultCell = row.Cells["Result"];

                if (resultCell?.Value == null)
                    return;

                // 3. Only color the row red if the result is FAIL
                if (resultCell.Value.ToString() == "FAIL")
                {
                    row.DefaultCellStyle.BackColor = Color.MistyRose;
                }
                else
                {
                    // Reset color for PASS rows (important when grid reuses rows)
                    row.DefaultCellStyle.BackColor = Color.White;
                }
            }
            catch (Exception ex)
            {
                // Fail silently — a cosmetic paint error should never crash the app
                Console.WriteLine("RowPrePaint error: " + ex.Message);
            }
        }

        // ---------------------------------------------------------------
        // TRAVEL & ENDURANCE FILTER
        // ---------------------------------------------------------------
        private void ApplyTravelEnduranceFilter()
        {
            DateTime fromDate = TE_DTP_FROM.Value.Date;
            DateTime toDate = TE_DTP_TO.Value.Date.AddDays(1).AddSeconds(-1);

            string timeFilter = TE_TXT_TIME.Text.Trim();
            string variantFilter = TE_CMB_VARIANT.Text;
            string shiftFilter = TE_CMB_SHIFT.Text;
            string resultFilter = TE_CMB_RESULT.Text;

            IEnumerable<TravelAndEnduranceLog> query = _teLogsMaster;

            query = query.Where(x => x.LoggedAt >= fromDate && x.LoggedAt <= toDate);

            if (!string.IsNullOrWhiteSpace(timeFilter))
                query = query.Where(x => x.Time.StartsWith(timeFilter));

            if (!string.IsNullOrWhiteSpace(variantFilter) && variantFilter != "(All)")
                query = query.Where(x => x.Variant == variantFilter);

            if (!string.IsNullOrWhiteSpace(shiftFilter) && shiftFilter != "(All)")
                query = query.Where(x => x.Shift == shiftFilter);

            if (!string.IsNullOrWhiteSpace(resultFilter) && resultFilter != "(All)")
                query = query.Where(x => x.Result == resultFilter);

            _teLogs = new BindingList<TravelAndEnduranceLog>(query.ToList());
            TET_DGV.DataSource = _teLogs;
        }

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

        private void DisplayQr(string qrData)
        {
            QR_LBL.Text = qrData;

            Bitmap qrImage = _qrCodeService.GenerateQr(qrData);

            if (QR_PB.Image != null)
            {
                QR_PB.Image.Dispose();
                QR_PB.Image = null;
            }

            QR_PB.SizeMode = PictureBoxSizeMode.Zoom;
            QR_PB.Image = qrImage;
        }

        private void SetupFunctionalTestGrid()
        {
            FT_DGV.AutoGenerateColumns = false;
            FT_DGV.AllowUserToAddRows = false;
            FT_DGV.ReadOnly = true;
            FT_DGV.RowHeadersVisible = false;
            FT_DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            FT_DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            FT_DGV.DataSource = _ftLogs;
        }

        private void SetupTravelEnduranceGrid()
        {
            TET_DGV.AutoGenerateColumns = false;
            TET_DGV.AllowUserToAddRows = false;
            TET_DGV.ReadOnly = true;
            TET_DGV.RowHeadersVisible = false;
            TET_DGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            TET_DGV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            TET_DGV.DataSource = _teLogs;
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

        private void btnTestSave_Click(object sender, EventArgs e)
        {
            // Build a fully populated TestLog with fake data
            var rnd = new Random();
            var fakeValues = new Dictionary<string, string>();

            // Meta fields
            fakeValues["D103"] = rnd.Next(0, 2) == 0 ? "PASS" : "FAIL";
            fakeValues["D104"] = new[] { "01", "02", "03", "04" }[rnd.Next(4)];
            fakeValues["D105"] = new[] { "A", "B", "C" }[rnd.Next(3)];

            // All measurement registers: D106 through D192
            for (int i = 106; i <= 192; i++)
            {
                fakeValues[$"D{i}"] = rnd.Next(10, 500).ToString();
            }

            // Call the same method the PLC trigger uses
            SaveSnapshot(fakeValues);

            MessageBox.Show(
                "Fake snapshot saved.\n\n" +
                "Check SSMS: SELECT TOP 5 * FROM TestLogs ORDER BY Id DESC\n" +
                "Check Report grid: a new row should appear.");
        }
    }
}