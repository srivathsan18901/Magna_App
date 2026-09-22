using HslCommunication;
using HslCommunication.Profinet.Melsec;
using System.Text.RegularExpressions;

namespace Magna_TestApplication.services
{
    public class PlcService
    {
        private MelsecMcNet _plc;
        private readonly object _lockObject = new object();
        private bool _isConnected = false;

        public bool IsConnected => _isConnected;

        /// <summary>
        /// Connects to the Mitsubishi PLC.
        /// </summary>
        public bool Connect(string ip, int port)
        {
            lock (_lockObject)
            {
                try
                {
                    _plc?.ConnectClose();

                    _plc = new MelsecMcNet(ip, port)
                    {
                        ConnectTimeOut = 3000,
                        ReceiveTimeOut = 3000
                    };

                    var result = _plc.ConnectServer();
                    _isConnected = result.IsSuccess;
                    return _isConnected;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"PLC Connection Error: {ex.Message}");
                    _plc?.ConnectClose();
                    _plc = null;
                    _isConnected = false;
                    return false;
                }
            }
        }

        /// <summary>
        /// Disconnects from the PLC.
        /// </summary>
        public void Disconnect()
        {
            lock (_lockObject)
            {
                _plc?.ConnectClose();
                _isConnected = false;
            }
        }

        /// <summary>
        /// Validates and formats the PLC address (e.g., D100, M50, X0, Y0).
        /// </summary>
        private string FormatAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address))
                return null;

            address = address.Trim().Replace(" ", "").ToUpper();

            // Regex matches D, M, X, Y, W followed by numbers
            if (Regex.IsMatch(address, @"^[DWMXY]\d+$"))
                return address;

            return null;
        }

        /// <summary>
        /// Reads a value from the PLC. Returns "1"/"0" for bits, or the numeric value for words.
        /// Returns "ERR: message" on failure.
        /// </summary>
        public string ReadValue(string address)
        {
            lock (_lockObject)
            {
                try
                {
                    if (!CheckPlcConnection()) return "ERR: Not Connected";

                    address = FormatAddress(address);
                    if (address == null) return "ERR: Invalid Address";

                    // Bit addresses (M, X, Y)
                    if (address.StartsWith("M") || address.StartsWith("X") || address.StartsWith("Y"))
                    {
                        var result = _plc.ReadBool(address);
                        if (!result.IsSuccess) return "ERR:" + result.Message;
                        return result.Content ? "1" : "0";
                    }

                    // Word addresses (D, W)
                    var read = _plc.ReadUInt16(address);
                    if (!read.IsSuccess) return "ERR:" + read.Message;
                    return read.Content.ToString();
                }
                catch (Exception ex)
                {
                    _isConnected = false;
                    return "ERR:" + ex.Message;
                }
            }
        }

        /// <summary>
        /// Writes a value to the PLC. 
        /// For M, X, Y addresses, use "1"/"0" or "true"/"false".
        /// For D, W addresses, use a numeric string.
        /// </summary>
        public bool WriteValue(string address, string value)
        {
            lock (_lockObject)
            {
                try
                {
                    if (!CheckPlcConnection()) return false;

                    address = FormatAddress(address);
                    if (string.IsNullOrEmpty(address)) return false;

                    OperateResult result;

                    if (address.StartsWith("M") || address.StartsWith("X") || address.StartsWith("Y"))
                    {
                        bool bitValue = value == "1" || value.Equals("true", StringComparison.OrdinalIgnoreCase);
                        result = _plc.Write(address, bitValue);
                    }
                    else
                    {
                        if (!short.TryParse(value, out short wordValue))
                            return false;
                        result = _plc.Write(address, wordValue);
                    }

                    return result != null && result.IsSuccess;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"PLC Write Error: {ex.Message}");
                    _isConnected = false;
                    return false;
                }
            }
        }

        /// <summary>
        /// Checks if the PLC object exists and is connected.
        /// </summary>
        private bool CheckPlcConnection()
        {
            return _isConnected && _plc != null;
        }
    }
}