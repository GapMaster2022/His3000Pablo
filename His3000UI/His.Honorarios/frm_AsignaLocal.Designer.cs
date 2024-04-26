namespace His.Honorarios
{
    partial class frm_AsignaLocal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_AsignaLocal));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmb_locales = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.frm_AsignaLocal_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.groupBox1.SuspendLayout();
            this.menu.SuspendLayout();
            this.frm_AsignaLocal_Fill_Panel.ClientArea.SuspendLayout();
            this.frm_AsignaLocal_Fill_Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.groupBox1.Controls.Add(this.cmb_locales);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(21, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // cmb_locales
            // 
            this.cmb_locales.FormattingEnabled = true;
            this.cmb_locales.Location = new System.Drawing.Point(115, 40);
            this.cmb_locales.Name = "cmb_locales";
            this.cmb_locales.Size = new System.Drawing.Size(234, 21);
            this.cmb_locales.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(52, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Local:";
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnGuardar,
            this.btnNuevo,
            this.btnActualizar,
            this.btnEliminar,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(433, 36);
            this.menu.TabIndex = 65;
            this.menu.Text = "menu";
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(66, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(58, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Visible = false;
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(70, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(63, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Visible = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(69, 33);
            this.btnCancelar.Text = "Cancelar";
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
            // frm_AsignaLocal_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop50;
            this.frm_AsignaLocal_Fill_Panel.Appearance = appearance1;
            // 
            // frm_AsignaLocal_Fill_Panel.ClientArea
            // 
            this.frm_AsignaLocal_Fill_Panel.ClientArea.Controls.Add(this.groupBox1);
            this.frm_AsignaLocal_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frm_AsignaLocal_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frm_AsignaLocal_Fill_Panel.Location = new System.Drawing.Point(0, 36);
            this.frm_AsignaLocal_Fill_Panel.Name = "frm_AsignaLocal_Fill_Panel";
            this.frm_AsignaLocal_Fill_Panel.Size = new System.Drawing.Size(433, 175);
            this.frm_AsignaLocal_Fill_Panel.TabIndex = 66;
            // 
            // frm_AsignaLocal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(433, 211);
            this.Controls.Add(this.frm_AsignaLocal_Fill_Panel);
            this.Controls.Add(this.menu);
            this.Name = "frm_AsignaLocal";
            this.Text = "Asignación de Local";
            this.Load += new System.EventHandler(this.frm_AsignaLocal_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.frm_AsignaLocal_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frm_AsignaLocal_Fill_Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmb_locales;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private Infragistics.Win.Misc.UltraPanel frm_AsignaLocal_Fill_Panel;
    }
}