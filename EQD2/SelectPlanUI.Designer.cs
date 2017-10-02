namespace EQD2
{
    partial class SelectPlanUI
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
            this.textBoxPatId = new System.Windows.Forms.TextBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.listBoxCourse = new System.Windows.Forms.ListBox();
            this.listBoxPlan = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxPatId
            // 
            this.textBoxPatId.Location = new System.Drawing.Point(85, 29);
            this.textBoxPatId.Name = "textBoxPatId";
            this.textBoxPatId.Size = new System.Drawing.Size(245, 20);
            this.textBoxPatId.TabIndex = 0;
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(368, 28);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(136, 21);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "Open";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(533, 28);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(136, 21);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // listBoxCourse
            // 
            this.listBoxCourse.FormattingEnabled = true;
            this.listBoxCourse.Location = new System.Drawing.Point(368, 72);
            this.listBoxCourse.Name = "listBoxCourse";
            this.listBoxCourse.Size = new System.Drawing.Size(136, 173);
            this.listBoxCourse.TabIndex = 4;
            this.listBoxCourse.SelectedIndexChanged += new System.EventHandler(this.listBoxCourse_SelectedIndexChanged);
            // 
            // listBoxPlan
            // 
            this.listBoxPlan.FormattingEnabled = true;
            this.listBoxPlan.Location = new System.Drawing.Point(533, 72);
            this.listBoxPlan.Name = "listBoxPlan";
            this.listBoxPlan.Size = new System.Drawing.Size(277, 173);
            this.listBoxPlan.TabIndex = 5;
            this.listBoxPlan.SelectedIndexChanged += new System.EventHandler(this.listBoxPlan_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "ID:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Select:";
            // 
            // textBoxPat
            // 
            this.textBoxPat.Location = new System.Drawing.Point(85, 72);
            this.textBoxPat.Multiline = true;
            this.textBoxPat.Name = "textBoxPat";
            this.textBoxPat.ReadOnly = true;
            this.textBoxPat.Size = new System.Drawing.Size(245, 173);
            this.textBoxPat.TabIndex = 8;
            // 
            // SelectPlanUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(860, 286);
            this.Controls.Add(this.textBoxPat);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBoxPlan);
            this.Controls.Add(this.listBoxCourse);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.textBoxPatId);
            this.Name = "SelectPlanUI";
            this.Text = "SelectPlanUI";
            this.Load += new System.EventHandler(this.SelectPlanUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPatId;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.ListBox listBoxCourse;
        private System.Windows.Forms.ListBox listBoxPlan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPat;
    }
}