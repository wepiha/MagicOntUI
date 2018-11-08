using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using MiniTelnet;
using ONTUI.Properties;
using static ONTUI.ONTInterface.ONTEventArgs;

namespace ONTUI
{
    class ONTInterface
    {
        public enum CommandState
        {
            RejectAll,
            AcceptInput,
            AcceptSLID
        };
        public enum ProvisionState
        {
            Default,
            Validated,
            Invalid,
            Disabled
        }

        private const int POLL_SLEEP = 100;

        protected static TelnetConnection MyTelnetConnection { get; private set; }

        public Boolean IsConnected
        {
            get
            {
                if (MyTelnetConnection == null)
                    return false;

                return MyTelnetConnection.IsConnected();
            }
        }
 
        public String ReadBuffer { get; private set; }
        public String WriteBuffer;

        public CommandState Input { get; private set; }
        public ProvisionState State { get; private set; }
        
        private Dictionary<String, TelnetReadResponseArgs> responseActions = new Dictionary<String, TelnetReadResponseArgs>();
        private BackgroundWorker mainWorker;

        private string _slid;
        private int _ticker;
        private Boolean _ping;

        public string SLID
        {
            get
            {
                return _slid;
            }
            set
            {
                if (State != ProvisionState.Default)
                {
                    if ((value != null) && (value != _slid))
                    {
                        State = ProvisionState.Default;
                    }
                }

                _slid = value;
            }
        }
        
        #region EventSource

        // Define a class to hold custom event info
        public class ONTEventArgs : EventArgs
        {
            public enum ConnectionEventType
            {
                Write,
                Read,
                Error
            }

            public ONTEventArgs(ConnectionEventType t, String s)
            {
                Type = t;
                Message = s;
            }

            public ConnectionEventType Type { get; }
            public string Message { get; private set; }
        }
        public class TelnetReadResponseArgs : EventArgs
        {            
            public TelnetReadResponseArgs(Action<object, object> a, String v)
            {
                this.Callback = a;
                this.Response = v;
            }

            public Delegate Callback { get; }
            public String Response { get; }
        }

        internal void Reset()
        {
            if (!mainWorker.IsBusy)
                mainWorker.RunWorkerAsync();

            if ((MyTelnetConnection != null) && (MyTelnetConnection.IsConnected()))
            {
                throw new Exception();
            }
        }

        public delegate void AutomaticTransmitData(TelnetConnection t, String s);

        // Declare the event using EventHandler<T>
        public event EventHandler<ONTEventArgs> OnConnectedEvent = delegate { };
        public event EventHandler<ONTEventArgs> OnDisconnectedEvent = delegate { };

        public event EventHandler<ONTEventArgs> OnCommunicationEvent = delegate { };
        public event EventHandler<ONTEventArgs> OnErrorEvent = delegate { };

        public event EventHandler<ONTEventArgs> OnSLIDValidationEvent = delegate { };
        public event EventHandler<ONTEventArgs> OnSLIDInvalidatedEvent = delegate { };

        private void HandleTelnetDataEvent(object sender, ONTEventArgs e)
        {
            if (e.Message != null)
            {
                if (e.Type == ConnectionEventType.Read)
                {
                    foreach (String line in e.Message.Split('\n'))
                    {
                        ReadBuffer = line;

                        foreach (String expected in responseActions.Keys)
                        {
                            if ((expected == line) || (line.StartsWith(expected)))
                            {
                                responseActions[expected].Callback.DynamicInvoke(MyTelnetConnection, responseActions[expected].Response);
                            }
                        }
                    }

                }
            }
        }
        private void HandleTelnetActionEvent(TelnetConnection t, TelnetReadResponseArgs e)
        {
            e.Callback?.DynamicInvoke(t, e.Response);
        }

        #endregion
        #region De/Constructors
        public ONTInterface(Boolean useAutoResponse = true)
        {
            mainWorker = new BackgroundWorker();
            mainWorker.DoWork += TelnetWork;

            if (useAutoResponse)
            {
                responseActions.Add("Login as: ", new TelnetReadResponseArgs(DoActionTransmit, Settings.Default.ONTLOGIN));
                responseActions.Add("Password: ", new TelnetReadResponseArgs(DoActionTransmit, Settings.Default.ONTPASS));

                responseActions.Add("Input command: ", new TelnetReadResponseArgs(DoCommandStateAcceptInput, null));
                responseActions.Add("Input new SLID: ", new TelnetReadResponseArgs(DoCommandStateAcceptSLID, null));

                responseActions.Add("VOS_CreateMsgQ Error", new TelnetReadResponseArgs(HandleSLIDUnconfigurable, null));
                responseActions.Add("Get pon state error!!!", new TelnetReadResponseArgs(HandleSLIDUnconfigurable, null));
                responseActions.Add("SLID configured successfully!!!", new TelnetReadResponseArgs(HandleSLIDValidated, null));
                responseActions.Add("SLID format is incorrect or over-length!!!", new TelnetReadResponseArgs(HandleSLIDInvalidated, null));

                OnCommunicationEvent += HandleTelnetDataEvent;
            }

            mainWorker.RunWorkerAsync();
        }
        ~ONTInterface()
        {
            MyTelnetConnection = null;
        }
        #endregion

        private static void DoActionTransmit(object arg1, object arg2)
        {
            ((TelnetConnection)arg1).WriteLine(arg2.ToString());
        }
        #region CommandState Modifiers
        private void DoCommandStateAcceptSLID(object arg1, object arg2)
        {
            if (Input == CommandState.AcceptInput)
                Input = CommandState.AcceptSLID;
        }
        private void DoCommandStateAcceptInput(object arg1, object arg2)
        {
            if (State != ProvisionState.Disabled)
                Input = CommandState.AcceptInput;
        }
        #endregion
        #region ConfigurationState Handlers
        private void HandleSLIDValidated(object arg1, object arg2)
        {
            State = ProvisionState.Validated;
            OnSLIDValidationEvent(this, new ONTEventArgs(ConnectionEventType.Read, ReadBuffer));
        }
        private void HandleSLIDInvalidated(object arg1, object arg2)
        {
            State = ProvisionState.Invalid;
            SLID = null;

            OnSLIDInvalidatedEvent(this, new ONTEventArgs(ConnectionEventType.Read, ReadBuffer));
        }
        private void HandleSLIDUnconfigurable(object arg1, object arg2)
        {
            State = ProvisionState.Disabled;
            Input = CommandState.RejectAll;

            OnErrorEvent(this, new ONTEventArgs(ConnectionEventType.Error, ReadBuffer));
        }
        #endregion

        #region Telnet Handlers
        private void TelnetWork(Object o, DoWorkEventArgs e)
        {
            while (true)
            {
                if ((_ticker++ % 10) == 0)
                    _ping = NetworkTests.GetPingResult(Settings.Default.ONTIPADDRESS);

                if (!_ping)
                {
                    TelnetDisconnect();
                    continue;
                }

                if (!this.IsConnected)
                {
                    Thread.Sleep(POLL_SLEEP * 2);
                    TelnetInit();

                    continue;
                }

                TelnetBufferRead();
                InjectSLID();
                TelnetBufferWrite();

                Thread.Sleep(POLL_SLEEP);
            }
        }
        private void TelnetInit()
        {
            if (MyTelnetConnection == null)
            {
                MyTelnetConnection = new TelnetConnection(
                    Settings.Default.ONTIPADDRESS,
                    Settings.Default.ONTPORT
                );
            }
            else
            {
                MyTelnetConnection.Disconnect();
                MyTelnetConnection.Connect();
            }

            if (MyTelnetConnection.IsConnected())
            {
                OnConnectedEvent(this, null);
            }
        }

        private void TelnetBufferRead()
        {
            String data = MyTelnetConnection?.Read();

            if (data != null)
            {
                OnCommunicationEvent(this, new ONTEventArgs(ConnectionEventType.Read, data));
            }
        }
        private void TelnetBufferWrite()
        {
            if (WriteBuffer == null)
                return;

            ConnectionEventType type = ConnectionEventType.Error;

            if (MyTelnetConnection.WriteLine(WriteBuffer))
            {
                if ((Input == CommandState.AcceptSLID) && (_slid == null))
                {
                    // wrote a slid manually set via the console 
                    _slid = WriteBuffer;
                }

                type = ConnectionEventType.Write;
            }
            else
            {
                Input = CommandState.RejectAll;
            }

            OnCommunicationEvent(this, new ONTEventArgs(type, WriteBuffer));

            WriteBuffer = null;
        }
        private void TelnetDisconnect()
        {
            if (MyTelnetConnection == null)
                return;

            if ((State != ProvisionState.Default) || (Input != CommandState.RejectAll))
            {
                OnDisconnectedEvent(this, null);
            }

            State = ProvisionState.Default;
            Input = CommandState.RejectAll;

            MyTelnetConnection = null;
        }
        #endregion

        private void InjectSLID()
        {
            if (Input == CommandState.RejectAll)
                return;

            if (State == ProvisionState.Validated)
                return;

            if (State == ProvisionState.Disabled)
                return;

            if (SLID == null)
                return;
            
            if ((WriteBuffer == null) || (WriteBuffer.Length == 0))
            {
                if (Input == CommandState.AcceptInput)
                    WriteBuffer = "2";

                if (Input == CommandState.AcceptSLID)
                    WriteBuffer = SLID;
            }
        }
    }
}