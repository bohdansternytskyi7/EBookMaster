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
			this.BookListGridView = new System.Windows.Forms.DataGridView();
			((System.ComponentModel.ISupportInitialize)(this.BookListGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// BookListGridView
			// 
			this.BookListGridView.AllowUserToAddRows = false;
			this.BookListGridView.AllowUserToDeleteRows = false;
			this.BookListGridView.AllowUserToOrderColumns = true;
			this.BookListGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BookListGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.BookListGridView.ColumnHeadersHeight = 29;
			this.BookListGridView.Location = new System.Drawing.Point(12, 19);
			this.BookListGridView.Margin = new System.Windows.Forms.Padding(10);
			this.BookListGridView.MultiSelect = false;
			this.BookListGridView.Name = "BookListGridView";
			this.BookListGridView.ReadOnly = true;
			this.BookListGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
			this.BookListGridView.RowTemplate.Height = 24;
			this.BookListGridView.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.BookListGridView.Size = new System.Drawing.Size(1635, 10);
			this.BookListGridView.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1666, 953);
			this.Controls.Add(this.BookListGridView);
			this.Name = "MainForm";
			this.Text = "List of e-books";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.BookListGridView)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.DataGridView BookListGridView;
	}
}