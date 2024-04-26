namespace His.Emergencia
{
    partial class frm_InformeMorbilidad
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_InformeMorbilidad));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.UltraChart.Resources.Appearance.GradientEffect gradientEffect1 = new Infragistics.UltraChart.Resources.Appearance.GradientEffect();
            Infragistics.UltraChart.Resources.Appearance.PieChartAppearance pieChartAppearance1 = new Infragistics.UltraChart.Resources.Appearance.PieChartAppearance();
            Infragistics.UltraChart.Resources.Appearance.ChartTextAppearance chartTextAppearance1 = new Infragistics.UltraChart.Resources.Appearance.ChartTextAppearance();
            Infragistics.UltraChart.Resources.Appearance.View3DAppearance view3DAppearance1 = new Infragistics.UltraChart.Resources.Appearance.View3DAppearance();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_Imprimir = new System.Windows.Forms.ToolStripButton();
            this.btn_Salir = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_Morbilidad = new System.Windows.Forms.ComboBox();
            this.btn_ConsultaMorb = new Infragistics.Win.Misc.UltraButton();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.btn_Consultar = new Infragistics.Win.Misc.UltraButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNombreReporte = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ultraGridPacientes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.ultraChartEmergencias = new Infragistics.Win.UltraWinChart.UltraChart();
            this.ultraSplitter1 = new Infragistics.Win.Misc.UltraSplitter();
            this.toolStrip1.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPacientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChartEmergencias)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_Imprimir,
            this.btn_Salir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(919, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // btn_Imprimir
            // 
            this.btn_Imprimir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Imprimir.Image")));
            this.btn_Imprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Imprimir.Name = "btn_Imprimir";
            this.btn_Imprimir.Size = new System.Drawing.Size(81, 36);
            this.btn_Imprimir.Text = "Imprimir";
            // 
            // btn_Salir
            // 
            this.btn_Salir.Image = ((System.Drawing.Image)(resources.GetObject("btn_Salir.Image")));
            this.btn_Salir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Salir.Name = "btn_Salir";
            this.btn_Salir.Size = new System.Drawing.Size(63, 36);
            this.btn_Salir.Text = "Salir";
            this.btn_Salir.Click += new System.EventHandler(this.btn_Salir_Click);
            // 
            // ultraPanel1
            // 
            this.ultraPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.Color.Transparent;
            appearance1.BackColor2 = System.Drawing.Color.WhiteSmoke;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            appearance1.BackHatchStyle = Infragistics.Win.BackHatchStyle.DiagonalCross;
            this.ultraPanel1.Appearance = appearance1;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.ultraPanel1.Location = new System.Drawing.Point(0, 39);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(913, 124);
            this.ultraPanel1.TabIndex = 1;
            // 
            // ultraGroupBox1
            // 
            appearance14.BackColor = System.Drawing.Color.Transparent;
            appearance14.BackColor2 = System.Drawing.Color.LightGray;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            this.ultraGroupBox1.Appearance = appearance14;
            this.ultraGroupBox1.Controls.Add(this.label1);
            this.ultraGroupBox1.Controls.Add(this.cb_Morbilidad);
            this.ultraGroupBox1.Controls.Add(this.btn_ConsultaMorb);
            this.ultraGroupBox1.Controls.Add(this.dtpFiltroHasta);
            this.ultraGroupBox1.Controls.Add(this.dtpFiltroDesde);
            this.ultraGroupBox1.Controls.Add(this.btn_Consultar);
            this.ultraGroupBox1.Controls.Add(this.label3);
            this.ultraGroupBox1.Controls.Add(this.label2);
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(12, 15);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(901, 100);
            this.ultraGroupBox1.TabIndex = 0;
            this.ultraGroupBox1.Text = "Filtro por Fecha";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(384, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Elija el tipo de Diagnóstico:";
            // 
            // cb_Morbilidad
            // 
            this.cb_Morbilidad.FormattingEnabled = true;
            this.cb_Morbilidad.Location = new System.Drawing.Point(524, 29);
            this.cb_Morbilidad.Name = "cb_Morbilidad";
            this.cb_Morbilidad.Size = new System.Drawing.Size(215, 21);
            this.cb_Morbilidad.TabIndex = 8;
            // 
            // btn_ConsultaMorb
            // 
            this.btn_ConsultaMorb.Location = new System.Drawing.Point(524, 64);
            this.btn_ConsultaMorb.Name = "btn_ConsultaMorb";
            this.btn_ConsultaMorb.Size = new System.Drawing.Size(124, 20);
            this.btn_ConsultaMorb.TabIndex = 7;
            this.btn_ConsultaMorb.Text = "Generar";
            this.btn_ConsultaMorb.Click += new System.EventHandler(this.btn_ConsultaMorb_Click);
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(69, 54);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(154, 20);
            this.dtpFiltroHasta.TabIndex = 6;
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(69, 27);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(154, 20);
            this.dtpFiltroDesde.TabIndex = 5;
            this.dtpFiltroDesde.Value = new System.DateTime(2011, 9, 7, 0, 0, 0, 0);
            // 
            // btn_Consultar
            // 
            this.btn_Consultar.Location = new System.Drawing.Point(238, 26);
            this.btn_Consultar.Name = "btn_Consultar";
            this.btn_Consultar.Size = new System.Drawing.Size(97, 23);
            this.btn_Consultar.TabIndex = 4;
            this.btn_Consultar.Text = "Consultar";
            this.btn_Consultar.Click += new System.EventHandler(this.btn_Consultar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Hasta :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(19, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Desde :";
            // 
            // lblNombreReporte
            // 
            this.lblNombreReporte.AutoSize = true;
            this.lblNombreReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreReporte.ForeColor = System.Drawing.Color.Chocolate;
            this.lblNombreReporte.Location = new System.Drawing.Point(273, 166);
            this.lblNombreReporte.Name = "lblNombreReporte";
            this.lblNombreReporte.Size = new System.Drawing.Size(268, 31);
            this.lblNombreReporte.TabIndex = 0;
            this.lblNombreReporte.Text = "Nombre de Reporte";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(2, 202);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ultraGridPacientes);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ultraChartEmergencias);
            this.splitContainer1.Panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel2_Paint);
            this.splitContainer1.Size = new System.Drawing.Size(911, 345);
            this.splitContainer1.SplitterDistance = 380;
            this.splitContainer1.TabIndex = 2;
            // 
            // ultraGridPacientes
            // 
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridPacientes.DisplayLayout.Appearance = appearance5;
            this.ultraGridPacientes.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridPacientes.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridPacientes.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridPacientes.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ultraGridPacientes.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridPacientes.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ultraGridPacientes.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridPacientes.DisplayLayout.MaxRowScrollRegions = 1;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridPacientes.DisplayLayout.Override.ActiveCellAppearance = appearance13;
            appearance8.BackColor = System.Drawing.SystemColors.Highlight;
            appearance8.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridPacientes.DisplayLayout.Override.ActiveRowAppearance = appearance8;
            this.ultraGridPacientes.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridPacientes.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridPacientes.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance6.BorderColor = System.Drawing.Color.Silver;
            appearance6.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridPacientes.DisplayLayout.Override.CellAppearance = appearance6;
            this.ultraGridPacientes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridPacientes.DisplayLayout.Override.CellPadding = 0;
            appearance10.BackColor = System.Drawing.SystemColors.Control;
            appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance10.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridPacientes.DisplayLayout.Override.GroupByRowAppearance = appearance10;
            appearance12.TextHAlignAsString = "Left";
            this.ultraGridPacientes.DisplayLayout.Override.HeaderAppearance = appearance12;
            this.ultraGridPacientes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridPacientes.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridPacientes.DisplayLayout.Override.RowAppearance = appearance11;
            this.ultraGridPacientes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance9.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridPacientes.DisplayLayout.Override.TemplateAddRowAppearance = appearance9;
            this.ultraGridPacientes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridPacientes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridPacientes.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGridPacientes.Location = new System.Drawing.Point(3, 6);
            this.ultraGridPacientes.Name = "ultraGridPacientes";
            this.ultraGridPacientes.Size = new System.Drawing.Size(378, 336);
            this.ultraGridPacientes.TabIndex = 0;
            this.ultraGridPacientes.Text = "ultraGrid1";
            // 
            //			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
            //			'ChartType' must be persisted ahead of any Axes change made in design time.
            //		
            this.ultraChartEmergencias.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.PieChart3D;
            // 
            // ultraChartEmergencias
            // 
            this.ultraChartEmergencias.Axis.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(220)))));
            this.ultraChartEmergencias.Axis.X.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.X.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChartEmergencias.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChartEmergencias.Axis.X.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChartEmergencias.Axis.X.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.X.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.X.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.X.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChartEmergencias.Axis.X.Labels.SeriesLabels.FormatString = "";
            this.ultraChartEmergencias.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChartEmergencias.Axis.X.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.X.Labels.Visible = false;
            this.ultraChartEmergencias.Axis.X.LineThickness = 1;
            this.ultraChartEmergencias.Axis.X.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.X.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChartEmergencias.Axis.X.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.X.MajorGridLines.Visible = true;
            this.ultraChartEmergencias.Axis.X.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.X.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChartEmergencias.Axis.X.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.X.MinorGridLines.Visible = false;
            this.ultraChartEmergencias.Axis.X.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChartEmergencias.Axis.X.Visible = false;
            this.ultraChartEmergencias.Axis.X2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.X2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChartEmergencias.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChartEmergencias.Axis.X2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChartEmergencias.Axis.X2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.X2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.X2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.X2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChartEmergencias.Axis.X2.Labels.SeriesLabels.FormatString = "";
            this.ultraChartEmergencias.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChartEmergencias.Axis.X2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.X2.Labels.Visible = false;
            this.ultraChartEmergencias.Axis.X2.LineThickness = 1;
            this.ultraChartEmergencias.Axis.X2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.X2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChartEmergencias.Axis.X2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.X2.MajorGridLines.Visible = true;
            this.ultraChartEmergencias.Axis.X2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.X2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChartEmergencias.Axis.X2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.X2.MinorGridLines.Visible = false;
            this.ultraChartEmergencias.Axis.X2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChartEmergencias.Axis.X2.Visible = false;
            this.ultraChartEmergencias.Axis.Y.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.Y.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChartEmergencias.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChartEmergencias.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChartEmergencias.Axis.Y.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.Y.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.Y.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.Y.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChartEmergencias.Axis.Y.Labels.SeriesLabels.FormatString = "";
            this.ultraChartEmergencias.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChartEmergencias.Axis.Y.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.Y.Labels.Visible = false;
            this.ultraChartEmergencias.Axis.Y.LineThickness = 1;
            this.ultraChartEmergencias.Axis.Y.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.Y.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChartEmergencias.Axis.Y.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.Y.MajorGridLines.Visible = true;
            this.ultraChartEmergencias.Axis.Y.Margin.Far.Value = 2.3529411764705883;
            this.ultraChartEmergencias.Axis.Y.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.Y.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChartEmergencias.Axis.Y.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.Y.MinorGridLines.Visible = false;
            this.ultraChartEmergencias.Axis.Y.TickmarkInterval = 50;
            this.ultraChartEmergencias.Axis.Y.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChartEmergencias.Axis.Y.Visible = false;
            this.ultraChartEmergencias.Axis.Y2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.Y2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChartEmergencias.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChartEmergencias.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.##>";
            this.ultraChartEmergencias.Axis.Y2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.Y2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.Y2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.Y2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChartEmergencias.Axis.Y2.Labels.SeriesLabels.FormatString = "";
            this.ultraChartEmergencias.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChartEmergencias.Axis.Y2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.Y2.Labels.Visible = false;
            this.ultraChartEmergencias.Axis.Y2.LineThickness = 1;
            this.ultraChartEmergencias.Axis.Y2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.Y2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChartEmergencias.Axis.Y2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.Y2.MajorGridLines.Visible = true;
            this.ultraChartEmergencias.Axis.Y2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.Y2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChartEmergencias.Axis.Y2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.Y2.MinorGridLines.Visible = false;
            this.ultraChartEmergencias.Axis.Y2.TickmarkInterval = 50;
            this.ultraChartEmergencias.Axis.Y2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChartEmergencias.Axis.Y2.Visible = false;
            this.ultraChartEmergencias.Axis.Z.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.Z.Labels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChartEmergencias.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChartEmergencias.Axis.Z.Labels.ItemFormatString = "";
            this.ultraChartEmergencias.Axis.Z.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.Z.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.Z.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.Z.Labels.SeriesLabels.FontColor = System.Drawing.Color.DimGray;
            this.ultraChartEmergencias.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
            this.ultraChartEmergencias.Axis.Z.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.Z.Labels.Visible = false;
            this.ultraChartEmergencias.Axis.Z.LineThickness = 1;
            this.ultraChartEmergencias.Axis.Z.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.Z.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChartEmergencias.Axis.Z.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.Z.MajorGridLines.Visible = true;
            this.ultraChartEmergencias.Axis.Z.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.Z.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChartEmergencias.Axis.Z.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.Z.MinorGridLines.Visible = false;
            this.ultraChartEmergencias.Axis.Z.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChartEmergencias.Axis.Z.Visible = false;
            this.ultraChartEmergencias.Axis.Z2.Labels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.Z2.Labels.FontColor = System.Drawing.Color.Gray;
            this.ultraChartEmergencias.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChartEmergencias.Axis.Z2.Labels.ItemFormatString = "<ITEM_LABEL>";
            this.ultraChartEmergencias.Axis.Z2.Labels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.Z2.Labels.SeriesLabels.Font = new System.Drawing.Font("Verdana", 7F);
            this.ultraChartEmergencias.Axis.Z2.Labels.SeriesLabels.FontColor = System.Drawing.Color.Gray;
            this.ultraChartEmergencias.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
            this.ultraChartEmergencias.Axis.Z2.Labels.SeriesLabels.Layout.Behavior = Infragistics.UltraChart.Shared.Styles.AxisLabelLayoutBehaviors.Auto;
            this.ultraChartEmergencias.Axis.Z2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
            this.ultraChartEmergencias.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.Axis.Z2.Labels.Visible = false;
            this.ultraChartEmergencias.Axis.Z2.LineThickness = 1;
            this.ultraChartEmergencias.Axis.Z2.MajorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.Z2.MajorGridLines.Color = System.Drawing.Color.Gainsboro;
            this.ultraChartEmergencias.Axis.Z2.MajorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.Z2.MajorGridLines.Visible = true;
            this.ultraChartEmergencias.Axis.Z2.MinorGridLines.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.Axis.Z2.MinorGridLines.Color = System.Drawing.Color.LightGray;
            this.ultraChartEmergencias.Axis.Z2.MinorGridLines.DrawStyle = Infragistics.UltraChart.Shared.Styles.LineDrawStyle.Dot;
            this.ultraChartEmergencias.Axis.Z2.MinorGridLines.Visible = false;
            this.ultraChartEmergencias.Axis.Z2.TickmarkStyle = Infragistics.UltraChart.Shared.Styles.AxisTickStyle.Smart;
            this.ultraChartEmergencias.Axis.Z2.Visible = false;
            this.ultraChartEmergencias.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ultraChartEmergencias.BackgroundImageStyle = Infragistics.UltraChart.Shared.Styles.ImageFitStyle.Centered;
            this.ultraChartEmergencias.ColorModel.AlphaLevel = ((byte)(255));
            this.ultraChartEmergencias.ColorModel.ColorBegin = System.Drawing.Color.Pink;
            this.ultraChartEmergencias.ColorModel.ColorEnd = System.Drawing.Color.DarkRed;
            this.ultraChartEmergencias.ColorModel.ModelStyle = Infragistics.UltraChart.Shared.Styles.ColorModels.CustomLinear;
            this.ultraChartEmergencias.ColorModel.Scaling = Infragistics.UltraChart.Shared.Styles.ColorScaling.Increasing;
            this.ultraChartEmergencias.Effects.Effects.Add(gradientEffect1);
            this.ultraChartEmergencias.Legend.Location = Infragistics.UltraChart.Shared.Styles.LegendLocation.Bottom;
            this.ultraChartEmergencias.Legend.Visible = true;
            this.ultraChartEmergencias.Location = new System.Drawing.Point(3, 3);
            this.ultraChartEmergencias.Name = "ultraChartEmergencias";
            chartTextAppearance1.ChartTextFont = new System.Drawing.Font("Arial", 7F);
            chartTextAppearance1.Column = -2;
            chartTextAppearance1.ItemFormatString = "<DATA_VALUE:00.00>";
            chartTextAppearance1.Row = -2;
            chartTextAppearance1.Visible = true;
            pieChartAppearance1.ChartText.Add(chartTextAppearance1);
            pieChartAppearance1.Labels.FormatString = "(<ITEM_LABEL>: <DATA_VALUE:#> (<PERCENT_VALUE:#>%))";
            this.ultraChartEmergencias.PieChart3D = pieChartAppearance1;
            this.ultraChartEmergencias.Size = new System.Drawing.Size(521, 339);
            this.ultraChartEmergencias.TabIndex = 0;
            this.ultraChartEmergencias.TitleTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold);
            this.ultraChartEmergencias.TitleTop.HorizontalAlign = System.Drawing.StringAlignment.Center;
            this.ultraChartEmergencias.TitleTop.Text = "Resumen de Datos Estadísticos";
            this.ultraChartEmergencias.Tooltips.HighlightFillColor = System.Drawing.Color.DimGray;
            this.ultraChartEmergencias.Tooltips.HighlightOutlineColor = System.Drawing.Color.DarkGray;
            this.ultraChartEmergencias.Tooltips.TooltipControl = null;
            view3DAppearance1.Perspective = 61F;
            view3DAppearance1.Scale = 63F;
            view3DAppearance1.XRotation = 54F;
            view3DAppearance1.YRotation = -130F;
            this.ultraChartEmergencias.Transform3D = view3DAppearance1;
            this.ultraChartEmergencias.ChartDataClicked += new Infragistics.UltraChart.Shared.Events.ChartDataClickedEventHandler(this.ultraChartEmergencias_ChartDataClicked);
            // 
            // ultraSplitter1
            // 
            this.ultraSplitter1.Location = new System.Drawing.Point(0, 39);
            this.ultraSplitter1.Name = "ultraSplitter1";
            this.ultraSplitter1.RestoreExtent = 2147483647;
            this.ultraSplitter1.Size = new System.Drawing.Size(6, 646);
            this.ultraSplitter1.TabIndex = 3;
            // 
            // frm_InformeMorbilidad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 685);
            this.Controls.Add(this.ultraSplitter1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.lblNombreReporte);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frm_InformeMorbilidad";
            this.Text = "Estadísticas";
            this.Load += new System.EventHandler(this.frm_InformeMorbilidad_Load_1);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridPacientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraChartEmergencias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripButton btn_Imprimir;
        private System.Windows.Forms.ToolStripButton btn_Salir;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private System.Windows.Forms.Label lblNombreReporte;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Infragistics.Win.Misc.UltraButton btn_Consultar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridPacientes;
        private Infragistics.Win.UltraWinChart.UltraChart ultraChartEmergencias;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private Infragistics.Win.Misc.UltraButton btn_ConsultaMorb;
        private System.Windows.Forms.ComboBox cb_Morbilidad;
        private System.Windows.Forms.Label label1;
        private Infragistics.Win.Misc.UltraSplitter ultraSplitter1;
        private System.Windows.Forms.ToolStrip toolStrip1;
    }
}