using ControlCustomer;

namespace Entities
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
            this.label1 = new System.Windows.Forms.Label();
            this.rText_Conn = new System.Windows.Forms.RichTextBox();
            this.Btn_Connect = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.Render = new System.Windows.Forms.TabPage();
            this.pnl_CheckColumn = new System.Windows.Forms.Panel();
            this.tb_ClassName = new System.Windows.Forms.TextBox();
            this.tb_NameVietNamese = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_NameSpace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rText_Result = new System.Windows.Forms.RichTextBox();
            this.btn_RenderID = new System.Windows.Forms.Button();
            this.btn_Test = new System.Windows.Forms.Button();
            this.btn_Controller = new System.Windows.Forms.Button();
            this.cb_MaxLenght = new System.Windows.Forms.CheckBox();
            this.btn_Service = new System.Windows.Forms.Button();
            this.btn_Model = new System.Windows.Forms.Button();
            this.cb_AndOr = new System.Windows.Forms.CheckBox();
            this.cb_Require = new System.Windows.Forms.CheckBox();
            this.btn_Render = new System.Windows.Forms.Button();
            this.cbb_Table = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.cb_IsRandom2 = new System.Windows.Forms.CheckBox();
            this.tb_AddCol2 = new System.Windows.Forms.TextBox();
            this.nb_Random2 = new System.Windows.Forms.NumericUpDown();
            this.nb_PageSize2 = new System.Windows.Forms.NumericUpDown();
            this.nb_Page2 = new System.Windows.Forms.NumericUpDown();
            this.btn_Convert2 = new System.Windows.Forms.Button();
            this.btn_Insert2 = new System.Windows.Forms.Button();
            this.btn_AddCol2 = new System.Windows.Forms.Button();
            this.btn_Get2 = new System.Windows.Forms.Button();
            this.pnlCol2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rtb_Result2 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dta2 = new System.Windows.Forms.DataGridView();
            this.cbb_Table2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tabSQLScript = new System.Windows.Forms.TabPage();
            this.btn_SQLScriptSelect = new System.Windows.Forms.Button();
            this.btn_SQLScriptGetColumn = new System.Windows.Forms.Button();
            this.tb_FindColumnByTableName = new System.Windows.Forms.TextBox();
            this.btn_SQLScriptGetTable = new System.Windows.Forms.Button();
            this.rtb_SuggestScript = new System.Windows.Forms.RichTextBox();
            this.rtb_ResultScript = new System.Windows.Forms.RichTextBox();
            this.btn_ExcuteScript = new System.Windows.Forms.Button();
            this.rtb_SQLScript = new ControlCustomer.MyRichTextBox();
            this.tabSQLExecute = new System.Windows.Forms.TabPage();
            this.dgv_SQLExecuteResult = new System.Windows.Forms.DataGridView();
            this.btn_SQLExecutePaste = new System.Windows.Forms.Button();
            this.btn_SQLExecute = new System.Windows.Forms.Button();
            this.rtb_SQLExecute = new ControlCustomer.MyRichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.cb_IsKeepDown = new System.Windows.Forms.CheckBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.rtb_ConvertText = new System.Windows.Forms.RichTextBox();
            this.btn_ConvertTextCSharp = new System.Windows.Forms.Button();
            this.autoCompleteSQLScript = new AutocompleteMenuNS.AutocompleteMenu();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btn_SQLScriptCreateTable = new System.Windows.Forms.Button();
            this.btn_SQLScriptDataType = new System.Windows.Forms.Button();
            this.cbb_SQLScriptType = new System.Windows.Forms.ComboBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.Render.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nb_Random2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nb_PageSize2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nb_Page2)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dta2)).BeginInit();
            this.tabSQLScript.SuspendLayout();
            this.tabSQLExecute.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SQLExecuteResult)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "ConnectionString";
            // 
            // rText_Conn
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.rText_Conn, null);
            this.rText_Conn.Location = new System.Drawing.Point(97, 6);
            this.rText_Conn.Name = "rText_Conn";
            this.rText_Conn.Size = new System.Drawing.Size(734, 46);
            this.rText_Conn.TabIndex = 2;
            this.rText_Conn.Text = "";
            // 
            // Btn_Connect
            // 
            this.Btn_Connect.Location = new System.Drawing.Point(6, 28);
            this.Btn_Connect.Name = "Btn_Connect";
            this.Btn_Connect.Size = new System.Drawing.Size(85, 24);
            this.Btn_Connect.TabIndex = 3;
            this.Btn_Connect.Text = "Connect";
            this.Btn_Connect.UseVisualStyleBackColor = true;
            this.Btn_Connect.Click += new System.EventHandler(this.Btn_Connect_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabSQLScript);
            this.tabControl1.Controls.Add(this.tabSQLExecute);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.ItemSize = new System.Drawing.Size(48, 18);
            this.tabControl1.Location = new System.Drawing.Point(2, 1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(850, 518);
            this.tabControl1.TabIndex = 8;
            this.tabControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyUp);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tabControl2);
            this.tabPage1.Controls.Add(this.rText_Conn);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.Btn_Connect);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(842, 492);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SQL Server";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.Render);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Location = new System.Drawing.Point(4, 59);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(831, 430);
            this.tabControl2.TabIndex = 17;
            // 
            // Render
            // 
            this.Render.Controls.Add(this.pnl_CheckColumn);
            this.Render.Controls.Add(this.tb_ClassName);
            this.Render.Controls.Add(this.tb_NameVietNamese);
            this.Render.Controls.Add(this.label11);
            this.Render.Controls.Add(this.tb_NameSpace);
            this.Render.Controls.Add(this.label4);
            this.Render.Controls.Add(this.label5);
            this.Render.Controls.Add(this.label2);
            this.Render.Controls.Add(this.rText_Result);
            this.Render.Controls.Add(this.btn_RenderID);
            this.Render.Controls.Add(this.btn_Test);
            this.Render.Controls.Add(this.btn_Controller);
            this.Render.Controls.Add(this.cb_MaxLenght);
            this.Render.Controls.Add(this.btn_Service);
            this.Render.Controls.Add(this.btn_Model);
            this.Render.Controls.Add(this.cb_AndOr);
            this.Render.Controls.Add(this.cb_Require);
            this.Render.Controls.Add(this.btn_Render);
            this.Render.Controls.Add(this.cbb_Table);
            this.Render.Controls.Add(this.label3);
            this.Render.Location = new System.Drawing.Point(4, 22);
            this.Render.Name = "Render";
            this.Render.Padding = new System.Windows.Forms.Padding(3);
            this.Render.Size = new System.Drawing.Size(823, 404);
            this.Render.TabIndex = 0;
            this.Render.Text = "Render";
            this.Render.UseVisualStyleBackColor = true;
            // 
            // pnl_CheckColumn
            // 
            this.pnl_CheckColumn.AutoScroll = true;
            this.pnl_CheckColumn.BackColor = System.Drawing.Color.OldLace;
            this.pnl_CheckColumn.Location = new System.Drawing.Point(7, 87);
            this.pnl_CheckColumn.Name = "pnl_CheckColumn";
            this.pnl_CheckColumn.Size = new System.Drawing.Size(151, 310);
            this.pnl_CheckColumn.TabIndex = 50;
            // 
            // tb_ClassName
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.tb_ClassName, null);
            this.tb_ClassName.Location = new System.Drawing.Point(491, 4);
            this.tb_ClassName.Name = "tb_ClassName";
            this.tb_ClassName.Size = new System.Drawing.Size(191, 20);
            this.tb_ClassName.TabIndex = 45;
            // 
            // tb_NameVietNamese
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.tb_NameVietNamese, null);
            this.tb_NameVietNamese.Location = new System.Drawing.Point(491, 32);
            this.tb_NameVietNamese.Name = "tb_NameVietNamese";
            this.tb_NameVietNamese.Size = new System.Drawing.Size(191, 20);
            this.tb_NameVietNamese.TabIndex = 45;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(422, 7);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 40;
            this.label11.Text = "ClassName:";
            // 
            // tb_NameSpace
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.tb_NameSpace, null);
            this.tb_NameSpace.Location = new System.Drawing.Point(97, 31);
            this.tb_NameSpace.Name = "tb_NameSpace";
            this.tb_NameSpace.Size = new System.Drawing.Size(273, 20);
            this.tb_NameSpace.TabIndex = 44;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(393, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 13);
            this.label4.TabIndex = 40;
            this.label4.Text = "VietNameseName";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 41;
            this.label5.Text = "NameSpace";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 42;
            this.label2.Text = "Col-Unique";
            // 
            // rText_Result
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.rText_Result, null);
            this.rText_Result.Location = new System.Drawing.Point(159, 87);
            this.rText_Result.Name = "rText_Result";
            this.rText_Result.Size = new System.Drawing.Size(658, 310);
            this.rText_Result.TabIndex = 34;
            this.rText_Result.Text = "";
            // 
            // btn_RenderID
            // 
            this.btn_RenderID.Location = new System.Drawing.Point(723, 58);
            this.btn_RenderID.Name = "btn_RenderID";
            this.btn_RenderID.Size = new System.Drawing.Size(85, 24);
            this.btn_RenderID.TabIndex = 35;
            this.btn_RenderID.Text = "RenderID";
            this.btn_RenderID.UseVisualStyleBackColor = true;
            this.btn_RenderID.Click += new System.EventHandler(this.Btn_RenderID_Click);
            // 
            // btn_Test
            // 
            this.btn_Test.Location = new System.Drawing.Point(473, 57);
            this.btn_Test.Name = "btn_Test";
            this.btn_Test.Size = new System.Drawing.Size(65, 24);
            this.btn_Test.TabIndex = 49;
            this.btn_Test.Text = "test";
            this.btn_Test.UseVisualStyleBackColor = true;
            this.btn_Test.Click += new System.EventHandler(this.btn_Test_Click);
            // 
            // btn_Controller
            // 
            this.btn_Controller.Location = new System.Drawing.Point(385, 57);
            this.btn_Controller.Name = "btn_Controller";
            this.btn_Controller.Size = new System.Drawing.Size(65, 24);
            this.btn_Controller.TabIndex = 49;
            this.btn_Controller.Text = "Controller";
            this.btn_Controller.UseVisualStyleBackColor = true;
            this.btn_Controller.Click += new System.EventHandler(this.Btn_Controller_Click);
            // 
            // cb_MaxLenght
            // 
            this.cb_MaxLenght.AutoSize = true;
            this.cb_MaxLenght.Checked = true;
            this.cb_MaxLenght.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_MaxLenght.Location = new System.Drawing.Point(729, 7);
            this.cb_MaxLenght.Name = "cb_MaxLenght";
            this.cb_MaxLenght.Size = new System.Drawing.Size(79, 17);
            this.cb_MaxLenght.TabIndex = 37;
            this.cb_MaxLenght.Text = "MaxLength";
            this.cb_MaxLenght.UseVisualStyleBackColor = true;
            // 
            // btn_Service
            // 
            this.btn_Service.Location = new System.Drawing.Point(305, 57);
            this.btn_Service.Name = "btn_Service";
            this.btn_Service.Size = new System.Drawing.Size(65, 24);
            this.btn_Service.TabIndex = 48;
            this.btn_Service.Text = "Service";
            this.btn_Service.UseVisualStyleBackColor = true;
            this.btn_Service.Click += new System.EventHandler(this.Btn_Service_Click);
            // 
            // btn_Model
            // 
            this.btn_Model.Location = new System.Drawing.Point(234, 57);
            this.btn_Model.Name = "btn_Model";
            this.btn_Model.Size = new System.Drawing.Size(65, 24);
            this.btn_Model.TabIndex = 47;
            this.btn_Model.Text = "Model";
            this.btn_Model.UseVisualStyleBackColor = true;
            this.btn_Model.Click += new System.EventHandler(this.Btn_Model_Click);
            // 
            // cb_AndOr
            // 
            this.cb_AndOr.AutoSize = true;
            this.cb_AndOr.Checked = true;
            this.cb_AndOr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_AndOr.Location = new System.Drawing.Point(81, 62);
            this.cb_AndOr.Name = "cb_AndOr";
            this.cb_AndOr.Size = new System.Drawing.Size(58, 17);
            this.cb_AndOr.TabIndex = 38;
            this.cb_AndOr.Text = "and/or";
            this.cb_AndOr.UseVisualStyleBackColor = true;
            // 
            // cb_Require
            // 
            this.cb_Require.AutoSize = true;
            this.cb_Require.Checked = true;
            this.cb_Require.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_Require.Location = new System.Drawing.Point(729, 30);
            this.cb_Require.Name = "cb_Require";
            this.cb_Require.Size = new System.Drawing.Size(63, 17);
            this.cb_Require.TabIndex = 39;
            this.cb_Require.Text = "Require";
            this.cb_Require.UseVisualStyleBackColor = true;
            // 
            // btn_Render
            // 
            this.btn_Render.Location = new System.Drawing.Point(159, 57);
            this.btn_Render.Name = "btn_Render";
            this.btn_Render.Size = new System.Drawing.Size(69, 24);
            this.btn_Render.TabIndex = 46;
            this.btn_Render.Text = "Entity";
            this.btn_Render.UseVisualStyleBackColor = true;
            this.btn_Render.Click += new System.EventHandler(this.Btn_Render_Click);
            // 
            // cbb_Table
            // 
            this.cbb_Table.AllowDrop = true;
            this.cbb_Table.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbb_Table.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbb_Table.FormattingEnabled = true;
            this.cbb_Table.Location = new System.Drawing.Point(97, 3);
            this.cbb_Table.Name = "cbb_Table";
            this.cbb_Table.Size = new System.Drawing.Size(273, 21);
            this.cbb_Table.TabIndex = 43;
            this.cbb_Table.Tag = "";
            this.cbb_Table.SelectedValueChanged += new System.EventHandler(this.Cbb_Table_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 36;
            this.label3.Text = "Choose Table";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.cb_IsRandom2);
            this.tabPage4.Controls.Add(this.tb_AddCol2);
            this.tabPage4.Controls.Add(this.nb_Random2);
            this.tabPage4.Controls.Add(this.nb_PageSize2);
            this.tabPage4.Controls.Add(this.nb_Page2);
            this.tabPage4.Controls.Add(this.btn_Convert2);
            this.tabPage4.Controls.Add(this.btn_Insert2);
            this.tabPage4.Controls.Add(this.btn_AddCol2);
            this.tabPage4.Controls.Add(this.btn_Get2);
            this.tabPage4.Controls.Add(this.pnlCol2);
            this.tabPage4.Controls.Add(this.label8);
            this.tabPage4.Controls.Add(this.panel3);
            this.tabPage4.Controls.Add(this.panel1);
            this.tabPage4.Controls.Add(this.cbb_Table2);
            this.tabPage4.Controls.Add(this.label9);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.label7);
            this.tabPage4.Controls.Add(this.label6);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(823, 404);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "Database";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // cb_IsRandom2
            // 
            this.cb_IsRandom2.AutoSize = true;
            this.cb_IsRandom2.Location = new System.Drawing.Point(301, 14);
            this.cb_IsRandom2.Name = "cb_IsRandom2";
            this.cb_IsRandom2.Size = new System.Drawing.Size(74, 17);
            this.cb_IsRandom2.TabIndex = 57;
            this.cb_IsRandom2.Text = "IsRandom";
            this.cb_IsRandom2.UseVisualStyleBackColor = true;
            // 
            // tb_AddCol2
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.tb_AddCol2, null);
            this.tb_AddCol2.Location = new System.Drawing.Point(7, 50);
            this.tb_AddCol2.Name = "tb_AddCol2";
            this.tb_AddCol2.Size = new System.Drawing.Size(82, 20);
            this.tb_AddCol2.TabIndex = 56;
            // 
            // nb_Random2
            // 
            this.nb_Random2.Location = new System.Drawing.Point(221, 49);
            this.nb_Random2.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nb_Random2.Name = "nb_Random2";
            this.nb_Random2.Size = new System.Drawing.Size(59, 20);
            this.nb_Random2.TabIndex = 55;
            this.nb_Random2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nb_PageSize2
            // 
            this.nb_PageSize2.Location = new System.Drawing.Point(553, 9);
            this.nb_PageSize2.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nb_PageSize2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nb_PageSize2.Name = "nb_PageSize2";
            this.nb_PageSize2.Size = new System.Drawing.Size(59, 20);
            this.nb_PageSize2.TabIndex = 55;
            this.nb_PageSize2.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // nb_Page2
            // 
            this.nb_Page2.Location = new System.Drawing.Point(425, 11);
            this.nb_Page2.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nb_Page2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nb_Page2.Name = "nb_Page2";
            this.nb_Page2.Size = new System.Drawing.Size(59, 20);
            this.nb_Page2.TabIndex = 55;
            this.nb_Page2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btn_Convert2
            // 
            this.btn_Convert2.Location = new System.Drawing.Point(494, 50);
            this.btn_Convert2.Name = "btn_Convert2";
            this.btn_Convert2.Size = new System.Drawing.Size(65, 23);
            this.btn_Convert2.TabIndex = 54;
            this.btn_Convert2.Text = "Json";
            this.btn_Convert2.UseVisualStyleBackColor = true;
            this.btn_Convert2.Click += new System.EventHandler(this.Btn_Convert2_Click);
            // 
            // btn_Insert2
            // 
            this.btn_Insert2.Location = new System.Drawing.Point(301, 47);
            this.btn_Insert2.Name = "btn_Insert2";
            this.btn_Insert2.Size = new System.Drawing.Size(65, 23);
            this.btn_Insert2.TabIndex = 54;
            this.btn_Insert2.Text = "Insert";
            this.btn_Insert2.UseVisualStyleBackColor = true;
            this.btn_Insert2.Click += new System.EventHandler(this.Btn_Insert2_Click);
            // 
            // btn_AddCol2
            // 
            this.btn_AddCol2.Location = new System.Drawing.Point(95, 48);
            this.btn_AddCol2.Name = "btn_AddCol2";
            this.btn_AddCol2.Size = new System.Drawing.Size(65, 23);
            this.btn_AddCol2.TabIndex = 54;
            this.btn_AddCol2.Text = "Add Col";
            this.btn_AddCol2.UseVisualStyleBackColor = true;
            this.btn_AddCol2.Click += new System.EventHandler(this.Btn_AddCol2_Click);
            // 
            // btn_Get2
            // 
            this.btn_Get2.Location = new System.Drawing.Point(621, 8);
            this.btn_Get2.Name = "btn_Get2";
            this.btn_Get2.Size = new System.Drawing.Size(75, 23);
            this.btn_Get2.TabIndex = 53;
            this.btn_Get2.Text = "Get Data";
            this.btn_Get2.UseVisualStyleBackColor = true;
            this.btn_Get2.Click += new System.EventHandler(this.Btn_Get2_Click);
            // 
            // pnlCol2
            // 
            this.pnlCol2.AutoScroll = true;
            this.pnlCol2.BackColor = System.Drawing.Color.OldLace;
            this.pnlCol2.Location = new System.Drawing.Point(9, 77);
            this.pnlCol2.Name = "pnlCol2";
            this.pnlCol2.Size = new System.Drawing.Size(151, 321);
            this.pnlCol2.TabIndex = 52;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(8, 37);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 51;
            this.label8.Text = "Columns";
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.rtb_Result2);
            this.panel3.Location = new System.Drawing.Point(494, 77);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(323, 321);
            this.panel3.TabIndex = 47;
            // 
            // rtb_Result2
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.rtb_Result2, null);
            this.rtb_Result2.Location = new System.Drawing.Point(5, 7);
            this.rtb_Result2.Name = "rtb_Result2";
            this.rtb_Result2.Size = new System.Drawing.Size(315, 314);
            this.rtb_Result2.TabIndex = 0;
            this.rtb_Result2.Text = "";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.dta2);
            this.panel1.Location = new System.Drawing.Point(166, 77);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(327, 321);
            this.panel1.TabIndex = 47;
            // 
            // dta2
            // 
            this.dta2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dta2.Location = new System.Drawing.Point(4, 0);
            this.dta2.Name = "dta2";
            this.dta2.Size = new System.Drawing.Size(320, 321);
            this.dta2.TabIndex = 0;
            // 
            // cbb_Table2
            // 
            this.cbb_Table2.AllowDrop = true;
            this.cbb_Table2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbb_Table2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbb_Table2.FormattingEnabled = true;
            this.cbb_Table2.Location = new System.Drawing.Point(99, 11);
            this.cbb_Table2.Name = "cbb_Table2";
            this.cbb_Table2.Size = new System.Drawing.Size(181, 21);
            this.cbb_Table2.TabIndex = 45;
            this.cbb_Table2.Tag = "";
            this.cbb_Table2.SelectedValueChanged += new System.EventHandler(this.Cbb_Table2_SelectedValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(167, 53);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 13);
            this.label9.TabIndex = 44;
            this.label9.Text = "Random";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(496, 12);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 13);
            this.label10.TabIndex = 44;
            this.label10.Text = "PageSize";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(387, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 13);
            this.label7.TabIndex = 44;
            this.label7.Text = "Page";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 14);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Choose Table";
            // 
            // tabSQLScript
            // 
            this.tabSQLScript.Controls.Add(this.cbb_SQLScriptType);
            this.tabSQLScript.Controls.Add(this.btn_SQLScriptDataType);
            this.tabSQLScript.Controls.Add(this.btn_SQLScriptCreateTable);
            this.tabSQLScript.Controls.Add(this.btn_SQLScriptSelect);
            this.tabSQLScript.Controls.Add(this.btn_SQLScriptGetColumn);
            this.tabSQLScript.Controls.Add(this.tb_FindColumnByTableName);
            this.tabSQLScript.Controls.Add(this.btn_SQLScriptGetTable);
            this.tabSQLScript.Controls.Add(this.rtb_SuggestScript);
            this.tabSQLScript.Controls.Add(this.rtb_ResultScript);
            this.tabSQLScript.Controls.Add(this.btn_ExcuteScript);
            this.tabSQLScript.Controls.Add(this.rtb_SQLScript);
            this.tabSQLScript.Location = new System.Drawing.Point(4, 22);
            this.tabSQLScript.Name = "tabSQLScript";
            this.tabSQLScript.Size = new System.Drawing.Size(842, 492);
            this.tabSQLScript.TabIndex = 2;
            this.tabSQLScript.Text = "SQL Script";
            this.tabSQLScript.UseVisualStyleBackColor = true;
            // 
            // btn_SQLScriptSelect
            // 
            this.btn_SQLScriptSelect.Location = new System.Drawing.Point(86, 2);
            this.btn_SQLScriptSelect.Name = "btn_SQLScriptSelect";
            this.btn_SQLScriptSelect.Size = new System.Drawing.Size(75, 23);
            this.btn_SQLScriptSelect.TabIndex = 8;
            this.btn_SQLScriptSelect.Text = "Suggest";
            this.btn_SQLScriptSelect.UseVisualStyleBackColor = true;
            this.btn_SQLScriptSelect.Click += new System.EventHandler(this.Btn_SQLScriptSelect_Click);
            // 
            // btn_SQLScriptGetColumn
            // 
            this.btn_SQLScriptGetColumn.Location = new System.Drawing.Point(546, 3);
            this.btn_SQLScriptGetColumn.Name = "btn_SQLScriptGetColumn";
            this.btn_SQLScriptGetColumn.Size = new System.Drawing.Size(75, 23);
            this.btn_SQLScriptGetColumn.TabIndex = 8;
            this.btn_SQLScriptGetColumn.Text = "Get Columns";
            this.btn_SQLScriptGetColumn.UseVisualStyleBackColor = true;
            this.btn_SQLScriptGetColumn.Click += new System.EventHandler(this.Btn_SQLScriptGetColumn_Click);
            // 
            // tb_FindColumnByTableName
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.tb_FindColumnByTableName, null);
            this.tb_FindColumnByTableName.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tb_FindColumnByTableName.Location = new System.Drawing.Point(349, 3);
            this.tb_FindColumnByTableName.Name = "tb_FindColumnByTableName";
            this.tb_FindColumnByTableName.Size = new System.Drawing.Size(188, 22);
            this.tb_FindColumnByTableName.TabIndex = 7;
            // 
            // btn_SQLScriptGetTable
            // 
            this.btn_SQLScriptGetTable.Location = new System.Drawing.Point(268, 3);
            this.btn_SQLScriptGetTable.Name = "btn_SQLScriptGetTable";
            this.btn_SQLScriptGetTable.Size = new System.Drawing.Size(75, 23);
            this.btn_SQLScriptGetTable.TabIndex = 6;
            this.btn_SQLScriptGetTable.Text = "Get Tables";
            this.btn_SQLScriptGetTable.UseVisualStyleBackColor = true;
            this.btn_SQLScriptGetTable.Click += new System.EventHandler(this.Btn_SQLScriptGetTable_Click);
            // 
            // rtb_SuggestScript
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.rtb_SuggestScript, null);
            this.rtb_SuggestScript.Location = new System.Drawing.Point(4, 365);
            this.rtb_SuggestScript.Name = "rtb_SuggestScript";
            this.rtb_SuggestScript.Size = new System.Drawing.Size(835, 124);
            this.rtb_SuggestScript.TabIndex = 5;
            this.rtb_SuggestScript.Text = "";
            this.rtb_SuggestScript.DoubleClick += new System.EventHandler(this.Rtb_SuggestScript_DoubleClick);
            // 
            // rtb_ResultScript
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.rtb_ResultScript, null);
            this.rtb_ResultScript.Location = new System.Drawing.Point(3, 298);
            this.rtb_ResultScript.Name = "rtb_ResultScript";
            this.rtb_ResultScript.Size = new System.Drawing.Size(835, 61);
            this.rtb_ResultScript.TabIndex = 4;
            this.rtb_ResultScript.Text = "";
            // 
            // btn_ExcuteScript
            // 
            this.btn_ExcuteScript.BackColor = System.Drawing.Color.Orange;
            this.btn_ExcuteScript.Location = new System.Drawing.Point(6, 2);
            this.btn_ExcuteScript.Name = "btn_ExcuteScript";
            this.btn_ExcuteScript.Size = new System.Drawing.Size(75, 23);
            this.btn_ExcuteScript.TabIndex = 3;
            this.btn_ExcuteScript.Text = "Execute";
            this.btn_ExcuteScript.UseVisualStyleBackColor = false;
            this.btn_ExcuteScript.Click += new System.EventHandler(this.Btn_ExcuteScript_Click);
            // 
            // rtb_SQLScript
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.rtb_SQLScript, null);
            this.rtb_SQLScript.Location = new System.Drawing.Point(2, 31);
            this.rtb_SQLScript.Name = "rtb_SQLScript";
            this.rtb_SQLScript.Size = new System.Drawing.Size(835, 263);
            this.rtb_SQLScript.TabIndex = 2;
            this.rtb_SQLScript.Text = "";
            this.rtb_SQLScript.WordWrap = false;
            this.rtb_SQLScript.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtb_SQLScript_KeyDown);
            this.rtb_SQLScript.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Rtb_SQLScript_MouseDoubleClick);
            // 
            // tabSQLExecute
            // 
            this.tabSQLExecute.Controls.Add(this.dgv_SQLExecuteResult);
            this.tabSQLExecute.Controls.Add(this.btn_SQLExecutePaste);
            this.tabSQLExecute.Controls.Add(this.btn_SQLExecute);
            this.tabSQLExecute.Controls.Add(this.rtb_SQLExecute);
            this.tabSQLExecute.Location = new System.Drawing.Point(4, 22);
            this.tabSQLExecute.Name = "tabSQLExecute";
            this.tabSQLExecute.Size = new System.Drawing.Size(842, 492);
            this.tabSQLExecute.TabIndex = 3;
            this.tabSQLExecute.Text = "SQLExecute";
            this.tabSQLExecute.UseVisualStyleBackColor = true;
            // 
            // dgv_SQLExecuteResult
            // 
            this.dgv_SQLExecuteResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_SQLExecuteResult.Location = new System.Drawing.Point(4, 230);
            this.dgv_SQLExecuteResult.Name = "dgv_SQLExecuteResult";
            this.dgv_SQLExecuteResult.Size = new System.Drawing.Size(831, 259);
            this.dgv_SQLExecuteResult.TabIndex = 7;
            this.dgv_SQLExecuteResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgv_SQLExecuteResult_CellFormatting);
            // 
            // btn_SQLExecutePaste
            // 
            this.btn_SQLExecutePaste.BackColor = System.Drawing.Color.Transparent;
            this.btn_SQLExecutePaste.Location = new System.Drawing.Point(98, 4);
            this.btn_SQLExecutePaste.Name = "btn_SQLExecutePaste";
            this.btn_SQLExecutePaste.Size = new System.Drawing.Size(75, 23);
            this.btn_SQLExecutePaste.TabIndex = 6;
            this.btn_SQLExecutePaste.Text = "Paste";
            this.btn_SQLExecutePaste.UseVisualStyleBackColor = false;
            this.btn_SQLExecutePaste.Click += new System.EventHandler(this.Btn_SQLExecutePaste_Click);
            // 
            // btn_SQLExecute
            // 
            this.btn_SQLExecute.BackColor = System.Drawing.Color.Orange;
            this.btn_SQLExecute.Location = new System.Drawing.Point(7, 4);
            this.btn_SQLExecute.Name = "btn_SQLExecute";
            this.btn_SQLExecute.Size = new System.Drawing.Size(75, 23);
            this.btn_SQLExecute.TabIndex = 6;
            this.btn_SQLExecute.Text = "Execute";
            this.btn_SQLExecute.UseVisualStyleBackColor = false;
            this.btn_SQLExecute.Click += new System.EventHandler(this.Btn_SQLExecute_Click);
            // 
            // rtb_SQLExecute
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.rtb_SQLExecute, null);
            this.rtb_SQLExecute.Location = new System.Drawing.Point(4, 30);
            this.rtb_SQLExecute.Name = "rtb_SQLExecute";
            this.rtb_SQLExecute.Size = new System.Drawing.Size(835, 193);
            this.rtb_SQLExecute.TabIndex = 5;
            this.rtb_SQLExecute.Text = "";
            this.rtb_SQLExecute.WordWrap = false;
            this.rtb_SQLExecute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtb_SQLExecute_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.cb_IsKeepDown);
            this.tabPage2.Controls.Add(this.btn_Clear);
            this.tabPage2.Controls.Add(this.rtb_ConvertText);
            this.tabPage2.Controls.Add(this.btn_ConvertTextCSharp);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(842, 492);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Text";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // cb_IsKeepDown
            // 
            this.cb_IsKeepDown.AutoSize = true;
            this.cb_IsKeepDown.Location = new System.Drawing.Point(273, 16);
            this.cb_IsKeepDown.Name = "cb_IsKeepDown";
            this.cb_IsKeepDown.Size = new System.Drawing.Size(87, 17);
            this.cb_IsKeepDown.TabIndex = 3;
            this.cb_IsKeepDown.Text = "IsKeepDown";
            this.cb_IsKeepDown.UseVisualStyleBackColor = true;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(477, 12);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 2;
            this.btn_Clear.Text = "Clear";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // rtb_ConvertText
            // 
            this.autoCompleteSQLScript.SetAutocompleteMenu(this.rtb_ConvertText, null);
            this.rtb_ConvertText.Location = new System.Drawing.Point(0, 44);
            this.rtb_ConvertText.Name = "rtb_ConvertText";
            this.rtb_ConvertText.Size = new System.Drawing.Size(835, 442);
            this.rtb_ConvertText.TabIndex = 1;
            this.rtb_ConvertText.Text = "";
            // 
            // btn_ConvertTextCSharp
            // 
            this.btn_ConvertTextCSharp.Location = new System.Drawing.Point(379, 12);
            this.btn_ConvertTextCSharp.Name = "btn_ConvertTextCSharp";
            this.btn_ConvertTextCSharp.Size = new System.Drawing.Size(75, 23);
            this.btn_ConvertTextCSharp.TabIndex = 0;
            this.btn_ConvertTextCSharp.Text = "Convert";
            this.btn_ConvertTextCSharp.UseVisualStyleBackColor = true;
            this.btn_ConvertTextCSharp.Click += new System.EventHandler(this.btn_ConvertTextCSharp_Click);
            // 
            // autoCompleteSQLScript
            // 
            this.autoCompleteSQLScript.AppearInterval = 200;
            this.autoCompleteSQLScript.Colors = ((AutocompleteMenuNS.Colors)(resources.GetObject("autoCompleteSQLScript.Colors")));
            this.autoCompleteSQLScript.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autoCompleteSQLScript.ImageList = this.imageList1;
            this.autoCompleteSQLScript.Items = new string[0];
            this.autoCompleteSQLScript.SearchPattern = "[\\w\\@.]";
            this.autoCompleteSQLScript.TargetControlWrapper = null;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "iconfinder_cut_36471.png");
            // 
            // btn_SQLScriptCreateTable
            // 
            this.btn_SQLScriptCreateTable.Location = new System.Drawing.Point(711, 5);
            this.btn_SQLScriptCreateTable.Name = "btn_SQLScriptCreateTable";
            this.btn_SQLScriptCreateTable.Size = new System.Drawing.Size(75, 23);
            this.btn_SQLScriptCreateTable.TabIndex = 8;
            this.btn_SQLScriptCreateTable.Text = "CreateTable";
            this.btn_SQLScriptCreateTable.UseVisualStyleBackColor = true;
            this.btn_SQLScriptCreateTable.Click += new System.EventHandler(this.btn_SQLScriptCreateTable_Click);
            // 
            // btn_SQLScriptDataType
            // 
            this.btn_SQLScriptDataType.Location = new System.Drawing.Point(630, 5);
            this.btn_SQLScriptDataType.Name = "btn_SQLScriptDataType";
            this.btn_SQLScriptDataType.Size = new System.Drawing.Size(75, 23);
            this.btn_SQLScriptDataType.TabIndex = 8;
            this.btn_SQLScriptDataType.Text = "DataType";
            this.btn_SQLScriptDataType.UseVisualStyleBackColor = true;
            this.btn_SQLScriptDataType.Click += new System.EventHandler(this.Btn_SQLScriptDataType_Click);
            // 
            // cbb_SQLScriptType
            // 
            this.cbb_SQLScriptType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb_SQLScriptType.FormattingEnabled = true;
            this.cbb_SQLScriptType.Location = new System.Drawing.Point(167, 4);
            this.cbb_SQLScriptType.Name = "cbb_SQLScriptType";
            this.cbb_SQLScriptType.Size = new System.Drawing.Size(86, 21);
            this.cbb_SQLScriptType.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(855, 527);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tool Csharp";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.Render.ResumeLayout(false);
            this.Render.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nb_Random2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nb_PageSize2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nb_Page2)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dta2)).EndInit();
            this.tabSQLScript.ResumeLayout(false);
            this.tabSQLScript.PerformLayout();
            this.tabSQLExecute.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_SQLExecuteResult)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rText_Conn;
        private System.Windows.Forms.Button Btn_Connect;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtb_ConvertText;
        private System.Windows.Forms.Button btn_ConvertTextCSharp;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage Render;
        private System.Windows.Forms.Panel pnl_CheckColumn;
        private System.Windows.Forms.TextBox tb_NameVietNamese;
        private System.Windows.Forms.TextBox tb_NameSpace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox rText_Result;
        private System.Windows.Forms.Button btn_RenderID;
        private System.Windows.Forms.Button btn_Controller;
        private System.Windows.Forms.CheckBox cb_MaxLenght;
        private System.Windows.Forms.Button btn_Service;
        private System.Windows.Forms.Button btn_Model;
        private System.Windows.Forms.CheckBox cb_AndOr;
        private System.Windows.Forms.CheckBox cb_Require;
        private System.Windows.Forms.Button btn_Render;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.ComboBox cbb_Table;
        private System.Windows.Forms.ComboBox cbb_Table2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown nb_Random2;
        private System.Windows.Forms.NumericUpDown nb_Page2;
        private System.Windows.Forms.Button btn_Insert2;
        private System.Windows.Forms.Button btn_AddCol2;
        private System.Windows.Forms.Button btn_Get2;
        private System.Windows.Forms.Panel pnlCol2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dta2;
        private System.Windows.Forms.TextBox tb_AddCol2;
        private System.Windows.Forms.Button btn_Convert2;
        private System.Windows.Forms.RichTextBox rtb_Result2;
        private System.Windows.Forms.NumericUpDown nb_PageSize2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox cb_IsRandom2;
        private System.Windows.Forms.CheckBox cb_IsKeepDown;
        private System.Windows.Forms.TextBox tb_ClassName;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btn_Test;
        private System.Windows.Forms.TabPage tabSQLScript;
        private System.Windows.Forms.RichTextBox rtb_ResultScript;
        private System.Windows.Forms.Button btn_ExcuteScript;
        private System.Windows.Forms.RichTextBox rtb_SuggestScript;
        private System.Windows.Forms.Button btn_SQLScriptGetTable;
        private System.Windows.Forms.TextBox tb_FindColumnByTableName;
        private System.Windows.Forms.Button btn_SQLScriptGetColumn;
        private System.Windows.Forms.TabPage tabSQLExecute;
        private System.Windows.Forms.DataGridView dgv_SQLExecuteResult;
        private System.Windows.Forms.Button btn_SQLExecute;
        private System.Windows.Forms.Button btn_SQLScriptSelect;
        private System.Windows.Forms.Button btn_SQLExecutePaste;
        private AutocompleteMenuNS.AutocompleteMenu autoCompleteSQLScript;
        private System.Windows.Forms.ImageList imageList1;
        private MyRichTextBox rtb_SQLScript;
        private MyRichTextBox rtb_SQLExecute;
        private System.Windows.Forms.Button btn_SQLScriptCreateTable;
        private System.Windows.Forms.Button btn_SQLScriptDataType;
        private System.Windows.Forms.ComboBox cbb_SQLScriptType;
    }
}

