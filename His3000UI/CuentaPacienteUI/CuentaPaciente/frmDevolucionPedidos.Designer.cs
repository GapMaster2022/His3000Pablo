namespace CuentaPaciente
{
    partial class frmDevolucionPedidos
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
            this.ProductosSolicitados = new System.Windows.Forms.DataGridView();
            this.PDD_CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PED_CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRO_CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRO_DESCRIPCION = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_CANTIDAD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadDevuelta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_VALOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_IVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_ESTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_COSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_FACTURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_ESTADO_FACTURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_FECHA_FACTURA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_RESULTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRO_CODIGO_BARRAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.DevCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRO_CODIGO_DEV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRO_DESCRIPCION_DEV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevDetCantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevDetValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevDetIva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DevDetIvaTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_CODIGO_DEV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProductosSolicitados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductosSolicitados
            // 
            this.ProductosSolicitados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProductosSolicitados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PDD_CODIGO,
            this.PED_CODIGO,
            this.PRO_CODIGO,
            this.PRO_DESCRIPCION,
            this.PDD_CANTIDAD,
            this.CantidadDevuelta,
            this.PDD_VALOR,
            this.PDD_IVA,
            this.PDD_TOTAL,
            this.PDD_ESTADO,
            this.PDD_COSTO,
            this.PDD_FACTURA,
            this.PDD_ESTADO_FACTURA,
            this.PDD_FECHA_FACTURA,
            this.PDD_RESULTADO,
            this.PRO_CODIGO_BARRAS});
            this.ProductosSolicitados.Location = new System.Drawing.Point(13, 28);
            this.ProductosSolicitados.Name = "ProductosSolicitados";
            this.ProductosSolicitados.Size = new System.Drawing.Size(945, 222);
            this.ProductosSolicitados.TabIndex = 0;
            this.ProductosSolicitados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ProductosSolicitados_KeyDown);
            // 
            // PDD_CODIGO
            // 
            this.PDD_CODIGO.HeaderText = "ID";
            this.PDD_CODIGO.Name = "PDD_CODIGO";
            this.PDD_CODIGO.Visible = false;
            // 
            // PED_CODIGO
            // 
            this.PED_CODIGO.HeaderText = "PEDIDO";
            this.PED_CODIGO.Name = "PED_CODIGO";
            this.PED_CODIGO.Width = 75;
            // 
            // PRO_CODIGO
            // 
            this.PRO_CODIGO.HeaderText = "PRODUCTO";
            this.PRO_CODIGO.Name = "PRO_CODIGO";
            this.PRO_CODIGO.Width = 75;
            // 
            // PRO_DESCRIPCION
            // 
            this.PRO_DESCRIPCION.HeaderText = "DESCRIPCION";
            this.PRO_DESCRIPCION.Name = "PRO_DESCRIPCION";
            this.PRO_DESCRIPCION.Width = 250;
            // 
            // PDD_CANTIDAD
            // 
            this.PDD_CANTIDAD.HeaderText = "CANTIDAD";
            this.PDD_CANTIDAD.Name = "PDD_CANTIDAD";
            // 
            // CantidadDevuelta
            // 
            this.CantidadDevuelta.HeaderText = "CANTIDAD DEV";
            this.CantidadDevuelta.Name = "CantidadDevuelta";
            // 
            // PDD_VALOR
            // 
            this.PDD_VALOR.HeaderText = "VALOR";
            this.PDD_VALOR.Name = "PDD_VALOR";
            // 
            // PDD_IVA
            // 
            this.PDD_IVA.HeaderText = "IVA";
            this.PDD_IVA.Name = "PDD_IVA";
            // 
            // PDD_TOTAL
            // 
            this.PDD_TOTAL.HeaderText = "TOTAL";
            this.PDD_TOTAL.Name = "PDD_TOTAL";
            // 
            // PDD_ESTADO
            // 
            this.PDD_ESTADO.HeaderText = "PDD_ESTADO";
            this.PDD_ESTADO.Name = "PDD_ESTADO";
            this.PDD_ESTADO.Visible = false;
            // 
            // PDD_COSTO
            // 
            this.PDD_COSTO.HeaderText = "PDD_COSTO";
            this.PDD_COSTO.Name = "PDD_COSTO";
            this.PDD_COSTO.Visible = false;
            // 
            // PDD_FACTURA
            // 
            this.PDD_FACTURA.HeaderText = "PDD_FACTURA";
            this.PDD_FACTURA.Name = "PDD_FACTURA";
            this.PDD_FACTURA.Visible = false;
            // 
            // PDD_ESTADO_FACTURA
            // 
            this.PDD_ESTADO_FACTURA.HeaderText = "PDD_ESTADO_FACTURA";
            this.PDD_ESTADO_FACTURA.Name = "PDD_ESTADO_FACTURA";
            this.PDD_ESTADO_FACTURA.Visible = false;
            // 
            // PDD_FECHA_FACTURA
            // 
            this.PDD_FECHA_FACTURA.HeaderText = "PDD_FECHA_FACTURA";
            this.PDD_FECHA_FACTURA.Name = "PDD_FECHA_FACTURA";
            this.PDD_FECHA_FACTURA.Visible = false;
            // 
            // PDD_RESULTADO
            // 
            this.PDD_RESULTADO.HeaderText = "PDD_RESULTADO";
            this.PDD_RESULTADO.Name = "PDD_RESULTADO";
            this.PDD_RESULTADO.Visible = false;
            // 
            // PRO_CODIGO_BARRAS
            // 
            this.PRO_CODIGO_BARRAS.HeaderText = "PRO_CODIGO_BARRAS";
            this.PRO_CODIGO_BARRAS.Name = "PRO_CODIGO_BARRAS";
            this.PRO_CODIGO_BARRAS.Visible = false;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DevCodigo,
            this.PRO_CODIGO_DEV,
            this.PRO_DESCRIPCION_DEV,
            this.DevDetCantidad,
            this.DevDetValor,
            this.DevDetIva,
            this.DevDetIvaTotal,
            this.PDD_CODIGO_DEV});
            this.dataGridView2.Location = new System.Drawing.Point(12, 314);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(945, 211);
            this.dataGridView2.TabIndex = 1;
            // 
            // DevCodigo
            // 
            this.DevCodigo.HeaderText = "CODIGO";
            this.DevCodigo.Name = "DevCodigo";
            this.DevCodigo.Visible = false;
            // 
            // PRO_CODIGO_DEV
            // 
            this.PRO_CODIGO_DEV.HeaderText = "CODIGO P.";
            this.PRO_CODIGO_DEV.Name = "PRO_CODIGO_DEV";
            // 
            // PRO_DESCRIPCION_DEV
            // 
            this.PRO_DESCRIPCION_DEV.HeaderText = "DESCRIPCION";
            this.PRO_DESCRIPCION_DEV.Name = "PRO_DESCRIPCION_DEV";
            this.PRO_DESCRIPCION_DEV.Width = 400;
            // 
            // DevDetCantidad
            // 
            this.DevDetCantidad.HeaderText = "CANTIDAD";
            this.DevDetCantidad.Name = "DevDetCantidad";
            // 
            // DevDetValor
            // 
            this.DevDetValor.HeaderText = "VALOR";
            this.DevDetValor.Name = "DevDetValor";
            // 
            // DevDetIva
            // 
            this.DevDetIva.HeaderText = "IVA";
            this.DevDetIva.Name = "DevDetIva";
            // 
            // DevDetIvaTotal
            // 
            this.DevDetIvaTotal.HeaderText = "TOTAL";
            this.DevDetIvaTotal.Name = "DevDetIvaTotal";
            // 
            // PDD_CODIGO_DEV
            // 
            this.PDD_CODIGO_DEV.HeaderText = "NDETALLE";
            this.PDD_CODIGO_DEV.Name = "PDD_CODIGO_DEV";
            this.PDD_CODIGO_DEV.Visible = false;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(720, 533);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(116, 26);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(842, 533);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 26);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(842, 271);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(116, 26);
            this.btnAgregar.TabIndex = 4;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(676, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Cantidad :";
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(737, 275);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.Size = new System.Drawing.Size(85, 20);
            this.txtCantidad.TabIndex = 6;
            this.txtCantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCantidad_KeyDown);
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, -1);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(987, 572);
            this.ultraGroupBox2.TabIndex = 102;
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // frmDevolucionPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 571);
            this.ControlBox = false;
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.ProductosSolicitados);
            this.Controls.Add(this.ultraGroupBox2);
            this.Name = "frmDevolucionPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Devolución de Pedidos";
            this.Load += new System.EventHandler(this.frmDevolucionPedidos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProductosSolicitados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ProductosSolicitados;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PED_CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRO_CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRO_DESCRIPCION;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_CANTIDAD;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadDevuelta;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_VALOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_IVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_TOTAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_ESTADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_COSTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_FACTURA;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_ESTADO_FACTURA;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_FECHA_FACTURA;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_RESULTADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRO_CODIGO_BARRAS;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevCodigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRO_CODIGO_DEV;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRO_DESCRIPCION_DEV;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevDetCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevDetValor;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevDetIva;
        private System.Windows.Forms.DataGridViewTextBoxColumn DevDetIvaTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_CODIGO_DEV;
        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
    }
}