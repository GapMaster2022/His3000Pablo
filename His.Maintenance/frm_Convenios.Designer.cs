namespace His.Maintenance
{
    partial class frm_Categoria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Categoria));
            this.grpDatosPrincipales = new System.Windows.Forms.GroupBox();
            this.Cmb_tempresas = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Cmb_aseguradoras = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridcategorias = new System.Windows.Forms.DataGridView();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.controlErrores = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.txt_descripcion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.l_nombre = new System.Windows.Forms.Label();
            this.grpDatosPrincipales.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridcategorias)).BeginInit();
            this.menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDatosPrincipales
            // 
            this.grpDatosPrincipales.BackColor = System.Drawing.Color.Transparent;
            this.grpDatosPrincipales.Controls.Add(this.Cmb_tempresas);
            this.grpDatosPrincipales.Controls.Add(this.label4);
            this.grpDatosPrincipales.Controls.Add(this.Cmb_aseguradoras);
            this.grpDatosPrincipales.Controls.Add(this.label3);
            this.grpDatosPrincipales.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatosPrincipales.Location = new System.Drawing.Point(12, 42);
            this.grpDatosPrincipales.Name = "grpDatosPrincipales";
            this.grpDatosPrincipales.Size = new System.Drawing.Size(643, 54);
            this.grpDatosPrincipales.TabIndex = 35;
            this.grpDatosPrincipales.TabStop = false;
            this.grpDatosPrincipales.Enter += new System.EventHandler(this.grpDatosPrincipales_Enter);
            // 
            // Cmb_tempresas
            // 
            this.Cmb_tempresas.FormattingEnabled = true;
            this.Cmb_tempresas.Items.AddRange(new object[] {
            "CONVENIOS",
            "CATEGORIAS"});
            this.Cmb_tempresas.Location = new System.Drawing.Point(155, 20);
            this.Cmb_tempresas.Name = "Cmb_tempresas";
            this.Cmb_tempresas.Size = new System.Drawing.Size(119, 23);
            this.Cmb_tempresas.TabIndex = 10;
            this.Cmb_tempresas.SelectedIndexChanged += new System.EventHandler(this.Cmb_tempresas_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(93, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Convenio";
            // 
            // Cmb_aseguradoras
            // 
            this.Cmb_aseguradoras.FormattingEnabled = true;
            this.Cmb_aseguradoras.Items.AddRange(new object[] {
            "CONVENIOS",
            "CATEGORIAS"});
            this.Cmb_aseguradoras.Location = new System.Drawing.Point(280, 20);
            this.Cmb_aseguradoras.Name = "Cmb_aseguradoras";
            this.Cmb_aseguradoras.Size = new System.Drawing.Size(324, 23);
            this.Cmb_aseguradoras.TabIndex = 8;
            this.Cmb_aseguradoras.SelectedIndexChanged += new System.EventHandler(this.Cmb_aseguradoras_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(269, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 16);
            this.label3.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.gridcategorias);
            this.panel1.Location = new System.Drawing.Point(12, 207);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(643, 199);
            this.panel1.TabIndex = 37;
            // 
            // gridcategorias
            // 
            this.gridcategorias.AllowUserToAddRows = false;
            this.gridcategorias.AllowUserToDeleteRows = false;
            this.gridcategorias.AllowUserToOrderColumns = true;
            this.gridcategorias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridcategorias.Location = new System.Drawing.Point(12, 14);
            this.gridcategorias.Name = "gridcategorias";
            this.gridcategorias.ReadOnly = true;
            this.gridcategorias.Size = new System.Drawing.Size(616, 174);
            this.gridcategorias.TabIndex = 0;
            this.gridcategorias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridcategorias_CellContentClick_1);
            this.gridcategorias.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridcateoria_RowHeaderMouseDoubleClick);
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
            this.menu.Size = new System.Drawing.Size(667, 36);
            this.menu.TabIndex = 38;
            this.menu.Text = "menu";
            this.menu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menu_ItemClicked);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(62, 33);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Image = ((System.Drawing.Image)(resources.GetObject("btnActualizar.Image")));
            this.btnActualizar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(78, 33);
            this.btnActualizar.Text = "Modificar";
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
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
            // btnCerrar
            // 
            this.btnCerrar.Image = ((System.Drawing.Image)(resources.GetObject("btnCerrar.Image")));
            this.btnCerrar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(49, 33);
            this.btnCerrar.Text = "Salir";
            this.btnCerrar.Visible = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // controlErrores
            // 
            this.controlErrores.ContainerControl = this;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.txt_descripcion);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txt_nombre);
            this.panel2.Controls.Add(this.l_nombre);
            this.panel2.Location = new System.Drawing.Point(14, 102);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(640, 99);
            this.panel2.TabIndex = 39;
            // 
            // txt_descripcion
            // 
            this.txt_descripcion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_descripcion.Location = new System.Drawing.Point(153, 32);
            this.txt_descripcion.MaxLength = 64;
            this.txt_descripcion.Multiline = true;
            this.txt_descripcion.Name = "txt_descripcion";
            this.txt_descripcion.Size = new System.Drawing.Size(449, 58);
            this.txt_descripcion.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(36, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Descripcion Convenio";
            // 
            // txt_nombre
            // 
            this.txt_nombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nombre.Location = new System.Drawing.Point(153, 5);
            this.txt_nombre.MaxLength = 64;
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(449, 20);
            this.txt_nombre.TabIndex = 7;
            // 
            // l_nombre
            // 
            this.l_nombre.AutoSize = true;
            this.l_nombre.Location = new System.Drawing.Point(103, 8);
            this.l_nombre.Name = "l_nombre";
            this.l_nombre.Size = new System.Drawing.Size(44, 13);
            this.l_nombre.TabIndex = 8;
            this.l_nombre.Text = "Nombre";
            // 
            // frm_Categoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(667, 412);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpDatosPrincipales);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frm_Categoria";
            this.Text = "Convenios y Categorias";
            this.Load += new System.EventHandler(this.frm_Convenios_Load);
            this.grpDatosPrincipales.ResumeLayout(false);
            this.grpDatosPrincipales.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridcategorias)).EndInit();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.controlErrores)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDatosPrincipales;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gridcategorias;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnCancelar;
        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.ErrorProvider controlErrores;
        private System.Windows.Forms.ComboBox Cmb_aseguradoras;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox Cmb_tempresas;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txt_descripcion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.Label l_nombre;
    }
}