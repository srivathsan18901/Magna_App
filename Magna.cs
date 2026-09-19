using Magna_TestApplication.Models;
using Magna_TestApplication.services;
using System.ComponentModel;

namespace Magna_TestApplication
{
    public partial class Magna : Form
    {
        private TscPrinterService _printerService;
        private QrCodeService _qrCodeService;
        private QrDataService _qrDataService;
        private SampleDataService _sampleDataService;

        // NEW: QR Decoder Service
        private QrDecoderService _qrDecoderService;

        private System.Windows.Forms.Timer _sampleDataTimer;

        // Functional Test collection
        private BindingList<FunctionalTestLog> _ftLogs;

        // NEW: Travel and Endurance collection
        private BindingList<TravelAndEnduranceLog> _teLogs;

        public Magna()
        {
            InitializeComponent();

            _qrCodeService = new QrCodeService();
            _qrDataService = new QrDataService();
            _sampleDataService = new SampleDataService();
            _qrDecoderService = new QrDecoderService(); // Initialize decoder

            _ftLogs = new BindingList<FunctionalTestLog>();
            _teLogs = new BindingList<TravelAndEnduranceLog>(); // Initialize TE logs

            SetupFunctionalTestGrid();
            SetupTravelEnduranceGrid(); // Setup new grid
            SetupSampleTimer();
        }

        private void SetupSampleTimer()
        {
            _sampleDataTimer = new System.Windows.Forms.Timer();
            _sampleDataTimer.Interval = 10000; // 10 seconds
            _sampleDataTimer.Tick += SampleDataTimer_Tick;
            _sampleDataTimer.Start();
        }

        private void SampleDataTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                // 1. Generate Functional Test Log
                FunctionalTestLog ftLog = _sampleDataService.GenerateNextLog();

                // 2. Add to Functional Test Grid
                _ftLogs.Insert(0, ftLog);
                QTY_LBL.Text = ftLog.SNo.ToString();

                // 3. Generate QR for Functional Test
                string qrData = _qrDataService.GenerateQrData(
                    ftLog.LoggedAt, ftLog.Shift, ftLog.Variant, ftLog.SNo);

                DisplayQr(qrData);

                // 4. PROCESS TRAVEL AND ENDURANCE TEST
                // This uses the QR data generated above. 
                // It does NOT affect the Functional Test log.
                ProcessTravelAndEnduranceTest(qrData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void ProcessTravelAndEnduranceTest(string qrData)
        {
            // Decode the QR data to get the info
            var decoded = _qrDecoderService.DecodeQrData(qrData);

            // Simulate reading Travel and Endurance results (e.g., from a PLC or user input)
            string travelResult = _sampleDataService.GetSampleResult();
            string enduranceResult = _sampleDataService.GetSampleResult();

            // Create ONE combined log
            var teLog = new TravelAndEnduranceLog
            {
                SNo = _teLogs.Count + 1, // Independent serial number for TE logs
                LoggedAt = decoded.dateTime, // Use time from QR
                Shift = decoded.shift,
                Variant = decoded.variant,
                SerialNumber = decoded.serialNumber,
                TravelResult = travelResult,
                EnduranceResult = enduranceResult
            };

            // Add to Travel and Endurance Grid
            _teLogs.Insert(0, teLog);

            // Update TE Quantity Label (if you have one)
            // TE_QTY_LBL.Text = teLog.SNo.ToString();
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

        // NEW: Setup for Travel and Endurance Grid
        private void SetupTravelEnduranceGrid()
        {
            // Assuming you have a DataGridView named TE_DGV
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