
namespace CuentaPaciente
{
    partial class frmReversionCopago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReversionCopago));
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.gridReversion = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoAccesoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.grpFechas = new Infragistics.Win.Misc.UltraGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHasta = new System.Windows.Forms.Label();
            this.dtpFiltroHasta = new System.Windows.Forms.DateTimePicker();
            this.dtpFiltroDesde = new System.Windows.Forms.DateTimePicker();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridReversion)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).BeginInit();
            this.grpFechas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridReversion
            // 
            this.gridReversion.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.gridReversion.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridReversion.DisplayLayout.GroupByBox.BandLabelAppearance = appearance2;
            this.gridReversion.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance3.BackColor2 = System.Drawing.SystemColors.Control;
            appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridReversion.DisplayLayout.GroupByBox.PromptAppearance = appearance3;
            this.gridReversion.DisplayLayout.MaxColScrollRegions = 1;
            this.gridReversion.DisplayLayout.MaxRowScrollRegions = 1;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridReversion.DisplayLayout.Override.ActiveCellAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Highlight;
            appearance5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridReversion.DisplayLayout.Override.ActiveRowAppearance = appearance5;
            this.gridReversion.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.gridReversion.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance6.BackColor = System.Drawing.SystemColors.Window;
            this.gridReversion.DisplayLayout.Override.CardAreaAppearance = appearance6;
            appearance7.BorderColor = System.Drawing.Color.Silver;
            appearance7.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.gridReversion.DisplayLayout.Override.CellAppearance = appearance7;
            this.gridReversion.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.gridReversion.DisplayLayout.Override.CellPadding = 0;
            appearance8.BackColor = System.Drawing.SystemColors.Control;
            appearance8.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance8.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance8.BorderColor = System.Drawing.SystemColors.Window;
            this.gridReversion.DisplayLayout.Override.GroupByRowAppearance = appearance8;
            appearance9.TextHAlignAsString = "Left";
            this.gridReversion.DisplayLayout.Override.HeaderAppearance = appearance9;
            this.gridReversion.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.gridReversion.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance10.BackColor = System.Drawing.SystemColors.Window;
            appearance10.BorderColor = System.Drawing.Color.Silver;
            this.gridReversion.DisplayLayout.Override.RowAppearance = appearance10;
            this.gridReversion.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance11.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gridReversion.DisplayLayout.Override.TemplateAddRowAppearance = appearance11;
            this.gridReversion.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.gridReversion.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.gridReversion.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.Horizontal;
            this.gridReversion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridReversion.Location = new System.Drawing.Point(0, 122);
            this.gridReversion.Margin = new System.Windows.Forms.Padding(4);
            this.gridReversion.Name = "gridReversion";
            this.gridReversion.Size = new System.Drawing.Size(1416, 521);
            this.gridReversion.TabIndex = 135;
            this.gridReversion.Text = "ultraGrid";
            this.gridReversion.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.gridReversion_InitializeLayout);
            this.gridReversion.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.gridReversion_AfterSelectChange);
            this.gridReversion.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gridReversion_MouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoAccesoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(190, 28);
            // 
            // nuevoAccesoToolStripMenuItem
            // 
            this.nuevoAccesoToolStripMenuItem.Name = "nuevoAccesoToolStripMenuItem";
            this.nuevoAccesoToolStripMenuItem.Size = new System.Drawing.Size(189, 24);
            this.nuevoAccesoToolStripMenuItem.Text = "Eliminar Copago";
            this.nuevoAccesoToolStripMenuItem.Click += new System.EventHandler(this.nuevoAccesoToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnGuardar,
            this.btnImprimir});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1416, 36);
            this.toolStrip1.TabIndex = 136;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Margin = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Padding = new System.Windows.Forms.Padding(2);
            this.btnNuevo.Size = new System.Drawing.Size(32, 32);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(29, 33);
            this.btnGuardar.Text = "Guardar ";
            this.btnGuardar.Visible = false;
            // 
            // btnImprimir
            // 
            this.btnImprimir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnImprimir.Enabled = false;
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(29, 33);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Visible = false;
            // 
            // ultraPanel1
            // 
            appearance14.BackColor = System.Drawing.Color.Silver;
            appearance14.BackColor2 = System.Drawing.Color.DimGray;
            appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop20;
            appearance14.BackHatchStyle = Infragistics.Win.BackHatchStyle.DiagonalCross;
            this.ultraPanel1.Appearance = appearance14;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.grpFechas);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 36);
            this.ultraPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(1416, 86);
            this.ultraPanel1.TabIndex = 137;
            // 
            // grpFechas
            // 
            appearance16.BackColor = System.Drawing.Color.Transparent;
            this.grpFechas.Appearance = appearance16;
            this.grpFechas.Controls.Add(this.label1);
            this.grpFechas.Controls.Add(this.lblHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroHasta);
            this.grpFechas.Controls.Add(this.dtpFiltroDesde);
            this.grpFechas.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.TopOutsideBorder;
            this.grpFechas.Location = new System.Drawing.Point(8, 4);
            this.grpFechas.Margin = new System.Windows.Forms.Padding(4);
            this.grpFechas.Name = "grpFechas";
            this.grpFechas.Size = new System.Drawing.Size(419, 71);
            this.grpFechas.TabIndex = 0;
            this.grpFechas.Text = "Filtro por Fechas:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Desde:";
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(192, 37);
            this.lblHasta.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(49, 17);
            this.lblHasta.TabIndex = 6;
            this.lblHasta.Text = "Hasta:";
            // 
            // dtpFiltroHasta
            // 
            this.dtpFiltroHasta.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroHasta.Location = new System.Drawing.Point(255, 33);
            this.dtpFiltroHasta.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFiltroHasta.Name = "dtpFiltroHasta";
            this.dtpFiltroHasta.Size = new System.Drawing.Size(114, 22);
            this.dtpFiltroHasta.TabIndex = 4;
            // 
            // dtpFiltroDesde
            // 
            this.dtpFiltroDesde.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFiltroDesde.Location = new System.Drawing.Point(70, 35);
            this.dtpFiltroDesde.Margin = new System.Windows.Forms.Padding(4);
            this.dtpFiltroDesde.Name = "dtpFiltroDesde";
            this.dtpFiltroDesde.Size = new System.Drawing.Size(114, 22);
            this.dtpFiltroDesde.TabIndex = 3;
            this.dtpFiltroDesde.Value = new System.DateTime(2010, 1, 1, 0, 0, 0, 0);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // frmReversionCopago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1416, 643);
            this.Controls.Add(this.gridReversion);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmReversionCopago";
            this.Text = "Reversión Copago";
            ((System.ComponentModel.ISupportInitialize)(this.gridReversion)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpFechas)).EndInit();
            this.grpFechas.ResumeLayout(false);
            this.grpFechas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid gridReversion;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nuevoAccesoToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnGuardar;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private Infragistics.Win.Misc.UltraGroupBox grpFechas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroHasta;
        private System.Windows.Forms.DateTimePicker dtpFiltroDesde;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}