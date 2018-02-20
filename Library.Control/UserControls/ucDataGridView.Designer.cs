namespace Library.Control.UserControls
{
	partial class ucDataGridView
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
			this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
			this.dgvData = new System.Windows.Forms.DataGridView();
			this.tlsSorts = new System.Windows.Forms.ToolStrip();
			this.cboColumnSorts = new System.Windows.Forms.ToolStripComboBox();
			this.cmdSortDirection = new System.Windows.Forms.ToolStripButton();
			this.sepSort = new System.Windows.Forms.ToolStripSeparator();
			this.txtSize = new System.Windows.Forms.ToolStripTextBox();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.cmdLastPage = new System.Windows.Forms.ToolStripButton();
			this.cmdNextPage = new System.Windows.Forms.ToolStripButton();
			this.lblPageCount = new System.Windows.Forms.ToolStripLabel();
			this.lblSeparator = new System.Windows.Forms.ToolStripLabel();
			this.txtPage = new System.Windows.Forms.ToolStripTextBox();
			this.cmdPreviousPage = new System.Windows.Forms.ToolStripButton();
			this.cmdFirstPage = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
			this.cmdSearch = new System.Windows.Forms.ToolStripButton();
			this.sepSearch = new System.Windows.Forms.ToolStripSeparator();
			this.lblMessage = new System.Windows.Forms.Label();
			this.tlpMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
			this.tlsSorts.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.ColumnCount = 1;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.Controls.Add(this.dgvData, 0, 1);
			this.tlpMain.Controls.Add(this.tlsSorts, 0, 0);
			this.tlpMain.Controls.Add(this.lblMessage, 0, 2);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 3;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpMain.Size = new System.Drawing.Size(1050, 484);
			this.tlpMain.TabIndex = 5;
			// 
			// dgvData
			// 
			this.dgvData.AllowUserToOrderColumns = true;
			this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgvData.Location = new System.Drawing.Point(3, 35);
			this.dgvData.Name = "dgvData";
			this.dgvData.Size = new System.Drawing.Size(1044, 426);
			this.dgvData.TabIndex = 4;
			// 
			// tlsSorts
			// 
			this.tlsSorts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlsSorts.ImageScalingSize = new System.Drawing.Size(29, 29);
			this.tlsSorts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboColumnSorts,
            this.cmdSortDirection,
            this.sepSort,
            this.txtSize,
            this.toolStripSeparator2,
            this.cmdLastPage,
            this.cmdNextPage,
            this.lblPageCount,
            this.lblSeparator,
            this.txtPage,
            this.cmdPreviousPage,
            this.cmdFirstPage,
            this.toolStripSeparator3,
            this.txtSearch,
            this.cmdSearch,
            this.sepSearch});
			this.tlsSorts.Location = new System.Drawing.Point(0, 0);
			this.tlsSorts.Name = "tlsSorts";
			this.tlsSorts.Size = new System.Drawing.Size(1050, 32);
			this.tlsSorts.TabIndex = 0;
			this.tlsSorts.Text = "toolStrip1";
			// 
			// cboColumnSorts
			// 
			this.cboColumnSorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboColumnSorts.DropDownWidth = 75;
			this.cboColumnSorts.Name = "cboColumnSorts";
			this.cboColumnSorts.Size = new System.Drawing.Size(130, 32);
			// 
			// cmdSortDirection
			// 
			this.cmdSortDirection.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdSortDirection.Image = global::Library.Control.Properties.Resources.sort_ascending_icon;
			this.cmdSortDirection.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdSortDirection.Name = "cmdSortDirection";
			this.cmdSortDirection.Size = new System.Drawing.Size(33, 29);
			this.cmdSortDirection.Text = "Ranger ordre croissant";
			this.cmdSortDirection.Click += new System.EventHandler(this.cmdSortDirection_Click);
			// 
			// sepSort
			// 
			this.sepSort.Name = "sepSort";
			this.sepSort.Size = new System.Drawing.Size(6, 32);
			// 
			// txtSize
			// 
			this.txtSize.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.txtSize.AutoSize = false;
			this.txtSize.Name = "txtSize";
			this.txtSize.Size = new System.Drawing.Size(50, 32);
			this.txtSize.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtSize.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSize_KeyDown);
			this.txtSize.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSize_KeyPress);
			this.txtSize.Validated += new System.EventHandler(this.txtSize_Validated);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 32);
			// 
			// cmdLastPage
			// 
			this.cmdLastPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.cmdLastPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdLastPage.Image = global::Library.Control.Properties.Resources.Actions_arrow_right_double_icon;
			this.cmdLastPage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdLastPage.Name = "cmdLastPage";
			this.cmdLastPage.Size = new System.Drawing.Size(33, 29);
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
			this.cmdNextPage.Size = new System.Drawing.Size(33, 29);
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
			this.lblSeparator.Size = new System.Drawing.Size(12, 29);
			this.lblSeparator.Text = "/";
			// 
			// txtPage
			// 
			this.txtPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.txtPage.AutoSize = false;
			this.txtPage.Name = "txtPage";
			this.txtPage.Size = new System.Drawing.Size(50, 32);
			this.txtPage.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtPage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPage_KeyDown);
			this.txtPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPage_KeyPress);
			this.txtPage.Validated += new System.EventHandler(this.txtPage_Validated);
			// 
			// cmdPreviousPage
			// 
			this.cmdPreviousPage.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.cmdPreviousPage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdPreviousPage.Image = global::Library.Control.Properties.Resources.Actions_arrow_left_icon;
			this.cmdPreviousPage.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdPreviousPage.Name = "cmdPreviousPage";
			this.cmdPreviousPage.Size = new System.Drawing.Size(33, 29);
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
			this.cmdFirstPage.Size = new System.Drawing.Size(33, 29);
			this.cmdFirstPage.Text = "Première page";
			this.cmdFirstPage.Click += new System.EventHandler(this.cmdFirstPage_Click);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 32);
			// 
			// txtSearch
			// 
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(200, 32);
			// 
			// cmdSearch
			// 
			this.cmdSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.cmdSearch.Image = global::Library.Control.Properties.Resources.find_icon;
			this.cmdSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.cmdSearch.Name = "cmdSearch";
			this.cmdSearch.Size = new System.Drawing.Size(33, 29);
			this.cmdSearch.Text = "toolStripButton1";
			// 
			// sepSearch
			// 
			this.sepSearch.Name = "sepSearch";
			this.sepSearch.Size = new System.Drawing.Size(6, 32);
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblMessage.Location = new System.Drawing.Point(3, 464);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(1044, 20);
			this.lblMessage.TabIndex = 5;
			this.lblMessage.Text = "label1";
			this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ucDataGridView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tlpMain);
			this.Name = "ucDataGridView";
			this.Size = new System.Drawing.Size(1050, 484);
			this.tlpMain.ResumeLayout(false);
			this.tlpMain.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
			this.tlsSorts.ResumeLayout(false);
			this.tlsSorts.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.TableLayoutPanel tlpMain;
		public System.Windows.Forms.ToolStrip tlsSorts;
		public System.Windows.Forms.ToolStripSeparator sepSort;
		public System.Windows.Forms.ToolStripComboBox cboColumnSorts;
		public System.Windows.Forms.DataGridView dgvData;
		public System.Windows.Forms.ToolStripButton cmdSortDirection;
		public System.Windows.Forms.Label lblMessage;
		public System.Windows.Forms.ToolStripTextBox txtSize;
		public System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		public System.Windows.Forms.ToolStripButton cmdLastPage;
		public System.Windows.Forms.ToolStripButton cmdNextPage;
		public System.Windows.Forms.ToolStripButton cmdPreviousPage;
		public System.Windows.Forms.ToolStripButton cmdFirstPage;
		public System.Windows.Forms.ToolStripTextBox txtPage;
		public System.Windows.Forms.ToolStripLabel lblPageCount;
		public System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		public System.Windows.Forms.ToolStripLabel lblSeparator;
		private System.Windows.Forms.ToolStripTextBox txtSearch;
		private System.Windows.Forms.ToolStripButton cmdSearch;
		private System.Windows.Forms.ToolStripSeparator sepSearch;


	}
}
