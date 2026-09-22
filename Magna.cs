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

        private System.Threading.Timer _plcCheckTimer;
        private bool _isCheckingPlc = false; // Prevents overlapping checks

        // Master data (never filtered)
        private List<FunctionalTestLog> _ftLogsMaster = new();
        private List<TravelAndEnduranceLog> _teLogsMaster = new();

        // Bound data (filtered view shown in grid)
        private BindingList<FunctionalTestLog> _ftLogs;
        private BindingList<TravelAndEnduranceLog> _teLogs;

        public Magna()
        {
            InitializeComponent();

            _plcService = new PlcService();
            _qrCodeService = new QrCodeService();
            _qrDataService = new QrDataService();
            _sampleDataService = new SampleDataService();
            _qrDecoderService = new QrDecoderService(); // Initialize decoder

            _ftLogs = new BindingList<FunctionalTestLog>();
            _teLogs = new BindingList<TravelAndEnduranceLog>();

            // Set default IP and Port (Do NOT connect here)
            PLC_IP.Text = "192.168.1.10";
            PLC_Port.Text = "5000";

            // Populate filter dropdowns
            InitFilterCombos(FT_CMB_VARIANT, new[] { "01", "02", "03", "04" });
            InitFilterCombos(FT_CMB_SHIFT, new[] { "A", "B", "C" });
            InitFilterCombos(FT_CMB_RESULT, new[] { "PASS", "FAIL" });

            InitFilterCombos(TE_CMB_VARIANT, new[] { "01", "02", "03", "04" });
            InitFilterCombos(TE_CMB_SHIFT, new[] { "A", "B", "C" });
            InitFilterCombos(TE_CMB_RESULT, new[] { "PASS", "FAIL" });

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
            // Perform initial connection on the UI thread (safe here, form is ready)
            // But to prevent hang on startup, let's do it in the background too.
            Task.Run(() => ConnectToPlc());

            // Start the background timer for PLC status updates
            // DueTime = 3000ms (wait 3 seconds before first check)
            // Period = 3000ms (check every 3 seconds)
            _plcCheckTimer = new System.Threading.Timer(PlcCheckTimerCallback, null, 3000, 3000);
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
            DateTime toDate = FT_DTP_TO.Value.Date.AddDays(1).AddSeconds(-1); // inclusive end of day

            string timeFilter = FT_TXT_TIME.Text.Trim();          // e.g. "12:55"
            string variantFilter = FT_CMB_VARIANT.Text;
            string shiftFilter = FT_CMB_SHIFT.Text;
            string resultFilter = FT_CMB_RESULT.Text;

            IEnumerable<FunctionalTestLog> query = _ftLogsMaster;

            // Date range
            query = query.Where(x => x.LoggedAt >= fromDate && x.LoggedAt <= toDate);

            // Time (matches HH:mm prefix)
            if (!string.IsNullOrWhiteSpace(timeFilter))
                query = query.Where(x => x.Time.StartsWith(timeFilter));

            // Variant
            if (!string.IsNullOrWhiteSpace(variantFilter) && variantFilter != "(All)")
                query = query.Where(x => x.Variant == variantFilter);

            // Shift
            if (!string.IsNullOrWhiteSpace(shiftFilter) && shiftFilter != "(All)")
                query = query.Where(x => x.Shift == shiftFilter);

            // Result
            if (!string.IsNullOrWhiteSpace(resultFilter) && resultFilter != "(All)")
                query = query.Where(x => x.Result == resultFilter);

            // Rebinding: replace contents of bound BindingList
            _ftLogs = new BindingList<FunctionalTestLog>(query.ToList());
            FT_DGV.DataSource = _ftLogs;
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
    }
}