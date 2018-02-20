namespace Library.Control.UserControls
{
    partial class ucNavigator
	{
		/// <summary> 
		/// Variable nécessaire au concepteur.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Nettoyage des ressources utilisées.
		/// </summary>
		/// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Code généré par le Concepteur de composants

		/// <summary> 
		/// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
		/// le contenu de cette méthode avec l'éditeur de code.
		/// </summary>
		private void InitializeComponent()
		{
            this.tlsSorts = new System.Windows.Forms.ToolStrip();
            this.txtSize = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cmdLastPage = new System.Windows.Forms.ToolStripButton();
            this.cmdNextPage = new System.Windows.Forms.ToolStripButton();
            this.lblPageCount = new System.Windows.Forms.ToolStripLabel();
            this.lblSeparator = new System.Windows.Forms.ToolStripLabel();
            this.txtPage = new System.Windows.Forms.ToolStripTextBox();
            this.cmdPreviousPage = new System.Windows.Forms.ToolStripButton();
            this.cmdFirstPage = new System.Windows.Forms.ToolStripButton();
            this.tlsSorts.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlsSorts
            // 
            this.tlsSorts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlsSorts.ImageScalingSize = new System.Drawing.Size(29, 29);
            this.tlsSorts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtSize,
            this.toolStripSeparator2,
            this.cmdLastPage,
            this.cmdNextPage,
            this.lblPageCount,
            this.lblSeparator,
            this.txtPage,
            this.cmdPreviousPage,
            this.cmdFirstPage});
            this.tlsSorts.Location = new System.Drawing.Point(0, 0);
            this.tlsSorts.Name = "tlsSorts";
            this.tlsSorts.Size = new System.Drawing.Size(421, 46);
            this.tlsSorts.TabIndex = 0;
            this.tlsSorts.Text = "toolStrip1";
            // 
            // txtSize
            // 
            this.txtSize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtSize.AutoSize = false;
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(50, 32);
            this.txtSize.Text = "100";
            this.txtSize.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSize.Enter += new System.EventHandler(this.txtSize_Enter);
            this.txtSize.Leave += new System.EventHandler(this.txtSize_Leave);
            this.txtSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSize_KeyPress);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 46);
            // 
            // cmdLastPage
            // 
            this.cmdLastPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmdLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdLastPage.Image = global::Library.Control.Properties.Resources.Actions_arrow_right_double_icon;
            this.cmdLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdLastPage.Name = "cmdLastPage";
            this.cmdLastPage.Size = new System.Drawing.Size(33, 43);
            this.cmdLastPage.Text = "Dernière page";
            this.cmdLastPage.Click += new System.EventHandler(this.cmdLastPage_Click);
            // 
            // cmdNextPage
            // 
            this.cmdNextPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmdNextPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdNextPage.Image = global::Library.Control.Properties.Resources.Actions_arrow_right_icon;
            this.cmdNextPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdNextPage.Name = "cmdNextPage";
            this.cmdNextPage.Size = new System.Drawing.Size(33, 43);
            this.cmdNextPage.Text = "Page suivante";
            this.cmdNextPage.Click += new System.EventHandler(this.cmdNextPage_Click);
            // 
            // lblPageCount
            // 
            this.lblPageCount.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblPageCount.AutoSize = false;
            this.lblPageCount.Name = "lblPageCount";
            this.lblPageCount.Size = new System.Drawing.Size(40, 29);
            this.lblPageCount.Text = "1";
            this.lblPageCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSeparator
            // 
            this.lblSeparator.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(12, 43);
            this.lblSeparator.Text = "/";
            // 
            // txtPage
            // 
            this.txtPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.txtPage.AutoSize = false;
            this.txtPage.Name = "txtPage";
            this.txtPage.Size = new System.Drawing.Size(50, 32);
            this.txtPage.Text = "1";
            this.txtPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPage.Enter += new System.EventHandler(this.txtPage_Enter);
            this.txtPage.Leave += new System.EventHandler(this.txtPage_Leave);
            this.txtPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPage_KeyPress);
            // 
            // cmdPreviousPage
            // 
            this.cmdPreviousPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmdPreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdPreviousPage.Image = global::Library.Control.Properties.Resources.Actions_arrow_left_icon;
            this.cmdPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdPreviousPage.Name = "cmdPreviousPage";
            this.cmdPreviousPage.Size = new System.Drawing.Size(33, 43);
            this.cmdPreviousPage.Text = "Page précédente";
            this.cmdPreviousPage.Click += new System.EventHandler(this.cmdPreviousPage_Click);
            // 
            // cmdFirstPage
            // 
            this.cmdFirstPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmdFirstPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cmdFirstPage.Image = global::Library.Control.Properties.Resources.Actions_arrow_left_double_icon;
            this.cmdFirstPage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cmdFirstPage.Name = "cmdFirstPage";
            this.cmdFirstPage.Size = new System.Drawing.Size(33, 43);
            this.cmdFirstPage.Text = "Première page";
            this.cmdFirstPage.Click += new System.EventHandler(this.cmdFirstPage_Click);
            // 
            // ucNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tlsSorts);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ucNavigator";
            this.Size = new System.Drawing.Size(421, 46);
            this.tlsSorts.ResumeLayout(false);
            this.tlsSorts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        public System.Windows.Forms.ToolStrip tlsSorts;
		public System.Windows.Forms.ToolStripTextBox txtSize;
		public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		public System.Windows.Forms.ToolStripButton cmdLastPage;
		public System.Windows.Forms.ToolStripButton cmdNextPage;
		public System.Windows.Forms.ToolStripButton cmdPreviousPage;
		public System.Windows.Forms.ToolStripButton cmdFirstPage;
		public System.Windows.Forms.ToolStripTextBox txtPage;
        public System.Windows.Forms.ToolStripLabel lblPageCount;
        public System.Windows.Forms.ToolStripLabel lblSeparator;


	}
}
