namespace His.Admision
{
    partial class frm_Devolucion
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
            this.ultraGroupBox2 = new Infragistics.Win.Misc.UltraGroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtobservacion = new System.Windows.Forms.TextBox();
            this.TablaDevolucion = new System.Windows.Forms.DataGridView();
            this.btn_Cancelar = new System.Windows.Forms.Button();
            this.btn_Aceptar = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.ped = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cod = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.des = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.val = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PDD_CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CueCodigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dev = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).BeginInit();
            this.ultraGroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaDevolucion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox2
            // 
            this.ultraGroupBox2.Controls.Add(this.label1);
            this.ultraGroupBox2.Controls.Add(this.txtobservacion);
            this.ultraGroupBox2.Controls.Add(this.TablaDevolucion);
            this.ultraGroupBox2.Controls.Add(this.btn_Cancelar);
            this.ultraGroupBox2.Controls.Add(this.btn_Aceptar);
            this.ultraGroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ultraGroupBox2.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox2.Name = "ultraGroupBox2";
            this.ultraGroupBox2.Size = new System.Drawing.Size(840, 286);
            this.ultraGroupBox2.TabIndex = 90;
            this.ultraGroupBox2.Text = "Devolucion de Productos";
            this.ultraGroupBox2.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(12, 204);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 98;
            this.label1.Text = "Observación: ";
            // 
            // txtobservacion
            // 
            this.txtobservacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtobservacion.Location = new System.Drawing.Point(91, 201);
            this.txtobservacion.Multiline = true;
            this.txtobservacion.Name = "txtobservacion";
            this.txtobservacion.Size = new System.Drawing.Size(735, 44);
            this.txtobservacion.TabIndex = 97;
            // 
            // TablaDevolucion
            // 
            this.TablaDevolucion.AllowUserToAddRows = false;
            this.TablaDevolucion.AllowUserToDeleteRows = false;
            this.TablaDevolucion.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.TablaDevolucion.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.TablaDevolucion.BackgroundColor = System.Drawing.Color.White;
            this.TablaDevolucion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TablaDevolucion.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.TablaDevolucion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TablaDevolucion.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ped,
            this.cod,
            this.des,
            this.cant,
            this.val,
            this.iva,
            this.tot,
            this.PDD_CODIGO,
            this.CueCodigo,
            this.dev});
            this.TablaDevolucion.EnableHeadersVisualStyles = false;
            this.TablaDevolucion.Location = new System.Drawing.Point(6, 19);
            this.TablaDevolucion.Name = "TablaDevolucion";
            this.TablaDevolucion.RowHeadersVisible = false;
            this.TablaDevolucion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TablaDevolucion.Size = new System.Drawing.Size(821, 176);
            this.TablaDevolucion.TabIndex = 96;
            this.TablaDevolucion.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.TablaDevolucion_CellBeginEdit);
            this.TablaDevolucion.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.TablaDevolucion_CellEndEdit);
            this.TablaDevolucion.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.TablaDevolucion_CellValidating);
            // 
            // btn_Cancelar
            // 
            this.btn_Cancelar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Cancelar.Location = new System.Drawing.Point(765, 251);
            this.btn_Cancelar.Name = "btn_Cancelar";
            this.btn_Cancelar.Size = new System.Drawing.Size(61, 23);
            this.btn_Cancelar.TabIndex = 94;
            this.btn_Cancelar.Text = "Cancelar";
            this.btn_Cancelar.UseVisualStyleBackColor = false;
            this.btn_Cancelar.Click += new System.EventHandler(this.btn_Cancelar_Click);
            // 
            // btn_Aceptar
            // 
            this.btn_Aceptar.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_Aceptar.Location = new System.Drawing.Point(694, 251);
            this.btn_Aceptar.Name = "btn_Aceptar";
            this.btn_Aceptar.Size = new System.Drawing.Size(61, 23);
            this.btn_Aceptar.TabIndex = 93;
            this.btn_Aceptar.Text = "Aceptar";
            this.btn_Aceptar.UseVisualStyleBackColor = false;
            this.btn_Aceptar.Click += new System.EventHandler(this.btn_Aceptar_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ped
            // 
            this.ped.HeaderText = "Pedido";
            this.ped.Name = "ped";
            this.ped.ReadOnly = true;
            this.ped.Width = 64;
            // 
            // cod
            // 
            this.cod.HeaderText = "Codigo";
            this.cod.Name = "cod";
            this.cod.ReadOnly = true;
            this.cod.Width = 64;
            // 
            // des
            // 
            this.des.HeaderText = "Descripcion";
            this.des.Name = "des";
            this.des.ReadOnly = true;
            this.des.Width = 87;
            // 
            // cant
            // 
            this.cant.HeaderText = "Cantidad";
            this.cant.Name = "cant";
            this.cant.Width = 73;
            // 
            // val
            // 
            this.val.HeaderText = "Valor";
            this.val.Name = "val";
            this.val.ReadOnly = true;
            this.val.Width = 55;
            // 
            // iva
            // 
            this.iva.HeaderText = "Iva";
            this.iva.Name = "iva";
            this.iva.ReadOnly = true;
            this.iva.Width = 46;
            // 
            // tot
            // 
            this.tot.HeaderText = "Total";
            this.tot.Name = "tot";
            this.tot.ReadOnly = true;
            this.tot.Width = 55;
            // 
            // PDD_CODIGO
            // 
            this.PDD_CODIGO.HeaderText = "pdd";
            this.PDD_CODIGO.Name = "PDD_CODIGO";
            this.PDD_CODIGO.Visible = false;
            this.PDD_CODIGO.Width = 49;
            // 
            // CueCodigo
            // 
            this.CueCodigo.HeaderText = "Cue";
            this.CueCodigo.Name = "CueCodigo";
            this.CueCodigo.Width = 50;
            // 
            // dev
            // 
            this.dev.HeaderText = "Devolucion";
            this.dev.Name = "dev";
            this.dev.Width = 66;
            // 
            // frm_Devolucion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 286);
            this.Controls.Add(this.ultraGroupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frm_Devolucion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Producto Devolucion";
            this.Load += new System.EventHandler(this.frm_Devolucion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox2)).EndInit();
            this.ultraGroupBox2.ResumeLayout(false);
            this.ultraGroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TablaDevolucion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox2;
        private System.Windows.Forms.Button btn_Cancelar;
        private System.Windows.Forms.Button btn_Aceptar;
        private System.Windows.Forms.DataGridView TablaDevolucion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtobservacion;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ped;
        private System.Windows.Forms.DataGridViewTextBoxColumn cod;
        private System.Windows.Forms.DataGridViewTextBoxColumn des;
        private System.Windows.Forms.DataGridViewTextBoxColumn cant;
        private System.Windows.Forms.DataGridViewTextBoxColumn val;
        private System.Windows.Forms.DataGridViewTextBoxColumn iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn tot;
        private System.Windows.Forms.DataGridViewTextBoxColumn PDD_CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CueCodigo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dev;
    }
}