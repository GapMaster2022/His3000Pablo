namespace His.Honorarios
{
    partial class frm_RepMedicos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_RepMedicos));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.cmb_tipohonorario = new System.Windows.Forms.ComboBox();
            this.cmb_tipomedico = new System.Windows.Forms.ComboBox();
            this.cmb_especialidad = new System.Windows.Forms.ComboBox();
            this.rbn_tipohonorario = new System.Windows.Forms.RadioButton();
            this.rbn_tipomedico = new System.Windows.Forms.RadioButton();
            this.rbn_especialidad = new System.Windows.Forms.RadioButton();
            this.rbn_todos = new System.Windows.Forms.RadioButton();
            this.ultraFormManager1 = new Infragistics.Win.UltraWinForm.UltraFormManager(this.components);
            this.frm_RepMedicos_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom = new Infragistics.Win.UltraWinForm.UltraFormDockArea();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).BeginInit();
            this.frm_RepMedicos_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_RepMedicos_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            this.SuspendLayout();
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
            this.menu.Size = new System.Drawing.Size(542, 36);
            this.menu.TabIndex = 63;
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
            // cmb_tipohonorario
            // 
            this.cmb_tipohonorario.FormattingEnabled = true;
            this.cmb_tipohonorario.Location = new System.Drawing.Point(190, 108);
            this.cmb_tipohonorario.Name = "cmb_tipohonorario";
            this.cmb_tipohonorario.Size = new System.Drawing.Size(236, 21);
            this.cmb_tipohonorario.TabIndex = 7;
            // 
            // cmb_tipomedico
            // 
            this.cmb_tipomedico.FormattingEnabled = true;
            this.cmb_tipomedico.Location = new System.Drawing.Point(190, 81);
            this.cmb_tipomedico.Name = "cmb_tipomedico";
            this.cmb_tipomedico.Size = new System.Drawing.Size(236, 21);
            this.cmb_tipomedico.TabIndex = 6;
            // 
            // cmb_especialidad
            // 
            this.cmb_especialidad.FormattingEnabled = true;
            this.cmb_especialidad.Location = new System.Drawing.Point(190, 52);
            this.cmb_especialidad.Name = "cmb_especialidad";
            this.cmb_especialidad.Size = new System.Drawing.Size(236, 21);
            this.cmb_especialidad.TabIndex = 5;
            // 
            // rbn_tipohonorario
            // 
            this.rbn_tipohonorario.AutoSize = true;
            this.rbn_tipohonorario.BackColor = System.Drawing.Color.Transparent;
            this.rbn_tipohonorario.Location = new System.Drawing.Point(44, 108);
            this.rbn_tipohonorario.Name = "rbn_tipohonorario";
            this.rbn_tipohonorario.Size = new System.Drawing.Size(110, 17);
            this.rbn_tipohonorario.TabIndex = 4;
            this.rbn_tipohonorario.TabStop = true;
            this.rbn_tipohonorario.Text = "Tipo de Honorario";
            this.rbn_tipohonorario.UseVisualStyleBackColor = false;
            this.rbn_tipohonorario.CheckedChanged += new System.EventHandler(this.rbn_tipohonorario_CheckedChanged);
            // 
            // rbn_tipomedico
            // 
            this.rbn_tipomedico.AutoSize = true;
            this.rbn_tipomedico.BackColor = System.Drawing.Color.Transparent;
            this.rbn_tipomedico.Location = new System.Drawing.Point(44, 82);
            this.rbn_tipomedico.Name = "rbn_tipomedico";
            this.rbn_tipomedico.Size = new System.Drawing.Size(99, 17);
            this.rbn_tipomedico.TabIndex = 3;
            this.rbn_tipomedico.TabStop = true;
            this.rbn_tipomedico.Text = "Tipo de Medico";
            this.rbn_tipomedico.UseVisualStyleBackColor = false;
            this.rbn_tipomedico.CheckedChanged += new System.EventHandler(this.rbn_tipomedico_CheckedChanged);
            // 
            // rbn_especialidad
            // 
            this.rbn_especialidad.AutoSize = true;
            this.rbn_especialidad.BackColor = System.Drawing.Color.Transparent;
            this.rbn_especialidad.Location = new System.Drawing.Point(44, 53);
            this.rbn_especialidad.Name = "rbn_especialidad";
            this.rbn_especialidad.Size = new System.Drawing.Size(104, 17);
            this.rbn_especialidad.TabIndex = 2;
            this.rbn_especialidad.TabStop = true;
            this.rbn_especialidad.Text = "Por Especialidad";
            this.rbn_especialidad.UseVisualStyleBackColor = false;
            this.rbn_especialidad.CheckedChanged += new System.EventHandler(this.rbn_especialidad_CheckedChanged);
            // 
            // rbn_todos
            // 
            this.rbn_todos.AutoSize = true;
            this.rbn_todos.BackColor = System.Drawing.Color.Transparent;
            this.rbn_todos.Location = new System.Drawing.Point(44, 30);
            this.rbn_todos.Name = "rbn_todos";
            this.rbn_todos.Size = new System.Drawing.Size(55, 17);
            this.rbn_todos.TabIndex = 1;
            this.rbn_todos.TabStop = true;
            this.rbn_todos.Text = "Todos";
            this.rbn_todos.UseVisualStyleBackColor = false;
            this.rbn_todos.CheckedChanged += new System.EventHandler(this.rbn_todos_CheckedChanged);
            // 
            // ultraFormManager1
            // 
            this.ultraFormManager1.Form = this;
            // 
            // frm_RepMedicos_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_RepMedicos_Fill_Panel.Appearance = appearance1;
            // 
            // frm_RepMedicos_Fill_Panel.ClientArea
            // 
            this.frm_RepMedicos_Fill_Panel.ClientArea.Controls.Add(this.ultraGroupBox1);
            this.frm_RepMedicos_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_RepMedicos_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_RepMedicos_Fill_Panel.Location = new System.Drawing.Point(4, 60);
            this.frm_RepMedicos_Fill_Panel.Name = "frm_RepMedicos_Fill_Panel";
            this.frm_RepMedicos_Fill_Panel.Size = new System.Drawing.Size(542, 192);
            this.frm_RepMedicos_Fill_Panel.TabIndex = 64;
            // 
            // _frm_RepMedicos_UltraFormManager_Dock_Area_Left
            // 
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Left;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.FormManager = this.ultraFormManager1;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.InitialResizeAreaExtent = 4;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.Location = new System.Drawing.Point(0, 24);
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.Name = "_frm_RepMedicos_UltraFormManager_Dock_Area_Left";
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Left.Size = new System.Drawing.Size(4, 228);
            // 
            // _frm_RepMedicos_UltraFormManager_Dock_Area_Right
            // 
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Right;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.FormManager = this.ultraFormManager1;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.InitialResizeAreaExtent = 4;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.Location = new System.Drawing.Point(546, 24);
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.Name = "_frm_RepMedicos_UltraFormManager_Dock_Area_Right";
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Right.Size = new System.Drawing.Size(4, 228);
            // 
            // _frm_RepMedicos_UltraFormManager_Dock_Area_Top
            // 
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Top;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top.FormManager = this.ultraFormManager1;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top.Location = new System.Drawing.Point(0, 0);
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top.Name = "_frm_RepMedicos_UltraFormManager_Dock_Area_Top";
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Top.Size = new System.Drawing.Size(550, 24);
            // 
            // _frm_RepMedicos_UltraFormManager_Dock_Area_Bottom
            // 
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.DockedPosition = Infragistics.Win.UltraWinForm.DockedPosition.Bottom;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.ForeColor = System.Drawing.SystemColors.ControlText;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.FormManager = this.ultraFormManager1;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.InitialResizeAreaExtent = 4;
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.Location = new System.Drawing.Point(0, 252);
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.Name = "_frm_RepMedicos_UltraFormManager_Dock_Area_Bottom";
            this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom.Size = new System.Drawing.Size(550, 4);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Controls.Add(this.cmb_tipohonorario);
            this.ultraGroupBox1.Controls.Add(this.cmb_especialidad);
            this.ultraGroupBox1.Controls.Add(this.cmb_tipomedico);
            this.ultraGroupBox1.Controls.Add(this.rbn_todos);
            this.ultraGroupBox1.Controls.Add(this.rbn_especialidad);
            this.ultraGroupBox1.Controls.Add(this.rbn_tipohonorario);
            this.ultraGroupBox1.Controls.Add(this.rbn_tipomedico);
            this.ultraGroupBox1.Location = new System.Drawing.Point(28, 20);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(483, 146);
            this.ultraGroupBox1.TabIndex = 65;
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frm_RepMedicos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(550, 256);
            this.Controls.Add(this.frm_RepMedicos_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Controls.Add(this._frm_RepMedicos_UltraFormManager_Dock_Area_Left);
            this.Controls.Add(this._frm_RepMedicos_UltraFormManager_Dock_Area_Right);
            this.Controls.Add(this._frm_RepMedicos_UltraFormManager_Dock_Area_Top);
            this.Controls.Add(this._frm_RepMedicos_UltraFormManager_Dock_Area_Bottom);
            this.Name = "frm_RepMedicos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Medicos";
            this.Load += new System.EventHandler(this.frm_RepMedicos_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraFormManager1)).EndInit();
            this.frm_RepMedicos_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_RepMedicos_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnImprimir;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.RadioButton rbn_tipohonorario;
        private System.Windows.Forms.RadioButton rbn_tipomedico;
        private System.Windows.Forms.RadioButton rbn_especialidad;
        private System.Windows.Forms.RadioButton rbn_todos;
        private System.Windows.Forms.ComboBox cmb_tipohonorario;
        private System.Windows.Forms.ComboBox cmb_tipomedico;
        private System.Windows.Forms.ComboBox cmb_especialidad;
        private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        private Infragistics.Win.Misc.UltraPanel frm_RepMedicos_Fill_Panel;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_RepMedicos_UltraFormManager_Dock_Area_Left;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_RepMedicos_UltraFormManager_Dock_Area_Right;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_RepMedicos_UltraFormManager_Dock_Area_Top;
        private Infragistics.Win.UltraWinForm.UltraFormDockArea _frm_RepMedicos_UltraFormManager_Dock_Area_Bottom;
    }
}