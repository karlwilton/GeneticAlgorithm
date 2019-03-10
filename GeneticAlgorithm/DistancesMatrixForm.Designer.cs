namespace GeneticAlgorithm
{
    partial class DistancesMatrixForm
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
            this.txtMatrix = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMatrix
            // 
            this.txtMatrix.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMatrix.Location = new System.Drawing.Point(0, 0);
            this.txtMatrix.Multiline = true;
            this.txtMatrix.Name = "txtMatrix";
            this.txtMatrix.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtMatrix.Size = new System.Drawing.Size(883, 550);
            this.txtMatrix.TabIndex = 0;
            this.txtMatrix.TextChanged += new System.EventHandler(this.txtMatrix_TextChanged);
            // 
            // DistancesMatrixForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 550);
            this.Controls.Add(this.txtMatrix);
            this.Name = "DistancesMatrixForm";
            this.Text = "DistancesMatrix";
            this.Load += new System.EventHandler(this.DistancesMatrixForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMatrix;
    }
}