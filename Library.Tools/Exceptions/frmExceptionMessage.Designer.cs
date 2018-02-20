namespace Library.Tools.Exceptions
{
    partial class frmExceptionMessage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExceptionMessage));
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.tlpFooter = new System.Windows.Forms.TableLayoutPanel();
            this.cmdOk = new System.Windows.Forms.Button();
            this.chkShowExceptionDetails = new System.Windows.Forms.CheckBox();
            this.txtExceptionDetails = new System.Windows.Forms.TextBox();
            this.tlpMain.SuspendLayout();
            this.tlpFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpMain
            // 
            this.tlpMain.ColumnCount = 3;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tlpMain.Controls.Add(this.lblMessage, 1, 1);
            this.tlpMain.Controls.Add(this.tlpFooter, 0, 2);
            this.tlpMain.Controls.Add(this.txtExceptionDetails, 1, 3);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(0, 0);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 4;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tlpMain.Size = new System.Drawing.Size(281, 417);
            this.tlpMain.TabIndex = 1;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Location = new System.Drawing.Point(23, 19);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(235, 67);
            this.lblMessage.TabIndex = 0;
            this.lblMessage.Text = "label1";
            // 
            // tlpFooter
            // 
            this.tlpFooter.BackColor = System.Drawing.Color.LightGray;
            this.tlpFooter.ColumnCount = 3;
            this.tlpMain.SetColumnSpan(this.tlpFooter, 3);
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlpFooter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpFooter.Controls.Add(this.cmdOk, 1, 0);
            this.tlpFooter.Controls.Add(this.chkShowExceptionDetails, 2, 0);
            this.tlpFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpFooter.Location = new System.Drawing.Point(1, 87);
            this.tlpFooter.Margin = new System.Windows.Forms.Padding(1);
            this.tlpFooter.Name = "tlpFooter";
            this.tlpFooter.RowCount = 1;
            this.tlpFooter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpFooter.Size = new System.Drawing.Size(279, 29);
            this.tlpFooter.TabIndex = 1;
            // 
            // cmdOk
            // 
            this.cmdOk.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmdOk.Location = new System.Drawing.Point(102, 3);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(74, 23);
            this.cmdOk.TabIndex = 0;
            this.cmdOk.Text = "Ok";
            this.cmdOk.UseVisualStyleBackColor = true;
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // chkShowExceptionDetails
            // 
            this.chkShowExceptionDetails.AutoSize = true;
            this.chkShowExceptionDetails.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkShowExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chkShowExceptionDetails.Location = new System.Drawing.Point(182, 3);
            this.chkShowExceptionDetails.Name = "chkShowExceptionDetails";
            this.chkShowExceptionDetails.Size = new System.Drawing.Size(94, 23);
            this.chkShowExceptionDetails.TabIndex = 1;
            this.chkShowExceptionDetails.UseVisualStyleBackColor = true;
            this.chkShowExceptionDetails.CheckedChanged += new System.EventHandler(this.chkShowExceptionDetails_CheckedChanged);
            // 
            // txtExceptionDetails
            // 
            this.txtExceptionDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExceptionDetails.Location = new System.Drawing.Point(23, 120);
            this.txtExceptionDetails.Multiline = true;
            this.txtExceptionDetails.Name = "txtExceptionDetails";
            this.txtExceptionDetails.ReadOnly = true;
            this.txtExceptionDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExceptionDetails.Size = new System.Drawing.Size(235, 294);
            this.txtExceptionDetails.TabIndex = 2;
            // 
            // frmExceptionMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 417);
            this.Controls.Add(this.tlpMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmExceptionMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Erreur";
            this.Load += new System.EventHandler(this.frmExceptionMessage_Load);
            this.tlpMain.ResumeLayout(false);
            this.tlpMain.PerformLayout();
            this.tlpFooter.ResumeLayout(false);
            this.tlpFooter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TableLayoutPanel tlpFooter;
        private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.CheckBox chkShowExceptionDetails;
        private System.Windows.Forms.TextBox txtExceptionDetails;
    }
}