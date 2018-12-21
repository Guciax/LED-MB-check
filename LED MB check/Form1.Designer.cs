namespace LED_MB_check
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
            this.textBoxZlecenie = new System.Windows.Forms.TextBox();
            this.label1zlecenie = new System.Windows.Forms.Label();
            this.labelTestReusltInfo = new System.Windows.Forms.Label();
            this.label4testhv = new System.Windows.Forms.Label();
            this.label5testF = new System.Windows.Forms.Label();
            this.label6testV = new System.Windows.Forms.Label();
            this.panelHV = new System.Windows.Forms.Panel();
            this.labelHVResult = new System.Windows.Forms.Label();
            this.panelVision = new System.Windows.Forms.Panel();
            this.labelVisionResult = new System.Windows.Forms.Label();
            this.panelFunc = new System.Windows.Forms.Panel();
            this.labelFuncResult = new System.Windows.Forms.Label();
            this.dataGridFunc = new System.Windows.Forms.DataGridView();
            this.dataGridLightUp = new System.Windows.Forms.DataGridView();
            this.dataGridHV = new System.Windows.Forms.DataGridView();
            this.textBoxMB = new System.Windows.Forms.TextBox();
            this.listViewFunc = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.radioOnyFailed = new System.Windows.Forms.RadioButton();
            this.radioShowAll = new System.Windows.Forms.RadioButton();
            this.listViewLightUp = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timerThread = new System.Windows.Forms.Timer(this.components);
            this.listViewHV = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3status = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.buttonDrawSettings = new System.Windows.Forms.Button();
            this.panelDrawSettings = new System.Windows.Forms.Panel();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label12Y1 = new System.Windows.Forms.Label();
            this.label11Y2 = new System.Windows.Forms.Label();
            this.c1Y = new System.Windows.Forms.NumericUpDown();
            this.label13X2 = new System.Windows.Forms.Label();
            this.label10X1 = new System.Windows.Forms.Label();
            this.c2Y = new System.Windows.Forms.NumericUpDown();
            this.c2X = new System.Windows.Forms.NumericUpDown();
            this.c1X = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxShowAll = new System.Windows.Forms.CheckBox();
            this.labelOperator = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hV_procedure_DataSet = new LED_MB_check.HV_procedure_DataSet();
            this.sp_hvBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_hvTableAdapter = new LED_MB_check.HV_procedure_DataSetTableAdapters.sp_hvTableAdapter();
            this.tableAdapterManager = new LED_MB_check.HV_procedure_DataSetTableAdapters.TableAdapterManager();
            this.dataSet1 = new LED_MB_check.DataSet1();
            this.sp_functionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_functionTableAdapter = new LED_MB_check.DataSet1TableAdapters.sp_functionTableAdapter();
            this.sp_visionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.sp_visionTableAdapter = new LED_MB_check.DataSet1TableAdapters.sp_visionTableAdapter();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonZlecenie = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonListen = new System.Windows.Forms.Button();
            this.buttonViewVisFailures = new System.Windows.Forms.Button();
            this.pictureBoxLoading = new System.Windows.Forms.PictureBox();
            this.panelHV.SuspendLayout();
            this.panelVision.SuspendLayout();
            this.panelFunc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFunc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLightUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHV)).BeginInit();
            this.panelDrawSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c2Y)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c2X)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1X)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hV_procedure_DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_hvBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_functionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_visionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxZlecenie
            // 
            this.textBoxZlecenie.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxZlecenie.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxZlecenie.Location = new System.Drawing.Point(12, 54);
            this.textBoxZlecenie.Name = "textBoxZlecenie";
            this.textBoxZlecenie.Size = new System.Drawing.Size(160, 38);
            this.textBoxZlecenie.TabIndex = 0;
            this.textBoxZlecenie.Text = "756763";
            this.textBoxZlecenie.Enter += new System.EventHandler(this.textBoxZlecenie_Enter);
            this.textBoxZlecenie.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBoxZlecenie.Leave += new System.EventHandler(this.textBoxZlecenie_Leave);
            // 
            // label1zlecenie
            // 
            this.label1zlecenie.AutoSize = true;
            this.label1zlecenie.BackColor = System.Drawing.Color.Transparent;
            this.label1zlecenie.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1zlecenie.Location = new System.Drawing.Point(14, 35);
            this.label1zlecenie.Name = "label1zlecenie";
            this.label1zlecenie.Size = new System.Drawing.Size(83, 17);
            this.label1zlecenie.TabIndex = 1;
            this.label1zlecenie.Text = "Nr zlecenia:";
            // 
            // labelTestReusltInfo
            // 
            this.labelTestReusltInfo.AutoSize = true;
            this.labelTestReusltInfo.BackColor = System.Drawing.Color.Transparent;
            this.labelTestReusltInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 26F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelTestReusltInfo.Location = new System.Drawing.Point(317, 79);
            this.labelTestReusltInfo.Name = "labelTestReusltInfo";
            this.labelTestReusltInfo.Size = new System.Drawing.Size(609, 39);
            this.labelTestReusltInfo.TabIndex = 3;
            this.labelTestReusltInfo.Text = "Wyniki testu panelu MB nr: _________";
            // 
            // label4testhv
            // 
            this.label4testhv.AutoSize = true;
            this.label4testhv.BackColor = System.Drawing.Color.Transparent;
            this.label4testhv.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label4testhv.Location = new System.Drawing.Point(153, 145);
            this.label4testhv.Name = "label4testhv";
            this.label4testhv.Size = new System.Drawing.Size(113, 31);
            this.label4testhv.TabIndex = 5;
            this.label4testhv.Text = "Test HV";
            // 
            // label5testF
            // 
            this.label5testF.AutoSize = true;
            this.label5testF.BackColor = System.Drawing.Color.Transparent;
            this.label5testF.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label5testF.Location = new System.Drawing.Point(560, 145);
            this.label5testF.Name = "label5testF";
            this.label5testF.Size = new System.Drawing.Size(190, 31);
            this.label5testF.TabIndex = 6;
            this.label5testF.Text = "Test funkcyjny";
            // 
            // label6testV
            // 
            this.label6testV.AutoSize = true;
            this.label6testV.BackColor = System.Drawing.Color.Transparent;
            this.label6testV.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label6testV.Location = new System.Drawing.Point(959, 145);
            this.label6testV.Name = "label6testV";
            this.label6testV.Size = new System.Drawing.Size(224, 31);
            this.label6testV.TabIndex = 7;
            this.label6testV.Text = "Test zaświecenia";
            // 
            // panelHV
            // 
            this.panelHV.BackColor = System.Drawing.Color.Gray;
            this.panelHV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelHV.Controls.Add(this.labelHVResult);
            this.panelHV.Location = new System.Drawing.Point(7, 179);
            this.panelHV.Name = "panelHV";
            this.panelHV.Size = new System.Drawing.Size(418, 144);
            this.panelHV.TabIndex = 9;
            // 
            // labelHVResult
            // 
            this.labelHVResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelHVResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelHVResult.Location = new System.Drawing.Point(0, 0);
            this.labelHVResult.Name = "labelHVResult";
            this.labelHVResult.Size = new System.Drawing.Size(416, 142);
            this.labelHVResult.TabIndex = 0;
            this.labelHVResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelVision
            // 
            this.panelVision.BackColor = System.Drawing.Color.Gray;
            this.panelVision.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelVision.Controls.Add(this.labelVisionResult);
            this.panelVision.Location = new System.Drawing.Point(855, 179);
            this.panelVision.Name = "panelVision";
            this.panelVision.Size = new System.Drawing.Size(418, 144);
            this.panelVision.TabIndex = 10;
            // 
            // labelVisionResult
            // 
            this.labelVisionResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelVisionResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelVisionResult.Location = new System.Drawing.Point(0, 0);
            this.labelVisionResult.Name = "labelVisionResult";
            this.labelVisionResult.Size = new System.Drawing.Size(416, 142);
            this.labelVisionResult.TabIndex = 0;
            this.labelVisionResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelFunc
            // 
            this.panelFunc.BackColor = System.Drawing.Color.Gray;
            this.panelFunc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFunc.Controls.Add(this.labelFuncResult);
            this.panelFunc.Location = new System.Drawing.Point(431, 179);
            this.panelFunc.Name = "panelFunc";
            this.panelFunc.Size = new System.Drawing.Size(417, 144);
            this.panelFunc.TabIndex = 10;
            // 
            // labelFuncResult
            // 
            this.labelFuncResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelFuncResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 40F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFuncResult.Location = new System.Drawing.Point(0, 0);
            this.labelFuncResult.Name = "labelFuncResult";
            this.labelFuncResult.Size = new System.Drawing.Size(415, 142);
            this.labelFuncResult.TabIndex = 0;
            this.labelFuncResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridFunc
            // 
            this.dataGridFunc.AllowUserToAddRows = false;
            this.dataGridFunc.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridFunc.Location = new System.Drawing.Point(431, 330);
            this.dataGridFunc.Name = "dataGridFunc";
            this.dataGridFunc.RowHeadersVisible = false;
            this.dataGridFunc.Size = new System.Drawing.Size(418, 520);
            this.dataGridFunc.TabIndex = 11;
            this.dataGridFunc.Visible = false;
            // 
            // dataGridLightUp
            // 
            this.dataGridLightUp.AllowUserToAddRows = false;
            this.dataGridLightUp.AllowUserToDeleteRows = false;
            this.dataGridLightUp.AllowUserToOrderColumns = true;
            this.dataGridLightUp.AllowUserToResizeRows = false;
            this.dataGridLightUp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridLightUp.Location = new System.Drawing.Point(855, 330);
            this.dataGridLightUp.Name = "dataGridLightUp";
            this.dataGridLightUp.RowHeadersVisible = false;
            this.dataGridLightUp.Size = new System.Drawing.Size(418, 520);
            this.dataGridLightUp.TabIndex = 12;
            this.dataGridLightUp.Visible = false;
            // 
            // dataGridHV
            // 
            this.dataGridHV.AllowUserToAddRows = false;
            this.dataGridHV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridHV.Location = new System.Drawing.Point(7, 329);
            this.dataGridHV.Name = "dataGridHV";
            this.dataGridHV.Size = new System.Drawing.Size(418, 520);
            this.dataGridHV.TabIndex = 13;
            this.dataGridHV.Visible = false;
            // 
            // textBoxMB
            // 
            this.textBoxMB.Enabled = false;
            this.textBoxMB.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxMB.Location = new System.Drawing.Point(591, 3);
            this.textBoxMB.Name = "textBoxMB";
            this.textBoxMB.Size = new System.Drawing.Size(189, 38);
            this.textBoxMB.TabIndex = 15;
            this.textBoxMB.Text = "756763_10";
            this.textBoxMB.Enter += new System.EventHandler(this.textBoxMB_Enter);
            this.textBoxMB.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox2_KeyDown);
            this.textBoxMB.Leave += new System.EventHandler(this.textBoxMB_Leave);
            // 
            // listViewFunc
            // 
            this.listViewFunc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewFunc.Location = new System.Drawing.Point(431, 329);
            this.listViewFunc.Name = "listViewFunc";
            this.listViewFunc.Size = new System.Drawing.Size(418, 520);
            this.listViewFunc.TabIndex = 16;
            this.listViewFunc.UseCompatibleStateImageBehavior = false;
            this.listViewFunc.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "PCB";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Pomiar";
            this.columnHeader2.Width = 280;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Wynik";
            this.columnHeader3.Width = 80;
            // 
            // radioOnyFailed
            // 
            this.radioOnyFailed.AutoSize = true;
            this.radioOnyFailed.Checked = true;
            this.radioOnyFailed.Location = new System.Drawing.Point(8, 17);
            this.radioOnyFailed.Name = "radioOnyFailed";
            this.radioOnyFailed.Size = new System.Drawing.Size(87, 17);
            this.radioOnyFailed.TabIndex = 17;
            this.radioOnyFailed.TabStop = true;
            this.radioOnyFailed.Text = "Widok prosty";
            this.radioOnyFailed.UseVisualStyleBackColor = true;
            // 
            // radioShowAll
            // 
            this.radioShowAll.AutoSize = true;
            this.radioShowAll.Location = new System.Drawing.Point(8, 35);
            this.radioShowAll.Name = "radioShowAll";
            this.radioShowAll.Size = new System.Drawing.Size(121, 17);
            this.radioShowAll.TabIndex = 18;
            this.radioShowAll.Text = "Widok szczegółowy";
            this.radioShowAll.UseVisualStyleBackColor = true;
            this.radioShowAll.CheckedChanged += new System.EventHandler(this.radioShowAll_CheckedChanged);
            // 
            // listViewLightUp
            // 
            this.listViewLightUp.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.listViewLightUp.Location = new System.Drawing.Point(855, 329);
            this.listViewLightUp.MultiSelect = false;
            this.listViewLightUp.Name = "listViewLightUp";
            this.listViewLightUp.Size = new System.Drawing.Size(418, 520);
            this.listViewLightUp.TabIndex = 19;
            this.listViewLightUp.UseCompatibleStateImageBehavior = false;
            this.listViewLightUp.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "PCB";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Pomiar";
            this.columnHeader5.Width = 250;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Wynik";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timerThread
            // 
            this.timerThread.Interval = 200;
            this.timerThread.Tick += new System.EventHandler(this.timerThread_Tick);
            // 
            // listViewHV
            // 
            this.listViewHV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listViewHV.Location = new System.Drawing.Point(7, 329);
            this.listViewHV.Name = "listViewHV";
            this.listViewHV.Size = new System.Drawing.Size(418, 520);
            this.listViewHV.TabIndex = 21;
            this.listViewHV.UseCompatibleStateImageBehavior = false;
            this.listViewHV.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "PCB";
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Pomiar";
            this.columnHeader8.Width = 250;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Wynik";
            this.columnHeader9.Width = 100;
            // 
            // label3status
            // 
            this.label3status.AutoSize = true;
            this.label3status.BackColor = System.Drawing.Color.Transparent;
            this.label3status.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label3status.Location = new System.Drawing.Point(9, 95);
            this.label3status.Name = "label3status";
            this.label3status.Size = new System.Drawing.Size(151, 17);
            this.label3status.TabIndex = 22;
            this.label3status.Text = "Nie wczytano zlecenia.";
            this.label3status.DoubleClick += new System.EventHandler(this.label3status_DoubleClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(1172, 86);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(106, 17);
            this.checkBox1.TabIndex = 23;
            this.checkBox1.Text = "Auto drukowanie";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // buttonDrawSettings
            // 
            this.buttonDrawSettings.Location = new System.Drawing.Point(1070, 82);
            this.buttonDrawSettings.Name = "buttonDrawSettings";
            this.buttonDrawSettings.Size = new System.Drawing.Size(95, 23);
            this.buttonDrawSettings.TabIndex = 24;
            this.buttonDrawSettings.Text = "Auto druk. ust.";
            this.toolTip1.SetToolTip(this.buttonDrawSettings, "Ustawienia automatycznego klikania");
            this.buttonDrawSettings.UseVisualStyleBackColor = true;
            this.buttonDrawSettings.Click += new System.EventHandler(this.button4_Click);
            // 
            // panelDrawSettings
            // 
            this.panelDrawSettings.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelDrawSettings.Controls.Add(this.label15);
            this.panelDrawSettings.Controls.Add(this.label14);
            this.panelDrawSettings.Controls.Add(this.button5);
            this.panelDrawSettings.Controls.Add(this.label12Y1);
            this.panelDrawSettings.Controls.Add(this.label11Y2);
            this.panelDrawSettings.Controls.Add(this.c1Y);
            this.panelDrawSettings.Controls.Add(this.label13X2);
            this.panelDrawSettings.Controls.Add(this.label10X1);
            this.panelDrawSettings.Controls.Add(this.c2Y);
            this.panelDrawSettings.Controls.Add(this.c2X);
            this.panelDrawSettings.Controls.Add(this.c1X);
            this.panelDrawSettings.Location = new System.Drawing.Point(1072, 108);
            this.panelDrawSettings.Name = "panelDrawSettings";
            this.panelDrawSettings.Size = new System.Drawing.Size(201, 82);
            this.panelDrawSettings.TabIndex = 25;
            this.panelDrawSettings.Visible = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label15.Location = new System.Drawing.Point(3, 29);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(13, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "2";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label14.Location = new System.Drawing.Point(3, 7);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(13, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "1";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(1, 58);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 4;
            this.button5.Text = "OK";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label12Y1
            // 
            this.label12Y1.AutoSize = true;
            this.label12Y1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label12Y1.Location = new System.Drawing.Point(107, 7);
            this.label12Y1.Name = "label12Y1";
            this.label12Y1.Size = new System.Drawing.Size(14, 13);
            this.label12Y1.TabIndex = 3;
            this.label12Y1.Text = "Y";
            // 
            // label11Y2
            // 
            this.label11Y2.AutoSize = true;
            this.label11Y2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label11Y2.Location = new System.Drawing.Point(108, 29);
            this.label11Y2.Name = "label11Y2";
            this.label11Y2.Size = new System.Drawing.Size(14, 13);
            this.label11Y2.TabIndex = 3;
            this.label11Y2.Text = "Y";
            // 
            // c1Y
            // 
            this.c1Y.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.c1Y.Location = new System.Drawing.Point(122, 4);
            this.c1Y.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.c1Y.Name = "c1Y";
            this.c1Y.Size = new System.Drawing.Size(71, 20);
            this.c1Y.TabIndex = 1;
            this.c1Y.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.c1Y.ValueChanged += new System.EventHandler(this.c1Y_ValueChanged);
            // 
            // label13X2
            // 
            this.label13X2.AutoSize = true;
            this.label13X2.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label13X2.Location = new System.Drawing.Point(21, 29);
            this.label13X2.Name = "label13X2";
            this.label13X2.Size = new System.Drawing.Size(14, 13);
            this.label13X2.TabIndex = 2;
            this.label13X2.Text = "X";
            // 
            // label10X1
            // 
            this.label10X1.AutoSize = true;
            this.label10X1.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.label10X1.Location = new System.Drawing.Point(21, 7);
            this.label10X1.Name = "label10X1";
            this.label10X1.Size = new System.Drawing.Size(14, 13);
            this.label10X1.TabIndex = 2;
            this.label10X1.Text = "X";
            // 
            // c2Y
            // 
            this.c2Y.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.c2Y.Location = new System.Drawing.Point(122, 29);
            this.c2Y.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.c2Y.Name = "c2Y";
            this.c2Y.Size = new System.Drawing.Size(71, 20);
            this.c2Y.TabIndex = 1;
            this.c2Y.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.c2Y.ValueChanged += new System.EventHandler(this.c2Y_ValueChanged);
            // 
            // c2X
            // 
            this.c2X.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.c2X.Location = new System.Drawing.Point(36, 29);
            this.c2X.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.c2X.Name = "c2X";
            this.c2X.Size = new System.Drawing.Size(71, 20);
            this.c2X.TabIndex = 0;
            this.c2X.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.c2X.ValueChanged += new System.EventHandler(this.c2X_ValueChanged);
            // 
            // c1X
            // 
            this.c1X.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.c1X.Location = new System.Drawing.Point(36, 4);
            this.c1X.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.c1X.Name = "c1X";
            this.c1X.Size = new System.Drawing.Size(71, 20);
            this.c1X.TabIndex = 0;
            this.c1X.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.c1X.ValueChanged += new System.EventHandler(this.c1X_ValueChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 813);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1275, 35);
            this.flowLayoutPanel1.TabIndex = 27;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxShowAll);
            this.groupBox1.Controls.Add(this.radioShowAll);
            this.groupBox1.Controls.Add(this.radioOnyFailed);
            this.groupBox1.Location = new System.Drawing.Point(1071, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 76);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opcje widoku";
            // 
            // checkBoxShowAll
            // 
            this.checkBoxShowAll.AutoSize = true;
            this.checkBoxShowAll.Checked = true;
            this.checkBoxShowAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowAll.Location = new System.Drawing.Point(8, 56);
            this.checkBoxShowAll.Name = "checkBoxShowAll";
            this.checkBoxShowAll.Size = new System.Drawing.Size(117, 17);
            this.checkBoxShowAll.TabIndex = 19;
            this.checkBoxShowAll.Text = "Pokazuj tylko wady";
            this.checkBoxShowAll.UseVisualStyleBackColor = true;
            this.checkBoxShowAll.CheckedChanged += new System.EventHandler(this.checkBoxShowAll_CheckedChanged);
            // 
            // labelOperator
            // 
            this.labelOperator.AutoSize = true;
            this.labelOperator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelOperator.Location = new System.Drawing.Point(13, 2);
            this.labelOperator.Name = "labelOperator";
            this.labelOperator.Size = new System.Drawing.Size(89, 17);
            this.labelOperator.TabIndex = 29;
            this.labelOperator.Text = "Zalogowano:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(229, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "_____________________________________";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(444, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 26);
            this.label2.TabIndex = 31;
            this.label2.Text = "Panel MB ID:";
            // 
            // hV_procedure_DataSet
            // 
            this.hV_procedure_DataSet.DataSetName = "HV_procedure_DataSet";
            this.hV_procedure_DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sp_hvBindingSource
            // 
            this.sp_hvBindingSource.DataMember = "sp_hv";
            this.sp_hvBindingSource.DataSource = this.hV_procedure_DataSet;
            // 
            // sp_hvTableAdapter
            // 
            this.sp_hvTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.Connection = null;
            this.tableAdapterManager.UpdateOrder = LED_MB_check.HV_procedure_DataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // sp_functionBindingSource
            // 
            this.sp_functionBindingSource.DataMember = "sp_function";
            this.sp_functionBindingSource.DataSource = this.dataSet1;
            // 
            // sp_functionTableAdapter
            // 
            this.sp_functionTableAdapter.ClearBeforeFill = true;
            // 
            // sp_visionBindingSource
            // 
            this.sp_visionBindingSource.DataMember = "sp_vision";
            this.sp_visionBindingSource.DataSource = this.dataSet1;
            // 
            // sp_visionTableAdapter
            // 
            this.sp_visionTableAdapter.ClearBeforeFill = true;
            // 
            // buttonZlecenie
            // 
            this.buttonZlecenie.Image = global::LED_MB_check.Properties.Resources.refresh;
            this.buttonZlecenie.Location = new System.Drawing.Point(178, 47);
            this.buttonZlecenie.Name = "buttonZlecenie";
            this.buttonZlecenie.Size = new System.Drawing.Size(50, 50);
            this.buttonZlecenie.TabIndex = 2;
            this.toolTip1.SetToolTip(this.buttonZlecenie, "Odśwież");
            this.buttonZlecenie.UseVisualStyleBackColor = true;
            this.buttonZlecenie.Click += new System.EventHandler(this.button1_Click);
            // 
            // button3
            // 
            this.button3.BackgroundImage = global::LED_MB_check.Properties.Resources._1478269080_onebit_33;
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button3.Location = new System.Drawing.Point(1220, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(60, 60);
            this.button3.TabIndex = 16;
            this.toolTip1.SetToolTip(this.button3, "WYLOGUJ");
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonListen
            // 
            this.buttonListen.BackColor = System.Drawing.Color.Lime;
            this.buttonListen.BackgroundImage = global::LED_MB_check.Properties.Resources.listen_ear_icon_0;
            this.buttonListen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonListen.Location = new System.Drawing.Point(785, 1);
            this.buttonListen.Name = "buttonListen";
            this.buttonListen.Size = new System.Drawing.Size(47, 43);
            this.buttonListen.TabIndex = 14;
            this.toolTip1.SetToolTip(this.buttonListen, "Nasłuchuj klawiaturę");
            this.buttonListen.UseVisualStyleBackColor = false;
            this.buttonListen.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonViewVisFailures
            // 
            this.buttonViewVisFailures.BackgroundImage = global::LED_MB_check.Properties.Resources.camera;
            this.buttonViewVisFailures.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonViewVisFailures.Location = new System.Drawing.Point(1208, 116);
            this.buttonViewVisFailures.Name = "buttonViewVisFailures";
            this.buttonViewVisFailures.Size = new System.Drawing.Size(63, 60);
            this.buttonViewVisFailures.TabIndex = 32;
            this.buttonViewVisFailures.UseVisualStyleBackColor = true;
            this.buttonViewVisFailures.Visible = false;
            this.buttonViewVisFailures.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pictureBoxLoading
            // 
            this.pictureBoxLoading.Image = global::LED_MB_check.Properties.Resources._30__1_;
            this.pictureBoxLoading.Location = new System.Drawing.Point(6, 109);
            this.pictureBoxLoading.Name = "pictureBoxLoading";
            this.pictureBoxLoading.Size = new System.Drawing.Size(200, 25);
            this.pictureBoxLoading.TabIndex = 20;
            this.pictureBoxLoading.TabStop = false;
            this.pictureBoxLoading.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 870);
            this.Controls.Add(this.buttonViewVisFailures);
            this.Controls.Add(this.buttonZlecenie);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelOperator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panelDrawSettings);
            this.Controls.Add(this.buttonDrawSettings);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3status);
            this.Controls.Add(this.listViewHV);
            this.Controls.Add(this.listViewLightUp);
            this.Controls.Add(this.listViewFunc);
            this.Controls.Add(this.textBoxMB);
            this.Controls.Add(this.dataGridHV);
            this.Controls.Add(this.buttonListen);
            this.Controls.Add(this.dataGridLightUp);
            this.Controls.Add(this.dataGridFunc);
            this.Controls.Add(this.panelVision);
            this.Controls.Add(this.panelFunc);
            this.Controls.Add(this.panelHV);
            this.Controls.Add(this.label6testV);
            this.Controls.Add(this.label5testF);
            this.Controls.Add(this.label4testhv);
            this.Controls.Add(this.labelTestReusltInfo);
            this.Controls.Add(this.label1zlecenie);
            this.Controls.Add(this.textBoxZlecenie);
            this.Controls.Add(this.pictureBoxLoading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.panelHV.ResumeLayout(false);
            this.panelVision.ResumeLayout(false);
            this.panelFunc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridFunc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridLightUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridHV)).EndInit();
            this.panelDrawSettings.ResumeLayout(false);
            this.panelDrawSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c2Y)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c2X)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1X)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hV_procedure_DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_hvBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_functionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sp_visionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLoading)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataSet1 dataSet1;
        private System.Windows.Forms.BindingSource sp_functionBindingSource;
        private DataSet1TableAdapters.sp_functionTableAdapter sp_functionTableAdapter;
        private System.Windows.Forms.TextBox textBoxZlecenie;
        private System.Windows.Forms.Label label1zlecenie;
        private System.Windows.Forms.Button buttonZlecenie;
        private System.Windows.Forms.Label labelTestReusltInfo;
        private System.Windows.Forms.Label label4testhv;
        private System.Windows.Forms.Label label5testF;
        private System.Windows.Forms.Label label6testV;
        private System.Windows.Forms.Panel panelHV;
        private System.Windows.Forms.Label labelHVResult;
        private System.Windows.Forms.Panel panelVision;
        private System.Windows.Forms.Label labelVisionResult;
        private System.Windows.Forms.Panel panelFunc;
        private System.Windows.Forms.Label labelFuncResult;
        private System.Windows.Forms.DataGridView dataGridFunc;
        private System.Windows.Forms.DataGridView dataGridLightUp;
        private System.Windows.Forms.DataGridView dataGridHV;
        private System.Windows.Forms.Button buttonListen;
        private System.Windows.Forms.TextBox textBoxMB;
        private System.Windows.Forms.BindingSource sp_visionBindingSource;
        private DataSet1TableAdapters.sp_visionTableAdapter sp_visionTableAdapter;
        //private System.Windows.Forms.ToolStrip fillToolStrip;
       // private System.Windows.Forms.ToolStripLabel numerZlecToolStripLabel;
        //private System.Windows.Forms.ToolStripTextBox numerZlecToolStripTextBox;
       // private System.Windows.Forms.ToolStripButton fillToolStripButton;
        private System.Windows.Forms.ListView listViewFunc;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.RadioButton radioOnyFailed;
        private System.Windows.Forms.RadioButton radioShowAll;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ListView listViewLightUp;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox pictureBoxLoading;
        private System.Windows.Forms.Timer timerThread;
        private System.Windows.Forms.ListView listViewHV;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.Label label3status;
        private System.Windows.Forms.CheckBox checkBox1;
        private HV_procedure_DataSet hV_procedure_DataSet;
        private System.Windows.Forms.BindingSource sp_hvBindingSource;
        private HV_procedure_DataSetTableAdapters.sp_hvTableAdapter sp_hvTableAdapter;
        private HV_procedure_DataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.Button buttonDrawSettings;
        private System.Windows.Forms.Panel panelDrawSettings;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label11Y2;
        private System.Windows.Forms.Label label10X1;
        private System.Windows.Forms.NumericUpDown c2Y;
        private System.Windows.Forms.NumericUpDown c1X;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12Y1;
        private System.Windows.Forms.NumericUpDown c1Y;
        private System.Windows.Forms.Label label13X2;
        private System.Windows.Forms.NumericUpDown c2X;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxShowAll;
        private System.Windows.Forms.Label labelOperator;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonViewVisFailures;
    }
}

