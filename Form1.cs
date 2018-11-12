using ONTUI.Properties;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using static ONTUI.ONTInterface;

using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Drawing;

namespace ONTUI
{
    public partial class Form1 : Form
    {
        List<NetworkInterface> networkInterfaces;

        ONTInterface ontInterface;
        Boolean _asyncSlidWritten;
        Boolean _asyncFormShowRequired;
        String statusText;

        String _asyncOutputBufferSpecial;
        String _asyncOutputBuffer;
        Boolean modulator;
        
        int ticker = 0;

        public Form1()
        {
            InitializeComponent();
            
            ontInterface = new ONTInterface(true);

            ontInterface.OnConnectedEvent += HandleONTConnected;
            ontInterface.OnDisconnectedEvent += HandleONTDisconnected;
            ontInterface.OnSLIDInvalidatedEvent += HandleONTInvalidated;
            ontInterface.OnCommunicationEvent += HandleTelnetData;
            ontInterface.OnErrorEvent += HandleONTErrorMessage;

            ontInterface.OnSLIDValidationEvent += HandleONTConfigured;

            ToggleConsole(Settings.Default.UseConsole);
            toolStripMenuItem2.Checked = Settings.Default.UsePopup;
            minimizeToTrayToolStripMenuItem.Checked = Settings.Default.UseAutoMini;

        }

        #region Asynchronous Event Handlers
        private void HandleONTConnected(object sender, ONTEventArgs e)
        {
            _asyncOutputBufferSpecial += "\r\n*** Connection Established ***\r\n";
            _asyncFormShowRequired = toolStripMenuItem2.Checked;
        }
        private void HandleONTInvalidated(object sender, ONTEventArgs e)
        {
            _asyncOutputBufferSpecial += "\r\nSLID format is incorrect or over-length!\r\n";
            _asyncFormShowRequired = toolStripMenuItem2.Checked;
        }
        private void HandleONTConfigured(object sender, ONTEventArgs e)
        {
            Settings.Default.LastEnteredSLID = ontInterface.SLID;
            Settings.Default.Save();

            _asyncOutputBufferSpecial += "\r\nSLID configured successfully!!!\r\n";
            _asyncSlidWritten = true;
        }

        private void HandleTelnetData(object sender, ONTEventArgs e)
        {
            if (e.Type == ONTEventArgs.ConnectionEventType.Write)
                return;

            _asyncOutputBuffer = e.Message;

        }

        private void HandleONTErrorMessage(object sender, ONTEventArgs e)
        {
            _asyncOutputBufferSpecial += "\r\nThe ONT is reporting an Error!\r\n";
            _asyncFormShowRequired = toolStripMenuItem2.Checked;
        }
        private void HandleONTDisconnected(object sender, ONTEventArgs e)
        {
            _asyncOutputBufferSpecial += "\r\n*** Connection Lost ***\r\n";
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateLabelStatus();
            UpdateIconStatus();

            UpdateEthernetAdapterStatus();

            if (!ontInterface.IsConnected)
            {
                ontInterface.Reset();
            }

            if (_asyncFormShowRequired)
            {
                Show();
                WindowState = FormWindowState.Normal;

                _asyncFormShowRequired = false;
            }

            if (ontInterface.State != ProvisionState.Default)
            {
                checkBoxInputGO.Checked = false;
            }

            if (_asyncOutputBuffer?.Length > 0)
            {
                richTextBox1.SelectionColor = Color.Gray;
                richTextBox1.SelectedText += _asyncOutputBuffer;
                _asyncOutputBuffer = "";

                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();

                richTextBox1.Focus();
            }

            if (_asyncOutputBufferSpecial?.Length > 0)
            {
                richTextBox1.SelectionColor = Color.White;
                richTextBox1.SelectedText += _asyncOutputBufferSpecial;
                _asyncOutputBufferSpecial = "";

                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();

                richTextBox1.Focus();
            }
            
            if (textBoxInputASIDOrSLID.Enabled && _asyncSlidWritten)
            {
                // this line will update the SLID line from console-input values
                textBoxInputASIDOrSLID.Text = ontInterface.SLID;

                textBoxInputASIDOrSLID.Focus();
                textBoxInputASIDOrSLID.SelectAll();

                _asyncSlidWritten = false;
                ontInterface.SLID = null;
            }

            textBoxConsoleInput.Enabled = (ontInterface.Input != CommandState.RejectAll);            
        }
        
        private void UpdateIconStatus()
        {
            this.Icon = new System.Drawing.Icon((ontInterface.IsConnected) ? (((ontInterface.State == ProvisionState.Disabled)) ? "fault.ico" : "green.ico") : "blank.ico");

            this.notifyIcon1.Icon = this.Icon;
        }

        private void UpdateLabelStatus()
        {
            Color defaultBackColor = Color.FromName("ControlLightLight");
            Color defaultForeColor = Color.FromName("GrayText");
            Color backColor = defaultBackColor;
            Color foreColor = defaultForeColor;

            if ((ticker % 10) == 0) modulator = !modulator;

            labelDisplayONTConnected.Enabled = (ontInterface.IsConnected);

            labelDisplayONTProvisioned.Enabled = ((ontInterface.State == ProvisionState.Validated) || (ontInterface.State == ProvisionState.Invalid));

            if (modulator)
            {
                if (ontInterface.State == ProvisionState.Validated)
                    backColor = Color.LimeGreen;
                if (ontInterface.State == ProvisionState.Invalid)
                    backColor = Color.OrangeRed;

                foreColor = Color.White;
            }

            labelDisplayONTProvisioned.BackColor = backColor;
            labelDisplayONTProvisioned.ForeColor = foreColor;

            labelDisplayFault.BackColor = (modulator && (ontInterface.State == ProvisionState.Disabled))
                ? Color.FromArgb(192, 0, 0)
                : defaultBackColor;
            labelDisplayFault.ForeColor = (modulator && (ontInterface.State == ProvisionState.Disabled))
                ? defaultBackColor
                : defaultForeColor;
            
            labelDisplayFault.Enabled = modulator ? (ontInterface.State == ProvisionState.Disabled) : false;

            labelDisplayWWW.Enabled = (NetworkTests.GetPingResult("www.google.com", 400));

            textBoxInputASIDOrSLID.Enabled = ((ontInterface.State != ProvisionState.Disabled) && !checkBoxInputGO.Checked);
            checkBoxInputGO.Enabled = !(ontInterface.State == ProvisionState.Disabled);

            switch (ontInterface.State)
            {
                case ProvisionState.Default:
                    statusText = (ontInterface.IsConnected)
                        ? "ONT is Connected"
                        : // ternary if below :D

                        ((labelDisplayStaticIP.Enabled)
                        ? "ONT is not Detected"
                        : "Ethernet IPv4 is not Configured"
                    );

                    break;
                case ProvisionState.Validated:
                    statusText = "SLID is OK, ONT Reboot Required";
                    break;
                case ProvisionState.Invalid:
                    statusText = "SLID caused Validation Error";
                    break;
                case ProvisionState.Disabled:
                    statusText = "ONT is reporting a Fault";
                    break;
                default:
                    break;
            }
            
            notifyIcon1.Text = statusText;

            if (ontInterface.State != ProvisionState.Default)
                ticker += 1;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToggleConsole();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBoxInputASIDOrSLID.Text = Settings.Default.LastEnteredSLID;
            textBoxInputASIDOrSLID.Focus();
            textBoxInputASIDOrSLID.SelectAll();

            timerInternalCalls.Enabled = true;
        }
        
        private void textBoxInputASIDOrSLID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                checkBoxInputGO.Checked = true;
                e.Handled = true;
            }
        }
        private void textBoxConsoleInput_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                if (ontInterface != null)
                {
                    ontInterface.WriteBuffer = textBoxConsoleInput.Text;
                    textBoxConsoleInput.Text = "";
                }
                e.Handled = true;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (minimizeToTrayToolStripMenuItem.Checked)
            {
                if (WindowState == FormWindowState.Minimized)
                {
                    this.Visible = false;
                }
            }
        }

        private void minimizeToTrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            minimizeToTrayToolStripMenuItem.Checked = !minimizeToTrayToolStripMenuItem.Checked;

            Settings.Default.UseAutoMini = minimizeToTrayToolStripMenuItem.Checked;
            Settings.Default.Save();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if ((richTextBox1.Text != null) && (richTextBox1.Text.Length > 0))
            {
                Clipboard.SetText(richTextBox1.Text);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = ontInterface.ReadBuffer;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Hide();
            }
            else
            {
                _asyncFormShowRequired = true;
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                toolStripMenuItem1_Click(null, null);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = !toolStripMenuItem2.Checked;

            Settings.Default.UsePopup = toolStripMenuItem2.Checked;
            Settings.Default.Save();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ToggleConsole();
        }

        private void ToggleConsole()
        {
            ToggleConsole(!flowLayoutPanelConsole.Visible);
            
        }
        private void ToggleConsole(Boolean visible)
        {
            flowLayoutPanelConsole.Visible = visible;
            toolStripMenuItem3.Checked = visible;

            Settings.Default.UseConsole = visible;
            Settings.Default.Save();
        }

        private void UpdateEthernetAdapterStatus()
        {
            //labelDisplayStaticIP.Enabled = (NetworkTests.UpdateNetworkInterfaceWithStaticIPList().Count > 0);
            List<NetworkInterface> adapters = NetworkTests.UpdateNetworkInterfaceWithStaticIPList();

            foreach (NetworkInterface adapter in adapters)
            {
                bool isKnown = false;

                if (networkInterfaces != null)
                {
                    foreach (NetworkInterface known in networkInterfaces)
                    {
                        if (known.Name == adapter.Name)
                        {
                            isKnown = true;
                            break;
                        }
                    }
                }

                if (!isKnown)
                {
                    _asyncOutputBufferSpecial += "Devices with Static IPv4 address:\r\n";
                    _asyncOutputBufferSpecial += "> " + adapter.Description + "\r\n";
                }
            }

            networkInterfaces = adapters;
        }
        private void linkLabelToggleConsole_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ToggleConsole();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            flowLayoutPanelRight.Visible = !flowLayoutPanelRight.Visible;
        }

        private void checkBoxInputGO_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxInputGO.Checked)
                ontInterface.SLID = textBoxInputASIDOrSLID.Text;
        }

        private void flowLayoutPanelConsole_VisibleChanged(object sender, EventArgs e)
        {
            if (flowLayoutPanelConsole.Visible)
            {
                richTextBox1.SelectionStart = richTextBox1.TextLength;
                richTextBox1.ScrollToCaret();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
