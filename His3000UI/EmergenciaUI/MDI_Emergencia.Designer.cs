namespace His.Emergencia
{
    partial class MDI_Emergencia
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDI_Emergencia));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.mnu_archivo = new System.Windows.Forms.ToolStripMenuItem();
            this.smnu_explorapacientes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.smnu_salir = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_admision = new System.Windows.Forms.ToolStripMenuItem();
            this.triajeYSignosVitalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.registroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ingresoFormO8ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evoluciónMedicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certificadoMedicoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recetaMedicaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.médicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.evoluciónMédicosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.epicrisisYTrasferenciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.certificadoMédicoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.recetaMédicaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_informacionmorbimortalidad = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.indexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.txtEmpresa = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtNombres = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtFecha = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtHora = new System.Windows.Forms.ToolStripStatusLabel();
            this.timHora = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.ultraTabbedMdiManager2 = new Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiManager2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(223)))), ((int)(((byte)(245)))));
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_archivo,
            this.mnu_admision,
            this.médicosToolStripMenuItem,
            this.mnu_informacionmorbimortalidad,
            this.helpMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(915, 28);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "MenuStrip";
            // 
            // mnu_archivo
            // 
            this.mnu_archivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smnu_explorapacientes,
            this.toolStripSeparator1,
            this.smnu_salir});
            this.mnu_archivo.Name = "mnu_archivo";
            this.mnu_archivo.Size = new System.Drawing.Size(73, 24);
            this.mnu_archivo.Text = "Archivo";
            this.mnu_archivo.Visible = false;
            // 
            // smnu_explorapacientes
            // 
            this.smnu_explorapacientes.Name = "smnu_explorapacientes";
            this.smnu_explorapacientes.Size = new System.Drawing.Size(224, 26);
            this.smnu_explorapacientes.Text = "Explorar Pacientes";
            this.smnu_explorapacientes.Click += new System.EventHandler(this.smnu_explorapacientes_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(221, 6);
            // 
            // smnu_salir
            // 
            this.smnu_salir.Name = "smnu_salir";
            this.smnu_salir.Size = new System.Drawing.Size(224, 26);
            this.smnu_salir.Text = "Salir";
            this.smnu_salir.Click += new System.EventHandler(this.smnu_salir_Click);
            // 
            // mnu_admision
            // 
            this.mnu_admision.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.triajeYSignosVitalesToolStripMenuItem,
            this.registroToolStripMenuItem,
            this.ingresoFormO8ToolStripMenuItem,
            this.evoluciónMedicosToolStripMenuItem,
            this.certificadoMedicoToolStripMenuItem,
            this.recetaMedicaToolStripMenuItem});
            this.mnu_admision.Name = "mnu_admision";
            this.mnu_admision.Size = new System.Drawing.Size(107, 24);
            this.mnu_admision.Text = "Emergencias";
            // 
            // triajeYSignosVitalesToolStripMenuItem
            // 
            this.triajeYSignosVitalesToolStripMenuItem.Name = "triajeYSignosVitalesToolStripMenuItem";
            this.triajeYSignosVitalesToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.triajeYSignosVitalesToolStripMenuItem.Text = "Triaje y Signos Vitales";
            this.triajeYSignosVitalesToolStripMenuItem.Click += new System.EventHandler(this.triajeYSignosVitalesToolStripMenuItem_Click);
            // 
            // registroToolStripMenuItem
            // 
            this.registroToolStripMenuItem.Name = "registroToolStripMenuItem";
            this.registroToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.registroToolStripMenuItem.Text = "Registro";
            this.registroToolStripMenuItem.Visible = false;
            this.registroToolStripMenuItem.Click += new System.EventHandler(this.registroToolStripMenuItem_Click);
            // 
            // ingresoFormO8ToolStripMenuItem
            // 
            this.ingresoFormO8ToolStripMenuItem.Name = "ingresoFormO8ToolStripMenuItem";
            this.ingresoFormO8ToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.ingresoFormO8ToolStripMenuItem.Text = "Formulario 008";
            this.ingresoFormO8ToolStripMenuItem.Click += new System.EventHandler(this.ingresoFormO8ToolStripMenuItem_Click);
            // 
            // evoluciónMedicosToolStripMenuItem
            // 
            this.evoluciónMedicosToolStripMenuItem.Name = "evoluciónMedicosToolStripMenuItem";
            this.evoluciónMedicosToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.evoluciónMedicosToolStripMenuItem.Text = "Evolución Medicos";
            this.evoluciónMedicosToolStripMenuItem.Click += new System.EventHandler(this.evoluciónMedicosToolStripMenuItem_Click);
            // 
            // certificadoMedicoToolStripMenuItem
            // 
            this.certificadoMedicoToolStripMenuItem.Name = "certificadoMedicoToolStripMenuItem";
            this.certificadoMedicoToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.certificadoMedicoToolStripMenuItem.Text = "Certificado Medico";
            this.certificadoMedicoToolStripMenuItem.Click += new System.EventHandler(this.certificadoMedicoToolStripMenuItem_Click);
            // 
            // recetaMedicaToolStripMenuItem
            // 
            this.recetaMedicaToolStripMenuItem.Name = "recetaMedicaToolStripMenuItem";
            this.recetaMedicaToolStripMenuItem.Size = new System.Drawing.Size(235, 26);
            this.recetaMedicaToolStripMenuItem.Text = "Receta Medica";
            this.recetaMedicaToolStripMenuItem.Click += new System.EventHandler(this.recetaMedicaToolStripMenuItem_Click);
            // 
            // médicosToolStripMenuItem
            // 
            this.médicosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.evoluciónMédicosToolStripMenuItem,
            this.epicrisisYTrasferenciaToolStripMenuItem,
            this.certificadoMédicoToolStripMenuItem1,
            this.recetaMédicaToolStripMenuItem1});
            this.médicosToolStripMenuItem.Name = "médicosToolStripMenuItem";
            this.médicosToolStripMenuItem.Size = new System.Drawing.Size(79, 24);
            this.médicosToolStripMenuItem.Text = "Médicos";
            this.médicosToolStripMenuItem.Visible = false;
            // 
            // evoluciónMédicosToolStripMenuItem
            // 
            this.evoluciónMédicosToolStripMenuItem.AutoSize = false;
            this.evoluciónMédicosToolStripMenuItem.Name = "evoluciónMédicosToolStripMenuItem";
            this.evoluciónMédicosToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.evoluciónMédicosToolStripMenuItem.Text = "Evolución Médicos";
            this.evoluciónMédicosToolStripMenuItem.Click += new System.EventHandler(this.evoluciónMédicosToolStripMenuItem_Click);
            // 
            // epicrisisYTrasferenciaToolStripMenuItem
            // 
            this.epicrisisYTrasferenciaToolStripMenuItem.Name = "epicrisisYTrasferenciaToolStripMenuItem";
            this.epicrisisYTrasferenciaToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.epicrisisYTrasferenciaToolStripMenuItem.Text = "Epicrisis y Trasferencia";
            this.epicrisisYTrasferenciaToolStripMenuItem.Click += new System.EventHandler(this.epicrisisYTrasferenciaToolStripMenuItem_Click);
            // 
            // certificadoMédicoToolStripMenuItem1
            // 
            this.certificadoMédicoToolStripMenuItem1.Name = "certificadoMédicoToolStripMenuItem1";
            this.certificadoMédicoToolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
            this.certificadoMédicoToolStripMenuItem1.Text = "CertificadoMédico";
            this.certificadoMédicoToolStripMenuItem1.Click += new System.EventHandler(this.certificadoMédicoToolStripMenuItem1_Click);
            // 
            // recetaMédicaToolStripMenuItem1
            // 
            this.recetaMédicaToolStripMenuItem1.Name = "recetaMédicaToolStripMenuItem1";
            this.recetaMédicaToolStripMenuItem1.Size = new System.Drawing.Size(239, 26);
            this.recetaMédicaToolStripMenuItem1.Text = "Receta Médica";
            this.recetaMédicaToolStripMenuItem1.Click += new System.EventHandler(this.recetaMédicaToolStripMenuItem1_Click);
            // 
            // mnu_informacionmorbimortalidad
            // 
            this.mnu_informacionmorbimortalidad.Name = "mnu_informacionmorbimortalidad";
            this.mnu_informacionmorbimortalidad.Size = new System.Drawing.Size(221, 24);
            this.mnu_informacionmorbimortalidad.Text = "Información Morbimortalidad";
            this.mnu_informacionmorbimortalidad.Visible = false;
            this.mnu_informacionmorbimortalidad.Click += new System.EventHandler(this.mnu_informacionmorbimortalidad_Click);
            // 
            // helpMenu
            // 
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contentsToolStripMenuItem,
            this.indexToolStripMenuItem,
            this.searchToolStripMenuItem,
            this.toolStripSeparator8,
            this.aboutToolStripMenuItem});
            this.helpMenu.Enabled = false;
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(65, 24);
            this.helpMenu.Text = "Ay&uda";
            this.helpMenu.Visible = false;
            // 
            // contentsToolStripMenuItem
            // 
            this.contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            this.contentsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F1)));
            this.contentsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.contentsToolStripMenuItem.Text = "&Contenido";
            // 
            // indexToolStripMenuItem
            // 
            this.indexToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("indexToolStripMenuItem.Image")));
            this.indexToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            this.indexToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.indexToolStripMenuItem.Text = "&Índice";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("searchToolStripMenuItem.Image")));
            this.searchToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Black;
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.searchToolStripMenuItem.Text = "&Buscar";
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(221, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.aboutToolStripMenuItem.Text = "&Acerca de... ...";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtEmpresa,
            this.txtNombres,
            this.txtFecha,
            this.txtHora});
            this.statusStrip.Location = new System.Drawing.Point(0, 465);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(915, 30);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "StatusStrip";
            // 
            // txtEmpresa
            // 
            this.txtEmpresa.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.txtEmpresa.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.txtEmpresa.Name = "txtEmpresa";
            this.txtEmpresa.Size = new System.Drawing.Size(70, 24);
            this.txtEmpresa.Text = "Empresa";
            // 
            // txtNombres
            // 
            this.txtNombres.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.txtNombres.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.txtNombres.Name = "txtNombres";
            this.txtNombres.Size = new System.Drawing.Size(61, 24);
            this.txtNombres.Text = "usuario";
            // 
            // txtFecha
            // 
            this.txtFecha.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.txtFecha.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(49, 24);
            this.txtFecha.Text = "fecha";
            // 
            // txtHora
            // 
            this.txtHora.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.txtHora.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.txtHora.Name = "txtHora";
            this.txtHora.Size = new System.Drawing.Size(43, 24);
            this.txtHora.Text = "hora";
            // 
            // timHora
            // 
            this.timHora.Tick += new System.EventHandler(this.timHora_Tick);
            // 
            // ultraTabbedMdiManager2
            // 
            appearance1.BackColor = System.Drawing.Color.GhostWhite;
            appearance1.BackColor2 = System.Drawing.Color.Gainsboro;
            appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            this.ultraTabbedMdiManager2.Appearance = appearance1;
            this.ultraTabbedMdiManager2.MdiParent = this;
            this.ultraTabbedMdiManager2.ViewStyle = Infragistics.Win.UltraWinTabbedMdi.ViewStyle.Office2007;
            // 
            // MDI_Emergencia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 495);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MDI_Emergencia";
            this.Text = "Emergencia";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MDI_Emergencia_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTabbedMdiManager2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnu_archivo;
        private System.Windows.Forms.ToolStripMenuItem smnu_explorapacientes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem smnu_salir;
        private System.Windows.Forms.ToolStripMenuItem mnu_admision;
        private System.Windows.Forms.ToolStripMenuItem mnu_informacionmorbimortalidad;
        private System.Windows.Forms.ToolStripMenuItem helpMenu;
        private System.Windows.Forms.ToolStripMenuItem contentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem indexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel txtEmpresa;
        private System.Windows.Forms.ToolStripStatusLabel txtNombres;
        private System.Windows.Forms.ToolStripStatusLabel txtFecha;
        private System.Windows.Forms.ToolStripStatusLabel txtHora;
        private System.Windows.Forms.Timer timHora;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem registroToolStripMenuItem;
        //private Infragistics.Win.UltraWinForm.UltraFormDockArea _MDI_Mantenimiento_UltraFormManager_Dock_Area_Left;
        //private Infragistics.Win.UltraWinForm.UltraFormManager ultraFormManager1;
        //private Infragistics.Win.UltraWinForm.UltraFormDockArea _MDI_Mantenimiento_UltraFormManager_Dock_Area_Right;
        //private Infragistics.Win.UltraWinForm.UltraFormDockArea _MDI_Mantenimiento_UltraFormManager_Dock_Area_Top;
        //private Infragistics.Win.UltraWinForm.UltraFormDockArea _MDI_Mantenimiento_UltraFormManager_Dock_Area_Bottom;
        //private Infragistics.Win.UltraWinStatusBar.UltraStatusBar ultraStatusBarTarifario;
        private Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager ultraTabbedMdiManager1;
        private Infragistics.Win.UltraWinTabbedMdi.UltraTabbedMdiManager ultraTabbedMdiManager2;
        private System.Windows.Forms.ToolStripMenuItem ingresoFormO8ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triajeYSignosVitalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem médicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem evoluciónMédicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem epicrisisYTrasferenciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem certificadoMédicoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem recetaMédicaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem evoluciónMedicosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem certificadoMedicoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem recetaMedicaToolStripMenuItem;
    }
}

