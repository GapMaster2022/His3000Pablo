namespace TarifariosUI
{
    partial class frmAdministrarConvenios
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAdministrarConvenios));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnCerrar = new System.Windows.Forms.ToolStripButton();
            this.lblEspecialidad = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lbCategoria = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAyudantia = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAnestesia = new System.Windows.Forms.TextBox();
            this.txtUvr = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.ToolStripButton();
            this.btnEliminar = new System.Windows.Forms.ToolStripButton();
            this.menu = new System.Windows.Forms.ToolStrip();
            this.btnNuevo = new System.Windows.Forms.ToolStripButton();
            this.btnActualizar = new System.Windows.Forms.ToolStripButton();
            this.btnImprimir = new System.Windows.Forms.ToolStripButton();
            this.lblTarifario = new System.Windows.Forms.Label();
            this.tarifarioList = new System.Windows.Forms.ComboBox();
            this.especialidadesPanel = new System.Windows.Forms.Panel();
            this.EspecialidadesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.tvEspecialidades = new System.Windows.Forms.TreeView();
            this.dgdbDetalleUvr = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.aseguradoraList = new System.Windows.Forms.ComboBox();
            this.lblAseguradora = new System.Windows.Forms.Label();
            this.tarifarioLis = new System.Windows.Forms.ComboBox();
            this.grpOpcAseguradoras = new System.Windows.Forms.GroupBox();
            this.rbtConcluidos = new System.Windows.Forms.RadioButton();
            this.rbtVigentes = new System.Windows.Forms.RadioButton();
            this.rbtTodos = new System.Windows.Forms.RadioButton();
            this.panelFiltros = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.menu.SuspendLayout();
            this.especialidadesPanel.SuspendLayout();
            this.EspecialidadesSplitContainer.Panel1.SuspendLayout();
            this.EspecialidadesSplitContainer.Panel2.SuspendLayout();
            this.EspecialidadesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgdbDetalleUvr)).BeginInit();
            this.grpOpcAseguradoras.SuspendLayout();
            this.panelFiltros.SuspendLayout();
            this.SuspendLayout();
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
            // lblEspecialidad
            // 
            this.lblEspecialidad.AutoSize = true;
            this.lblEspecialidad.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEspecialidad.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblEspecialidad.Location = new System.Drawing.Point(50, 3);
            this.lblEspecialidad.Name = "lblEspecialidad";
            this.lblEspecialidad.Size = new System.Drawing.Size(189, 14);
            this.lblEspecialidad.TabIndex = 18;
            this.lblEspecialidad.Text = "Clasificación de Especialidades";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lbCategoria);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.txtAyudantia);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtAnestesia);
            this.panel2.Controls.Add(this.txtUvr);
            this.panel2.Location = new System.Drawing.Point(9, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(888, 50);
            this.panel2.TabIndex = 0;
            // 
            // lbCategoria
            // 
            this.lbCategoria.AutoSize = true;
            this.lbCategoria.Location = new System.Drawing.Point(13, 15);
            this.lbCategoria.MaximumSize = new System.Drawing.Size(370, 30);
            this.lbCategoria.Name = "lbCategoria";
            this.lbCategoria.Size = new System.Drawing.Size(0, 13);
            this.lbCategoria.TabIndex = 10;
            this.lbCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(779, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "%";
            // 
            // txtAyudantia
            // 
            this.txtAyudantia.Enabled = false;
            this.txtAyudantia.Location = new System.Drawing.Point(735, 12);
            this.txtAyudantia.MaxLength = 2;
            this.txtAyudantia.Name = "txtAyudantia";
            this.txtAyudantia.Size = new System.Drawing.Size(33, 20);
            this.txtAyudantia.TabIndex = 8;
            this.txtAyudantia.Text = "0";
            this.txtAyudantia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(683, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ayudantia:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(433, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Valor de Anestesia:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Valor de conversión UVR:";
            // 
            // txtAnestesia
            // 
            this.txtAnestesia.Enabled = false;
            this.txtAnestesia.Location = new System.Drawing.Point(537, 12);
            this.txtAnestesia.Name = "txtAnestesia";
            this.txtAnestesia.Size = new System.Drawing.Size(69, 20);
            this.txtAnestesia.TabIndex = 1;
            this.txtAnestesia.Text = "0";
            this.txtAnestesia.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAnestesia.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAnestesia_KeyPress);
            // 
            // txtUvr
            // 
            this.txtUvr.Enabled = false;
            this.txtUvr.Location = new System.Drawing.Point(340, 14);
            this.txtUvr.Name = "txtUvr";
            this.txtUvr.Size = new System.Drawing.Size(67, 20);
            this.txtUvr.TabIndex = 0;
            this.txtUvr.Text = "0";
            this.txtUvr.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUvr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUvr_KeyPress);
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
            // btnEliminar
            // 
            this.btnEliminar.Image = ((System.Drawing.Image)(resources.GetObject("btnEliminar.Image")));
            this.btnEliminar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(70, 33);
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // menu
            // 
            this.menu.AutoSize = false;
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNuevo,
            this.btnActualizar,
            this.btnEliminar,
            this.btnGuardar,
            this.btnImprimir,
            this.btnCerrar});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(903, 36);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
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
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.Image = ((System.Drawing.Image)(resources.GetObject("btnImprimir.Image")));
            this.btnImprimir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(73, 33);
            this.btnImprimir.Text = "Imprimir";
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
            // 
            // lblTarifario
            // 
            this.lblTarifario.AutoSize = true;
            this.lblTarifario.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTarifario.Location = new System.Drawing.Point(-3, -16);
            this.lblTarifario.Name = "lblTarifario";
            this.lblTarifario.Size = new System.Drawing.Size(111, 13);
            this.lblTarifario.TabIndex = 20;
            this.lblTarifario.Text = "Seleccione el Tarifario";
            // 
            // tarifarioList
            // 
            this.tarifarioList.BackColor = System.Drawing.Color.RosyBrown;
            this.tarifarioList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tarifarioList.FormattingEnabled = true;
            this.tarifarioList.Location = new System.Drawing.Point(127, -19);
            this.tarifarioList.Name = "tarifarioList";
            this.tarifarioList.Size = new System.Drawing.Size(340, 21);
            this.tarifarioList.TabIndex = 19;
            // 
            // especialidadesPanel
            // 
            this.especialidadesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.especialidadesPanel.BackColor = System.Drawing.Color.Transparent;
            this.especialidadesPanel.Controls.Add(this.EspecialidadesSplitContainer);
            this.especialidadesPanel.Location = new System.Drawing.Point(9, 168);
            this.especialidadesPanel.Name = "especialidadesPanel";
            this.especialidadesPanel.Size = new System.Drawing.Size(894, 342);
            this.especialidadesPanel.TabIndex = 17;
            // 
            // EspecialidadesSplitContainer
            // 
            this.EspecialidadesSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.EspecialidadesSplitContainer.BackColor = System.Drawing.Color.Gainsboro;
            this.EspecialidadesSplitContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EspecialidadesSplitContainer.Location = new System.Drawing.Point(4, 3);
            this.EspecialidadesSplitContainer.Name = "EspecialidadesSplitContainer";
            // 
            // EspecialidadesSplitContainer.Panel1
            // 
            this.EspecialidadesSplitContainer.Panel1.Controls.Add(this.tvEspecialidades);
            this.EspecialidadesSplitContainer.Panel1.Controls.Add(this.lblEspecialidad);
            // 
            // EspecialidadesSplitContainer.Panel2
            // 
            this.EspecialidadesSplitContainer.Panel2.Controls.Add(this.dgdbDetalleUvr);
            this.EspecialidadesSplitContainer.Size = new System.Drawing.Size(887, 336);
            this.EspecialidadesSplitContainer.SplitterDistance = 300;
            this.EspecialidadesSplitContainer.TabIndex = 3;
            // 
            // tvEspecialidades
            // 
            this.tvEspecialidades.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tvEspecialidades.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tvEspecialidades.Location = new System.Drawing.Point(4, 20);
            this.tvEspecialidades.Name = "tvEspecialidades";
            this.tvEspecialidades.Size = new System.Drawing.Size(291, 311);
            this.tvEspecialidades.TabIndex = 0;
            this.tvEspecialidades.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvEspecialidades_BeforeSelect);
            this.tvEspecialidades.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvEspecialidades_AfterSelect);
            // 
            // dgdbDetalleUvr
            // 
            this.dgdbDetalleUvr.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgdbDetalleUvr.BackgroundColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgdbDetalleUvr.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgdbDetalleUvr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgdbDetalleUvr.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgdbDetalleUvr.Location = new System.Drawing.Point(3, 3);
            this.dgdbDetalleUvr.Name = "dgdbDetalleUvr";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgdbDetalleUvr.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgdbDetalleUvr.Size = new System.Drawing.Size(575, 328);
            this.dgdbDetalleUvr.TabIndex = 0;
            this.dgdbDetalleUvr.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgdbDetalleUvr_CellContentClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(87, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "Seleccione el Tarifario";
            // 
            // aseguradoraList
            // 
            this.aseguradoraList.BackColor = System.Drawing.Color.Silver;
            this.aseguradoraList.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aseguradoraList.FormattingEnabled = true;
            this.aseguradoraList.Location = new System.Drawing.Point(204, 41);
            this.aseguradoraList.Name = "aseguradoraList";
            this.aseguradoraList.Size = new System.Drawing.Size(296, 21);
            this.aseguradoraList.TabIndex = 2;
            this.aseguradoraList.SelectedIndexChanged += new System.EventHandler(this.aseguradoraList_SelectedIndexChanged);
            this.aseguradoraList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.aseguradoraList_KeyPress);
            // 
            // lblAseguradora
            // 
            this.lblAseguradora.AutoSize = true;
            this.lblAseguradora.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAseguradora.Location = new System.Drawing.Point(12, 41);
            this.lblAseguradora.Name = "lblAseguradora";
            this.lblAseguradora.Size = new System.Drawing.Size(186, 13);
            this.lblAseguradora.TabIndex = 25;
            this.lblAseguradora.Text = "Seleccione la Aseguradora o Empresa";
            // 
            // tarifarioLis
            // 
            this.tarifarioLis.BackColor = System.Drawing.Color.Silver;
            this.tarifarioLis.FormattingEnabled = true;
            this.tarifarioLis.Location = new System.Drawing.Point(204, 11);
            this.tarifarioLis.Name = "tarifarioLis";
            this.tarifarioLis.Size = new System.Drawing.Size(296, 21);
            this.tarifarioLis.TabIndex = 1;
            this.tarifarioLis.SelectedIndexChanged += new System.EventHandler(this.tarifarioLis_SelectedIndexChanged);
            this.tarifarioLis.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tarifarioLis_KeyPress);
            // 
            // grpOpcAseguradoras
            // 
            this.grpOpcAseguradoras.Controls.Add(this.rbtConcluidos);
            this.grpOpcAseguradoras.Controls.Add(this.rbtVigentes);
            this.grpOpcAseguradoras.Controls.Add(this.rbtTodos);
            this.grpOpcAseguradoras.Location = new System.Drawing.Point(516, 18);
            this.grpOpcAseguradoras.Name = "grpOpcAseguradoras";
            this.grpOpcAseguradoras.Size = new System.Drawing.Size(266, 44);
            this.grpOpcAseguradoras.TabIndex = 28;
            this.grpOpcAseguradoras.TabStop = false;
            this.grpOpcAseguradoras.Text = "Estado Convenio";
            // 
            // rbtConcluidos
            // 
            this.rbtConcluidos.AutoSize = true;
            this.rbtConcluidos.Location = new System.Drawing.Point(170, 19);
            this.rbtConcluidos.Name = "rbtConcluidos";
            this.rbtConcluidos.Size = new System.Drawing.Size(77, 17);
            this.rbtConcluidos.TabIndex = 2;
            this.rbtConcluidos.Text = "Concluidos";
            this.rbtConcluidos.UseVisualStyleBackColor = true;
            this.rbtConcluidos.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // rbtVigentes
            // 
            this.rbtVigentes.AutoSize = true;
            this.rbtVigentes.Location = new System.Drawing.Point(98, 19);
            this.rbtVigentes.Name = "rbtVigentes";
            this.rbtVigentes.Size = new System.Drawing.Size(66, 17);
            this.rbtVigentes.TabIndex = 1;
            this.rbtVigentes.Text = "Vigentes";
            this.rbtVigentes.UseVisualStyleBackColor = true;
            this.rbtVigentes.CheckedChanged += new System.EventHandler(this.rbtVigentes_CheckedChanged);
            // 
            // rbtTodos
            // 
            this.rbtTodos.AutoSize = true;
            this.rbtTodos.Checked = true;
            this.rbtTodos.Location = new System.Drawing.Point(23, 19);
            this.rbtTodos.Name = "rbtTodos";
            this.rbtTodos.Size = new System.Drawing.Size(55, 17);
            this.rbtTodos.TabIndex = 0;
            this.rbtTodos.TabStop = true;
            this.rbtTodos.Text = "Todos";
            this.rbtTodos.UseVisualStyleBackColor = true;
            this.rbtTodos.CheckedChanged += new System.EventHandler(this.rbtTodos_CheckedChanged);
            // 
            // panelFiltros
            // 
            this.panelFiltros.BackColor = System.Drawing.Color.Gainsboro;
            this.panelFiltros.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelFiltros.Controls.Add(this.grpOpcAseguradoras);
            this.panelFiltros.Controls.Add(this.label3);
            this.panelFiltros.Controls.Add(this.tarifarioLis);
            this.panelFiltros.Controls.Add(this.lblAseguradora);
            this.panelFiltros.Controls.Add(this.aseguradoraList);
            this.panelFiltros.Location = new System.Drawing.Point(9, 37);
            this.panelFiltros.Name = "panelFiltros";
            this.panelFiltros.Size = new System.Drawing.Size(888, 72);
            this.panelFiltros.TabIndex = 29;
            // 
            // frmAdministrarConvenios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(903, 533);
            this.Controls.Add(this.panelFiltros);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.menu);
            this.Controls.Add(this.lblTarifario);
            this.Controls.Add(this.tarifarioList);
            this.Controls.Add(this.especialidadesPanel);
            this.Name = "frmAdministrarConvenios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Aseguradoras y Convenios";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmAdministrarConvenios_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.especialidadesPanel.ResumeLayout(false);
            this.EspecialidadesSplitContainer.Panel1.ResumeLayout(false);
            this.EspecialidadesSplitContainer.Panel1.PerformLayout();
            this.EspecialidadesSplitContainer.Panel2.ResumeLayout(false);
            this.EspecialidadesSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgdbDetalleUvr)).EndInit();
            this.grpOpcAseguradoras.ResumeLayout(false);
            this.grpOpcAseguradoras.PerformLayout();
            this.panelFiltros.ResumeLayout(false);
            this.panelFiltros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.ToolStripButton btnCerrar;
        private System.Windows.Forms.Label lblEspecialidad;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAnestesia;
        private System.Windows.Forms.TextBox txtUvr;
        protected System.Windows.Forms.ToolStripButton btnGuardar;
        protected System.Windows.Forms.ToolStripButton btnEliminar;
        protected System.Windows.Forms.ToolStrip menu;
        protected System.Windows.Forms.ToolStripButton btnNuevo;
        protected System.Windows.Forms.ToolStripButton btnActualizar;
        private System.Windows.Forms.Label lblTarifario;
        private System.Windows.Forms.ComboBox tarifarioList;
        private System.Windows.Forms.Panel especialidadesPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox aseguradoraList;
        private System.Windows.Forms.Label lblAseguradora;
        private System.Windows.Forms.ComboBox tarifarioLis;
        private System.Windows.Forms.GroupBox grpOpcAseguradoras;
        private System.Windows.Forms.RadioButton rbtConcluidos;
        private System.Windows.Forms.RadioButton rbtVigentes;
        private System.Windows.Forms.RadioButton rbtTodos;
        private System.Windows.Forms.SplitContainer EspecialidadesSplitContainer;
        private System.Windows.Forms.TreeView tvEspecialidades;
        private System.Windows.Forms.DataGridView dgdbDetalleUvr;
        private System.Windows.Forms.ToolStripButton btnImprimir;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAyudantia;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelFiltros;
        private System.Windows.Forms.Label lbCategoria;
    }
}