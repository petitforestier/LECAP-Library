namespace Library.Control.UserControls
{
	public partial class ucPictures
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
			this.picImage = new System.Windows.Forms.PictureBox();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.cmdZoom = new System.Windows.Forms.Button();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.cmdNext = new System.Windows.Forms.Button();
			this.cmdPrevious = new System.Windows.Forms.Button();
			this.lblPagination = new System.Windows.Forms.Label();
			this.tlpMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
			this.tableLayoutPanel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tlpMain
			// 
			this.tlpMain.ColumnCount = 1;
			this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.Controls.Add(this.picImage, 0, 0);
			this.tlpMain.Controls.Add(this.tableLayoutPanel2, 0, 1);
			this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpMain.Location = new System.Drawing.Point(0, 0);
			this.tlpMain.Name = "tlpMain";
			this.tlpMain.RowCount = 2;
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
			this.tlpMain.Size = new System.Drawing.Size(526, 422);
			this.tlpMain.TabIndex = 1;
			// 
			// picImage
			// 
			this.picImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.picImage.Location = new System.Drawing.Point(3, 3);
			this.picImage.Name = "picImage";
			this.picImage.Size = new System.Drawing.Size(520, 360);
			this.picImage.TabIndex = 1;
			this.picImage.TabStop = false;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.cmdZoom, 2, 0);
			this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 369);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(520, 50);
			this.tableLayoutPanel2.TabIndex = 2;
			// 
			// cmdZoom
			// 
			this.cmdZoom.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmdZoom.Image = global::Library.Control.Properties.Resources.find_icon;
			this.cmdZoom.Location = new System.Drawing.Point(413, 5);
			this.cmdZoom.Name = "cmdZoom";
			this.cmdZoom.Size = new System.Drawing.Size(86, 39);
			this.cmdZoom.TabIndex = 4;
			this.cmdZoom.Text = "Zoom";
			this.cmdZoom.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdZoom.UseVisualStyleBackColor = true;
			this.cmdZoom.Click += new System.EventHandler(this.cmdZoom_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 49F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.cmdNext, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.cmdPrevious, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.lblPagination, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(113, 3);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(294, 44);
			this.tableLayoutPanel1.TabIndex = 5;
			// 
			// cmdNext
			// 
			this.cmdNext.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.cmdNext.Image = global::Library.Control.Properties.Resources.next_icon;
			this.cmdNext.Location = new System.Drawing.Point(174, 3);
			this.cmdNext.Name = "cmdNext";
			this.cmdNext.Size = new System.Drawing.Size(100, 38);
			this.cmdNext.TabIndex = 1;
			this.cmdNext.Text = "Suivante";
			this.cmdNext.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
			this.cmdNext.UseVisualStyleBackColor = true;
			this.cmdNext.Click += new System.EventHandler(this.cmdNext_Click);
			// 
			// cmdPrevious
			// 
			this.cmdPrevious.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.cmdPrevious.Image = global::Library.Control.Properties.Resources.back_icon;
			this.cmdPrevious.Location = new System.Drawing.Point(14, 3);
			this.cmdPrevious.Name = "cmdPrevious";
			this.cmdPrevious.Size = new System.Drawing.Size(105, 38);
			this.cmdPrevious.TabIndex = 0;
			this.cmdPrevious.Text = "Précédente";
			this.cmdPrevious.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.cmdPrevious.UseVisualStyleBackColor = true;
			this.cmdPrevious.Click += new System.EventHandler(this.cmdPrevious_Click);
			// 
			// lblPagination
			// 
			this.lblPagination.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.lblPagination.AutoSize = true;
			this.lblPagination.Location = new System.Drawing.Point(125, 15);
			this.lblPagination.Name = "lblPagination";
			this.lblPagination.Size = new System.Drawing.Size(43, 13);
			this.lblPagination.TabIndex = 2;
			this.lblPagination.Text = "label1";
			this.lblPagination.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// ucPictures
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tlpMain);
			this.Name = "ucPictures";
			this.Size = new System.Drawing.Size(526, 422);
			this.tlpMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tlpMain;
		private System.Windows.Forms.PictureBox picImage;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button cmdZoom;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button cmdNext;
		private System.Windows.Forms.Button cmdPrevious;
		private System.Windows.Forms.Label lblPagination;
	}
}
