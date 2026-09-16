using System.Net.Sockets;
using System.Text;

namespace Magna_TestApplication.services
{
    internal class TscPrinterService
    {
        private readonly string _ipAddress;
        private readonly int _port;

        public bool IsConnected { get; private set; }

        public TscPrinterService(
            string ipAddress,
            int port = 9100)
        {
            _ipAddress = ipAddress;
            _port = port;
        }

        public bool CheckPrinter()
        {
            try
            {
                using TcpClient client = new TcpClient();

                client.Connect(
                    _ipAddress,
                    _port);

                IsConnected = client.Connected;

                return IsConnected;
            }
            catch
            {
                IsConnected = false;

                return false;
            }
        }

        public bool PrintLabel(string tsplCommand)
        {
            try
            {
                using TcpClient client = new TcpClient();

                client.Connect(
                    _ipAddress,
                    _port);

                using NetworkStream stream =
                    client.GetStream();

                byte[] data =
                    Encoding.ASCII.GetBytes(tsplCommand);

                stream.Write(
                    data,
                    0,
                    data.Length);

                stream.Flush();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}