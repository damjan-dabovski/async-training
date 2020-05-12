namespace _10DemoUI
{
    partial class Form1
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
            this.getDataBtn = new System.Windows.Forms.Button();
            this.contentTxb = new System.Windows.Forms.RichTextBox();
            this.logTbx = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // getDataBtn
            // 
            this.getDataBtn.Location = new System.Drawing.Point(13, 13);
            this.getDataBtn.Name = "getDataBtn";
            this.getDataBtn.Size = new System.Drawing.Size(163, 23);
            this.getDataBtn.TabIndex = 0;
            this.getDataBtn.Text = "Get Data";
            this.getDataBtn.UseVisualStyleBackColor = true;
            this.getDataBtn.Click += new System.EventHandler(this.GetDataBtn_Click);
            // 
            // contentTxb
            // 
            this.contentTxb.Location = new System.Drawing.Point(13, 42);
            this.contentTxb.Name = "contentTxb";
            this.contentTxb.Size = new System.Drawing.Size(437, 384);
            this.contentTxb.TabIndex = 1;
            this.contentTxb.Text = "";
            // 
            // logTbx
            // 
            this.logTbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logTbx.Location = new System.Drawing.Point(456, 42);
            this.logTbx.Name = "logTbx";
            this.logTbx.Size = new System.Drawing.Size(254, 384);
            this.logTbx.TabIndex = 2;
            this.logTbx.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(675, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Logs";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(722, 434);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.logTbx);
            this.Controls.Add(this.contentTxb);
            this.Controls.Add(this.getDataBtn);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getDataBtn;
        private System.Windows.Forms.RichTextBox contentTxb;
        private System.Windows.Forms.RichTextBox logTbx;
        private System.Windows.Forms.Label label1;
    }
}

