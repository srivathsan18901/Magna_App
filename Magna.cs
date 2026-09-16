using Magna_TestApplication.Models;
using Magna_TestApplication.services;
using System.ComponentModel;

namespace Magna_TestApplication
{
    public partial class Magna : Form
    {
        private TscPrinterService _printerService;
        private System.Windows.Forms.Timer _printerStatusTimer;

        // QR services
        private QrCodeService _qrCodeService;
        private QrDataService _qrDataService;

        // Sample data service (NEW)
        private SampleDataService _sampleDataService;

        // Sample data timer
        private System.Windows.Forms.Timer _sampleDataTimer;

        // Functional Test log collection
        private BindingList<FunctionalTestLog> _ftLogs;

        public Magna()
        {
            InitializeComponent();

            //_printerService = new TscPrinterService();

            _qrCodeService = new QrCodeService();
            _qrDataService = new QrDataService();

            // Initialize the new service (NEW)
            _sampleDataService = new SampleDataService();

            _ftLogs = new BindingList<FunctionalTestLog>();

            SetupFunctionalTestGrid();
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
                // 1. Get the generated log from the service (NEW)
                FunctionalTestLog log = _sampleDataService.GenerateNextLog();

                // 2. Add to DGV
                _ftLogs.Insert(0, log);

                // 3. Display quantity (using SNo as the running quantity/serial)
                QTY_LBL.Text = log.SNo.ToString();

                // 4. Generate QR
                string qrData = _qrDataService.GenerateQrData(
                    log.LoggedAt,
                    log.Shift,
                    log.Variant,
                    log.SNo); // Passing SNo as the quantity parameter

                // 5. Display QR
                DisplayQr(qrData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sample Data Error : " + ex.Message);
            }
        }

        private void DisplayQr(string qrData)
        {
            // Show QR data as text
            QR_LBL.Text = qrData;

            // Generate QR image
            Bitmap qrImage = _qrCodeService.GenerateQr(qrData);

            // Dispose previous image
            if (QR_PB.Image != null)
            {
                QR_PB.Image.Dispose();
                QR_PB.Image = null;
            }

            // Display QR
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

        private void panel2_Paint(object sender, PaintEventArgs e) { }

        private void textBox1_TextChanged(object sender, EventArgs e) { }

        private void label3_Click(object sender, EventArgs e) { }

        private void ExportBTN_Click(object sender, EventArgs e) { }
    }
}