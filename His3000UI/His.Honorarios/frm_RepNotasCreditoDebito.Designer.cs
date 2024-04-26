namespace His.Honorarios
{
    partial class frm_RepNotasCreditoDebito
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RepNotasCreditoDebito));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.txt_proceso = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_medicos = new System.Windows.Forms.ComboBox();
            this.chk_automaticas = new System.Windows.Forms.CheckBox();
            this.rbn_ndebito = new System.Windows.Forms.RadioButton();
            this.rbn_ncredito = new System.Windows.Forms.RadioButton();
            this.txt_fecfin = new System.Windows.Forms.MaskedTextBox();
            this.txt_fecini = new System.Windows.Forms.MaskedTextBox();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.frm_RepNotasCreditoDebito_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.groupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.menu.SuspendLayout();
            this.frm_RepNotasCreditoDebito_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_RepNotasCreditoDebito_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_proceso
            // 
            this.txt_proceso.Location = new System.Drawing.Point(344, 115);
            this.txt_proceso.Name = "txt_proceso";
            this.txt_proceso.Size = new System.Drawing.Size(79, 20);
            this.txt_proceso.TabIndex = 15;
            this.txt_proceso.Leave += new System.EventHandler(this.txt_proceso_Leave);
            this.txt_proceso.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_proceso_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(32, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Médico:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(242, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Fecha Final:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(32, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Fecha Inicio:";
            // 
            // cmb_medicos
            // 
            this.cmb_medicos.FormattingEnabled = true;
            this.cmb_medicos.Location = new System.Drawing.Point(115, 64);
            this.cmb_medicos.Name = "cmb_medicos";
            this.cmb_medicos.Size = new System.Drawing.Size(308, 21);
            this.cmb_medicos.TabIndex = 11;
            // 
            // chk_automaticas
            // 
            this.chk_automaticas.AutoSize = true;
            this.chk_automaticas.BackColor = System.Drawing.Color.Transparent;
            this.chk_automaticas.Location = new System.Drawing.Point(223, 117);
            this.chk_automaticas.Name = "chk_automaticas";
            this.chk_automaticas.Size = new System.Drawing.Size(115, 17);
            this.chk_automaticas.TabIndex = 10;
            this.chk_automaticas.Text = "Notas Automáticas";
            this.chk_automaticas.UseVisualStyleBackColor = false;
            this.chk_automaticas.CheckedChanged += new System.EventHandler(this.chk_automaticas_CheckedChanged);
            // 
            // rbn_ndebito
            // 
            this.rbn_ndebito.AutoSize = true;
            this.rbn_ndebito.BackColor = System.Drawing.Color.Transparent;
            this.rbn_ndebito.Location = new System.Drawing.Point(115, 118);
            this.rbn_ndebito.Name = "rbn_ndebito";
            this.rbn_ndebito.Size = new System.Drawing.Size(102, 17);
            this.rbn_ndebito.TabIndex = 9;
            this.rbn_ndebito.TabStop = true;
            this.rbn_ndebito.Text = "Notas de Débito";
            this.rbn_ndebito.UseVisualStyleBackColor = false;
            this.rbn_ndebito.CheckedChanged += new System.EventHandler(this.rbn_ndebito_CheckedChanged);
            // 
            // rbn_ncredito
            // 
            this.rbn_ncredito.AutoSize = true;
            this.rbn_ncredito.BackColor = System.Drawing.Color.Transparent;
            this.rbn_ncredito.Location = new System.Drawing.Point(115, 95);
            this.rbn_ncredito.Name = "rbn_ncredito";
            this.rbn_ncredito.Size = new System.Drawing.Size(104, 17);
            this.rbn_ncredito.TabIndex = 8;
            this.rbn_ncredito.TabStop = true;
            this.rbn_ncredito.Text = "Notas de Crédito";
            this.rbn_ncredito.UseVisualStyleBackColor = false;
            this.rbn_ncredito.CheckedChanged += new System.EventHandler(this.rbn_ncredito_CheckedChanged);
            // 
            // txt_fecfin
            // 
            this.txt_fecfin.Location = new System.Drawing.Point(313, 38);
            this.txt_fecfin.Mask = "00/00/0000";
            this.txt_fecfin.Name = "txt_fecfin";
            this.txt_fecfin.Size = new System.Drawing.Size(110, 20);
            this.txt_fecfin.TabIndex = 7;
            this.txt_fecfin.ValidatingType = typeof(System.DateTime);
            this.txt_fecfin.Leave += new System.EventHandler(this.txt_fecfin_Leave);
            this.txt_fecfin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_fecfin_KeyPress);
            // 
            // txt_fecini
            // 
            this.txt_fecini.Location = new System.Drawing.Point(115, 38);
            this.txt_fecini.Mask = "00/00/0000";
            this.txt_fecini.Name = "txt_fecini";
            this.txt_fecini.Size = new System.Drawing.Size(110, 20);
            this.txt_fecini.TabIndex = 6;
            this.txt_fecini.ValidatingType = typeof(System.DateTime);
            this.txt_fecini.Leave += new System.EventHandler(this.txt_fecini_Leave);
            this.txt_fecini.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_fecini_KeyPress);
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnImprimir,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(4, 24);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(520, 36);
            this.menu.TabIndex = 64;
            this.menu.Text = "menu";
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(65, 33);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(47, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            // 
            // frm_RepNotasCreditoDebito_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_RepNotasCreditoDebito_Fill_Panel.Appearance = appearance1;
            // 
            // frm_RepNotasCreditoDebito_Fill_Panel.ClientArea
            // 
            this.frm_RepNotasCreditoDebito_Fill_Panel.ClientArea.Controls.Add(this.groupBox1);
            this.frm_RepNotasCreditoDebito_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_RepNotasCreditoDebito_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_RepNotasCreditoDebito_Fill_Panel.Location = new System.Drawing.Point(4, 60);
            this.frm_RepNotasCreditoDebito_Fill_Panel.Name = "frm_RepNotasCreditoDebito_Fill_Panel";
            this.frm_RepNotasCreditoDebito_Fill_Panel.Size = new System.Drawing.Size(520, 202);
            this.frm_RepNotasCreditoDebito_Fill_Panel.TabIndex = 65;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_proceso);
            this.groupBox1.Controls.Add(this.txt_fecfin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_fecini);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.rbn_ncredito);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.rbn_ndebito);
            this.groupBox1.Controls.Add(this.cmb_medicos);
            this.groupBox1.Controls.Add(this.chk_automaticas);
            this.groupBox1.Location = new System.Drawing.Point(28, 21);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 148);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // _frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left
            // 
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 24);
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.Name = "_frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left";
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 238);
            // 
            // _frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right
            // 
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(524, 24);
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.Name = "_frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right";
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 238);
            // 
            // _frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top
            // 
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top.Name = "_frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top";
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(528, 24);
            // 
            // _frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom
            // 
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 262);
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.Name = "_frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom";
            this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(528, 4);
            // 
            // frm_RepNotasCreditoDebito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(528, 266);
            this.Controls.Add(this.frm_RepNotasCreditoDebito_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Controls.Add(this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom);
            this.Name = "frm_RepNotasCreditoDebito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes de Notas de Credito o Debito";
            this.Load += new System.EventHandler(this.frm_RepNotasCreditoDebito_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.frm_RepNotasCreditoDebito_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_RepNotasCreditoDebito_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txt_fecfin;
        private System.Windows.Forms.MaskedTextBox txt_fecini;
        private System.Windows.Forms.ComboBox cmb_medicos;
        private System.Windows.Forms.CheckBox chk_automaticas;
        private System.Windows.Forms.RadioButton rbn_ndebito;
        private System.Windows.Forms.RadioButton rbn_ncredito;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnImprimir;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_proceso;
        private Infragistics.Win.Misc.UltraPanel frm_RepNotasCreditoDebito_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox groupBox1;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_RepNotasCreditoDebito_UltraFormManager_Dock_Area_Bottom;
    }
}