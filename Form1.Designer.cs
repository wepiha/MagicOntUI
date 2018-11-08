namespace ONTUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timerInternalCalls = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.labelDisplayStaticIP = new System.Windows.Forms.Label();
            this.labelDisplayONTConnected = new System.Windows.Forms.Label();
            this.labelDisplayONTProvisioned = new System.Windows.Forms.Label();
            this.labelDisplayFault = new System.Windows.Forms.Label();
            this.labelDisplayWWW = new System.Windows.Forms.Label();
            this.textBoxConsoleInput = new System.Windows.Forms.TextBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToTrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanelMain = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelHeader = new System.Windows.Forms.FlowLayoutPanel();
            this.labelDisplaySLID = new System.Windows.Forms.Label();
            this.labelDisplayOr = new System.Windows.Forms.Label();
            this.labelDisplayASID = new System.Windows.Forms.Label();
            this.flowLayoutPanelEntry = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxInputASIDOrSLID = new System.Windows.Forms.TextBox();
            this.checkBoxInputGO = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanelToggles = new System.Windows.Forms.FlowLayoutPanel();
            this.linkLabelToggleConsole = new System.Windows.Forms.LinkLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.flowLayoutPanelConsole = new System.Windows.Forms.FlowLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanelFooter = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelRight = new System.Windows.Forms.FlowLayoutPanel();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.flowLayoutPanelMain.SuspendLayout();
            this.flowLayoutPanelLeft.SuspendLayout();
            this.flowLayoutPanelHeader.SuspendLayout();
            this.flowLayoutPanelEntry.SuspendLayout();
            this.flowLayoutPanelToggles.SuspendLayout();
            this.flowLayoutPanelConsole.SuspendLayout();
            this.flowLayoutPanelFooter.SuspendLayout();
            this.flowLayoutPanelRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // timerInternalCalls
            // 
            this.timerInternalCalls.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(172, 48);
            // 
            // copyToClipboardToolStripMenuItem
            // 
            this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
            this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
            this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // labelDisplayStaticIP
            // 
            this.labelDisplayStaticIP.AutoSize = true;
            this.labelDisplayStaticIP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(118)))), ((int)(((byte)(197)))));
            this.labelDisplayStaticIP.Location = new System.Drawing.Point(16, 4);
            this.labelDisplayStaticIP.Margin = new System.Windows.Forms.Padding(0);
            this.labelDisplayStaticIP.MinimumSize = new System.Drawing.Size(56, 0);
            this.labelDisplayStaticIP.Name = "labelDisplayStaticIP";
            this.labelDisplayStaticIP.Padding = new System.Windows.Forms.Padding(3);
            this.labelDisplayStaticIP.Size = new System.Drawing.Size(56, 19);
            this.labelDisplayStaticIP.TabIndex = 6;
            this.labelDisplayStaticIP.Text = "Static IP";
            this.labelDisplayStaticIP.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.labelDisplayStaticIP, "An Ethernet Adapter with a Static IPv4 Address is Present");
            // 
            // labelDisplayONTConnected
            // 
            this.labelDisplayONTConnected.AutoSize = true;
            this.labelDisplayONTConnected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(118)))), ((int)(((byte)(197)))));
            this.labelDisplayONTConnected.Location = new System.Drawing.Point(72, 4);
            this.labelDisplayONTConnected.Margin = new System.Windows.Forms.Padding(0);
            this.labelDisplayONTConnected.MinimumSize = new System.Drawing.Size(56, 0);
            this.labelDisplayONTConnected.Name = "labelDisplayONTConnected";
            this.labelDisplayONTConnected.Padding = new System.Windows.Forms.Padding(3);
            this.labelDisplayONTConnected.Size = new System.Drawing.Size(56, 19);
            this.labelDisplayONTConnected.TabIndex = 4;
            this.labelDisplayONTConnected.Text = "ONT";
            this.labelDisplayONTConnected.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.labelDisplayONTConnected, "Established connection to an Optical Network Terminal (ONT)");
            // 
            // labelDisplayONTProvisioned
            // 
            this.labelDisplayONTProvisioned.AutoSize = true;
            this.labelDisplayONTProvisioned.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDisplayONTProvisioned.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelDisplayONTProvisioned.Location = new System.Drawing.Point(128, 4);
            this.labelDisplayONTProvisioned.Margin = new System.Windows.Forms.Padding(0);
            this.labelDisplayONTProvisioned.MinimumSize = new System.Drawing.Size(56, 0);
            this.labelDisplayONTProvisioned.Name = "labelDisplayONTProvisioned";
            this.labelDisplayONTProvisioned.Padding = new System.Windows.Forms.Padding(3);
            this.labelDisplayONTProvisioned.Size = new System.Drawing.Size(56, 19);
            this.labelDisplayONTProvisioned.TabIndex = 5;
            this.labelDisplayONTProvisioned.Text = "SLID";
            this.labelDisplayONTProvisioned.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.labelDisplayONTProvisioned, "The Subscriber-Line ID has been written to the ONT");
            // 
            // labelDisplayFault
            // 
            this.labelDisplayFault.AutoSize = true;
            this.labelDisplayFault.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDisplayFault.ForeColor = System.Drawing.Color.Gray;
            this.labelDisplayFault.Location = new System.Drawing.Point(184, 4);
            this.labelDisplayFault.Margin = new System.Windows.Forms.Padding(0);
            this.labelDisplayFault.MinimumSize = new System.Drawing.Size(56, 0);
            this.labelDisplayFault.Name = "labelDisplayFault";
            this.labelDisplayFault.Padding = new System.Windows.Forms.Padding(3);
            this.labelDisplayFault.Size = new System.Drawing.Size(56, 19);
            this.labelDisplayFault.TabIndex = 5;
            this.labelDisplayFault.Text = "Fault";
            this.labelDisplayFault.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.labelDisplayFault, "The ONT is reporting an Error and cannot be provisioned. Reset the ONT to try aga" +
        "in");
            // 
            // labelDisplayWWW
            // 
            this.labelDisplayWWW.AutoSize = true;
            this.labelDisplayWWW.Dock = System.Windows.Forms.DockStyle.Right;
            this.labelDisplayWWW.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(118)))), ((int)(((byte)(197)))));
            this.labelDisplayWWW.Location = new System.Drawing.Point(240, 4);
            this.labelDisplayWWW.Margin = new System.Windows.Forms.Padding(0);
            this.labelDisplayWWW.MinimumSize = new System.Drawing.Size(56, 0);
            this.labelDisplayWWW.Name = "labelDisplayWWW";
            this.labelDisplayWWW.Padding = new System.Windows.Forms.Padding(3);
            this.labelDisplayWWW.Size = new System.Drawing.Size(56, 19);
            this.labelDisplayWWW.TabIndex = 4;
            this.labelDisplayWWW.Text = "Internet";
            this.labelDisplayWWW.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.toolTip1.SetToolTip(this.labelDisplayWWW, "An internet connection is available");
            // 
            // textBoxConsoleInput
            // 
            this.textBoxConsoleInput.AcceptsTab = true;
            this.textBoxConsoleInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxConsoleInput.Location = new System.Drawing.Point(0, 194);
            this.textBoxConsoleInput.Margin = new System.Windows.Forms.Padding(0);
            this.textBoxConsoleInput.Name = "textBoxConsoleInput";
            this.textBoxConsoleInput.Size = new System.Drawing.Size(319, 20);
            this.textBoxConsoleInput.TabIndex = 5;
            this.toolTip1.SetToolTip(this.textBoxConsoleInput, "Command Input to ONT");
            this.textBoxConsoleInput.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxConsoleInput_KeyPress);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator2,
            this.toolStripMenuItem2,
            this.minimizeToTrayToolStripMenuItem,
            this.toolStripSeparator1,
            this.toolStripMenuItem3,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(220, 132);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem1.Text = "Show / Hide";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem2.Text = "Popup window on Connect";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // minimizeToTrayToolStripMenuItem
            // 
            this.minimizeToTrayToolStripMenuItem.Name = "minimizeToTrayToolStripMenuItem";
            this.minimizeToTrayToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.minimizeToTrayToolStripMenuItem.Text = "Minimize to Tray";
            this.minimizeToTrayToolStripMenuItem.Click += new System.EventHandler(this.minimizeToTrayToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(216, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(219, 22);
            this.toolStripMenuItem3.Text = "Display Console";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(216, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // flowLayoutPanelMain
            // 
            this.flowLayoutPanelMain.AutoSize = true;
            this.flowLayoutPanelMain.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelMain.Controls.Add(this.flowLayoutPanelLeft);
            this.flowLayoutPanelMain.Controls.Add(this.flowLayoutPanelRight);
            this.flowLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelMain.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelMain.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelMain.Name = "flowLayoutPanelMain";
            this.flowLayoutPanelMain.Size = new System.Drawing.Size(794, 434);
            this.flowLayoutPanelMain.TabIndex = 30;
            // 
            // flowLayoutPanelLeft
            // 
            this.flowLayoutPanelLeft.AutoSize = true;
            this.flowLayoutPanelLeft.Controls.Add(this.flowLayoutPanelHeader);
            this.flowLayoutPanelLeft.Controls.Add(this.flowLayoutPanelEntry);
            this.flowLayoutPanelLeft.Controls.Add(this.flowLayoutPanelToggles);
            this.flowLayoutPanelLeft.Controls.Add(this.flowLayoutPanelConsole);
            this.flowLayoutPanelLeft.Controls.Add(this.flowLayoutPanelFooter);
            this.flowLayoutPanelLeft.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanelLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelLeft.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelLeft.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelLeft.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelLeft.Name = "flowLayoutPanelLeft";
            this.flowLayoutPanelLeft.Size = new System.Drawing.Size(319, 352);
            this.flowLayoutPanelLeft.TabIndex = 31;
            this.flowLayoutPanelLeft.WrapContents = false;
            // 
            // flowLayoutPanelHeader
            // 
            this.flowLayoutPanelHeader.AutoSize = true;
            this.flowLayoutPanelHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelHeader.Controls.Add(this.labelDisplaySLID);
            this.flowLayoutPanelHeader.Controls.Add(this.labelDisplayOr);
            this.flowLayoutPanelHeader.Controls.Add(this.labelDisplayASID);
            this.flowLayoutPanelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelHeader.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelHeader.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelHeader.Name = "flowLayoutPanelHeader";
            this.flowLayoutPanelHeader.Padding = new System.Windows.Forms.Padding(8, 16, 8, 2);
            this.flowLayoutPanelHeader.Size = new System.Drawing.Size(319, 31);
            this.flowLayoutPanelHeader.TabIndex = 31;
            // 
            // labelDisplaySLID
            // 
            this.labelDisplaySLID.AutoSize = true;
            this.labelDisplaySLID.Location = new System.Drawing.Point(11, 16);
            this.labelDisplaySLID.Name = "labelDisplaySLID";
            this.labelDisplaySLID.Size = new System.Drawing.Size(31, 13);
            this.labelDisplaySLID.TabIndex = 12;
            this.labelDisplaySLID.Text = "SLID";
            // 
            // labelDisplayOr
            // 
            this.labelDisplayOr.AutoSize = true;
            this.labelDisplayOr.Enabled = false;
            this.labelDisplayOr.Location = new System.Drawing.Point(48, 16);
            this.labelDisplayOr.Name = "labelDisplayOr";
            this.labelDisplayOr.Size = new System.Drawing.Size(16, 13);
            this.labelDisplayOr.TabIndex = 11;
            this.labelDisplayOr.Text = "or";
            this.labelDisplayOr.Visible = false;
            // 
            // labelDisplayASID
            // 
            this.labelDisplayASID.AutoSize = true;
            this.labelDisplayASID.Enabled = false;
            this.labelDisplayASID.Location = new System.Drawing.Point(70, 16);
            this.labelDisplayASID.Name = "labelDisplayASID";
            this.labelDisplayASID.Size = new System.Drawing.Size(32, 13);
            this.labelDisplayASID.TabIndex = 10;
            this.labelDisplayASID.Text = "ASID";
            this.labelDisplayASID.Visible = false;
            // 
            // flowLayoutPanelEntry
            // 
            this.flowLayoutPanelEntry.AutoSize = true;
            this.flowLayoutPanelEntry.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelEntry.Controls.Add(this.textBoxInputASIDOrSLID);
            this.flowLayoutPanelEntry.Controls.Add(this.checkBoxInputGO);
            this.flowLayoutPanelEntry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelEntry.Location = new System.Drawing.Point(0, 31);
            this.flowLayoutPanelEntry.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelEntry.MinimumSize = new System.Drawing.Size(266, 0);
            this.flowLayoutPanelEntry.Name = "flowLayoutPanelEntry";
            this.flowLayoutPanelEntry.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.flowLayoutPanelEntry.Size = new System.Drawing.Size(319, 32);
            this.flowLayoutPanelEntry.TabIndex = 30;
            this.flowLayoutPanelEntry.WrapContents = false;
            // 
            // textBoxInputASIDOrSLID
            // 
            this.textBoxInputASIDOrSLID.Font = new System.Drawing.Font("Lucida Sans Typewriter", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxInputASIDOrSLID.Location = new System.Drawing.Point(12, 4);
            this.textBoxInputASIDOrSLID.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.textBoxInputASIDOrSLID.MaxLength = 12;
            this.textBoxInputASIDOrSLID.Name = "textBoxInputASIDOrSLID";
            this.textBoxInputASIDOrSLID.Size = new System.Drawing.Size(215, 23);
            this.textBoxInputASIDOrSLID.TabIndex = 1;
            this.textBoxInputASIDOrSLID.Text = "12345678";
            this.textBoxInputASIDOrSLID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxInputASIDOrSLID_KeyPress);
            // 
            // checkBoxInputGO
            // 
            this.checkBoxInputGO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxInputGO.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxInputGO.Location = new System.Drawing.Point(231, 4);
            this.checkBoxInputGO.Margin = new System.Windows.Forms.Padding(0);
            this.checkBoxInputGO.Name = "checkBoxInputGO";
            this.checkBoxInputGO.Size = new System.Drawing.Size(76, 24);
            this.checkBoxInputGO.TabIndex = 2;
            this.checkBoxInputGO.Text = "Go";
            this.checkBoxInputGO.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxInputGO.UseVisualStyleBackColor = true;
            this.checkBoxInputGO.CheckedChanged += new System.EventHandler(this.checkBoxInputGO_CheckedChanged);
            // 
            // flowLayoutPanelToggles
            // 
            this.flowLayoutPanelToggles.AutoSize = true;
            this.flowLayoutPanelToggles.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelToggles.Controls.Add(this.linkLabelToggleConsole);
            this.flowLayoutPanelToggles.Controls.Add(this.button1);
            this.flowLayoutPanelToggles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelToggles.Location = new System.Drawing.Point(0, 63);
            this.flowLayoutPanelToggles.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelToggles.MinimumSize = new System.Drawing.Size(266, 0);
            this.flowLayoutPanelToggles.Name = "flowLayoutPanelToggles";
            this.flowLayoutPanelToggles.Padding = new System.Windows.Forms.Padding(8, 4, 12, 4);
            this.flowLayoutPanelToggles.Size = new System.Drawing.Size(319, 32);
            this.flowLayoutPanelToggles.TabIndex = 29;
            this.flowLayoutPanelToggles.WrapContents = false;
            // 
            // linkLabelToggleConsole
            // 
            this.linkLabelToggleConsole.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(118)))), ((int)(((byte)(197)))));
            this.linkLabelToggleConsole.Location = new System.Drawing.Point(8, 4);
            this.linkLabelToggleConsole.Margin = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.linkLabelToggleConsole.Name = "linkLabelToggleConsole";
            this.linkLabelToggleConsole.Padding = new System.Windows.Forms.Padding(4, 4, 0, 4);
            this.linkLabelToggleConsole.Size = new System.Drawing.Size(219, 24);
            this.linkLabelToggleConsole.TabIndex = 3;
            this.linkLabelToggleConsole.TabStop = true;
            this.linkLabelToggleConsole.Text = "Toggle Console";
            this.linkLabelToggleConsole.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelToggleConsole_LinkClicked);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(231, 4);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Options >>";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // flowLayoutPanelConsole
            // 
            this.flowLayoutPanelConsole.AutoSize = true;
            this.flowLayoutPanelConsole.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelConsole.Controls.Add(this.richTextBox1);
            this.flowLayoutPanelConsole.Controls.Add(this.textBoxConsoleInput);
            this.flowLayoutPanelConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelConsole.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelConsole.Location = new System.Drawing.Point(0, 95);
            this.flowLayoutPanelConsole.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelConsole.MinimumSize = new System.Drawing.Size(266, 0);
            this.flowLayoutPanelConsole.Name = "flowLayoutPanelConsole";
            this.flowLayoutPanelConsole.Size = new System.Drawing.Size(319, 214);
            this.flowLayoutPanelConsole.TabIndex = 28;
            this.flowLayoutPanelConsole.Visible = false;
            this.flowLayoutPanelConsole.WrapContents = false;
            this.flowLayoutPanelConsole.VisibleChanged += new System.EventHandler(this.flowLayoutPanelConsole_VisibleChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(28)))), ((int)(((byte)(44)))));
            this.richTextBox1.ContextMenuStrip = this.contextMenuStrip2;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.richTextBox1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Bold);
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.richTextBox1.Size = new System.Drawing.Size(313, 188);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // flowLayoutPanelFooter
            // 
            this.flowLayoutPanelFooter.AutoSize = true;
            this.flowLayoutPanelFooter.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelFooter.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flowLayoutPanelFooter.Controls.Add(this.labelDisplayStaticIP);
            this.flowLayoutPanelFooter.Controls.Add(this.labelDisplayONTConnected);
            this.flowLayoutPanelFooter.Controls.Add(this.labelDisplayONTProvisioned);
            this.flowLayoutPanelFooter.Controls.Add(this.labelDisplayFault);
            this.flowLayoutPanelFooter.Controls.Add(this.labelDisplayWWW);
            this.flowLayoutPanelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelFooter.Location = new System.Drawing.Point(0, 309);
            this.flowLayoutPanelFooter.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelFooter.MinimumSize = new System.Drawing.Size(266, 0);
            this.flowLayoutPanelFooter.Name = "flowLayoutPanelFooter";
            this.flowLayoutPanelFooter.Padding = new System.Windows.Forms.Padding(16, 4, 0, 6);
            this.flowLayoutPanelFooter.Size = new System.Drawing.Size(319, 29);
            this.flowLayoutPanelFooter.TabIndex = 22;
            this.flowLayoutPanelFooter.WrapContents = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 341);
            this.flowLayoutPanel1.MinimumSize = new System.Drawing.Size(0, 8);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(313, 8);
            this.flowLayoutPanel1.TabIndex = 32;
            // 
            // flowLayoutPanelRight
            // 
            this.flowLayoutPanelRight.BackColor = System.Drawing.Color.Linen;
            this.flowLayoutPanelRight.Controls.Add(this.button2);
            this.flowLayoutPanelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelRight.Location = new System.Drawing.Point(319, 0);
            this.flowLayoutPanelRight.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelRight.MinimumSize = new System.Drawing.Size(278, 0);
            this.flowLayoutPanelRight.Name = "flowLayoutPanelRight";
            this.flowLayoutPanelRight.Size = new System.Drawing.Size(278, 352);
            this.flowLayoutPanelRight.TabIndex = 30;
            this.flowLayoutPanelRight.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(794, 434);
            this.Controls.Add(this.flowLayoutPanelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "ONTUI";
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.flowLayoutPanelMain.ResumeLayout(false);
            this.flowLayoutPanelMain.PerformLayout();
            this.flowLayoutPanelLeft.ResumeLayout(false);
            this.flowLayoutPanelLeft.PerformLayout();
            this.flowLayoutPanelHeader.ResumeLayout(false);
            this.flowLayoutPanelHeader.PerformLayout();
            this.flowLayoutPanelEntry.ResumeLayout(false);
            this.flowLayoutPanelEntry.PerformLayout();
            this.flowLayoutPanelToggles.ResumeLayout(false);
            this.flowLayoutPanelConsole.ResumeLayout(false);
            this.flowLayoutPanelConsole.PerformLayout();
            this.flowLayoutPanelFooter.ResumeLayout(false);
            this.flowLayoutPanelFooter.PerformLayout();
            this.flowLayoutPanelRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timerInternalCalls;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem minimizeToTrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelLeft;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFooter;
        private System.Windows.Forms.Label labelDisplayStaticIP;
        private System.Windows.Forms.Label labelDisplayONTConnected;
        private System.Windows.Forms.Label labelDisplayONTProvisioned;
        private System.Windows.Forms.Label labelDisplayFault;
        private System.Windows.Forms.Label labelDisplayWWW;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelConsole;
        private System.Windows.Forms.TextBox textBoxConsoleInput;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelToggles;
        private System.Windows.Forms.LinkLabel linkLabelToggleConsole;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelRight;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelHeader;
        private System.Windows.Forms.Label labelDisplaySLID;
        private System.Windows.Forms.Label labelDisplayOr;
        private System.Windows.Forms.Label labelDisplayASID;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelEntry;
        private System.Windows.Forms.TextBox textBoxInputASIDOrSLID;
        private System.Windows.Forms.CheckBox checkBoxInputGO;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
    }
}

