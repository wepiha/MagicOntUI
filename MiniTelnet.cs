// minimalistic telnet implementation
// conceived by Tom Janssens on 2007/06/06  for codeproject
//
// http://www.corebvba.be

using System;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace MiniTelnet
{
    class TelnetConnection
    {
        enum Verbs
        {
            WILL = 251,
            WONT = 252,
            DO = 253,
            DONT = 254,
            IAC = 255
        }
        enum Options
        {
            SGA = 3
        }

        TcpClient tcpClient;
        public IAsyncResult LastIAsyncResult { get; private set; }

        private string _address;
        private int _port;

        int TimeOutMs = 50;

        //public bool AcceptsWrites {
        //    get
        //    {
        //        if (tcpClient == null)
        //            return false;

        //        return (tcpClient.Available == 0);
        //    }
        //}

        public TelnetConnection(string address, int port)
        {
            try
            {
                if (tcpClient is null)
                {
                    tcpClient = new TcpClient
                    {
                        ExclusiveAddressUse = true,
                        ReceiveTimeout = 120000,
                        SendTimeout = 120000,
                    };
                }

                if (!tcpClient.Connected)
                {
                    LastIAsyncResult = Connect(address, port);
                }
            }
            catch (Exception)
            {

            }
        }

        public IAsyncResult Connect(string address, int port)
        {
            try
            {
                LastIAsyncResult = tcpClient.BeginConnect(address, port, null, null);
            }
            catch (Exception)
            {
                
            }
            _address = address;
            _port = port;

            while (LastIAsyncResult.IsCompleted == false)
            {
                Thread.Sleep(100);
            }


            return LastIAsyncResult;
        }

        public void Disconnect()
        {
            tcpClient.Close();
        }

        public IAsyncResult Connect()
        {
            return Connect(_address, _port);
        }

        public Boolean WriteLine(string cmd)
        {
            return Write(cmd + "\n");
        }
        public Boolean Write(string cmd)
        {
            try
            {
                while (tcpClient.Available > 0)
                {
                    Thread.Sleep(100);
                };

                byte[] buf = System.Text.ASCIIEncoding.ASCII.GetBytes(cmd.Replace("\0xFF", "\0xFF\0xFF"));
                tcpClient.GetStream().Write(buf, 0, buf.Length);
            }
            catch (Exception)
            {
                return false;
            }

            return tcpClient.Connected;
        }

        public String Read()
        {
            if (!tcpClient.Connected)
                return null;
            
            StringBuilder sb = new StringBuilder();
            do
            {
                sb.Append(ParseTelnet());
                System.Threading.Thread.Sleep(TimeOutMs);
            } while (tcpClient.Available > 0);

            if (sb.Length == 0)
                return null;

            return sb?.ToString();
        }

        public Boolean IsConnected()
        {
            try
            {
                return tcpClient.Connected;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private StringBuilder ParseTelnet()
        {
            StringBuilder receivedData = new StringBuilder();

            while (tcpClient.Available > 0)
            {
                int input = tcpClient.GetStream().ReadByte();
                switch (input)
                {
                    case -1:
                        break;
                    case (int)Verbs.IAC:

                        // interpret as command
                        int inputverb = tcpClient.GetStream().ReadByte();
                        if (inputverb == -1)
                            break;

                        switch (inputverb)
                        {
                            case (int)Verbs.IAC:
                                //literal IAC = 255 escaped, so append char 255 to string
                                receivedData.Append(inputverb);
                                break;
                            case (int)Verbs.DO:
                            case (int)Verbs.DONT:
                            case (int)Verbs.WILL:
                            case (int)Verbs.WONT:

                                Verbs reply;

                                // reply to all commands with "WONT", unless it is SGA (suppres go ahead)
                                int inputoption = tcpClient.GetStream().ReadByte();

                                if (inputoption == -1)
                                    break;
                                
                                tcpClient.GetStream().WriteByte((byte)Verbs.IAC);

                                if (inputoption == (int)Options.SGA)
                                    reply = (inputverb == (int)Verbs.DO) ? Verbs.WILL : Verbs.DO;
                                else
                                    reply = (inputverb == (int)Verbs.DO) ? Verbs.WONT : Verbs.DONT;

                                tcpClient.GetStream().WriteByte((byte)reply);
                                tcpClient.GetStream().WriteByte((byte)inputoption);
                                break;

                            default:
                                break;
                        }
                        break;
                    default:
                        receivedData.Append((char)input);
                        break;
                }
            }
            return receivedData;
        }

        ~TelnetConnection()
        {
            try
            {
                //tcpClient.GetStream().Close();
                tcpClient.Close();
            }
            catch (Exception)
            {

            }
        }
    }
}
