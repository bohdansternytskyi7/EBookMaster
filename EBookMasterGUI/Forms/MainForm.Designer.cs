namespace EBookMasterGUI.Forms
{
	partial class MainForm
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
			this.BooksDataGrid = new System.Windows.Forms.DataGridView();
			this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PublishingHouse = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PublicationYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Series = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Catogories = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.BooksDataGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// BooksDataGrid
			// 
			this.BooksDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.BooksDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Author,
            this.PublishingHouse,
            this.PublicationYear,
            this.Series,
            this.Catogories});
			this.BooksDataGrid.Location = new System.Drawing.Point(182, 12);
			this.BooksDataGrid.Name = "BooksDataGrid";
			this.BooksDataGrid.RowHeadersWidth = 51;
			this.BooksDataGrid.RowTemplate.Height = 24;
			this.BooksDataGrid.Size = new System.Drawing.Size(804, 506);
			this.BooksDataGrid.TabIndex = 0;
			// 
			// Title
			// 
			this.Title.HeaderText = "Title";
			this.Title.MinimumWidth = 6;
			this.Title.Name = "Title";
			this.Title.Width = 125;
			// 
			// Author
			// 
			this.Author.HeaderText = "Author";
			this.Author.MinimumWidth = 6;
			this.Author.Name = "Author";
			this.Author.Width = 125;
			// 
			// PublishingHouse
			// 
			this.PublishingHouse.HeaderText = "Publishing house";
			this.PublishingHouse.MinimumWidth = 6;
			this.PublishingHouse.Name = "PublishingHouse";
			this.PublishingHouse.Width = 125;
			// 
			// PublicationYear
			// 
			this.PublicationYear.HeaderText = "Publication year";
			this.PublicationYear.MinimumWidth = 6;
			this.PublicationYear.Name = "PublicationYear";
			this.PublicationYear.Width = 125;
			// 
			// Series
			// 
			this.Series.HeaderText = "Series";
			this.Series.MinimumWidth = 6;
			this.Series.Name = "Series";
			this.Series.Width = 125;
			// 
			// Catogories
			// 
			this.Catogories.HeaderText = "Catogories";
			this.Catogories.MinimumWidth = 6;
			this.Catogories.Name = "Catogories";
			this.Catogories.Width = 125;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(998, 530);
			this.Controls.Add(this.BooksDataGrid);
			this.Name = "MainForm";
			this.Text = "MainForm";
			((System.ComponentModel.ISupportInitialize)(this.BooksDataGrid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView BooksDataGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn Title;
		private System.Windows.Forms.DataGridViewTextBoxColumn Author;
		private System.Windows.Forms.DataGridViewTextBoxColumn PublishingHouse;
		private System.Windows.Forms.DataGridViewTextBoxColumn PublicationYear;
		private System.Windows.Forms.DataGridViewTextBoxColumn Series;
		private System.Windows.Forms.DataGridViewTextBoxColumn Catogories;
	}
}