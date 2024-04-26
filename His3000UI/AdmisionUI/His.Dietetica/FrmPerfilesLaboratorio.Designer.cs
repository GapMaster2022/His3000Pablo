
namespace His.Dietetica
{
    partial class FrmPerfilesLaboratorio
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPerfilesLaboratorio));
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
            this.tools = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnEditar = new System.Windows.Forms.ToolStripButton();
            this.btnCancelar = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSalir = new System.Windows.Forms.ToolStripButton();
            this.ultraExpandableGroupBox1 = new Infragistics.Win.Misc.UltraExpandableGroupBox();
            this.ultraExpandableGroupBoxPanel1 = new Infragistics.Win.Misc.UltraExpandableGroupBoxPanel();
            this.cmbPedido = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.ultraLabel2 = new Infragistics.Win.Misc.UltraLabel();
            this.btnEliminar = new Infragistics.Win.Misc.UltraButton();
            this.btnAñadir = new Infragistics.Win.Misc.UltraButton();
            this.btnEliminarProce = new Infragistics.Win.Misc.UltraButton();
            this.cmbProducto = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.lblproducto = new Infragistics.Win.Misc.UltraLabel();
            this.txtprocedimiento = new Infragistics.Win.UltraWinEditors.UltraTextEditor();
            this.ultraLabel1 = new Infragistics.Win.Misc.UltraLabel();
            this.btnGuardarProc = new Infragistics.Win.Misc.UltraButton();
            this.ultraGridProcedimiento = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.errores = new System.Windows.Forms.ErrorProvider(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoProcedimientoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dsProcedimiento1 = new His.Datos.dsProcedimiento();
            this.tools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExpandableGroupBox1)).BeginInit();
            this.ultraExpandableGroupBox1.SuspendLayout();
            this.ultraExpandableGroupBoxPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPedido)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProducto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprocedimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridProcedimiento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsProcedimiento1)).BeginInit();
            this.SuspendLayout();
            // 
            // tools
            // 
            this.tools.AutoSize = false;
            this.tools.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tools.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnEditar,
            this.btnCancelar,
            this.toolStripSeparator1,
            this.btnSalir});
            this.tools.Location = new System.Drawing.Point(0, 0);
            this.tools.Name = "tools";
            this.tools.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.tools.Size = new System.Drawing.Size(1126, 69);
            this.tools.TabIndex = 81;
            this.tools.Text = "toolStrip1";
            // 
            // btnNuevo
            // 
            this.btnNuevo.AutoSize = false;
            this.btnNuevo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNuevo.Image = ((System.Drawing.Image)(resources.GetObject("btnNuevo.Image")));
            this.btnNuevo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNuevo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(42, 42);
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.ToolTipText = "Nuevo Procedimiento";
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.AutoSize = false;
            this.btnEditar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnEditar.Image = global::His.Dietetica.Properties.Resources.file_edit_114433__1_;
            this.btnEditar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnEditar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(42, 42);
            this.btnEditar.Text = "Editar";
            this.btnEditar.ToolTipText = "Editar Procedimiento";
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.AutoSize = false;
            this.btnCancelar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnCancelar.Image = global::His.Dietetica.Properties.Resources.HIS_CANCELAR;
            this.btnCancelar.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnCancelar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(42, 42);
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.ToolTipText = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 69);
            // 
            // btnSalir
            // 
            this.btnSalir.AutoSize = false;
            this.btnSalir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSalir.Image = ((System.Drawing.Image)(resources.GetObject("btnSalir.Image")));
            this.btnSalir.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSalir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(42, 42);
            this.btnSalir.Text = "toolStripButton1";
            this.btnSalir.ToolTipText = "Salir";
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // ultraExpandableGroupBox1
            // 
            this.ultraExpandableGroupBox1.Controls.Add(this.ultraExpandableGroupBoxPanel1);
            this.ultraExpandableGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraExpandableGroupBox1.ExpandedSize = new System.Drawing.Size(1126, 181);
            this.ultraExpandableGroupBox1.Location = new System.Drawing.Point(0, 69);
            this.ultraExpandableGroupBox1.Name = "ultraExpandableGroupBox1";
            this.ultraExpandableGroupBox1.Size = new System.Drawing.Size(1126, 181);
            this.ultraExpandableGroupBox1.TabIndex = 82;
            this.ultraExpandableGroupBox1.Text = "Datos de Procedimiento";
            this.ultraExpandableGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2000;
            // 
            // ultraExpandableGroupBoxPanel1
            // 
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.cmbPedido);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.ultraLabel2);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.btnEliminar);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.btnAñadir);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.btnEliminarProce);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.cmbProducto);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.lblproducto);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.txtprocedimiento);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.ultraLabel1);
            this.ultraExpandableGroupBoxPanel1.Controls.Add(this.btnGuardarProc);
            this.ultraExpandableGroupBoxPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraExpandableGroupBoxPanel1.Location = new System.Drawing.Point(3, 23);
            this.ultraExpandableGroupBoxPanel1.Name = "ultraExpandableGroupBoxPanel1";
            this.ultraExpandableGroupBoxPanel1.Size = new System.Drawing.Size(1120, 155);
            this.ultraExpandableGroupBoxPanel1.TabIndex = 0;
            // 
            // cmbPedido
            // 
            this.cmbPedido.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
            this.cmbPedido.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cmbPedido.Location = new System.Drawing.Point(91, 95);
            this.cmbPedido.Name = "cmbPedido";
            this.cmbPedido.Size = new System.Drawing.Size(514, 28);
            this.cmbPedido.TabIndex = 18;
            // 
            // ultraLabel2
            // 
            this.ultraLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel2.Location = new System.Drawing.Point(3, 98);
            this.ultraLabel2.Name = "ultraLabel2";
            this.ultraLabel2.Size = new System.Drawing.Size(100, 25);
            this.ultraLabel2.TabIndex = 17;
            this.ultraLabel2.Text = "Producto*:";
            // 
            // btnEliminar
            // 
            this.btnEliminar.Location = new System.Drawing.Point(739, 94);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(112, 29);
            this.btnEliminar.TabIndex = 16;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // btnAñadir
            // 
            this.btnAñadir.Location = new System.Drawing.Point(620, 94);
            this.btnAñadir.Name = "btnAñadir";
            this.btnAñadir.Size = new System.Drawing.Size(112, 29);
            this.btnAñadir.TabIndex = 15;
            this.btnAñadir.Text = "Añadir";
            this.btnAñadir.Click += new System.EventHandler(this.btnAñadir_Click);
            // 
            // btnEliminarProce
            // 
            this.btnEliminarProce.Location = new System.Drawing.Point(818, 3);
            this.btnEliminarProce.Name = "btnEliminarProce";
            this.btnEliminarProce.Size = new System.Drawing.Size(112, 29);
            this.btnEliminarProce.TabIndex = 14;
            this.btnEliminarProce.Text = "Eliminar";
            this.btnEliminarProce.Click += new System.EventHandler(this.btnEliminarProce_Click);
            // 
            // cmbProducto
            // 
            this.cmbProducto.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend;
            this.cmbProducto.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains;
            this.cmbProducto.Location = new System.Drawing.Point(91, 46);
            this.cmbProducto.Name = "cmbProducto";
            this.cmbProducto.Size = new System.Drawing.Size(514, 28);
            this.cmbProducto.TabIndex = 7;
            this.cmbProducto.ValueChanged += new System.EventHandler(this.cmbProducto_ValueChanged);
            // 
            // lblproducto
            // 
            this.lblproducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblproducto.Location = new System.Drawing.Point(3, 49);
            this.lblproducto.Name = "lblproducto";
            this.lblproducto.Size = new System.Drawing.Size(100, 25);
            this.lblproducto.TabIndex = 6;
            this.lblproducto.Text = "Seccion*:";
            // 
            // txtprocedimiento
            // 
            this.txtprocedimiento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtprocedimiento.Enabled = false;
            this.txtprocedimiento.Location = new System.Drawing.Point(231, 5);
            this.txtprocedimiento.Name = "txtprocedimiento";
            this.txtprocedimiento.Size = new System.Drawing.Size(462, 28);
            this.txtprocedimiento.TabIndex = 5;
            // 
            // ultraLabel1
            // 
            this.ultraLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ultraLabel1.Location = new System.Drawing.Point(3, 8);
            this.ultraLabel1.Name = "ultraLabel1";
            this.ultraLabel1.Size = new System.Drawing.Size(166, 25);
            this.ultraLabel1.TabIndex = 4;
            this.ultraLabel1.Text = "Nombre del Perfil*:";
            // 
            // btnGuardarProc
            // 
            this.btnGuardarProc.Location = new System.Drawing.Point(699, 3);
            this.btnGuardarProc.Name = "btnGuardarProc";
            this.btnGuardarProc.Size = new System.Drawing.Size(112, 29);
            this.btnGuardarProc.TabIndex = 0;
            this.btnGuardarProc.Text = "Guardar";
            this.btnGuardarProc.Click += new System.EventHandler(this.btnGuardarProc_Click_1);
            // 
            // ultraGridProcedimiento
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.ultraGridProcedimiento.DisplayLayout.Appearance = appearance1;
            this.ultraGridProcedimiento.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.ultraGridProcedimiento.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridProcedimiento.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridProcedimiento.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.ultraGridProcedimiento.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.ultraGridProcedimiento.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.ultraGridProcedimiento.DisplayLayout.MaxColScrollRegions = 1;
            this.ultraGridProcedimiento.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ultraGridProcedimiento.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ultraGridProcedimiento.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.ultraGridProcedimiento.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.ultraGridProcedimiento.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.ultraGridProcedimiento.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.ultraGridProcedimiento.DisplayLayout.Override.CellAppearance = appearance8;
            this.ultraGridProcedimiento.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.ultraGridProcedimiento.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.ultraGridProcedimiento.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.ultraGridProcedimiento.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.ultraGridProcedimiento.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.ultraGridProcedimiento.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.ultraGridProcedimiento.DisplayLayout.Override.RowAppearance = appearance11;
            this.ultraGridProcedimiento.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ultraGridProcedimiento.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.ultraGridProcedimiento.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.ultraGridProcedimiento.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.ultraGridProcedimiento.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.ultraGridProcedimiento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGridProcedimiento.Location = new System.Drawing.Point(0, 250);
            this.ultraGridProcedimiento.Name = "ultraGridProcedimiento";
            this.ultraGridProcedimiento.Size = new System.Drawing.Size(1126, 375);
            this.ultraGridProcedimiento.TabIndex = 83;
            this.ultraGridProcedimiento.Text = "ultraGrid1";
            this.ultraGridProcedimiento.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.ultraGridProcedimiento_InitializeLayout);
            this.ultraGridProcedimiento.DoubleClickCell += new Infragistics.Win.UltraWinGrid.DoubleClickCellEventHandler(this.ultraGridProcedimiento_DoubleClickCell);
            this.ultraGridProcedimiento.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ultraGridProcedimiento_MouseClick);
            // 
            // errores
            // 
            this.errores.ContainerControl = this;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoProcedimientoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(216, 36);
            // 
            // nuevoProcedimientoToolStripMenuItem
            // 
            this.nuevoProcedimientoToolStripMenuItem.Name = "nuevoProcedimientoToolStripMenuItem";
            this.nuevoProcedimientoToolStripMenuItem.Size = new System.Drawing.Size(215, 32);
            this.nuevoProcedimientoToolStripMenuItem.Text = "Nuevo producto";
            this.nuevoProcedimientoToolStripMenuItem.Click += new System.EventHandler(this.nuevoProcedimientoToolStripMenuItem_Click);
            // 
            // dsProcedimiento1
            // 
            this.dsProcedimiento1.DataSetName = "dsProcedimiento";
            this.dsProcedimiento1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FrmPerfilesLaboratorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1126, 625);
            this.Controls.Add(this.ultraGridProcedimiento);
            this.Controls.Add(this.ultraExpandableGroupBox1);
            this.Controls.Add(this.tools);
            this.Name = "FrmPerfilesLaboratorio";
            this.Text = "Crear Perfiles Laboratorio";
            this.tools.ResumeLayout(false);
            this.tools.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraExpandableGroupBox1)).EndInit();
            this.ultraExpandableGroupBox1.ResumeLayout(false);
            this.ultraExpandableGroupBoxPanel1.ResumeLayout(false);
            this.ultraExpandableGroupBoxPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPedido)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbProducto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtprocedimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGridProcedimiento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errores)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dsProcedimiento1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip tools;
        private System.Windows.Forms.ToolStripButton btnNuevo;
        private System.Windows.Forms.ToolStripButton btnEditar;
        private System.Windows.Forms.ToolStripButton btnCancelar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btnSalir;
        private Infragistics.Win.Misc.UltraExpandableGroupBox ultraExpandableGroupBox1;
        private Infragistics.Win.Misc.UltraExpandableGroupBoxPanel ultraExpandableGroupBoxPanel1;
        private Infragistics.Win.Misc.UltraButton btnEliminarProce;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cmbProducto;
        private Infragistics.Win.Misc.UltraLabel lblproducto;
        private Infragistics.Win.UltraWinEditors.UltraTextEditor txtprocedimiento;
        private Infragistics.Win.Misc.UltraLabel ultraLabel1;
        private Infragistics.Win.Misc.UltraButton btnGuardarProc;
        private Infragistics.Win.UltraWinGrid.UltraGrid ultraGridProcedimiento;
        private Infragistics.Win.Misc.UltraButton btnEliminar;
        private Infragistics.Win.Misc.UltraButton btnAñadir;
        private System.Windows.Forms.ErrorProvider errores;
        private Datos.dsProcedimiento dsProcedimiento1;
        private Infragistics.Win.UltraWinEditors.UltraComboEditor cmbPedido;
        private Infragistics.Win.Misc.UltraLabel ultraLabel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem nuevoProcedimientoToolStripMenuItem;
    }
}