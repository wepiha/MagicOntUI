using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.Net;
/*
namespace FastONT
{
    public partial class Form1 : Form
    {

        public class SelectedAdapter
        {
            public SelectedAdapter()
            {

            }

            protected NetworkInterface myInterface;
            public NetworkInterface Interface
            {
                get { return myInterface; }
                set { myInterface = value; }
            }

        }


        Boolean allowPolling;

        enum Status
        {
            Poll,
            Initialize,
            Store,
            Configure,
            Verify,
            Connect,
            Login,
            Provision,
            Reboot,
            Restore,
            Test
        }

        Status setup;

        List<NetworkInterface> knownAdapters = new List<NetworkInterface>();
        SelectedAdapter currentAdapter;


        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            allowPolling = true;
            adapters = new AdapterList<NetworkInterface>();
            backgroundWorker1.RunWorkerAsync();
        }

        NetworkInterface GetNetworkInterfaceFromTag(String tag)
        {
            foreach (NetworkInterface adapter in NetworkInterface.GetAllNetworkInterfaces())
                if (adapter.Id == tag)
                    return adapter;

            return null;
        }
        private static Color GetColorFromOperationalStatus(OperationalStatus o)
        {
            switch (o)
            {
                case OperationalStatus.Up:
                    return Color.Green;

                case OperationalStatus.NotPresent:
                    return Color.Red;

                case OperationalStatus.Down:
                    return Color.Gray;

                case OperationalStatus.Dormant:
                    return Color.Purple;

                default:
                    return Color.Gray;
            }
        }

        Boolean IsNetworkAdapterInList(NetworkInterface adapter, List<NetworkInterface> list)
        {
            foreach (NetworkInterface item in list)
                if (item.Id == adapter.Id)
                    return true;

            return false;
        }

        private void toolStripDropDownButtonDevice_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

            ToolStripItem m = (ToolStripItem)e.ClickedItem;
            NetworkInterface selectedAdapter = GetNetworkInterfaceFromTag(m.Tag.ToString());

            ChangeSelectedAdapter(selectedAdapter, true);
        }

        private void ChangeSelectedAdapter(NetworkInterface selectedAdapter, Boolean storePreferred = false)
        {
            if (!allowPolling)
                return;

            if (currentAdapter != null)
                if (currentAdapter.Id == selectedAdapter.Id)
                    return;

            if (selectedAdapter == null)
            {

            }
            else
            {
                allowPolling = false;

                currentAdapter = selectedAdapter;

                Invoke((MethodInvoker)delegate
                {
                    toolStripDropDownButtonDevice.Text = currentAdapter.Description;

                    toolStripStatusLabelStatus.ForeColor = GetColorFromOperationalStatus(currentAdapter.OperationalStatus);
                    toolStripStatusLabelStatus.Text = currentAdapter.OperationalStatus.ToString();
                });

                if (storePreferred)
                {
                    Properties.Settings.Default.PreferredAdapterId = selectedAdapter.Id;
                    Properties.Settings.Default.Save();
                }

                allowPolling = true;
            }

        }
        private void toolStripStatusLabelStatus_TextChanged(object sender, EventArgs e)
        {
            if (currentAdapter == null)
                return;

            String info = "";

            switch (currentAdapter.OperationalStatus)
            {
                case OperationalStatus.Dormant:
                    break;
                case OperationalStatus.Down:
                    info = "Check cable...";
                    break;
                case OperationalStatus.LowerLayerDown:
                    break;
                case OperationalStatus.NotPresent:
                    break;
                case OperationalStatus.Testing:
                    break;
                case OperationalStatus.Unknown:
                    break;
                case OperationalStatus.Up:
                    info = "Ready!";
                    break;
                default:
                    break;
            }

            toolStripStatusLabelInfo.Text = info;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //textBox1.Enabled = false;
            //button1.Enabled = false;
            Debug.Print(GetNetworkInterfaceIPAddress(currentAdapter).ToString());
        }
        private Boolean IsNetworkInterfaceUsingDHCP(NetworkInterface adapter = null)
        {
            if (adapter == null)
                if (currentAdapter == null)
                    return true;
                else
                    adapter = currentAdapter;

            IPInterfaceProperties adapterProperties = adapter.GetIPProperties();
            IPv4InterfaceProperties p = adapterProperties.GetIPv4Properties();

            return p.IsDhcpEnabled;
        }
        IPAddress GetNetworkInterfaceIPAddress(NetworkInterface adapter)
        {
            foreach (UnicastIPAddressInformation ip in adapter.GetIPProperties().UnicastAddresses)
                if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    return ip.Address;

            return IPAddress.Any;
        }
        private void SetNetworkInterfaceIPAddress()
        {
            ManagementClass objMC = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection objMOC = objMC.GetInstances();

            foreach (ManagementObject objMO in objMOC)
            {
                if ((bool)objMO["IPEnabled"])
                {
                    try
                    {
                        ManagementBaseObject setIP;
                        ManagementBaseObject newIP =
                            objMO.GetMethodParameters("EnableStatic");

                        newIP["IPAddress"] = new string[] { Properties.Settings.Default.IPAddress };
                        newIP["SubnetMask"] = new string[] { Properties.Settings.Default.Subnet };

                        setIP = objMO.InvokeMethod("EnableStatic", newIP, null);
                    }
                    catch (Exception)
                    {

                    }

                }
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (backgroundWorker1.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                switch (setup)
                {
                    case Status.Poll:
                        Poll();
                        break;
                    case Status.Initialize:
                        break;
                    case Status.Store:
                        break;
                    case Status.Configure:
                        break;
                    case Status.Verify:
                        break;
                    case Status.Connect:
                        break;
                    case Status.Login:
                        break;
                    case Status.Provision:
                        break;
                    case Status.Reboot:
                        break;
                    case Status.Restore:
                        break;
                    case Status.Test:
                        break;
                    default:
                        break;
                }

                adapters.Refresh();
                System.Threading.Thread.Sleep(500);
            }
        }
        private void Poll()
        {
            if (!allowPolling)
                return;

            NetworkInterface returnedAdapter = currentAdapter;

            List<NetworkInterface> newAdapters = new List<NetworkInterface>(NetworkInterface.GetAllNetworkInterfaces());
            List<NetworkInterface> allAdapters = new List<NetworkInterface>(knownAdapters);

            /// Filter new adapters
            foreach (NetworkInterface adapter in newAdapters)
            {
                Boolean allow = false;

                ///
                if (adapter.NetworkInterfaceType.ToString().Contains("Ethernet"))
                    allow = true;

                ///
                if (adapter.Description.Contains("Virtual"))
                    allow = Properties.Settings.Default.AllowVirtualDevices;

                // check if this adapter is already known
                if (IsNetworkAdapterInList(adapter, knownAdapters))
                {
                    /// adapter is known, update it's status in the dropdown list 
                    Invoke((MethodInvoker)delegate
                    {
                        foreach (ToolStripItem t in toolStripDropDownButtonDevice.DropDownItems)
                            if (t.Text == adapter.Description)
                            {
                                t.ForeColor = GetColorFromOperationalStatus(adapter.OperationalStatus);
                                break;
                            }
                    });
                }
                else
                {
                    if (allow)
                    {
                        // adapter is not known, add this to known list
                        knownAdapters.Add(adapter);

                        Invoke((MethodInvoker)delegate
                        {
                            ToolStripItem t = toolStripDropDownButtonDevice.DropDownItems.Add(adapter.Description);
                            t.Tag = adapter.Id;
                        });

                        // if this adapter is preferred, set it as the current
                        if (adapter.Id == Properties.Settings.Default.PreferredAdapterId)
                            returnedAdapter = adapter;
                    }
                }
            }

            foreach (NetworkInterface adapter in allAdapters)
            {
                if (!IsNetworkAdapterInList(adapter, newAdapters))
                {
                    // device no longer present
                    knownAdapters.Remove(adapter);

                    Invoke((MethodInvoker)delegate
                    {
                        foreach (ToolStripItem t in toolStripDropDownButtonDevice.DropDownItems)
                            if (t.Text == adapter.Description)
                            {
                                toolStripDropDownButtonDevice.DropDownItems.Remove(t);
                                break;
                            }
                    });

                    if (currentAdapter.Id == adapter.Id)
                        returnedAdapter = null;
                }
            }

            if (knownAdapters.Count == 0)
            {
                Invoke((MethodInvoker)delegate
                {
                    toolStripDropDownButtonDevice.Text = "No devices detected";

                    toolStripStatusLabelStatus.ForeColor = Color.Gray;
                    toolStripStatusLabelStatus.Text = "...";
                    toolStripStatusLabelInfo.Text = "";
                });

                returnedAdapter = null;
            }
            else
            {
                if (returnedAdapter == null)
                    returnedAdapter = knownAdapters[0];
            }

            ChangeSelectedAdapter(returnedAdapter);
            System.Threading.Thread.Sleep(500);
        }
    }

    static class SendArp
    {

        public static void Test()
        {
            IPAddress ipAddr = IPAddress.Parse("192.168.1.251");
            PhysicalAddress macAddress = GetDestinationMacAddress(ipAddr);
            Console.WriteLine("MAC Address is {1}", ipAddr, macAddress);

            ipAddr = IPAddress.Any;
            try
            {
                macAddress = GetDestinationMacAddress(ipAddr);
                Console.WriteLine("MAC Address is {1}", ipAddr, macAddress);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }

            ///
            try
            {
                ipAddr = IPAddress.Broadcast;
                macAddress = GetDestinationMacAddress(ipAddr);
                Console.WriteLine("MAC Address is {1}", ipAddr, macAddress);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }

            ///
            try
            {
                ipAddr = IPAddress.Loopback;
                macAddress = GetDestinationMacAddress(ipAddr);
                Console.WriteLine("MAC Address is {1}", ipAddr, macAddress);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
        }

        public static PhysicalAddress GetDestinationMacAddress(System.Net.IPAddress address)
        {
            return GetDestinationMacAddress(address, System.Net.IPAddress.Any);
        }

        public static PhysicalAddress GetDestinationMacAddress(System.Net.IPAddress address, System.Net.IPAddress sourceAddress)
        {
            byte[] macAddrBytes = GetDestinationMacAddressBytes(address, sourceAddress);

            PhysicalAddress macAddress = new PhysicalAddress(macAddrBytes);
            return macAddress;
        }

        public static byte[] GetDestinationMacAddressBytes(System.Net.IPAddress address, System.Net.IPAddress sourceAddress)
        {
            if (address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                throw new ArgumentException("Only supports IPv4 Addresses.");
            }

            Int32 addrInt = IpAddressAsInt32(address);
            Int32 srcAddrInt = IpAddressAsInt32(address);

            ///
            const int MacAddressLength = 6;// 48bits
            byte[] macAddress = new byte[MacAddressLength];
            Int32 macAddrLen = macAddress.Length;

            Int32 ret = NativeMethods.SendArp(addrInt, srcAddrInt, macAddress, ref macAddrLen);

            if (ret != 0)
            {
                throw new System.ComponentModel.Win32Exception(ret);
            }

            System.Diagnostics.Debug.Assert(macAddrLen == MacAddressLength, "out macAddrLen==4");

            return macAddress;
        }

        private static Int32 IpAddressAsInt32(System.Net.IPAddress address)
        {
            byte[] ipAddrBytes = address.GetAddressBytes();
            System.Diagnostics.Debug.Assert(ipAddrBytes.Length == 4, "GetAddressBytes: .Length==4");
            Int32 addrInt = BitConverter.ToInt32(ipAddrBytes, 0);
            return addrInt;
        }

        static class NativeMethods
        {

            /// <summary>
            /// Sends an ARP request to obtain the physical address that corresponds
            /// to the specified destination IP address.
            /// </summary>
            /// -
            /// <param name="destIpAddress">Destination IP address, in the form of
            /// a <see cref="T:System.Int32"/>. The ARP request attempts to obtain
            /// the physical address that corresponds to this IP address.
            /// </param>
            /// <param name="srcIpAddress">IP address of the sender, in the form of
            /// a <see cref="T:System.Int32"/>. This parameter is optional. The caller
            /// may specify zero for the parameter.
            /// </param>
            /// <param name="macAddress">
            /// </param>
            /// <param name="macAddressLength">On input, specifies the maximum buffer
            /// size the user has set aside at pMacAddr to receive the MAC address,
            /// in bytes. On output, specifies the number of bytes written to
            /// pMacAddr.</param>
            /// -
            /// <returns>If the function succeeds, the return value is NO_ERROR.
            /// If the function fails, use FormatMessage to obtain the message string
            /// for the returned error.
            /// </returns>
            [System.Runtime.InteropServices.DllImport("Iphlpapi.dll", EntryPoint = "SendARP")]

            internal extern static Int32 SendArp(Int32 destIpAddress, Int32 srcIpAddress, byte[] macAddress, ref Int32 macAddressLength);
        }
    }

    public class Adapter : INotifyPropertyChanged
    {
        private NetworkInterface nic;
        public event PropertyChangedEventHandler PropertyChanged;

        public Adapter()
        {

        }

        public Adapter(NetworkInterface adapter)
        {
            this.nic = adapter;
        }

        public NetworkInterface AdapterNIC
        {
            get { return nic; }
            set
            {
                nic = value;
                OnPropertyChanged("AdapterNIC");
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}

namespace FastONT
{
    public class AdapterList : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private NetworkInterface myAdapter;

        private Boolean updating;
        private NetworkInterface adapter;

        class EventListener
        {
            private ListWithChangedEvent List;

            public EventListener(ListWithChangedEvent list)
            {
                List = list;
                // Add "ListChanged" to the Changed event on "List".
                List.Changed += new ChangedEventHandler(ListChanged);
            }

            // This will be called whenever the list changes.
            private void ListChanged(object sender, EventArgs e)
            {
                Console.WriteLine("This is called when the event fires.");
            }

            public void Detach()
            {
                // Detach the event and delete the list
                List.Changed -= new ChangedEventHandler(ListChanged);
                List = null;
            }
        }

        public AdapterList()
        {
            updating = false;

            myAdapters = new ListWithChangedEvent();
            EventListener listener = new EventListener(myAdapters);

            this.Refresh();
        }

        public AdapterList(String tagSelected) : base()
        {
            try
            {
                this.SelectedAdapterId = tagSelected;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void AddAdapter()
        {

        }

        private Boolean IsNetworkAdapterInList(NetworkInterface adapter, List<NetworkInterface> list)
        {
            foreach (NetworkInterface item in list)
                if (item.Id == adapter.Id)
                    return true;

            return false;
        }

        private Boolean IsAdapterAllowed(NetworkInterface adapter)
        {
            Boolean result = false;

            ///
            if (adapter.NetworkInterfaceType.ToString().Contains("Ethernet"))
                result = true;

            if (adapter.Description.Contains("Virtual"))
                result = ONTUI.Properties.Settings.Default.AllowVirtualDevices;

            return result;
        }


        public List<NetworkInterface> Refresh()
        {
            this.updating = true;

            List<NetworkInterface> adapterSample = new List<NetworkInterface>(NetworkInterface.GetAllNetworkInterfaces());

            foreach (NetworkInterface adapter in adapterSample)
            {
                if (IsNetworkAdapterInList(adapter, myAdapters))
                {

                }
                else
                {
                    if (IsAdapterAllowed(adapter))
                    {
                        myAdapters.Add(adapter);

                    }
                }
            }

            this.updating = false;
            return myAdapters;
        }

        public NetworkInterface SelectedAdapter
        {
            get { return adapter; }
            set
            {
                adapter = value;
                OnPropertyChanged("SelectedAdapter");
            }
        }

        public String SelectedAdapterId
        {
            get { return adapter.Id; }
            set
            {
                Boolean success = false;

                foreach (NetworkInterface item in adapterList)
                {
                    if (item.Id == value)
                    {
                        this.adapter = item;
                        success = true;
                    }
                }

                if (success)
                {
                    OnPropertyChanged("SelectedAdapter");
                }
                else
                {
                    throw new IndexOutOfRangeException("A NetworkInterface with the Id " + value + " does not exist");
                }
            }
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
    }

}

namespace MyCollections
{
    using System.Collections;

    class AdapterList<NetworkInterface> : List<NetworkInterface>
    {
        public event EventHandler OnAdd;
        public event EventHandler OnRemove;

        public void Add(NetworkInterface item)
        {
            OnAdd?.Invoke(this, null);
            base.Add(item);
        }

        public void Remove(NetworkInterface item)
        {
            OnRemove?.Invoke(this, null);
            base.Remove(item);
        }

    }
}


















using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using System.Net;

namespace FastONT
{
    public partial class Form1 : Form
    {

        private AdapterListener adapterListener;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            adapterListener = new AdapterListener();

            adapterListener.OnAdd = new EventHandler(adapterListener_OnAdd);

            backgroundWorker1.RunWorkerAsync();
        }

        void adapterListener_OnAdd(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButtonDevice_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            ToolStripItem m = (ToolStripItem)e.ClickedItem;
            Properties.Settings.Default.PreferredAdapterId = m.Tag.ToString();
            Properties.Settings.Default.Save();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (backgroundWorker1.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                adapterListener.Refresh();
                System.Threading.Thread.Sleep(500);
            }
        }
    }

    static class SendArp
    {

        public static void Test()
        {
            IPAddress ipAddr = IPAddress.Parse("192.168.2.1");
            PhysicalAddress macAddress = GetDestinationMacAddress(ipAddr);
            Console.WriteLine("MAC Address is {1}", ipAddr, macAddress);

            ipAddr = IPAddress.Any;
            try
            {
                macAddress = GetDestinationMacAddress(ipAddr);
                Console.WriteLine("MAC Address is {1}", ipAddr, macAddress);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }

            ///
            try
            {
                ipAddr = IPAddress.Broadcast;
                macAddress = GetDestinationMacAddress(ipAddr);
                Console.WriteLine("MAC Address is {1}", ipAddr, macAddress);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }

            ///
            try
            {
                ipAddr = IPAddress.Loopback;
                macAddress = GetDestinationMacAddress(ipAddr);
                Console.WriteLine("MAC Address is {1}", ipAddr, macAddress);
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.GetType().Name + ": " + ex.Message);
            }
        }

        public static PhysicalAddress GetDestinationMacAddress(System.Net.IPAddress address)
        {
            return GetDestinationMacAddress(address, System.Net.IPAddress.Any);
        }

        public static PhysicalAddress GetDestinationMacAddress(System.Net.IPAddress address, System.Net.IPAddress sourceAddress)
        {
            byte[] macAddrBytes = GetDestinationMacAddressBytes(address, sourceAddress);

            PhysicalAddress macAddress = new PhysicalAddress(macAddrBytes);
            return macAddress;
        }

        public static byte[] GetDestinationMacAddressBytes(System.Net.IPAddress address, System.Net.IPAddress sourceAddress)
        {
            if (address.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                throw new ArgumentException("Only supports IPv4 Addresses.");
            }

            Int32 addrInt = IpAddressAsInt32(address);
            Int32 srcAddrInt = IpAddressAsInt32(address);

            ///
            const int MacAddressLength = 6;// 48bits
            byte[] macAddress = new byte[MacAddressLength];
            Int32 macAddrLen = macAddress.Length;

            Int32 ret = NativeMethods.SendArp(addrInt, srcAddrInt, macAddress, ref macAddrLen);

            if (ret != 0)
            {
                throw new System.ComponentModel.Win32Exception(ret);
            }

            System.Diagnostics.Debug.Assert(macAddrLen == MacAddressLength, "out macAddrLen==4");

            return macAddress;
        }

        private static Int32 IpAddressAsInt32(System.Net.IPAddress address)
        {
            byte[] ipAddrBytes = address.GetAddressBytes();
            System.Diagnostics.Debug.Assert(ipAddrBytes.Length == 4, "GetAddressBytes: .Length==4");
            Int32 addrInt = BitConverter.ToInt32(ipAddrBytes, 0);
            return addrInt;
        }

        static class NativeMethods
        {

            /// <summary>
            /// Sends an ARP request to obtain the physical address that corresponds
            /// to the specified destination IP address.
            /// </summary>
            /// -
            /// <param name="destIpAddress">Destination IP address, in the form of
            /// a <see cref="T:System.Int32"/>. The ARP request attempts to obtain
            /// the physical address that corresponds to this IP address.
            /// </param>
            /// <param name="srcIpAddress">IP address of the sender, in the form of
            /// a <see cref="T:System.Int32"/>. This parameter is optional. The caller
            /// may specify zero for the parameter.
            /// </param>
            /// <param name="macAddress">
            /// </param>
            /// <param name="macAddressLength">On input, specifies the maximum buffer
            /// size the user has set aside at pMacAddr to receive the MAC address,
            /// in bytes. On output, specifies the number of bytes written to
            /// pMacAddr.</param>
            /// -
            /// <returns>If the function succeeds, the return value is NO_ERROR.
            /// If the function fails, use FormatMessage to obtain the message string
            /// for the returned error.
            /// </returns>
            [System.Runtime.InteropServices.DllImport("Iphlpapi.dll", EntryPoint = "SendARP")]

            internal extern static Int32 SendArp(Int32 destIpAddress, Int32 srcIpAddress, byte[] macAddress, ref Int32 macAddressLength);
        }
    }

    public class AdapterListener<NetworkInterface> : List<NetworkInterface>
    {

        public event EventHandler OnAdd;
        public event EventHandler OnRemove;

        new public void Add(NetworkInterface item)
        {
            if (null != OnAdd)
            {
                OnAdd(this, null);
            }
            base.Add(item);
        }

        new public void Remove(NetworkInterface item)
        {
            if (null != OnRemove)
            {
                OnRemove(this, null);
            }
            base.Remove(item);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private Boolean updating;

        public AdapterListener()
        {
            updating = false;

            this.Refresh();
        }
        /*
        public AdapterListener(String tagSelected)
            : base()
        {
            try
            {
                this.SelectedAdapterId = tagSelected;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void AddAdapter()
        {

        }

        private Boolean IsNetworkAdapterInList(NetworkInterface adapter, List<NetworkInterface> list)
        {
            /**
            foreach (NetworkInterface item in list)
                if (item.Id == adapter.Id)
                    return true;

            return false;
        }

        private Boolean IsAdapterAllowed(NetworkInterface adapter)
        {
            Boolean result = false;

            /*
            ///
            if (adapter.NetworkInterfaceType.ToString().Contains("Ethernet"))
                result = true;

            if (adapter.Description.Contains("Virtual"))
                result = Properties.Settings.Default.AllowVirtualDevices;

            return true;
        }


        public List<NetworkInterface> Refresh()
        {
            this.updating = true;

            List<NetworkInterface> adapterSample = new List<NetworkInterface>(NetworkInterface.GetAllNetworkInterfaces());

            foreach (NetworkInterface adapter in adapterSample)
            {
                if (IsNetworkAdapterInList(adapter, myAdapters))
                {

                }
                else
                {
                    if (IsAdapterAllowed(adapter))
                    {
                        myAdapters.Add(adapter);

                    }
                }
            }

            this.updating = false;
            return this;
        }

        /*
        public NetworkInterface SelectedAdapter
        {
            get { return adapter; }
            set
            {
                adapter = value;
                OnPropertyChanged("SelectedAdapter");
            }
        }

        public String SelectedAdapterId
        {
            get { return adapter.Id; }
            set
            {
                Boolean success = false;

                foreach (NetworkInterface item in adapterList)
                {
                    if (item.Id == value)
                    {
                        this.adapter = item;
                        success = true;
                    }
                }

                if (success)
                {
                    OnPropertyChanged("SelectedAdapter");
                }
                else
                {
                    throw new IndexOutOfRangeException("A NetworkInterface with the Id " + value + " does not exist");
                }
            }
        }


        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

    }

}
*/