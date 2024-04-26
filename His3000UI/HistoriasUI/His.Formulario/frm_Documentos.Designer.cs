namespace His.Formulario
{
    partial class frm_Documentos
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
            this.ultraGridDocumentExporter1 = new Infragistics.Win.UltraWinGrid.DocumentExport.UltraGridDocumentExporter(this.components);
            this.SuspendLayout();
            // 
            // ultraGridDocumentExporter1
            // 
            this.ultraGridDocumentExporter1.TargetPaperSize = new Infragistics.Documents.Report.PageSize(595F, 842F);
            // 
            // frm_Documentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 390);
            this.Name = "frm_Documentos";
            this.Text = "frm_Documentos";
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.DocumentExport.UltraGridDocumentExporter ultraGridDocumentExporter1;


    }
}