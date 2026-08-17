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

        // Sample data timer
        private System.Windows.Forms.Timer _sampleDataTimer;

        // Functional Test log collection
        private BindingList<FunctionalTestLog> _ftLogs;

        private int _sampleSerialNumber = 0;
        private Random _random = new Random();

        public Magna()
        {
            InitializeComponent();

            //_printerService = new TscPrinterService();

            _qrCodeService = new QrCodeService();
            _qrDataService = new QrDataService();

            _ftLogs =
                new BindingList<FunctionalTestLog>();

            SetupFunctionalTestGrid();

            SetupSampleTimer();
        }

        private void SetupSampleTimer()
        {
            _sampleDataTimer =
                new System.Windows.Forms.Timer();

            _sampleDataTimer.Interval = 10000; // 10 seconds

            _sampleDataTimer.Tick +=
                SampleDataTimer_Tick;

            _sampleDataTimer.Start();
        }

        private void SampleDataTimer_Tick(
    object sender,
    EventArgs e)
        {
            try
            {
                DateTime now = DateTime.Now;

                _sampleSerialNumber++;

                string shift =
                    GetSampleShift();

                string variant =
                    GetSampleVariant();

                string result =
                    GetSampleResult();

                int quantity = _sampleSerialNumber;

                var log =
                    new FunctionalTestLog
                    {
                        SNo = _sampleSerialNumber,
                        LoggedAt = now,
                        Shift = shift,
                        Variant = variant,
                        Result = result
                    };

                // Add to DGV
                _ftLogs.Insert(0, log);

                // Display quantity
                QTY_LBL.Text =
                    quantity.ToString();

                // Generate QR
                string qrData =
                    _qrDataService.GenerateQrData(
                        now,
                        shift,
                        variant,
                        quantity);

                // Display QR
                DisplayQr(qrData);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Sample Data Error : " +
                    ex.Message);
            }
        }

        private string GetSampleShift()
        {
            string[] shifts =
            {
        "A",
        "B",
        "C"
    };

            return shifts[
                _random.Next(shifts.Length)];
        }

        private string GetSampleVariant()
        {
            string[] variants =
            {
        "01",
        "02",
        "03",
        "04"
    };

            return variants[
                _random.Next(variants.Length)];
        }

        private string GetSampleResult()
        {
            return _random.Next(0, 2) == 0
                ? "PASS"
                : "FAIL";
        }

        private void DisplayQr(string qrData)
        {
            // Show QR data as text
            QR_LBL.Text = qrData;

            // Generate QR image
            Bitmap qrImage =
                _qrCodeService.GenerateQr(qrData);

            // Dispose previous image
            if (QR_PB.Image != null)
            {
                QR_PB.Image.Dispose();
                QR_PB.Image = null;
            }

            // Display QR
            QR_PB.SizeMode =
                PictureBoxSizeMode.Zoom;

            QR_PB.Image = qrImage;
        }

        private void SetupFunctionalTestGrid()
        {
            FT_DGV.AutoGenerateColumns = false;
            FT_DGV.AllowUserToAddRows = false;
            FT_DGV.ReadOnly = true;

            FT_DGV.RowHeadersVisible = false;

            FT_DGV.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            FT_DGV.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            FT_DGV.DataSource = _ftLogs;
        }

        private void panel2_Paint(
            object sender,
            PaintEventArgs e)
        {
        }
    }
}