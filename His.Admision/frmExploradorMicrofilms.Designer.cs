namespace His.Admision
{
    partial class frmExploradorMicrofilms
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.lvwListaMicrofilms = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblNumAtencion = new System.Windows.Forms.Label();
            this.lbltit1 = new System.Windows.Forms.Label();
            this.lblAtencion = new System.Windows.Forms.Label();
            this.lblPaciente = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.txtDirectorio = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lvwListaMicrofilms);
            this.panel1.Location = new System.Drawing.Point(4, 124);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 269);
            this.panel1.TabIndex = 0;
            // 
            // lvwListaMicrofilms
            // 
            this.lvwListaMicrofilms.AllowDrop = true;
            this.lvwListaMicrofilms.FormattingEnabled = true;
            this.lvwListaMicrofilms.Location = new System.Drawing.Point(3, 3);
            this.lvwListaMicrofilms.Name = "lvwListaMicrofilms";
            this.lvwListaMicrofilms.Size = new System.Drawing.Size(332, 264);
            this.lvwListaMicrofilms.TabIndex = 0;
            this.lvwListaMicrofilms.SelectedIndexChanged += new System.EventHandler(this.lvwListaMicrofilms_SelectedIndexChanged);
            this.lvwListaMicrofilms.DoubleClick += new System.EventHandler(this.lvwListaMicrofilms_DoubleClick);
            this.lvwListaMicrofilms.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvwListaMicrofilms_DragEnter);
            this.lvwListaMicrofilms.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwListaMicrofilms_KeyDown);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblNumAtencion);
            this.panel2.Controls.Add(this.lbltit1);
            this.panel2.Controls.Add(this.lblAtencion);
            this.panel2.Controls.Add(this.lblPaciente);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(338, 53);
            this.panel2.TabIndex = 1;
            // 
            // lblNumAtencion
            // 
            this.lblNumAtencion.AutoSize = true;
            this.lblNumAtencion.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumAtencion.Location = new System.Drawing.Point(12, 29);
            this.lblNumAtencion.Name = "lblNumAtencion";
            this.lblNumAtencion.Size = new System.Drawing.Size(75, 16);
            this.lblNumAtencion.TabIndex = 3;
            this.lblNumAtencion.Text = "N° Atención";
            // 
            // lbltit1
            // 
            this.lbltit1.AutoSize = true;
            this.lbltit1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbltit1.Location = new System.Drawing.Point(12, 6);
            this.lbltit1.Name = "lbltit1";
            this.lbltit1.Size = new System.Drawing.Size(61, 16);
            this.lbltit1.TabIndex = 2;
            this.lbltit1.Text = "Paciente:";
            // 
            // lblAtencion
            // 
            this.lblAtencion.AutoSize = true;
            this.lblAtencion.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAtencion.Location = new System.Drawing.Point(100, 32);
            this.lblAtencion.Name = "lblAtencion";
            this.lblAtencion.Size = new System.Drawing.Size(41, 13);
            this.lblAtencion.TabIndex = 1;
            this.lblAtencion.Text = "label1";
            // 
            // lblPaciente
            // 
            this.lblPaciente.AutoSize = true;
            this.lblPaciente.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaciente.Location = new System.Drawing.Point(100, 9);
            this.lblPaciente.Name = "lblPaciente";
            this.lblPaciente.Size = new System.Drawing.Size(41, 13);
            this.lblPaciente.TabIndex = 0;
            this.lblPaciente.Text = "label1";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(4, 88);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(31, 30);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(41, 88);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(31, 30);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtDirectorio
            // 
            this.txtDirectorio.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDirectorio.Location = new System.Drawing.Point(4, 62);
            this.txtDirectorio.Name = "txtDirectorio";
            this.txtDirectorio.ReadOnly = true;
            this.txtDirectorio.Size = new System.Drawing.Size(255, 22);
            this.txtDirectorio.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(265, 62);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 22);
            this.button1.TabIndex = 7;
            this.button1.Text = "Seleccionar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmExploradorMicrofilms
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 395);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtDirectorio);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "frmExploradorMicrofilms";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmExploradorMicrofilms";
            this.Load += new System.EventHandler(this.frmExploradorMicrofilms_Load);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.frmExploradorMicrofilms_DragEnter);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lvwListaMicrofilms;
        private System.Windows.Forms.Button btnAdd;
        public System.Windows.Forms.Label lblPaciente;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblNumAtencion;
        private System.Windows.Forms.Label lbltit1;
        public System.Windows.Forms.Label lblAtencion;
        private System.Windows.Forms.TextBox txtDirectorio;
        private System.Windows.Forms.Button button1;
    }
}