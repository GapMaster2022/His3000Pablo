namespace His.Maintenance
{
    partial class frmDivisionPolitica
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDivisionPolitica));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.txtLongitud = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.txtLatitud = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.txtCodPadre = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.lblLongitud = new Infragistics.Win.Misc.UltraLabel();
            this.lblLatitud = new Infragistics.Win.Misc.UltraLabel();
            this.cb_tipoLocalidades = new System.Windows.Forms.ComboBox();
            this.ultraLabel5 = new Infragistics.Win.Misc.UltraLabel();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.ultraPanel1 = new Infragistics.Win.Misc.UltraPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.ultraTree = new Infragistics.Win.UltraWinTree.UltraTree();
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.cb_claseLocalidades = new System.Windows.Forms.ComboBox();
            this.ultraLabel4 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraLabel3 = new Infragistics.Win.Misc.UltraLabel();
            this.txtPadre = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.txtCodInec = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
            this.txtNombre = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.ultraSplitter1 = new Infragistics.Win.Misc.UltraSplitter();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.infMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtCodPadre)).BeginInit();
            this.ultraPanel1.ClientArea.SuspendLayout();
            this.ultraPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPadre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre)).BeginInit();
            this.infMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(78, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = ((System.Drawing.Image)(resources.GetObject("btnGuardar.Image")));
            this.btnGuardar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(69, 33);
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelar.Image")));
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(73, 33);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtLongitud
            // 
            this.txtLongitud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLongitud.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txtLongitud.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.txtLongitud.InputMask = "nnnnnnnnn";
            this.txtLongitud.Location = new System.Drawing.Point(103, 120);
            this.txtLongitud.Name = "txtLongitud";
            this.txtLongitud.Size = new System.Drawing.Size(153, 20);
            this.txtLongitud.TabIndex = 3;
            // 
            // txtLatitud
            // 
            this.txtLatitud.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLatitud.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txtLatitud.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
            this.txtLatitud.InputMask = "+nnnnnnnnn";
            this.txtLatitud.Location = new System.Drawing.Point(103, 90);
            this.txtLatitud.Name = "txtLatitud";
            this.txtLatitud.Size = new System.Drawing.Size(153, 20);
            this.txtLatitud.TabIndex = 2;
            // 
            // txtCodPadre
            // 
            this.txtCodPadre.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txtCodPadre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCodPadre.Location = new System.Drawing.Point(103, 152);
            this.txtCodPadre.Name = "txtCodPadre";
            this.txtCodPadre.Size = new System.Drawing.Size(129, 21);
            this.txtCodPadre.TabIndex = 4;
            // 
            // lblLongitud
            // 
            appearance1.BackColor = System.Drawing.Color.Transparent;
            this.lblLongitud.Appearance = appearance1;
            this.lblLongitud.Location = new System.Drawing.Point(14, 125);
            this.lblLongitud.Name = "lblLongitud";
            this.lblLongitud.Size = new System.Drawing.Size(71, 20);
            this.lblLongitud.TabIndex = 15;
            this.lblLongitud.Text = "Longitud:";
            // 
            // lblLatitud
            // 
            appearance2.BackColor = System.Drawing.Color.Transparent;
            this.lblLatitud.Appearance = appearance2;
            this.lblLatitud.Location = new System.Drawing.Point(13, 95);
            this.lblLatitud.Name = "lblLatitud";
            this.lblLatitud.Size = new System.Drawing.Size(72, 20);
            this.lblLatitud.TabIndex = 13;
            this.lblLatitud.Text = "Latitud:";
            // 
            // cb_tipoLocalidades
            // 
            this.cb_tipoLocalidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_tipoLocalidades.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_tipoLocalidades.FormattingEnabled = true;
            this.cb_tipoLocalidades.Location = new System.Drawing.Point(103, 210);
            this.cb_tipoLocalidades.Name = "cb_tipoLocalidades";
            this.cb_tipoLocalidades.Size = new System.Drawing.Size(153, 21);
            this.cb_tipoLocalidades.TabIndex = 7;
            // 
            // ultraLabel5
            // 
            appearance3.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel5.Appearance = appearance3;
            this.ultraLabel5.Location = new System.Drawing.Point(13, 215);
            this.ultraLabel5.Name = "ultraLabel5";
            this.ultraLabel5.Size = new System.Drawing.Size(46, 20);
            this.ultraLabel5.TabIndex = 9;
            this.ultraLabel5.Text = "Tipo:";
            // 
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(49, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // ultraPanel1
            // 
            appearance8.BackColor = System.Drawing.Color.LightGray;
            appearance8.BackColor2 = System.Drawing.Color.GhostWhite;
            appearance8.BackGradientStyle = Infragistics.Win.GradientStyle.GlassTop37;
            this.ultraPanel1.Appearance = appearance8;
            // 
            // ultraPanel1.ClientArea
            // 
            this.ultraPanel1.ClientArea.Controls.Add(this.splitContainer1);
            this.ultraPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraPanel1.Location = new System.Drawing.Point(0, 36);
            this.ultraPanel1.Name = "ultraPanel1";
            this.ultraPanel1.Size = new System.Drawing.Size(565, 284);
            this.ultraPanel1.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ultraTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ultraGroupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.ultraSplitter1);
            this.splitContainer1.Size = new System.Drawing.Size(559, 278);
            this.splitContainer1.SplitterDistance = 230;
            this.splitContainer1.TabIndex = 0;
            // 
            // ultraTree
            // 
            this.ultraTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraTree.Location = new System.Drawing.Point(3, 3);
            this.ultraTree.Name = "ultraTree";
            this.ultraTree.Size = new System.Drawing.Size(224, 272);
            this.ultraTree.TabIndex = 0;
            this.ultraTree.BeforeActivate += new Infragistics.Win.UltraWinTree.BeforeNodeChangedEventHandler(this.ultraTree_BeforeActivate);
            this.ultraTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ultraTree_MouseDown);
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ultraGroupBox1.Controls.Add(this.txtLongitud);
            this.ultraGroupBox1.Controls.Add(this.txtLatitud);
            this.ultraGroupBox1.Controls.Add(this.txtCodPadre);
            this.ultraGroupBox1.Controls.Add(this.lblLongitud);
            this.ultraGroupBox1.Controls.Add(this.lblLatitud);
            this.ultraGroupBox1.Controls.Add(this.cb_tipoLocalidades);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel5);
            this.ultraGroupBox1.Controls.Add(this.cb_claseLocalidades);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel4);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel3);
            this.ultraGroupBox1.Controls.Add(this.txtPadre);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel2);
            this.ultraGroupBox1.Controls.Add(this.txtCodInec);
            this.ultraGroupBox1.Controls.Add(this.txtNombre);
            this.ultraGroupBox1.Controls.Add(this.ultraLabel1);
            this.ultraGroupBox1.Location = new System.Drawing.Point(30, 14);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(281, 247);
            this.ultraGroupBox1.TabIndex = 1;
            this.ultraGroupBox1.Text = "Información";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.XP;
            // 
            // cb_claseLocalidades
            // 
            this.cb_claseLocalidades.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_claseLocalidades.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cb_claseLocalidades.FormattingEnabled = true;
            this.cb_claseLocalidades.Location = new System.Drawing.Point(103, 180);
            this.cb_claseLocalidades.Name = "cb_claseLocalidades";
            this.cb_claseLocalidades.Size = new System.Drawing.Size(153, 21);
            this.cb_claseLocalidades.TabIndex = 6;
            // 
            // ultraLabel4
            // 
            appearance4.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel4.Appearance = appearance4;
            this.ultraLabel4.Location = new System.Drawing.Point(13, 185);
            this.ultraLabel4.Name = "ultraLabel4";
            this.ultraLabel4.Size = new System.Drawing.Size(46, 20);
            this.ultraLabel4.TabIndex = 7;
            this.ultraLabel4.Text = "Clase:";
            // 
            // ultraLabel3
            // 
            appearance5.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel3.Appearance = appearance5;
            this.ultraLabel3.Location = new System.Drawing.Point(13, 155);
            this.ultraLabel3.Name = "ultraLabel3";
            this.ultraLabel3.Size = new System.Drawing.Size(72, 20);
            this.ultraLabel3.TabIndex = 5;
            this.ultraLabel3.Text = "Pertenece a:";
            // 
            // txtPadre
            // 
            this.txtPadre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPadre.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txtPadre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPadre.Location = new System.Drawing.Point(233, 150);
            this.txtPadre.Name = "txtPadre";
            this.txtPadre.Size = new System.Drawing.Size(23, 21);
            this.txtPadre.TabIndex = 5;
            // 
            // ultraLabel2
            // 
            appearance6.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel2.Appearance = appearance6;
            this.ultraLabel2.Location = new System.Drawing.Point(13, 65);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(72, 20);
            this.ultraLabel2.TabIndex = 3;
            this.ultraLabel2.Text = "Nombre:";
            // 
            // txtCodInec
            // 
            this.txtCodInec.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodInec.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txtCodInec.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.Integer;
            this.txtCodInec.InputMask = "nnnnnnnnnnnnnnnnnnn";
            this.txtCodInec.Location = new System.Drawing.Point(103, 30);
            this.txtCodInec.Name = "txtCodInec";
            this.txtCodInec.Size = new System.Drawing.Size(153, 20);
            this.txtCodInec.TabIndex = 0;
            // 
            // txtNombre
            // 
            this.txtNombre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNombre.BorderStyle = Infragistics.Win.UIElementBorderStyle.Rounded1;
            this.txtNombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombre.Location = new System.Drawing.Point(103, 60);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(153, 21);
            this.txtNombre.TabIndex = 1;
            // 
            // ultraLabel1
            // 
            appearance7.BackColor = System.Drawing.Color.Transparent;
            this.ultraLabel1.Appearance = appearance7;
            this.ultraLabel1.Location = new System.Drawing.Point(13, 35);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(83, 20);
            this.ultraLabel1.TabIndex = 0;
            this.ultraLabel1.Text = "Código INEC:";
            // 
            // ultraSplitter1
            // 
            this.ultraSplitter1.CollapseUIType = Infragistics.Win.Misc.CollapseUIType.None;
            this.ultraSplitter1.Location = new System.Drawing.Point(0, 0);
            this.ultraSplitter1.Name = "ultraSplitter1";
            this.ultraSplitter1.RestoreExtent = 2147483647;
            this.ultraSplitter1.Size = new System.Drawing.Size(10, 278);
            this.ultraSplitter1.TabIndex = 0;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Visible = false;
            // 
            // infMenuStrip
            // 
            this.infMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem,
            this.eliminarToolStripMenuItem});
            this.infMenuStrip.Name = "infMenuStrip";
            this.infMenuStrip.Size = new System.Drawing.Size(118, 48);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.nuevoToolStripMenuItem.Text = "Agregar";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevoToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarToolStripMenuItem_Click);
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnActualizar,
            this.btnEliminar,
            this.btnGuardar,
            this.btnCancelar,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(565, 36);
            this.menu.TabIndex = 2;
            this.menu.Text = "menu";
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(62, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Visible = false;
            // 
            // frmDivisionPolitica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(565, 320);
            this.Controls.Add(this.ultraPanel1);
            this.Controls.Add(this.menu);
            this.Name = "frmDivisionPolitica";
            this.Text = "Division Politica";
            ((System.ComponentModel.ISupportInitialize)(this.txtCodPadre)).EndInit();
            this.ultraPanel1.ClientArea.ResumeLayout(false);
            this.ultraPanel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            this.ultraGroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPadre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNombre)).EndInit();
            this.infMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit txtLongitud;
        private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit txtLatitud;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtCodPadre;
        private Infragistics.Win.Misc.UltraLabel lblLongitud;
        private Infragistics.Win.Misc.UltraLabel lblLatitud;
        private System.Windows.Forms.ComboBox cb_tipoLocalidades;
        private Infragistics.Win.Misc.UltraLabel ultraLabel5;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private Infragistics.Win.Misc.UltraPanel ultraPanel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Infragistics.Win.UltraWinTree.UltraTree ultraTree;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.ComboBox cb_claseLocalidades;
        private Infragistics.Win.Misc.UltraLabel ultraLabel4;
        private Infragistics.Win.Misc.UltraLabel ultraLabel3;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtPadre;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit txtCodInec;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtNombre;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraSplitter ultraSplitter1;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        private System.Windows.Forms.ContextMenuStrip infMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.ErrorProvider errorProvider;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
    }
}