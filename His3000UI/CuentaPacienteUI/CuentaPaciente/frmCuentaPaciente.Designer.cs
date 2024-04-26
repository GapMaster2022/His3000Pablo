namespace CuentaPaciente
{
    partial class frmCuentaPaciente
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
            this.cmb_Areas = new System.Windows.Forms.ComboBox();
            this.cmb_SubArea = new System.Windows.Forms.ComboBox();
            this.lblRubros = new System.Windows.Forms.Label();
            this.btnSolicitar = new System.Windows.Forms.Button();
            this.label34 = new System.Windows.Forms.Label();
            this.gridDetalleFactura = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetalleFactura)).BeginInit();
            this.SuspendLayout();
            // 
            // cmb_Areas
            // 
            this.cmb_Areas.FormattingEnabled = true;
            this.cmb_Areas.Location = new System.Drawing.Point(57, 38);
            this.cmb_Areas.Name = "cmb_Areas";
            this.cmb_Areas.Size = new System.Drawing.Size(169, 21);
            this.cmb_Areas.TabIndex = 94;
            // 
            // cmb_SubArea
            // 
            this.cmb_SubArea.FormattingEnabled = true;
            this.cmb_SubArea.Location = new System.Drawing.Point(437, 43);
            this.cmb_SubArea.Name = "cmb_SubArea";
            this.cmb_SubArea.Size = new System.Drawing.Size(121, 21);
            this.cmb_SubArea.TabIndex = 97;
            // 
            // lblRubros
            // 
            this.lblRubros.AutoSize = true;
            this.lblRubros.BackColor = System.Drawing.Color.Transparent;
            this.lblRubros.Location = new System.Drawing.Point(9, 43);
            this.lblRubros.Name = "lblRubros";
            this.lblRubros.Size = new System.Drawing.Size(35, 13);
            this.lblRubros.TabIndex = 93;
            this.lblRubros.Text = "Área :";
            // 
            // btnSolicitar
            // 
            this.btnSolicitar.Location = new System.Drawing.Point(244, 38);
            this.btnSolicitar.Name = "btnSolicitar";
            this.btnSolicitar.Size = new System.Drawing.Size(55, 23);
            this.btnSolicitar.TabIndex = 95;
            this.btnSolicitar.Text = "Solicitar";
            this.btnSolicitar.UseVisualStyleBackColor = true;
            this.btnSolicitar.Click += new System.EventHandler(this.btnSolicitar_Click);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Location = new System.Drawing.Point(434, 27);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(54, 13);
            this.label34.TabIndex = 96;
            this.label34.Text = "SubArea :";
            // 
            // gridDetalleFactura
            // 
            this.gridDetalleFactura.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.gridDetalleFactura.DisplayLayout.Appearance = appearance1;
            this.gridDetalleFactura.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.gridDetalleFactura.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.gridDetalleFactura.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridDetalleFactura.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.gridDetalleFactura.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.gridDetalleFactura.DisplayLayout.GroupByBox.Hidden = true;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.gridDetalleFactura.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.gridDetalleFactura.DisplayLayout.MaxColScrollRegions = 1;
            this.gridDetalleFactura.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.gridDetalleFactura.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.gridDetalleFactura.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.gridDetalleFactura.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.gridDetalleFactura.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.gridDetalleFactura.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.gridDetalleFactura.DisplayLayout.Override.CellAppearance = appearance8;
            this.gridDetalleFactura.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.gridDetalleFactura.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.gridDetalleFactura.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.gridDetalleFactura.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.gridDetalleFactura.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.gridDetalleFactura.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.gridDetalleFactura.DisplayLayout.Override.RowAppearance = appearance11;
            this.gridDetalleFactura.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.gridDetalleFactura.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.gridDetalleFactura.DisplayLayout.Override.WrapHeaderText = Infragistics.Win.DefaultableBoolean.False;
            this.gridDetalleFactura.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.gridDetalleFactura.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.gridDetalleFactura.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.gridDetalleFactura.Location = new System.Drawing.Point(12, 88);
            this.gridDetalleFactura.Name = "gridDetalleFactura";
            this.gridDetalleFactura.Size = new System.Drawing.Size(570, 205);
            this.gridDetalleFactura.TabIndex = 100;
            this.gridDetalleFactura.Text = "ultraGridDetallefactura";
            // 
            // frmCuentaPaciente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 421);
            this.Controls.Add(this.gridDetalleFactura);
            this.Controls.Add(this.cmb_Areas);
            this.Controls.Add(this.cmb_SubArea);
            this.Controls.Add(this.lblRubros);
            this.Controls.Add(this.btnSolicitar);
            this.Controls.Add(this.label34);
            this.Name = "frmCuentaPaciente";
            this.Text = "9";
            ((System.ComponentModel.ISupportInitialize)(this.gridDetalleFactura)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmb_Areas;
        private System.Windows.Forms.ComboBox cmb_SubArea;
        private System.Windows.Forms.Label lblRubros;
        private System.Windows.Forms.Button btnSolicitar;
        private System.Windows.Forms.Label label34;
        private Infragistics.Win.UltraWinGrid.UltraGrid gridDetalleFactura;
    }
}