namespace Library.Control.UserControls
{
    partial class ucWebBrowser
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
            this.wbbWebBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // wbbWebBrowser
            // 
            this.wbbWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbbWebBrowser.Location = new System.Drawing.Point(0, 0);
            this.wbbWebBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbbWebBrowser.Name = "wbbWebBrowser";
            this.wbbWebBrowser.Size = new System.Drawing.Size(529, 370);
            this.wbbWebBrowser.TabIndex = 0;
            // 
            // ucWebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.wbbWebBrowser);
            this.Name = "ucWebBrowser";
            this.Size = new System.Drawing.Size(529, 370);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser wbbWebBrowser;
    }
}
