using Magna_TestApplication.services;

namespace Magna_TestApplication
{
    public partial class Magna : Form
    {
        private TscPrinterService _printerService;
        private System.Windows.Forms.Timer _printerStatusTimer;

        public Magna()
        {
            InitializeComponent();

            InitializePrinter();
            InitializePrinterStatusTimer();
        }

        private void InitializePrinter()
        {
            _printerService =
                new TscPrinterService(
                    "192.168.1.105",
                    9100);
        }

        private void InitializePrinterStatusTimer()
        {
            _printerStatusTimer = new System.Windows.Forms.Timer();

            _printerStatusTimer.Interval = 1000;

            _printerStatusTimer.Tick += PrinterStatusTimer_Tick;

            _printerStatusTimer.Start();
        }

        private void PrinterStatusTimer_Tick(object sender, EventArgs e)
        {
            UpdatePrinterStatus();
        }

        private string CreateTestLabel()
        {
            return
                "SIZE 100 mm,50 mm\r\n" +
                "GAP 3 mm,0 mm\r\n" +
                "DIRECTION 1\r\n" +
                "CLS\r\n" +
                "TEXT 50,50,\"0\",0,2,2,\"MAGNA TEST\"\r\n" +
                "TEXT 50,100,\"0\",0,1,1,\"123456789\"\r\n" +
                "PRINT 1,1\r\n";
        }

        private void PrintButton_Click(object sender, EventArgs e)
        {
            string tspl = CreateTestLabel();

            bool result = _printerService.PrintLabel(tspl);

            if (result)
            {
                MessageBox.Show(
                    "Label printed successfully.",
                    "TSC Printer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Printing failed.",
                    "TSC Printer",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void UpdatePrinterStatus()
        {
            bool result = _printerService.CheckPrinter();

            if (result)
            {
                ConnectionStatus_LBL.Text = "Connected";
                ConnectionStatus_LBL.ForeColor = Color.Green;

                PrinterStatus_LBL.Text = "Ready";
                PrinterStatus_LBL.ForeColor = Color.Green;
            }
            else
            {
                ConnectionStatus_LBL.Text = "Disconnected";
                ConnectionStatus_LBL.ForeColor = Color.Red;

                PrinterStatus_LBL.Text = "Not Ready";
                PrinterStatus_LBL.ForeColor = Color.Red;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}