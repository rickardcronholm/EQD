namespace EQD2
{
    partial class EQD2Control
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
            this.textBoxPat = new System.Windows.Forms.TextBox();
            this.textBoxPlan = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBoxFractionation = new System.Windows.Forms.TextBox();
            this.buttonGenEQD2 = new System.Windows.Forms.Button();
            this.textBoxOut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxPat
            // 
            this.textBoxPat.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPat.Location = new System.Drawing.Point(8, 13);
            this.textBoxPat.Name = "textBoxPat";
            this.textBoxPat.ReadOnly = true;
            this.textBoxPat.Size = new System.Drawing.Size(509, 20);
            this.textBoxPat.TabIndex = 0;
            // 
            // textBoxPlan
            // 
            this.textBoxPlan.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPlan.Location = new System.Drawing.Point(8, 48);
            this.textBoxPlan.Name = "textBoxPlan";
            this.textBoxPlan.ReadOnly = true;
            this.textBoxPlan.Size = new System.Drawing.Size(338, 20);
            this.textBoxPlan.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter α/β (0, 10]:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(117, 80);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(53, 20);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // textBoxFractionation
            // 
            this.textBoxFractionation.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxFractionation.Location = new System.Drawing.Point(352, 48);
            this.textBoxFractionation.Name = "textBoxFractionation";
            this.textBoxFractionation.ReadOnly = true;
            this.textBoxFractionation.Size = new System.Drawing.Size(165, 20);
            this.textBoxFractionation.TabIndex = 4;
            // 
            // buttonGenEQD2
            // 
            this.buttonGenEQD2.Location = new System.Drawing.Point(8, 106);
            this.buttonGenEQD2.Name = "buttonGenEQD2";
            this.buttonGenEQD2.Size = new System.Drawing.Size(127, 23);
            this.buttonGenEQD2.TabIndex = 5;
            this.buttonGenEQD2.Text = "Generate EQD2";
            this.buttonGenEQD2.UseVisualStyleBackColor = true;
            this.buttonGenEQD2.Click += new System.EventHandler(this.buttonGenEQD2_Click);
            // 
            // textBoxOut
            // 
            this.textBoxOut.Location = new System.Drawing.Point(8, 135);
            this.textBoxOut.Multiline = true;
            this.textBoxOut.Name = "textBoxOut";
            this.textBoxOut.ReadOnly = true;
            this.textBoxOut.Size = new System.Drawing.Size(509, 120);
            this.textBoxOut.TabIndex = 6;
            // 
            // EQD2Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 262);
            this.Controls.Add(this.textBoxOut);
            this.Controls.Add(this.buttonGenEQD2);
            this.Controls.Add(this.textBoxFractionation);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxPlan);
            this.Controls.Add(this.textBoxPat);
            this.Name = "EQD2Control";
            this.Text = "EQD2Control";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPat;
        private System.Windows.Forms.TextBox textBoxPlan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBoxFractionation;
        private System.Windows.Forms.Button buttonGenEQD2;
        private System.Windows.Forms.TextBox textBoxOut;
    }
}