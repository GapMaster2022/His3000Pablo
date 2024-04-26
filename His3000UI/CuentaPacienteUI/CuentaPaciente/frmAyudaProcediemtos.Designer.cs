namespace CuentaPaciente
{
    partial class frmAyudaProcediemtos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAyudaProcediemtos));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.btn_Aceptar = new System.Windows.Forms.Button();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_total = new System.Windows.Forms.TextBox();
            this.lvwHonorarios = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Observaciones = new System.Windows.Forms.TextBox();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
            this.toolStripMedicos = new System.Windows.Forms.ToolStrip();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.tpuvr = new System.Windows.Forms.ToolStripMenuItem();
            this.tpanestesia = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsdbPorcentajes = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmiCienPor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetentaCincoPor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCincuentaPor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiVeinteCincoPor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOtro = new System.Windows.Forms.ToolStripMenuItem();
            this.tsTxtPorcentajeCobrar = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txt_valor1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.txt_Cantidad1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.txtVale = new System.Windows.Forms.ToolStripTextBox();
            this.tarifariosDetalleGrid = new System.Windows.Forms.DataGridView();
            this.ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btn_Buscar = new System.Windows.Forms.Button();
            this.txt_Nombre = new System.Windows.Forms.TextBox();
            this.optCodigo = new System.Windows.Forms.RadioButton();
            this.optDescripcion = new System.Windows.Forms.RadioButton();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).BeginInit();
            this.ultraGroupBox4.SuspendLayout();
            this.toolStripMedicos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tarifariosDetalleGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).BeginInit();
            this.ultraGroupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Cancelar.Location = new System.Drawing.Point(269, 165);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(67, 23);
            this.btn_Cancelar.TabIndex = 98;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = false;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // btn_Aceptar
            // 
            this.btn_Aceptar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Aceptar.Location = new System.Drawing.Point(208, 165);
            this.btn_Aceptar.Name = "btn_Aceptar";
            this.btn_Aceptar.Size = new System.Drawing.Size(55, 23);
            this.btn_Aceptar.TabIndex = 106;
            this.btn_Aceptar.Text = "Aceptar";
            this.btn_Aceptar.UseVisualStyleBackColor = false;
            this.btn_Aceptar.Click += new System.EventHandler(this.btn_Aceptar_Click);
            this.btn_Aceptar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.btn_Aceptar_KeyPress);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.btn_Cancelar);
            this.ultraGroupBox2.Controls.Add(this.label4);
            this.ultraGroupBox2.Controls.Add(this.btn_Aceptar);
            this.ultraGroupBox2.Controls.Add(this.txt_total);
            this.ultraGroupBox2.Controls.Add(this.lvwHonorarios);
            this.ultraGroupBox2.Controls.Add(this.label2);
            this.ultraGroupBox2.Controls.Add(this.txt_Observaciones);
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 268);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(713, 206);
            this.ultraGroupBox2.TabIndex = 96;
            this.ultraGroupBox2.Text = "Productos Solicitados";
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(521, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 98;
            this.label4.Text = "Valor Total:";
            // 
            // txt_total
            // 
            this.txt_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total.Location = new System.Drawing.Point(588, 112);
            this.txt_total.Multiline = true;
            this.txt_total.Name = "txt_total";
            this.txt_total.ReadOnly = true;
            this.txt_total.Size = new System.Drawing.Size(66, 21);
            this.txt_total.TabIndex = 104;
            this.txt_total.Text = "0";
            this.txt_total.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_total.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_total_KeyPress);
            // 
            // lvwHonorarios
            // 
            this.lvwHonorarios.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.lvwHonorarios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lvwHonorarios.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lvwHonorarios.Location = new System.Drawing.Point(6, 19);
            this.lvwHonorarios.Name = "lvwHonorarios";
            this.lvwHonorarios.Size = new System.Drawing.Size(698, 75);
            this.lvwHonorarios.TabIndex = 103;
            this.lvwHonorarios.UseCompatibleStateImageBehavior = false;
            this.lvwHonorarios.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwHonorarios_ColumnClick);
            this.lvwHonorarios.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lvwHonorarios_KeyPress);
            this.lvwHonorarios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwHonorarios_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(6, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 95;
            this.label2.Text = "Observaciones :";
            // 
            // txt_Observaciones
            // 
            this.txt_Observaciones.Location = new System.Drawing.Point(96, 114);
            this.txt_Observaciones.Multiline = true;
            this.txt_Observaciones.Name = "txt_Observaciones";
            this.txt_Observaciones.Size = new System.Drawing.Size(414, 45);
            this.txt_Observaciones.TabIndex = 105;
            this.txt_Observaciones.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Observaciones_KeyPress);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.ultraGroupBox4);
            this.ultraGroupBox1.Controls.Add(this.ultraGroupBox3);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(713, 268);
            this.ultraGroupBox1.TabIndex = 95;
            this.ultraGroupBox1.Text = "Lista Honorarios";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // ultraGroupBox4
            // 
            this.ultraGroupBox4.Controls.Add(this.toolStripMedicos);
            this.ultraGroupBox4.Controls.Add(this.tarifariosDetalleGrid);
            this.ultraGroupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox4.Location = new System.Drawing.Point(3, 97);
            this.ultraGroupBox4.Name = "ultraGroupBox4";
            this.ultraGroupBox4.Size = new System.Drawing.Size(707, 143);
            this.ultraGroupBox4.TabIndex = 104;
            // 
            // toolStripMedicos
            // 
            this.toolStripMedicos.AutoSize = false;
            this.toolStripMedicos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSplitButton1,
            this.toolStripSeparator2,
            this.tsdbPorcentajes,
            this.tsTxtPorcentajeCobrar,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.txt_valor1,
            this.toolStripSeparator3,
            this.toolStripLabel2,
            this.toolStripButton1,
            this.txt_Cantidad1,
            this.toolStripSeparator4,
            this.toolStripLabel3,
            this.txtVale});
            this.toolStripMedicos.Location = new System.Drawing.Point(3, 0);
            this.toolStripMedicos.Name = "toolStripMedicos";
            this.toolStripMedicos.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.toolStripMedicos.Size = new System.Drawing.Size(701, 31);
            this.toolStripMedicos.TabIndex = 100;
            this.toolStripMedicos.Text = "Clasificar por:";
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tpuvr,
            this.tpanestesia});
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(98, 28);
            this.toolStripSplitButton1.Text = "Valor a Generar";
            // 
            // tpuvr
            // 
            this.tpuvr.Checked = true;
            this.tpuvr.CheckOnClick = true;
            this.tpuvr.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tpuvr.Name = "tpuvr";
            this.tpuvr.Size = new System.Drawing.Size(132, 22);
            this.tpuvr.Text = "UVR";
            this.tpuvr.Click += new System.EventHandler(this.tpuvr_Click);
            // 
            // tpanestesia
            // 
            this.tpanestesia.CheckOnClick = true;
            this.tpanestesia.Name = "tpanestesia";
            this.tpanestesia.Size = new System.Drawing.Size(132, 22);
            this.tpanestesia.Text = "Anestesia";
            this.tpanestesia.Click += new System.EventHandler(this.tpanestesia_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsdbPorcentajes
            // 
            this.tsdbPorcentajes.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsdbPorcentajes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiCienPor,
            this.tsmiSetentaCincoPor,
            this.tsmiCincuentaPor,
            this.tsmiVeinteCincoPor,
            this.tsmiOtro});
            this.tsdbPorcentajes.Image = ((System.Drawing.Image)(resources.GetObject("tsdbPorcentajes.Image")));
            this.tsdbPorcentajes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsdbPorcentajes.Name = "tsdbPorcentajes";
            this.tsdbPorcentajes.Size = new System.Drawing.Size(124, 28);
            this.tsdbPorcentajes.Text = "Porcentajes de Cobro";
            // 
            // tsmiCienPor
            // 
            this.tsmiCienPor.Checked = true;
            this.tsmiCienPor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsmiCienPor.Name = "tsmiCienPor";
            this.tsmiCienPor.Size = new System.Drawing.Size(114, 22);
            this.tsmiCienPor.Tag = "100";
            this.tsmiCienPor.Text = "100%";
            this.tsmiCienPor.Click += new System.EventHandler(this.tsmiCienPor_Click);
            // 
            // tsmiSetentaCincoPor
            // 
            this.tsmiSetentaCincoPor.Name = "tsmiSetentaCincoPor";
            this.tsmiSetentaCincoPor.Size = new System.Drawing.Size(114, 22);
            this.tsmiSetentaCincoPor.Tag = "75";
            this.tsmiSetentaCincoPor.Text = "75%";
            this.tsmiSetentaCincoPor.Click += new System.EventHandler(this.tsmiSetentaCincoPor_Click);
            // 
            // tsmiCincuentaPor
            // 
            this.tsmiCincuentaPor.Name = "tsmiCincuentaPor";
            this.tsmiCincuentaPor.Size = new System.Drawing.Size(114, 22);
            this.tsmiCincuentaPor.Tag = "50";
            this.tsmiCincuentaPor.Text = "50%";
            this.tsmiCincuentaPor.Click += new System.EventHandler(this.tsmiCincuentaPor_Click);
            // 
            // tsmiVeinteCincoPor
            // 
            this.tsmiVeinteCincoPor.Name = "tsmiVeinteCincoPor";
            this.tsmiVeinteCincoPor.Size = new System.Drawing.Size(114, 22);
            this.tsmiVeinteCincoPor.Tag = "25";
            this.tsmiVeinteCincoPor.Text = "25%";
            this.tsmiVeinteCincoPor.Click += new System.EventHandler(this.tsmiVeinteCincoPor_Click);
            // 
            // tsmiOtro
            // 
            this.tsmiOtro.Name = "tsmiOtro";
            this.tsmiOtro.Size = new System.Drawing.Size(114, 22);
            this.tsmiOtro.Text = "Otro";
            this.tsmiOtro.Click += new System.EventHandler(this.tsmiOtro_Click);
            // 
            // tsTxtPorcentajeCobrar
            // 
            this.tsTxtPorcentajeCobrar.MaxLength = 2;
            this.tsTxtPorcentajeCobrar.Name = "tsTxtPorcentajeCobrar";
            this.tsTxtPorcentajeCobrar.Size = new System.Drawing.Size(30, 31);
            this.tsTxtPorcentajeCobrar.Text = "0";
            this.tsTxtPorcentajeCobrar.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tsTxtPorcentajeCobrar.Visible = false;
            this.tsTxtPorcentajeCobrar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tsTxtPorcentajeCobrar_KeyPress);
            this.tsTxtPorcentajeCobrar.TextChanged += new System.EventHandler(this.tsTxtPorcentajeCobrar_TextChanged);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(35, 28);
            this.toolStripLabel1.Text = "Valor:";
            // 
            // txt_valor1
            // 
            this.txt_valor1.Name = "txt_valor1";
            this.txt_valor1.Size = new System.Drawing.Size(40, 31);
            this.txt_valor1.Text = "0";
            this.txt_valor1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_valor1_KeyPress);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(50, 28);
            this.toolStripLabel2.Text = "Cantidad";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(58, 28);
            this.toolStripButton1.Text = "Añadir";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // txt_Cantidad1
            // 
            this.txt_Cantidad1.Name = "txt_Cantidad1";
            this.txt_Cantidad1.Size = new System.Drawing.Size(40, 31);
            this.txt_Cantidad1.Text = "1";
            this.txt_Cantidad1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Cantidad1_KeyPress);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(42, 28);
            this.toolStripLabel3.Text = "N° Vale";
            // 
            // txtVale
            // 
            this.txtVale.Name = "txtVale";
            this.txtVale.Size = new System.Drawing.Size(60, 31);
            // 
            // tarifariosDetalleGrid
            // 
            this.tarifariosDetalleGrid.AllowUserToAddRows = false;
            this.tarifariosDetalleGrid.AllowUserToDeleteRows = false;
            this.tarifariosDetalleGrid.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tarifariosDetalleGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.tarifariosDetalleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.tarifariosDetalleGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.tarifariosDetalleGrid.Location = new System.Drawing.Point(3, 38);
            this.tarifariosDetalleGrid.Name = "tarifariosDetalleGrid";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tarifariosDetalleGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.tarifariosDetalleGrid.Size = new System.Drawing.Size(692, 99);
            this.tarifariosDetalleGrid.TabIndex = 99;
            this.tarifariosDetalleGrid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tarifariosDetalleGrid_KeyDown);
            this.tarifariosDetalleGrid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tarifariosDetalleGrid_KeyPress);
            // 
            // ultraGroupBox3
            // 
            this.ultraGroupBox3.Controls.Add(this.btn_Buscar);
            this.ultraGroupBox3.Controls.Add(this.txt_Nombre);
            this.ultraGroupBox3.Controls.Add(this.optCodigo);
            this.ultraGroupBox3.Controls.Add(this.optDescripcion);
            this.ultraGroupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox3.Location = new System.Drawing.Point(3, 16);
            this.ultraGroupBox3.Name = "ultraGroupBox3";
            this.ultraGroupBox3.Size = new System.Drawing.Size(707, 81);
            this.ultraGroupBox3.TabIndex = 103;
            this.ultraGroupBox3.Text = "Filtros";
            // 
            // btn_Buscar
            // 
            this.btn_Buscar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Buscar.Location = new System.Drawing.Point(360, 19);
            this.btn_Buscar.Name = "btn_Buscar";
            this.btn_Buscar.Size = new System.Drawing.Size(55, 23);
            this.btn_Buscar.TabIndex = 98;
            this.btn_Buscar.Text = "Buscar";
            this.btn_Buscar.UseVisualStyleBackColor = false;
            this.btn_Buscar.Click += new System.EventHandler(this.btn_Buscar_Click);
            // 
            // txt_Nombre
            // 
            this.txt_Nombre.Location = new System.Drawing.Point(11, 19);
            this.txt_Nombre.Name = "txt_Nombre";
            this.txt_Nombre.Size = new System.Drawing.Size(343, 20);
            this.txt_Nombre.TabIndex = 97;
            this.txt_Nombre.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_Nombre_KeyPress);
            // 
            // optCodigo
            // 
            this.optCodigo.AutoSize = true;
            this.optCodigo.BackColor = System.Drawing.Color.Transparent;
            this.optCodigo.Checked = true;
            this.optCodigo.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCodigo.ForeColor = System.Drawing.Color.DimGray;
            this.optCodigo.Location = new System.Drawing.Point(18, 51);
            this.optCodigo.Name = "optCodigo";
            this.optCodigo.Size = new System.Drawing.Size(96, 17);
            this.optCodigo.TabIndex = 97;
            this.optCodigo.TabStop = true;
            this.optCodigo.Text = "Por Referencia";
            this.optCodigo.UseVisualStyleBackColor = false;
            // 
            // optDescripcion
            // 
            this.optDescripcion.AutoSize = true;
            this.optDescripcion.BackColor = System.Drawing.Color.Transparent;
            this.optDescripcion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optDescripcion.ForeColor = System.Drawing.Color.DimGray;
            this.optDescripcion.Location = new System.Drawing.Point(120, 51);
            this.optDescripcion.Name = "optDescripcion";
            this.optDescripcion.Size = new System.Drawing.Size(98, 17);
            this.optDescripcion.TabIndex = 98;
            this.optDescripcion.Text = "Por Descripción";
            this.optDescripcion.UseVisualStyleBackColor = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "aa";
            this.dataGridViewTextBoxColumn1.HeaderText = "Column1";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // frmAyudaProcediemtos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 485);
            this.Controls.Add(this.ultraGroupBox2);
            this.Controls.Add(this.ultraGroupBox1);
            this.Name = "frmAyudaProcediemtos";
            this.Text = "Procedimientos Tarifario";
            this.Load += new System.EventHandler(this.frmAyudaProcediemtos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).EndInit();
            this.ultraGroupBox4.ResumeLayout(false);
            this.toolStripMedicos.ResumeLayout(false);
            this.toolStripMedicos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tarifariosDetalleGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).EndInit();
            this.ultraGroupBox3.ResumeLayout(false);
            this.ultraGroupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancelar;
        private System.Windows.Forms.Button btn_Aceptar;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Observaciones;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.TextBox txt_Nombre;
        private System.Windows.Forms.Button btn_Buscar;
        private System.Windows.Forms.RadioButton optCodigo;
        private System.Windows.Forms.RadioButton optDescripcion;
        private System.Windows.Forms.ListView lvwHonorarios;
        private System.Windows.Forms.TextBox txt_total;
        private System.Windows.Forms.DataGridView tarifariosDetalleGrid;
        private System.Windows.Forms.Label label4;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox3;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox4;
        private System.Windows.Forms.ToolStrip toolStripMedicos;
        private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
        private System.Windows.Forms.ToolStripMenuItem tpuvr;
        private System.Windows.Forms.ToolStripMenuItem tpanestesia;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripDropDownButton tsdbPorcentajes;
        private System.Windows.Forms.ToolStripMenuItem tsmiCienPor;
        private System.Windows.Forms.ToolStripMenuItem tsmiSetentaCincoPor;
        private System.Windows.Forms.ToolStripMenuItem tsmiCincuentaPor;
        private System.Windows.Forms.ToolStripMenuItem tsmiVeinteCincoPor;
        private System.Windows.Forms.ToolStripMenuItem tsmiOtro;
        private System.Windows.Forms.ToolStripTextBox tsTxtPorcentajeCobrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txt_valor1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox txt_Cantidad1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox txtVale;

    }
}