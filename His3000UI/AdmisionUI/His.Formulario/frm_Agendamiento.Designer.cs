namespace His.ConsultaExterna
{
    partial class frm_Agendamiento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_Agendamiento));
            this.ultraGroupBox1 = new Infragistics.Win.Misc.UltraGroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ultraDayView1 = new Infragistics.Win.UltraWinSchedule.UltraDayView();
            this.ultraMonthViewSingle1 = new Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle();
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).BeginInit();
            this.ultraGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ultraDayView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraMonthViewSingle1)).BeginInit();
            this.SuspendLayout();
            // 
            // ultraGroupBox1
            // 
            this.ultraGroupBox1.CaptionAlignment = Infragistics.Win.Misc.GroupBoxCaptionAlignment.Center;
            this.ultraGroupBox1.Controls.Add(this.button2);
            this.ultraGroupBox1.Controls.Add(this.button1);
            this.ultraGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.ultraGroupBox1.HeaderPosition = Infragistics.Win.Misc.GroupBoxHeaderPosition.BottomInsideBorder;
            this.ultraGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.ultraGroupBox1.Name = "ultraGroupBox1";
            this.ultraGroupBox1.Size = new System.Drawing.Size(1167, 110);
            this.ultraGroupBox1.TabIndex = 2;
            this.ultraGroupBox1.Text = "Organizar";
            this.ultraGroupBox1.ViewStyle = Infragistics.Win.Misc.GroupBoxViewStyle.Office2007;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.Location = new System.Drawing.Point(12, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 78);
            this.button1.TabIndex = 0;
            this.button1.Text = "Hoy";
            this.button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.Location = new System.Drawing.Point(93, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(81, 78);
            this.button2.TabIndex = 1;
            this.button2.Text = "Mes";
            this.button2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // ultraDayView1
            // 
            this.ultraDayView1.Location = new System.Drawing.Point(12, 116);
            this.ultraDayView1.Name = "ultraDayView1";
            this.ultraDayView1.Size = new System.Drawing.Size(588, 390);
            this.ultraDayView1.TabIndex = 3;
            this.ultraDayView1.Text = "ultraDayView1";
            // 
            // ultraMonthViewSingle1
            // 
            this.ultraMonthViewSingle1.Location = new System.Drawing.Point(606, 115);
            this.ultraMonthViewSingle1.Name = "ultraMonthViewSingle1";
            this.ultraMonthViewSingle1.Size = new System.Drawing.Size(549, 391);
            this.ultraMonthViewSingle1.TabIndex = 4;
            // 
            // frm_Agendamiento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1167, 518);
            this.Controls.Add(this.ultraMonthViewSingle1);
            this.Controls.Add(this.ultraDayView1);
            this.Controls.Add(this.ultraGroupBox1);
            this.Name = "frm_Agendamiento";
            this.Text = "Calendario Medico";
            ((System.ComponentModel.ISupportInitialize)(this.ultraGroupBox1)).EndInit();
            this.ultraGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ultraDayView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ultraMonthViewSingle1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.Misc.UltraGroupBox ultraGroupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private Infragistics.Win.UltraWinSchedule.UltraDayView ultraDayView1;
        private Infragistics.Win.UltraWinSchedule.UltraMonthViewSingle ultraMonthViewSingle1;
    }
}