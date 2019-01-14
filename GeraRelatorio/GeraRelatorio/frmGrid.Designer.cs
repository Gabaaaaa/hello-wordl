namespace GeraRelatorio
{
	partial class frmGrid
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
			this.lblLocal = new System.Windows.Forms.Label();
			this.lblConexao = new System.Windows.Forms.Label();
			this.grdRelatorio = new System.Windows.Forms.DataGridView();
			this.label1 = new System.Windows.Forms.Label();
			this.btnExcel = new System.Windows.Forms.Button();
			this.btnPdf = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grdRelatorio)).BeginInit();
			this.SuspendLayout();
			// 
			// lblLocal
			// 
			this.lblLocal.AutoSize = true;
			this.lblLocal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblLocal.Location = new System.Drawing.Point(104, 9);
			this.lblLocal.Name = "lblLocal";
			this.lblLocal.Size = new System.Drawing.Size(57, 20);
			this.lblLocal.TabIndex = 0;
			this.lblLocal.Text = "label1";
			// 
			// lblConexao
			// 
			this.lblConexao.AutoSize = true;
			this.lblConexao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblConexao.Location = new System.Drawing.Point(167, 9);
			this.lblConexao.Name = "lblConexao";
			this.lblConexao.Size = new System.Drawing.Size(57, 20);
			this.lblConexao.TabIndex = 1;
			this.lblConexao.Text = "label2";
			// 
			// grdRelatorio
			// 
			this.grdRelatorio.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdRelatorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdRelatorio.Location = new System.Drawing.Point(4, 29);
			this.grdRelatorio.MultiSelect = false;
			this.grdRelatorio.Name = "grdRelatorio";
			this.grdRelatorio.ReadOnly = true;
			this.grdRelatorio.Size = new System.Drawing.Size(1232, 565);
			this.grdRelatorio.TabIndex = 2;
			this.grdRelatorio.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdRelatorio_CellContentClick);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(101, 20);
			this.label1.TabIndex = 3;
			this.label1.Text = "Localidade:";
			// 
			// btnExcel
			// 
			this.btnExcel.Location = new System.Drawing.Point(921, -3);
			this.btnExcel.Name = "btnExcel";
			this.btnExcel.Size = new System.Drawing.Size(95, 32);
			this.btnExcel.TabIndex = 4;
			this.btnExcel.Text = "Excel";
			this.btnExcel.UseVisualStyleBackColor = true;
			this.btnExcel.Click += new System.EventHandler(this.btnExcel_Click);
			// 
			// btnPdf
			// 
			this.btnPdf.Location = new System.Drawing.Point(1047, -3);
			this.btnPdf.Name = "btnPdf";
			this.btnPdf.Size = new System.Drawing.Size(95, 32);
			this.btnPdf.TabIndex = 5;
			this.btnPdf.Text = "Pdf";
			this.btnPdf.UseVisualStyleBackColor = true;
			this.btnPdf.Click += new System.EventHandler(this.btnPdf_Click);
			// 
			// frmGrid
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(1256, 606);
			this.Controls.Add(this.btnPdf);
			this.Controls.Add(this.btnExcel);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.grdRelatorio);
			this.Controls.Add(this.lblConexao);
			this.Controls.Add(this.lblLocal);
			this.Name = "frmGrid";
			this.Text = "FrmGrid";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.FrmGrid_Load);
			((System.ComponentModel.ISupportInitialize)(this.grdRelatorio)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label lblLocal;
		public System.Windows.Forms.Label lblConexao;
		public System.Windows.Forms.DataGridView grdRelatorio;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnExcel;
		private System.Windows.Forms.Button btnPdf;
	}
}