namespace GeraRelatorio
{
	partial class frmFormularioPrincipal
	{
		/// <summary>
		/// Variável de designer necessária.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpar os recursos que estão sendo usados.
		/// </summary>
		/// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Código gerado pelo Windows Form Designer

		/// <summary>
		/// Método necessário para suporte ao Designer - não modifique 
		/// o conteúdo deste método com o editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rbtHistoricoVisitas = new System.Windows.Forms.RadioButton();
			this.rbtCartaoSobrenome = new System.Windows.Forms.RadioButton();
			this.rbtCadastroNumeroPessoa = new System.Windows.Forms.RadioButton();
			this.rbtCadastroArmarioNumero = new System.Windows.Forms.RadioButton();
			this.rbtCadastroArmarioIncompleto = new System.Windows.Forms.RadioButton();
			this.rbtCrachaSemAcesso = new System.Windows.Forms.RadioButton();
			this.rbtIdentificacaoAtivadas = new System.Windows.Forms.RadioButton();
			this.rbtAcessoNegadoPorEDV = new System.Windows.Forms.RadioButton();
			this.rbtAcessoNegadoEGarantidoCC = new System.Windows.Forms.RadioButton();
			this.rbtAcessoNegadoEGarantido = new System.Windows.Forms.RadioButton();
			this.rbtAcessoGarantidoEdv = new System.Windows.Forms.RadioButton();
			this.dtaFinal = new System.Windows.Forms.DateTimePicker();
			this.dtaInicial = new System.Windows.Forms.DateTimePicker();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtEDV = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.comboBanco = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rbtHistoricoVisitas);
			this.groupBox1.Controls.Add(this.rbtCartaoSobrenome);
			this.groupBox1.Controls.Add(this.rbtCadastroNumeroPessoa);
			this.groupBox1.Controls.Add(this.rbtCadastroArmarioNumero);
			this.groupBox1.Controls.Add(this.rbtCadastroArmarioIncompleto);
			this.groupBox1.Controls.Add(this.rbtCrachaSemAcesso);
			this.groupBox1.Controls.Add(this.rbtIdentificacaoAtivadas);
			this.groupBox1.Controls.Add(this.rbtAcessoNegadoPorEDV);
			this.groupBox1.Controls.Add(this.rbtAcessoNegadoEGarantidoCC);
			this.groupBox1.Controls.Add(this.rbtAcessoNegadoEGarantido);
			this.groupBox1.Controls.Add(this.rbtAcessoGarantidoEdv);
			this.groupBox1.Location = new System.Drawing.Point(18, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(840, 143);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// rbtHistoricoVisitas
			// 
			this.rbtHistoricoVisitas.AutoSize = true;
			this.rbtHistoricoVisitas.Location = new System.Drawing.Point(417, 112);
			this.rbtHistoricoVisitas.Name = "rbtHistoricoVisitas";
			this.rbtHistoricoVisitas.Size = new System.Drawing.Size(114, 17);
			this.rbtHistoricoVisitas.TabIndex = 10;
			this.rbtHistoricoVisitas.TabStop = true;
			this.rbtHistoricoVisitas.Text = "Historico de Visitas";
			this.rbtHistoricoVisitas.UseVisualStyleBackColor = true;
			this.rbtHistoricoVisitas.CheckedChanged += new System.EventHandler(this.rbtHistoricoVisitas_CheckedChanged);
			// 
			// rbtCartaoSobrenome
			// 
			this.rbtCartaoSobrenome.AutoSize = true;
			this.rbtCartaoSobrenome.Location = new System.Drawing.Point(185, 112);
			this.rbtCartaoSobrenome.Name = "rbtCartaoSobrenome";
			this.rbtCartaoSobrenome.Size = new System.Drawing.Size(131, 17);
			this.rbtCartaoSobrenome.TabIndex = 9;
			this.rbtCartaoSobrenome.TabStop = true;
			this.rbtCartaoSobrenome.Text = "Cartão por Sobrenome";
			this.rbtCartaoSobrenome.UseVisualStyleBackColor = true;
			this.rbtCartaoSobrenome.CheckedChanged += new System.EventHandler(this.rbtCartaoSobrenome_CheckedChanged);
			// 
			// rbtCadastroNumeroPessoa
			// 
			this.rbtCadastroNumeroPessoa.AutoSize = true;
			this.rbtCadastroNumeroPessoa.Location = new System.Drawing.Point(7, 112);
			this.rbtCadastroNumeroPessoa.Name = "rbtCadastroNumeroPessoa";
			this.rbtCadastroNumeroPessoa.Size = new System.Drawing.Size(162, 17);
			this.rbtCadastroNumeroPessoa.TabIndex = 8;
			this.rbtCadastroNumeroPessoa.TabStop = true;
			this.rbtCadastroNumeroPessoa.Text = "Cadastro Armario Por Pessoa";
			this.rbtCadastroNumeroPessoa.UseVisualStyleBackColor = true;
			this.rbtCadastroNumeroPessoa.CheckedChanged += new System.EventHandler(this.rbtCadastroNumeroPessoa_CheckedChanged);
			// 
			// rbtCadastroArmarioNumero
			// 
			this.rbtCadastroArmarioNumero.AutoSize = true;
			this.rbtCadastroArmarioNumero.Location = new System.Drawing.Point(644, 64);
			this.rbtCadastroArmarioNumero.Name = "rbtCadastroArmarioNumero";
			this.rbtCadastroArmarioNumero.Size = new System.Drawing.Size(164, 17);
			this.rbtCadastroArmarioNumero.TabIndex = 7;
			this.rbtCadastroArmarioNumero.TabStop = true;
			this.rbtCadastroArmarioNumero.Text = "Cadastro Armario Por Número";
			this.rbtCadastroArmarioNumero.UseVisualStyleBackColor = true;
			this.rbtCadastroArmarioNumero.CheckedChanged += new System.EventHandler(this.rbtCadastroArmarioNumero_CheckedChanged);
			// 
			// rbtCadastroArmarioIncompleto
			// 
			this.rbtCadastroArmarioIncompleto.AutoSize = true;
			this.rbtCadastroArmarioIncompleto.Location = new System.Drawing.Point(417, 64);
			this.rbtCadastroArmarioIncompleto.Name = "rbtCadastroArmarioIncompleto";
			this.rbtCadastroArmarioIncompleto.Size = new System.Drawing.Size(170, 17);
			this.rbtCadastroArmarioIncompleto.TabIndex = 6;
			this.rbtCadastroArmarioIncompleto.TabStop = true;
			this.rbtCadastroArmarioIncompleto.Text = "Cadastro Armarios Incompletos";
			this.rbtCadastroArmarioIncompleto.UseVisualStyleBackColor = true;
			this.rbtCadastroArmarioIncompleto.CheckedChanged += new System.EventHandler(this.rbtCadastroArmarioIncompleto_CheckedChanged);
			// 
			// rbtCrachaSemAcesso
			// 
			this.rbtCrachaSemAcesso.AutoSize = true;
			this.rbtCrachaSemAcesso.Location = new System.Drawing.Point(185, 64);
			this.rbtCrachaSemAcesso.Name = "rbtCrachaSemAcesso";
			this.rbtCrachaSemAcesso.Size = new System.Drawing.Size(124, 17);
			this.rbtCrachaSemAcesso.TabIndex = 5;
			this.rbtCrachaSemAcesso.TabStop = true;
			this.rbtCrachaSemAcesso.Text = "Crachás sem Acesso";
			this.rbtCrachaSemAcesso.UseVisualStyleBackColor = true;
			// 
			// rbtIdentificacaoAtivadas
			// 
			this.rbtIdentificacaoAtivadas.AutoSize = true;
			this.rbtIdentificacaoAtivadas.Location = new System.Drawing.Point(6, 64);
			this.rbtIdentificacaoAtivadas.Name = "rbtIdentificacaoAtivadas";
			this.rbtIdentificacaoAtivadas.Size = new System.Drawing.Size(130, 17);
			this.rbtIdentificacaoAtivadas.TabIndex = 4;
			this.rbtIdentificacaoAtivadas.TabStop = true;
			this.rbtIdentificacaoAtivadas.Text = "Identificação Ativadas";
			this.rbtIdentificacaoAtivadas.UseVisualStyleBackColor = true;
			this.rbtIdentificacaoAtivadas.CheckedChanged += new System.EventHandler(this.rbtIdentificacaoAtivadas_CheckedChanged);
			// 
			// rbtAcessoNegadoPorEDV
			// 
			this.rbtAcessoNegadoPorEDV.AutoSize = true;
			this.rbtAcessoNegadoPorEDV.Location = new System.Drawing.Point(644, 19);
			this.rbtAcessoNegadoPorEDV.Name = "rbtAcessoNegadoPorEDV";
			this.rbtAcessoNegadoPorEDV.Size = new System.Drawing.Size(145, 17);
			this.rbtAcessoNegadoPorEDV.TabIndex = 3;
			this.rbtAcessoNegadoPorEDV.TabStop = true;
			this.rbtAcessoNegadoPorEDV.Text = "Acesso Negado Por EDV";
			this.rbtAcessoNegadoPorEDV.UseVisualStyleBackColor = true;
			// 
			// rbtAcessoNegadoEGarantidoCC
			// 
			this.rbtAcessoNegadoEGarantidoCC.AutoSize = true;
			this.rbtAcessoNegadoEGarantidoCC.Location = new System.Drawing.Point(417, 19);
			this.rbtAcessoNegadoEGarantidoCC.Name = "rbtAcessoNegadoEGarantidoCC";
			this.rbtAcessoNegadoEGarantidoCC.Size = new System.Drawing.Size(201, 17);
			this.rbtAcessoNegadoEGarantidoCC.TabIndex = 2;
			this.rbtAcessoNegadoEGarantidoCC.TabStop = true;
			this.rbtAcessoNegadoEGarantidoCC.Text = " Acesso_Negado_Garantido_Por_CC";
			this.rbtAcessoNegadoEGarantidoCC.UseVisualStyleBackColor = true;
			// 
			// rbtAcessoNegadoEGarantido
			// 
			this.rbtAcessoNegadoEGarantido.AutoSize = true;
			this.rbtAcessoNegadoEGarantido.Location = new System.Drawing.Point(185, 19);
			this.rbtAcessoNegadoEGarantido.Name = "rbtAcessoNegadoEGarantido";
			this.rbtAcessoNegadoEGarantido.Size = new System.Drawing.Size(175, 17);
			this.rbtAcessoNegadoEGarantido.TabIndex = 1;
			this.rbtAcessoNegadoEGarantido.TabStop = true;
			this.rbtAcessoNegadoEGarantido.Text = "Acesso Negado Garantido EDV";
			this.rbtAcessoNegadoEGarantido.UseVisualStyleBackColor = true;
			// 
			// rbtAcessoGarantidoEdv
			// 
			this.rbtAcessoGarantidoEdv.AutoSize = true;
			this.rbtAcessoGarantidoEdv.Location = new System.Drawing.Point(6, 19);
			this.rbtAcessoGarantidoEdv.Name = "rbtAcessoGarantidoEdv";
			this.rbtAcessoGarantidoEdv.Size = new System.Drawing.Size(153, 17);
			this.rbtAcessoGarantidoEdv.TabIndex = 0;
			this.rbtAcessoGarantidoEdv.TabStop = true;
			this.rbtAcessoGarantidoEdv.Text = "Acesso Garantido Por EDV";
			this.rbtAcessoGarantidoEdv.UseVisualStyleBackColor = true;
			// 
			// dtaFinal
			// 
			this.dtaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtaFinal.Location = new System.Drawing.Point(611, 188);
			this.dtaFinal.Name = "dtaFinal";
			this.dtaFinal.Size = new System.Drawing.Size(185, 20);
			this.dtaFinal.TabIndex = 5;
			// 
			// dtaInicial
			// 
			this.dtaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtaInicial.Location = new System.Drawing.Point(149, 187);
			this.dtaInicial.Name = "dtaInicial";
			this.dtaInicial.Size = new System.Drawing.Size(185, 20);
			this.dtaInicial.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(77, 193);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Data Inicial :";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(547, 195);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(58, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Data Final:";
			// 
			// txtEDV
			// 
			this.txtEDV.Location = new System.Drawing.Point(149, 257);
			this.txtEDV.Name = "txtEDV";
			this.txtEDV.Size = new System.Drawing.Size(200, 20);
			this.txtEDV.TabIndex = 6;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(111, 264);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "EDV:";
			// 
			// comboBanco
			// 
			this.comboBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBanco.FormattingEnabled = true;
			this.comboBanco.Location = new System.Drawing.Point(611, 257);
			this.comboBanco.Name = "comboBanco";
			this.comboBanco.Size = new System.Drawing.Size(185, 21);
			this.comboBanco.TabIndex = 7;
			this.comboBanco.SelectedIndexChanged += new System.EventHandler(this.comboBanco_SelectedIndexChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(549, 265);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(57, 13);
			this.label4.TabIndex = 8;
			this.label4.Text = "Conexões:";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(369, 377);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(115, 39);
			this.button1.TabIndex = 8;
			this.button1.Text = "Buscar";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// frmFormularioPrincipal
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(870, 476);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.comboBanco);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtEDV);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dtaInicial);
			this.Controls.Add(this.dtaFinal);
			this.Controls.Add(this.groupBox1);
			this.Name = "frmFormularioPrincipal";
			this.Text = "Principal";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbtAcessoGarantidoEdv;
		public System.Windows.Forms.DateTimePicker dtaFinal;
		public System.Windows.Forms.DateTimePicker dtaInicial;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtEDV;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBanco;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.RadioButton rbtAcessoNegadoPorEDV;
		public System.Windows.Forms.RadioButton rbtAcessoNegadoEGarantidoCC;
		public System.Windows.Forms.RadioButton rbtAcessoNegadoEGarantido;
		public System.Windows.Forms.RadioButton rbtIdentificacaoAtivadas;
		public System.Windows.Forms.RadioButton rbtCrachaSemAcesso;
		public System.Windows.Forms.RadioButton rbtCadastroArmarioIncompleto;
		public System.Windows.Forms.RadioButton rbtCadastroArmarioNumero;
		public System.Windows.Forms.RadioButton rbtCadastroNumeroPessoa;
		public System.Windows.Forms.RadioButton rbtCartaoSobrenome;
		public System.Windows.Forms.RadioButton rbtHistoricoVisitas;
	}
}

