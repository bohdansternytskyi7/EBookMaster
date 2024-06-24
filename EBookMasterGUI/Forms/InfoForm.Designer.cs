namespace EBookMasterGUI.Forms
{
	partial class InfoForm
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
			this.TitleText = new DevExpress.XtraEditors.TextEdit();
			this.BookBorrowingGridView = new System.Windows.Forms.DataGridView();
			this.BackBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.TitleText.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.BookBorrowingGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// TitleText
			// 
			this.TitleText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TitleText.Location = new System.Drawing.Point(12, 12);
			this.TitleText.Name = "TitleText";
			this.TitleText.TabStop = false;
			this.TitleText.Properties.Appearance.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.TitleText.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.TitleText.Properties.Appearance.Options.UseBackColor = true;
			this.TitleText.Properties.Appearance.Options.UseFont = true;
			this.TitleText.Properties.ReadOnly = true;
			this.TitleText.Size = new System.Drawing.Size(603, 28);
			this.TitleText.TabIndex = 0;
			// 
			// BookBorrowingGridView
			// 
			this.BookBorrowingGridView.AllowUserToAddRows = false;
			this.BookBorrowingGridView.AllowUserToDeleteRows = false;
			this.BookBorrowingGridView.AllowUserToOrderColumns = true;
			this.BookBorrowingGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BookBorrowingGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.BookBorrowingGridView.BackgroundColor = System.Drawing.Color.Gainsboro;
			this.BookBorrowingGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.BookBorrowingGridView.Location = new System.Drawing.Point(12, 46);
			this.BookBorrowingGridView.MultiSelect = false;
			this.BookBorrowingGridView.Name = "BookBorrowingGridView";
			this.BookBorrowingGridView.ReadOnly = true;
			this.BookBorrowingGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
			this.BookBorrowingGridView.RowTemplate.Height = 24;
			this.BookBorrowingGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.BookBorrowingGridView.Size = new System.Drawing.Size(776, 150);
			this.BookBorrowingGridView.TabIndex = 1;
			// 
			// BackBtn
			// 
			this.BackBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BackBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.BackBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.BackBtn.Location = new System.Drawing.Point(621, 12);
			this.BackBtn.Name = "BackBtn";
			this.BackBtn.Size = new System.Drawing.Size(167, 28);
			this.BackBtn.TabIndex = 2;
			this.BackBtn.Text = "Wróć";
			this.BackBtn.UseVisualStyleBackColor = false;
			this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
			// 
			// InfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.BackBtn);
			this.Controls.Add(this.BookBorrowingGridView);
			this.Controls.Add(this.TitleText);
			this.Name = "InfoForm";
			this.Text = "Szczegóły";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.TitleText.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.BookBorrowingGridView)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.TextEdit TitleText;
		public System.Windows.Forms.DataGridView BookBorrowingGridView;
		private System.Windows.Forms.Button BackBtn;
	}
}