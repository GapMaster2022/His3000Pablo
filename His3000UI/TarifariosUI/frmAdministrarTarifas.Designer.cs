namespace TarifariosUI
{
    partial class frmAdministrarTarifas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdministrarTarifas));
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.tvEspecialidades = new System.Windows.Forms.TreeView();
            this.lblTarifario = new System.Windows.Forms.Label();
            this.tarifarioList = new System.Windows.Forms.ComboBox();
            this.especialidadesPanel = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tarifariosDetalleGrid = new System.Windows.Forms.DataGridView();
            this.lblTarifas = new System.Windows.Forms.Label();
            this.paneFiltro = new System.Windows.Forms.Panel();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.menu.SuspendLayout();
            this.especialidadesPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tarifariosDetalleGrid)).BeginInit();
            this.paneFiltro.SuspendLayout();
            this.SuspendLayout();
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
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(47, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblEspecialidad
            // 
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEspecialidad.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEspecialidad.Location = new System.Drawing.Point(17, 40);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(65, 13);
            this.lblEspecialidad.TabIndex = 17;
            this.lblEspecialidad.Text = "Especialidad";
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnGuardar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(950, 36);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            // 
            // tvEspecialidades
            // 
            this.tvEspecialidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvEspecialidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tvEspecialidades.Location = new System.Drawing.Point(3, 3);
            this.tvEspecialidades.Name = "tvEspecialidades";
            this.tvEspecialidades.Size = new System.Drawing.Size(757, 119);
            this.tvEspecialidades.TabIndex = 0;
            this.tvEspecialidades.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEspecialidades_AfterSelect);
            // 
            // lblTarifario
            // 
            this.lblTarifario.AutoSize = true;
            this.lblTarifario.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarifario.Location = new System.Drawing.Point(17, 10);
            this.lblTarifario.Name = "lblTarifario";
            this.lblTarifario.Size = new System.Drawing.Size(111, 13);
            this.lblTarifario.TabIndex = 19;
            this.lblTarifario.Text = "Seleccione el Tarifario";
            // 
            // tarifarioList
            // 
            this.tarifarioList.BackColor = System.Drawing.Color.PowderBlue;
            this.tarifarioList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tarifarioList.FormattingEnabled = true;
            this.tarifarioList.Location = new System.Drawing.Point(134, 10);
            this.tarifarioList.Name = "tarifarioList";
            this.tarifarioList.Size = new System.Drawing.Size(200, 21);
            this.tarifarioList.TabIndex = 1;
            this.tarifarioList.SelectedIndexChanged += new System.EventHandler(this.tarifarioList_SelectedIndexChanged);
            // 
            // especialidadesPanel
            // 
            this.especialidadesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.especialidadesPanel.BackColor = System.Drawing.Color.Silver;
            this.especialidadesPanel.Controls.Add(this.tvEspecialidades);
            this.especialidadesPanel.Location = new System.Drawing.Point(134, 37);
            this.especialidadesPanel.Name = "especialidadesPanel";
            this.especialidadesPanel.Size = new System.Drawing.Size(764, 125);
            this.especialidadesPanel.TabIndex = 16;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.tarifariosDetalleGrid);
            this.panel1.Location = new System.Drawing.Point(12, 250);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(927, 210);
            this.panel1.TabIndex = 3;
            // 
            // tarifariosDetalleGrid
            // 
            this.tarifariosDetalleGrid.AllowUserToAddRows = false;
            this.tarifariosDetalleGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tarifariosDetalleGrid.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.tarifariosDetalleGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tarifariosDetalleGrid.Location = new System.Drawing.Point(3, 3);
            this.tarifariosDetalleGrid.Name = "tarifariosDetalleGrid";
            this.tarifariosDetalleGrid.Size = new System.Drawing.Size(919, 202);
            this.tarifariosDetalleGrid.TabIndex = 0;
            // 
            // lblTarifas
            // 
            this.lblTarifas.AutoSize = true;
            this.lblTarifas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarifas.Location = new System.Drawing.Point(13, 231);
            this.lblTarifas.Name = "lblTarifas";
            this.lblTarifas.Size = new System.Drawing.Size(126, 16);
            this.lblTarifas.TabIndex = 2;
            this.lblTarifas.Text = "Detalle del Tarifario";
            // 
            // paneFiltro
            // 
            this.paneFiltro.BackColor = System.Drawing.Color.Gainsboro;
            this.paneFiltro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.paneFiltro.Controls.Add(this.lblEspecialidad);
            this.paneFiltro.Controls.Add(this.lblTarifario);
            this.paneFiltro.Controls.Add(this.tarifarioList);
            this.paneFiltro.Controls.Add(this.especialidadesPanel);
            this.paneFiltro.Location = new System.Drawing.Point(12, 39);
            this.paneFiltro.Name = "paneFiltro";
            this.paneFiltro.Size = new System.Drawing.Size(926, 189);
            this.paneFiltro.TabIndex = 1;
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(58, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // frmAdministrarTarifas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(950, 472);
            this.Controls.Add(this.paneFiltro);
            this.Controls.Add(this.lblTarifas);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menu);
            this.Name = "frmAdministrarTarifas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración de tarifas";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAdministrarTarifas_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.especialidadesPanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tarifariosDetalleGrid)).EndInit();
            this.paneFiltro.ResumeLayout(false);
            this.paneFiltro.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.Label lblEspecialidad;
        protected System.Windows.Forms.ToolStrip menu;
        private System.Windows.Forms.TreeView tvEspecialidades;
        private System.Windows.Forms.Label lblTarifario;
        private System.Windows.Forms.ComboBox tarifarioList;
        private System.Windows.Forms.Panel especialidadesPanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTarifas;
        private System.Windows.Forms.Panel paneFiltro;
        private System.Windows.Forms.DataGridView tarifariosDetalleGrid;
        private System.Windows.Forms.ToolStripButton btnNuevo;
    }
}