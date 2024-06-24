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
			this.BorrowBtn = new System.Windows.Forms.Button();
			this.ReturnBtn = new System.Windows.Forms.Button();
			this.InfoBtn = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.BookListGridView)).BeginInit();
			this.SuspendLayout();
			// 
			// BookListGridView
			// 
			this.BookListGridView.AllowUserToAddRows = false;
			this.BookListGridView.AllowUserToDeleteRows = false;
			this.BookListGridView.AllowUserToOrderColumns = true;
			this.BookListGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.BookListGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.BookListGridView.BackgroundColor = System.Drawing.Color.Gainsboro;
			this.BookListGridView.ColumnHeadersHeight = 29;
			this.BookListGridView.Location = new System.Drawing.Point(12, 70);
			this.BookListGridView.Margin = new System.Windows.Forms.Padding(10);
			this.BookListGridView.MultiSelect = false;
			this.BookListGridView.Name = "BookListGridView";
			this.BookListGridView.ReadOnly = true;
			this.BookListGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders;
			this.BookListGridView.RowTemplate.Height = 24;
			this.BookListGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.BookListGridView.Size = new System.Drawing.Size(1635, 186);
			this.BookListGridView.TabIndex = 0;
			// 
			// BorrowBtn
			// 
			this.BorrowBtn.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.BorrowBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.BorrowBtn.Location = new System.Drawing.Point(23, 22);
			this.BorrowBtn.Name = "BorrowBtn";
			this.BorrowBtn.Size = new System.Drawing.Size(144, 35);
			this.BorrowBtn.TabIndex = 1;
			this.BorrowBtn.Text = "Borrow";
			this.BorrowBtn.UseVisualStyleBackColor = false;
			this.BorrowBtn.Click += new System.EventHandler(this.BorrowBtn_Click);
			// 
			// ReturnBtn
			// 
			this.ReturnBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
			this.ReturnBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.ReturnBtn.Location = new System.Drawing.Point(173, 22);
			this.ReturnBtn.Name = "ReturnBtn";
			this.ReturnBtn.Size = new System.Drawing.Size(144, 35);
			this.ReturnBtn.TabIndex = 2;
			this.ReturnBtn.Text = "Return";
			this.ReturnBtn.UseVisualStyleBackColor = false;
			this.ReturnBtn.Click += new System.EventHandler(this.ReturnBtn_Click);
			// 
			// InfoBtn
			// 
			this.InfoBtn.BackColor = System.Drawing.SystemColors.Info;
			this.InfoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
			this.InfoBtn.Location = new System.Drawing.Point(323, 22);
			this.InfoBtn.Name = "InfoBtn";
			this.InfoBtn.Size = new System.Drawing.Size(144, 35);
			this.InfoBtn.TabIndex = 3;
			this.InfoBtn.Text = "Info";
			this.InfoBtn.UseVisualStyleBackColor = false;
			this.InfoBtn.Click += new System.EventHandler(this.InfoBtn_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1666, 953);
			this.Controls.Add(this.InfoBtn);
			this.Controls.Add(this.ReturnBtn);
			this.Controls.Add(this.BorrowBtn);
			this.Controls.Add(this.BookListGridView);
			this.Name = "MainForm";
			this.Text = "List of e-books";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			((System.ComponentModel.ISupportInitialize)(this.BookListGridView)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		private System.Windows.Forms.Button BorrowBtn;
		private System.Windows.Forms.Button ReturnBtn;
		public System.Windows.Forms.DataGridView BookListGridView;
		private System.Windows.Forms.Button InfoBtn;
	}
}