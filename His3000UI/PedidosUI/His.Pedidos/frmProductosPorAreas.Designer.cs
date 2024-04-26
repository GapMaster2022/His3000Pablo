namespace His.Pedidos
{
    partial class frmProductosPorAreas
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            this.brnCerrar = new System.Windows.Forms.Button();
            this.btnQuitarTodos = new System.Windows.Forms.Button();
            this.btnQuitar = new System.Windows.Forms.Button();
            this.frmUsuariosPorAreas_Fill_Panel = new Infragistics.Win.Misc.UltraPanel();
            this.btnAsignarTodos = new System.Windows.Forms.Button();
            this.btnAsignar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.grdDisponibles = new System.Windows.Forms.DataGridView();
            this.grdAsignados = new System.Windows.Forms.DataGridView();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.SuspendLayout();
            this.frmUsuariosPorAreas_Fill_Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDisponibles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAsignados)).BeginInit();
            this.SuspendLayout();
            // 
            // brnCerrar
            // 
            this.brnCerrar.Location = new System.Drawing.Point(433, 395);
            this.brnCerrar.Name = "brnCerrar";
            this.brnCerrar.Size = new System.Drawing.Size(71, 23);
            this.brnCerrar.TabIndex = 10;
            this.brnCerrar.Text = "Cerrar";
            this.brnCerrar.UseVisualStyleBackColor = true;
            // 
            // btnQuitarTodos
            // 
            this.btnQuitarTodos.Location = new System.Drawing.Point(246, 280);
            this.btnQuitarTodos.Name = "btnQuitarTodos";
            this.btnQuitarTodos.Size = new System.Drawing.Size(31, 32);
            this.btnQuitarTodos.TabIndex = 9;
            this.btnQuitarTodos.Text = "<<";
            this.btnQuitarTodos.UseVisualStyleBackColor = true;
            // 
            // btnQuitar
            // 
            this.btnQuitar.Location = new System.Drawing.Point(246, 242);
            this.btnQuitar.Name = "btnQuitar";
            this.btnQuitar.Size = new System.Drawing.Size(31, 32);
            this.btnQuitar.TabIndex = 8;
            this.btnQuitar.Text = "<";
            this.btnQuitar.UseVisualStyleBackColor = true;
            // 
            // frmUsuariosPorAreas_Fill_Panel
            // 
            appearance1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.frmUsuariosPorAreas_Fill_Panel.Appearance = appearance1;
            // 
            // frmUsuariosPorAreas_Fill_Panel.ClientArea
            // 
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.button1);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.txtBuscar);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.brnCerrar);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.btnQuitarTodos);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.btnQuitar);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.btnAsignarTodos);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.btnAsignar);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.label3);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.label2);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.label1);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.comboBox1);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.grdDisponibles);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.Controls.Add(this.grdAsignados);
            this.frmUsuariosPorAreas_Fill_Panel.Cursor = System.Windows.Forms.Cursors.Default;
            this.frmUsuariosPorAreas_Fill_Panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.frmUsuariosPorAreas_Fill_Panel.Location = new System.Drawing.Point(0, 0);
            this.frmUsuariosPorAreas_Fill_Panel.Name = "frmUsuariosPorAreas_Fill_Panel";
            this.frmUsuariosPorAreas_Fill_Panel.Size = new System.Drawing.Size(527, 425);
            this.frmUsuariosPorAreas_Fill_Panel.TabIndex = 1;
            // 
            // btnAsignarTodos
            // 
            this.btnAsignarTodos.Location = new System.Drawing.Point(246, 204);
            this.btnAsignarTodos.Name = "btnAsignarTodos";
            this.btnAsignarTodos.Size = new System.Drawing.Size(31, 32);
            this.btnAsignarTodos.TabIndex = 7;
            this.btnAsignarTodos.Text = ">>";
            this.btnAsignarTodos.UseVisualStyleBackColor = true;
            // 
            // btnAsignar
            // 
            this.btnAsignar.Location = new System.Drawing.Point(246, 166);
            this.btnAsignar.Name = "btnAsignar";
            this.btnAsignar.Size = new System.Drawing.Size(31, 32);
            this.btnAsignar.TabIndex = 6;
            this.btnAsignar.Text = ">";
            this.btnAsignar.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "AREAS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Disponibles";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Asignados";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(234, 35);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(270, 21);
            this.comboBox1.TabIndex = 2;
            // 
            // grdDisponibles
            // 
            this.grdDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdDisponibles.Location = new System.Drawing.Point(31, 141);
            this.grdDisponibles.Name = "grdDisponibles";
            this.grdDisponibles.Size = new System.Drawing.Size(209, 234);
            this.grdDisponibles.TabIndex = 1;
            // 
            // grdAsignados
            // 
            this.grdAsignados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAsignados.Location = new System.Drawing.Point(295, 112);
            this.grdAsignados.Name = "grdAsignados";
            this.grdAsignados.Size = new System.Drawing.Size(209, 263);
            this.grdAsignados.TabIndex = 0;
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(31, 115);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(143, 20);
            this.txtBuscar.TabIndex = 11;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(180, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 20);
            this.button1.TabIndex = 12;
            this.button1.Text = "Buscar";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // frmProductosPorAreas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 425);
            this.Controls.Add(this.frmUsuariosPorAreas_Fill_Panel);
            this.Name = "frmProductosPorAreas";
            this.Text = "Productos por Areas";
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.ResumeLayout(false);
            this.frmUsuariosPorAreas_Fill_Panel.ClientArea.PerformLayout();
            this.frmUsuariosPorAreas_Fill_Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDisponibles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdAsignados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button brnCerrar;
        private System.Windows.Forms.Button btnQuitarTodos;
        private System.Windows.Forms.Button btnQuitar;
        private Infragistics.Win.Misc.UltraPanel frmUsuariosPorAreas_Fill_Panel;
        private System.Windows.Forms.Button btnAsignarTodos;
        private System.Windows.Forms.Button btnAsignar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.DataGridView grdDisponibles;
        private System.Windows.Forms.DataGridView grdAsignados;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtBuscar;
    }
}