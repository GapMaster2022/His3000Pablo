
namespace His.Honorarios
{
    partial class frmExploradorLiquidacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExploradorLiquidacion));
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnExportar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripDropDownButton();
            this.liquidaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.asientoContableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnAnulacion = new System.Windows.Forms.ToolStripDropDownButton();
            this.reversarLiquidacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anularAsientoContableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraGroupBox4 = new Infragistics.Win.Misc.UltraGroupBox();
            this.btnF1 = new System.Windows.Forms.Button();
            this.lblMedico = new System.Windows.Forms.Label();
            this.txtMedico = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ultraGroupBox3 = new Infragistics.Win.Misc.UltraGroupBox();
            this.txtdocumento = new System.Windows.Forms.TextBox();
            this.chkLiquidados = new System.Windows.Forms.CheckBox();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLiquidacion = new System.Windows.Forms.TextBox();
            this.chknumero = new System.Windows.Forms.CheckBox();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.dtpHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpDesde = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ultraGridLiquidados = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraGridExcelExporter1 = new Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(this.components);
            this.tools.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).BeginInit();
            this.ultraGroupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).BeginInit();
            this.ultraGroupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLiquidados)).BeginInit();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActualizar,
            this.btnExportar,
            this.btnImprimir,
            this.toolStripSeparator1,
            this.btnCancelar,
            this.btnAnulacion,
            this.btnSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tools.Size = new System.Drawing.Size(1352, 69);
            this.tools.TabIndex = 82;
            this.tools.Text = "toolStrip1";
            // 
            // btnActualizar
            // 
            this.btnActualizar.AutoSize = false;
            this.btnActualizar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnActualizar.Image = global::His.Honorarios.Properties.Resources.HIS_REFRESH;
            this.btnActualizar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(42, 42);
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.ToolTipText = "Actualizar";
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.AutoSize = false;
            this.btnExportar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnExportar.Image = global::His.Honorarios.Properties.Resources.HIS_EXPORT_TO_EXCEL;
            this.btnExportar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnExportar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(42, 42);
            this.btnExportar.Text = "Exportar";
            this.btnExportar.ToolTipText = "Exportar";
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.liquidaciónToolStripMenuItem,
            this.asientoContableToolStripMenuItem});
            this.btnImprimir.Image = global::His.Honorarios.Properties.Resources.HIS_IMPRIMIR;
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(42, 64);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.ToolTipText = "Imprimir";
            // 
            // liquidaciónToolStripMenuItem
            // 
            this.liquidaciónToolStripMenuItem.Name = "liquidaciónToolStripMenuItem";
            this.liquidaciónToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.liquidaciónToolStripMenuItem.Text = "Liquidación Honorario";
            this.liquidaciónToolStripMenuItem.Click += new System.EventHandler(this.liquidaciónToolStripMenuItem_Click);
            // 
            // asientoContableToolStripMenuItem
            // 
            this.asientoContableToolStripMenuItem.Name = "asientoContableToolStripMenuItem";
            this.asientoContableToolStripMenuItem.Size = new System.Drawing.Size(290, 34);
            this.asientoContableToolStripMenuItem.Text = "Asiento Contable";
            this.asientoContableToolStripMenuItem.Click += new System.EventHandler(this.asientoContableToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 69);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = false;
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = global::His.Honorarios.Properties.Resources.Button_Delete_01_25095;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(42, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAnulacion
            // 
            this.btnAnulacion.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAnulacion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reversarLiquidacionToolStripMenuItem,
            this.anularAsientoContableToolStripMenuItem});
            this.btnAnulacion.Image = global::His.Honorarios.Properties.Resources.HIS_ELIMINAR;
            this.btnAnulacion.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAnulacion.Name = "btnAnulacion";
            this.btnAnulacion.Size = new System.Drawing.Size(42, 64);
            this.btnAnulacion.Text = "Reversion / Anulacion";
            this.btnAnulacion.ToolTipText = "Reversion / Anulacion";
            // 
            // reversarLiquidacionToolStripMenuItem
            // 
            this.reversarLiquidacionToolStripMenuItem.Name = "reversarLiquidacionToolStripMenuItem";
            this.reversarLiquidacionToolStripMenuItem.Size = new System.Drawing.Size(306, 34);
            this.reversarLiquidacionToolStripMenuItem.Text = "Reversar Liquidacion";
            this.reversarLiquidacionToolStripMenuItem.Click += new System.EventHandler(this.reversarLiquidacionToolStripMenuItem_Click);
            // 
            // anularAsientoContableToolStripMenuItem
            // 
            this.anularAsientoContableToolStripMenuItem.Name = "anularAsientoContableToolStripMenuItem";
            this.anularAsientoContableToolStripMenuItem.Size = new System.Drawing.Size(306, 34);
            this.anularAsientoContableToolStripMenuItem.Text = "Anular Asiento Contable";
            this.anularAsientoContableToolStripMenuItem.Click += new System.EventHandler(this.anularAsientoContableToolStripMenuItem_Click);
            // 
            // btnSalir
            // 
            this.btnSalir.AutoSize = false;
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(42, 42);
            this.btnSalir.Text = "toolStripButton1";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // ultraPanel1
            // 
            appearance13.BackColor = System.Drawing.Color.LightSteelBlue;
            appearance13.BackColor2 = System.Drawing.Color.LightBlue;
            appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            appearance13.BackHatchStyle = Infragistics.Win.BackHatchStyle.DiagonalCross;
            this.ultraPanel1.Appearance = appearance13;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox4);
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox3);
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox2);
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 69);
            this.ultraPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(1352, 123);
            this.ultraPanel1.TabIndex = 83;
            this.ultraPanel1.UseOsThemes = Infragistics.Win.DefaultableBoolean.True;
            // 
            // ultraGroupBox4
            // 
            this.ultraGroupBox4.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center;
            this.ultraGroupBox4.Controls.Add(this.btnF1);
            this.ultraGroupBox4.Controls.Add(this.lblMedico);
            this.ultraGroupBox4.Controls.Add(this.txtMedico);
            this.ultraGroupBox4.Controls.Add(this.checkBox1);
            this.ultraGroupBox4.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder;
            this.ultraGroupBox4.Location = new System.Drawing.Point(958, 9);
            this.ultraGroupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ultraGroupBox4.Name = "ultraGroupBox4";
            this.ultraGroupBox4.Size = new System.Drawing.Size(296, 109);
            this.ultraGroupBox4.TabIndex = 85;
            this.ultraGroupBox4.Text = "Filtro por Médico";
            this.ultraGroupBox4.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // btnF1
            // 
            this.btnF1.Enabled = false;
            this.btnF1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnF1.ForeColor = System.Drawing.Color.SteelBlue;
            this.btnF1.Location = new System.Drawing.Point(226, 29);
            this.btnF1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnF1.Name = "btnF1";
            this.btnF1.Size = new System.Drawing.Size(46, 35);
            this.btnF1.TabIndex = 3;
            this.btnF1.Text = "F1";
            this.btnF1.UseVisualStyleBackColor = true;
            this.btnF1.Click += new System.EventHandler(this.btnF1_Click);
            // 
            // lblMedico
            // 
            this.lblMedico.AutoSize = true;
            this.lblMedico.BackColor = System.Drawing.Color.Transparent;
            this.lblMedico.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMedico.Location = new System.Drawing.Point(9, 77);
            this.lblMedico.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMedico.Name = "lblMedico";
            this.lblMedico.Size = new System.Drawing.Size(0, 17);
            this.lblMedico.TabIndex = 2;
            // 
            // txtMedico
            // 
            this.txtMedico.Enabled = false;
            this.txtMedico.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMedico.Location = new System.Drawing.Point(118, 31);
            this.txtMedico.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMedico.Name = "txtMedico";
            this.txtMedico.Size = new System.Drawing.Size(97, 30);
            this.txtMedico.TabIndex = 1;
            this.txtMedico.Text = "0";
            this.txtMedico.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMedico_KeyDown);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.Color.Transparent;
            this.checkBox1.Location = new System.Drawing.Point(9, 38);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 24);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "Médico: ";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // ultraGroupBox3
            // 
            this.ultraGroupBox3.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center;
            this.ultraGroupBox3.Controls.Add(this.txtdocumento);
            this.ultraGroupBox3.Controls.Add(this.chkLiquidados);
            this.ultraGroupBox3.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder;
            this.ultraGroupBox3.Location = new System.Drawing.Point(648, 9);
            this.ultraGroupBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ultraGroupBox3.Name = "ultraGroupBox3";
            this.ultraGroupBox3.Size = new System.Drawing.Size(302, 109);
            this.ultraGroupBox3.TabIndex = 7;
            this.ultraGroupBox3.Text = "Filtro por Estado";
            this.ultraGroupBox3.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // txtdocumento
            // 
            this.txtdocumento.Enabled = false;
            this.txtdocumento.Location = new System.Drawing.Point(134, 35);
            this.txtdocumento.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtdocumento.MaxLength = 15;
            this.txtdocumento.Name = "txtdocumento";
            this.txtdocumento.Size = new System.Drawing.Size(144, 26);
            this.txtdocumento.TabIndex = 2;
            this.txtdocumento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtdocumento_KeyPress);
            // 
            // chkLiquidados
            // 
            this.chkLiquidados.AutoSize = true;
            this.chkLiquidados.BackColor = System.Drawing.Color.Transparent;
            this.chkLiquidados.Location = new System.Drawing.Point(9, 38);
            this.chkLiquidados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chkLiquidados.Name = "chkLiquidados";
            this.chkLiquidados.Size = new System.Drawing.Size(104, 24);
            this.chkLiquidados.TabIndex = 1;
            this.chkLiquidados.Text = "Liquidado";
            this.chkLiquidados.UseVisualStyleBackColor = false;
            this.chkLiquidados.CheckedChanged += new System.EventHandler(this.chkLiquidados_CheckedChanged);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center;
            this.ultraGroupBox2.Controls.Add(this.label3);
            this.ultraGroupBox2.Controls.Add(this.txtLiquidacion);
            this.ultraGroupBox2.Controls.Add(this.chknumero);
            this.ultraGroupBox2.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder;
            this.ultraGroupBox2.Location = new System.Drawing.Point(327, 9);
            this.ultraGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(312, 109);
            this.ultraGroupBox2.TabIndex = 6;
            this.ultraGroupBox2.Text = "Filtro por Liquidación";
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "[Ingrese el número de liquidación]";
            // 
            // txtLiquidacion
            // 
            this.txtLiquidacion.Enabled = false;
            this.txtLiquidacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLiquidacion.Location = new System.Drawing.Point(160, 31);
            this.txtLiquidacion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLiquidacion.Name = "txtLiquidacion";
            this.txtLiquidacion.Size = new System.Drawing.Size(74, 30);
            this.txtLiquidacion.TabIndex = 1;
            this.txtLiquidacion.Text = "0";
            this.txtLiquidacion.Enter += new System.EventHandler(this.txtLiquidacion_Enter);
            this.txtLiquidacion.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLiquidacion_KeyPress);
            this.txtLiquidacion.Leave += new System.EventHandler(this.txtLiquidacion_Leave);
            // 
            // chknumero
            // 
            this.chknumero.AutoSize = true;
            this.chknumero.BackColor = System.Drawing.Color.Transparent;
            this.chknumero.Location = new System.Drawing.Point(9, 38);
            this.chknumero.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chknumero.Name = "chknumero";
            this.chknumero.Size = new System.Drawing.Size(135, 24);
            this.chknumero.TabIndex = 0;
            this.chknumero.Text = "N° Liquidación";
            this.chknumero.UseVisualStyleBackColor = false;
            this.chknumero.CheckedChanged += new System.EventHandler(this.chknumero_CheckedChanged);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center;
            this.ultraGroupBox1.Controls.Add(this.dtpHasta);
            this.ultraGroupBox1.Controls.Add(this.dtpDesde);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOnBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(4, 9);
            this.ultraGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(314, 109);
            this.ultraGroupBox1.TabIndex = 5;
            this.ultraGroupBox1.Text = "Filtro por Fecha";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // dtpHasta
            // 
            this.dtpHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpHasta.Location = new System.Drawing.Point(88, 71);
            this.dtpHasta.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpHasta.Name = "dtpHasta";
            this.dtpHasta.Size = new System.Drawing.Size(162, 26);
            this.dtpHasta.TabIndex = 8;
            // 
            // dtpDesde
            // 
            this.dtpDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDesde.Location = new System.Drawing.Point(88, 31);
            this.dtpDesde.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtpDesde.Name = "dtpDesde";
            this.dtpDesde.Size = new System.Drawing.Size(162, 26);
            this.dtpDesde.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(14, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Hasta: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(14, 40);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Desde: ";
            // 
            // ultraGridLiquidados
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridLiquidados.DisplayLayout.Appearance = appearance1;
            this.ultraGridLiquidados.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridLiquidados.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridLiquidados.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridLiquidados.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ultraGridLiquidados.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridLiquidados.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ultraGridLiquidados.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridLiquidados.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridLiquidados.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridLiquidados.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ultraGridLiquidados.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridLiquidados.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridLiquidados.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridLiquidados.DisplayLayout.Override.CellAppearance = appearance8;
            this.ultraGridLiquidados.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridLiquidados.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridLiquidados.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ultraGridLiquidados.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ultraGridLiquidados.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridLiquidados.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridLiquidados.DisplayLayout.Override.RowAppearance = appearance11;
            this.ultraGridLiquidados.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridLiquidados.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.ultraGridLiquidados.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridLiquidados.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridLiquidados.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGridLiquidados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridLiquidados.Location = new System.Drawing.Point(0, 192);
            this.ultraGridLiquidados.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ultraGridLiquidados.Name = "ultraGridLiquidados";
            this.ultraGridLiquidados.Size = new System.Drawing.Size(1352, 402);
            this.ultraGridLiquidados.TabIndex = 84;
            this.ultraGridLiquidados.Text = "ultraGrid1";
            this.ultraGridLiquidados.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridLiquidados_InitializeLayout);
            // 
            // frmExploradorLiquidacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1352, 594);
            this.Controls.Add(this.ultraGridLiquidados);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this.tools);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmExploradorLiquidacion";
            this.Text = "Explorador Liquidacion";
            this.Load += new System.EventHandler(this.frmExploradorLiquidacion_Load);
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox4)).EndInit();
            this.ultraGroupBox4.ResumeLayout(false);
            this.ultraGroupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox3)).EndInit();
            this.ultraGroupBox3.ResumeLayout(false);
            this.ultraGroupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridLiquidados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.ToolStripButton btnExportar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpHasta;
        private System.Windows.Forms.DateTimePicker dtpDesde;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.TextBox txtLiquidacion;
        private System.Windows.Forms.CheckBox chknumero;
        private System.Windows.Forms.Label label3;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox3;
        private System.Windows.Forms.TextBox txtdocumento;
        private System.Windows.Forms.CheckBox chkLiquidados;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridLiquidados;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox4;
        private System.Windows.Forms.Label lblMedico;
        private System.Windows.Forms.TextBox txtMedico;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnF1;
        private Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter ultraGridExcelExporter1;
        private System.Windows.Forms.ToolStripDropDownButton btnImprimir;
        private System.Windows.Forms.ToolStripMenuItem liquidaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem asientoContableToolStripMenuItem;
        private System.Windows.Forms.ToolStripDropDownButton btnAnulacion;
        private System.Windows.Forms.ToolStripMenuItem reversarLiquidacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anularAsientoContableToolStripMenuItem;
    }
}