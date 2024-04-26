
namespace His.Formulario
{
    partial class frm_MedicamentoCompuesto
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
            this.cmb_medidas = new System.Windows.Forms.ComboBox();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.btnCargar = new System.Windows.Forms.Button();
            this.txtCantidadManual = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDosisUnitaria = new System.Windows.Forms.TextBox();
            this.dtgCabeceraKardex = new System.Windows.Forms.DataGridView();
            this.id_producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detalle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.via = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dosis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_reservas = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_reserva = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_dosis_registra = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_dosis_turno = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_frecuencia_hora = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbFrecuencia = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_listado_compuestos = new System.Windows.Forms.ComboBox();
            this.lblMedicamento = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbVia = new System.Windows.Forms.ComboBox();
            this.btnMedicamento = new System.Windows.Forms.Button();
            this.P_MENU = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.erroresPaciente = new System.Windows.Forms.ErrorProvider(this.components);
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtAplicacion = new System.Windows.Forms.TextBox();
            this.txtInfusion = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dtgCabeceraKardex)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.P_MENU.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.erroresPaciente)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmb_medidas
            // 
            this.cmb_medidas.FormattingEnabled = true;
            this.cmb_medidas.Items.AddRange(new object[] {
            "CC",
            "ML",
            "MG",
            "G"});
            this.cmb_medidas.Location = new System.Drawing.Point(114, 203);
            this.cmb_medidas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_medidas.Name = "cmb_medidas";
            this.cmb_medidas.Size = new System.Drawing.Size(80, 28);
            this.cmb_medidas.TabIndex = 29;
            this.cmb_medidas.TabStop = false;
            this.cmb_medidas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmb_medidas_KeyPress);
            // 
            // lblCantidad
            // 
            this.lblCantidad.Location = new System.Drawing.Point(273, 157);
            this.lblCantidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(114, 42);
            this.lblCantidad.TabIndex = 22;
            this.lblCantidad.Text = "Cantidad total de dosis:";
            this.lblCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCargar
            // 
            this.btnCargar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCargar.Location = new System.Drawing.Point(984, 178);
            this.btnCargar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.Size = new System.Drawing.Size(148, 55);
            this.btnCargar.TabIndex = 17;
            this.btnCargar.TabStop = false;
            this.btnCargar.Text = "CARGA MEDICAMENTO";
            this.btnCargar.UseVisualStyleBackColor = true;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // txtCantidadManual
            // 
            this.txtCantidadManual.Location = new System.Drawing.Point(278, 203);
            this.txtCantidadManual.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtCantidadManual.Name = "txtCantidadManual";
            this.txtCantidadManual.Size = new System.Drawing.Size(108, 26);
            this.txtCantidadManual.TabIndex = 21;
            this.txtCantidadManual.TabStop = false;
            this.txtCantidadManual.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadManual_KeyPress);
            this.txtCantidadManual.Leave += new System.EventHandler(this.txtCantidadManual_Leave);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(48, 157);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(148, 42);
            this.label5.TabIndex = 14;
            this.label5.Text = "Presentacion del Medicamento:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtDosisUnitaria
            // 
            this.txtDosisUnitaria.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDosisUnitaria.Location = new System.Drawing.Point(50, 203);
            this.txtDosisUnitaria.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtDosisUnitaria.Name = "txtDosisUnitaria";
            this.txtDosisUnitaria.Size = new System.Drawing.Size(54, 26);
            this.txtDosisUnitaria.TabIndex = 13;
            this.txtDosisUnitaria.TabStop = false;
            this.txtDosisUnitaria.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDosisUnitaria_KeyPress);
            // 
            // dtgCabeceraKardex
            // 
            this.dtgCabeceraKardex.AllowUserToAddRows = false;
            this.dtgCabeceraKardex.AllowUserToDeleteRows = false;
            this.dtgCabeceraKardex.AllowUserToOrderColumns = true;
            this.dtgCabeceraKardex.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dtgCabeceraKardex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgCabeceraKardex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_producto,
            this.detalle,
            this.via,
            this.dosis,
            this.cant});
            this.dtgCabeceraKardex.Location = new System.Drawing.Point(9, 354);
            this.dtgCabeceraKardex.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.dtgCabeceraKardex.MultiSelect = false;
            this.dtgCabeceraKardex.Name = "dtgCabeceraKardex";
            this.dtgCabeceraKardex.RowHeadersWidth = 62;
            this.dtgCabeceraKardex.Size = new System.Drawing.Size(1373, 525);
            this.dtgCabeceraKardex.TabIndex = 4;
            // 
            // id_producto
            // 
            this.id_producto.HeaderText = "ID";
            this.id_producto.MinimumWidth = 8;
            this.id_producto.Name = "id_producto";
            this.id_producto.ReadOnly = true;
            this.id_producto.Width = 50;
            // 
            // detalle
            // 
            this.detalle.HeaderText = "PRESENTACIÓN";
            this.detalle.MinimumWidth = 8;
            this.detalle.Name = "detalle";
            this.detalle.ReadOnly = true;
            this.detalle.Width = 400;
            // 
            // via
            // 
            this.via.HeaderText = "VÍA";
            this.via.MinimumWidth = 8;
            this.via.Name = "via";
            this.via.Width = 150;
            // 
            // dosis
            // 
            this.dosis.HeaderText = "DOSIS UNITARIA";
            this.dosis.MinimumWidth = 8;
            this.dosis.Name = "dosis";
            this.dosis.ReadOnly = true;
            this.dosis.Width = 150;
            // 
            // cant
            // 
            this.cant.HeaderText = "CANTIDAD";
            this.cant.MinimumWidth = 8;
            this.cant.Name = "cant";
            this.cant.Visible = false;
            this.cant.Width = 150;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_reservas);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txt_reserva);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_dosis_registra);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_dosis_turno);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmb_frecuencia_hora);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbFrecuencia);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmb_listado_compuestos);
            this.groupBox1.Controls.Add(this.btnCargar);
            this.groupBox1.Controls.Add(this.cmb_medidas);
            this.groupBox1.Controls.Add(this.lblMedicamento);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbVia);
            this.groupBox1.Controls.Add(this.lblCantidad);
            this.groupBox1.Controls.Add(this.btnMedicamento);
            this.groupBox1.Controls.Add(this.txtCantidadManual);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtDosisUnitaria);
            this.groupBox1.Location = new System.Drawing.Point(9, 78);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(1149, 245);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btn_reservas
            // 
            this.btn_reservas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_reservas.Location = new System.Drawing.Point(984, 114);
            this.btn_reservas.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_reservas.Name = "btn_reservas";
            this.btn_reservas.Size = new System.Drawing.Size(152, 55);
            this.btn_reservas.TabIndex = 43;
            this.btn_reservas.TabStop = false;
            this.btn_reservas.Text = "RESERVA DE MEDICAMENTO";
            this.btn_reservas.UseVisualStyleBackColor = true;
            this.btn_reservas.Click += new System.EventHandler(this.btn_reservas_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(843, 157);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 42);
            this.label8.TabIndex = 42;
            this.label8.Text = "Reserva de dosis:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_reserva
            // 
            this.txt_reserva.Location = new System.Drawing.Point(842, 203);
            this.txt_reserva.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_reserva.Name = "txt_reserva";
            this.txt_reserva.ReadOnly = true;
            this.txt_reserva.Size = new System.Drawing.Size(108, 26);
            this.txt_reserva.TabIndex = 41;
            this.txt_reserva.TabStop = false;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(654, 157);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 42);
            this.label7.TabIndex = 40;
            this.label7.Text = "Dosis a registrar en kardex:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_dosis_registra
            // 
            this.txt_dosis_registra.Location = new System.Drawing.Point(666, 203);
            this.txt_dosis_registra.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_dosis_registra.Name = "txt_dosis_registra";
            this.txt_dosis_registra.ReadOnly = true;
            this.txt_dosis_registra.Size = new System.Drawing.Size(108, 26);
            this.txt_dosis_registra.TabIndex = 39;
            this.txt_dosis_registra.TabStop = false;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(464, 157);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 42);
            this.label6.TabIndex = 38;
            this.label6.Text = "Dosis a aplicar en el turno:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_dosis_turno
            // 
            this.txt_dosis_turno.Location = new System.Drawing.Point(468, 203);
            this.txt_dosis_turno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_dosis_turno.Name = "txt_dosis_turno";
            this.txt_dosis_turno.Size = new System.Drawing.Size(108, 26);
            this.txt_dosis_turno.TabIndex = 37;
            this.txt_dosis_turno.TabStop = false;
            this.txt_dosis_turno.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_dosis_turno_KeyPress);
            this.txt_dosis_turno.Leave += new System.EventHandler(this.txt_dosis_turno_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(340, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(109, 20);
            this.label9.TabIndex = 36;
            this.label9.Text = "HORA INICIO";
            // 
            // cmb_frecuencia_hora
            // 
            this.cmb_frecuencia_hora.FormattingEnabled = true;
            this.cmb_frecuencia_hora.Location = new System.Drawing.Point(303, 57);
            this.cmb_frecuencia_hora.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_frecuencia_hora.Name = "cmb_frecuencia_hora";
            this.cmb_frecuencia_hora.Size = new System.Drawing.Size(180, 28);
            this.cmb_frecuencia_hora.TabIndex = 35;
            this.cmb_frecuencia_hora.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(570, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 20);
            this.label1.TabIndex = 31;
            this.label1.Text = "LISTADO DE COMPUESTOS";
            // 
            // cmbFrecuencia
            // 
            this.cmbFrecuencia.FormattingEnabled = true;
            this.cmbFrecuencia.Location = new System.Drawing.Point(10, 57);
            this.cmbFrecuencia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbFrecuencia.Name = "cmbFrecuencia";
            this.cmbFrecuencia.Size = new System.Drawing.Size(262, 28);
            this.cmbFrecuencia.TabIndex = 5;
            this.cmbFrecuencia.TabStop = false;
            this.cmbFrecuencia.SelectedIndexChanged += new System.EventHandler(this.cmbFrecuencia_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "FRECUENCIA";
            // 
            // cmb_listado_compuestos
            // 
            this.cmb_listado_compuestos.FormattingEnabled = true;
            this.cmb_listado_compuestos.Location = new System.Drawing.Point(514, 57);
            this.cmb_listado_compuestos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmb_listado_compuestos.Name = "cmb_listado_compuestos";
            this.cmb_listado_compuestos.Size = new System.Drawing.Size(318, 28);
            this.cmb_listado_compuestos.TabIndex = 30;
            this.cmb_listado_compuestos.TabStop = false;
            // 
            // lblMedicamento
            // 
            this.lblMedicamento.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.lblMedicamento.Location = new System.Drawing.Point(69, 111);
            this.lblMedicamento.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lblMedicamento.Name = "lblMedicamento";
            this.lblMedicamento.ReadOnly = true;
            this.lblMedicamento.Size = new System.Drawing.Size(763, 26);
            this.lblMedicamento.TabIndex = 18;
            this.lblMedicamento.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(903, 31);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "VÍA DE ADMINSITRACIÓN";
            // 
            // cmbVia
            // 
            this.cmbVia.FormattingEnabled = true;
            this.cmbVia.Location = new System.Drawing.Point(858, 57);
            this.cmbVia.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cmbVia.Name = "cmbVia";
            this.cmbVia.Size = new System.Drawing.Size(276, 28);
            this.cmbVia.TabIndex = 7;
            this.cmbVia.TabStop = false;
            // 
            // btnMedicamento
            // 
            this.btnMedicamento.Location = new System.Drawing.Point(12, 111);
            this.btnMedicamento.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnMedicamento.Name = "btnMedicamento";
            this.btnMedicamento.Size = new System.Drawing.Size(44, 35);
            this.btnMedicamento.TabIndex = 3;
            this.btnMedicamento.TabStop = false;
            this.btnMedicamento.Text = "F1";
            this.btnMedicamento.UseVisualStyleBackColor = true;
            this.btnMedicamento.Click += new System.EventHandler(this.btnMedicamento_Click);
            // 
            // P_MENU
            // 
            this.P_MENU.Controls.Add(this.btnCancelar);
            this.P_MENU.Controls.Add(this.btnGuardar);
            this.P_MENU.Dock = System.Windows.Forms.DockStyle.Top;
            this.P_MENU.Location = new System.Drawing.Point(0, 0);
            this.P_MENU.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.P_MENU.Name = "P_MENU";
            this.P_MENU.Size = new System.Drawing.Size(1396, 75);
            this.P_MENU.TabIndex = 6;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Image = global::His.Formulario.Properties.Resources.HIS_SALIR;
            this.btnCancelar.Location = new System.Drawing.Point(1095, 9);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(63, 57);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.TabStop = false;
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Image = global::His.Formulario.Properties.Resources.HIS_GUARDAR;
            this.btnGuardar.Location = new System.Drawing.Point(9, 9);
            this.btnGuardar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(63, 57);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.TabStop = false;
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // erroresPaciente
            // 
            this.erroresPaciente.ContainerControl = this;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "PRESENTACIÓN";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 400;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "VÍA";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "DOSIS UNITARIA";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "cue_codigo";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 8;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Visible = false;
            this.dataGridViewTextBoxColumn5.Width = 150;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtAplicacion);
            this.groupBox2.Controls.Add(this.txtInfusion);
            this.groupBox2.Location = new System.Drawing.Point(1165, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 245);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 144);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(207, 20);
            this.label10.TabIndex = 160;
            this.label10.Text = "NUMERO DE APLICACIÓN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(215, 20);
            this.label4.TabIndex = 44;
            this.label4.Text = "VELOCIDAD DE INFUCIÓN ";
            // 
            // txtAplicacion
            // 
            this.txtAplicacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtAplicacion.Location = new System.Drawing.Point(7, 169);
            this.txtAplicacion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtAplicacion.Multiline = true;
            this.txtAplicacion.Name = "txtAplicacion";
            this.txtAplicacion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtAplicacion.Size = new System.Drawing.Size(206, 68);
            this.txtAplicacion.TabIndex = 159;
            // 
            // txtInfusion
            // 
            this.txtInfusion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtInfusion.Location = new System.Drawing.Point(7, 57);
            this.txtInfusion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtInfusion.Multiline = true;
            this.txtInfusion.Name = "txtInfusion";
            this.txtInfusion.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtInfusion.Size = new System.Drawing.Size(206, 68);
            this.txtInfusion.TabIndex = 158;
            // 
            // frm_MedicamentoCompuesto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1396, 897);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.P_MENU);
            this.Controls.Add(this.dtgCabeceraKardex);
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frm_MedicamentoCompuesto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Medicamento Compuesto";
            ((System.ComponentModel.ISupportInitialize)(this.dtgCabeceraKardex)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.P_MENU.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.erroresPaciente)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox cmb_medidas;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.TextBox txtCantidadManual;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDosisUnitaria;
        private System.Windows.Forms.DataGridView dtgCabeceraKardex;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox lblMedicamento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbVia;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbFrecuencia;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_listado_compuestos;
        private System.Windows.Forms.Panel P_MENU;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnMedicamento;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_frecuencia_hora;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_reserva;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_dosis_registra;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_dosis_turno;
        private System.Windows.Forms.Button btn_reservas;
        private System.Windows.Forms.ErrorProvider erroresPaciente;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn detalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn via;
        private System.Windows.Forms.DataGridViewTextBoxColumn dosis;
        private System.Windows.Forms.DataGridViewTextBoxColumn cant;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAplicacion;
        private System.Windows.Forms.TextBox txtInfusion;
    }
}