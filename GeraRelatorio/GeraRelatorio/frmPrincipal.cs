using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Data.OracleClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Globalization;

namespace GeraRelatorio
{
	public partial class frmFormularioPrincipal : Form
	{
		public frmFormularioPrincipal()
		{
			InitializeComponent();

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			rbtAcessoGarantidoEdv.Select();


			dtaFinal.Value = DateTime.Today.AddDays(-1);
			dtaInicial.Value = DateTime.Today.AddDays(-1);
			txtEDV.MaxLength = 20;

			comboBanco.Items.Clear();

			List<Planta> plantas = new List<Planta>();

			string path = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);


			//using (StreamReader arquivo = File.OpenText(@"C:\Conexoes\Estados.txt"))
			using (StreamReader arquivo = File.OpenText(path + "\\Localidades.txt"))
			{
				string linha;
				while ((linha = arquivo.ReadLine()) != null)
				{
					var espaçoArquivo = linha.Split(':');

					var planta = new Planta();
					planta.Local = espaçoArquivo[0];
					planta.Banco = espaçoArquivo[1];
					planta.Oracle = espaçoArquivo[2];


					plantas.Add(planta);
				}
			}

			foreach (Planta result in plantas)
			{
				comboBanco.Items.Add(result);
			}
			comboBanco.DisplayMember = "Local";
			comboBanco.ValueMember = "Banco";

			comboBanco.SelectedIndex = 0;


		}

		public void ConfereRelatorioData()
		{
			if (rbtIdentificacaoAtivadas.Checked || rbtCrachaSemAcesso.Checked || rbtCadastroArmarioIncompleto.Checked || rbtCadastroNumeroPessoa.Checked || rbtCartaoSobrenome.Checked || rbtHistoricoVisitas.Checked || rbtCadastroArmarioNumero.Checked)
			{
				dtaInicial.Enabled = false;
				dtaFinal.Enabled = false;


			}
			else
			{
				dtaInicial.Enabled = true;
				dtaFinal.Enabled = true;
			}


		}

		private void comboBanco_SelectedIndexChanged(object sender, EventArgs e)
		{

			frmGrid formb = new frmGrid();
			rbtCadastroArmarioIncompleto.Enabled = true;
			rbtCadastroArmarioNumero.Enabled = true;
			rbtCadastroNumeroPessoa.Enabled = true;

			if (rbtCadastroArmarioNumero.Enabled == true || rbtCadastroNumeroPessoa.Enabled == true || rbtCadastroArmarioIncompleto.Enabled == true)
			{
				rbtAcessoGarantidoEdv.Checked = true;
			}

			switch (((Planta)comboBanco.SelectedItem).Local)
			{
				case "CT":
					formb.lblLocal.Text = ((Planta)comboBanco.SelectedItem).Local;
					break;

				case "CU":
					formb.lblLocal.Text = ((Planta)comboBanco.SelectedItem).Local;
					break;

				case "AT":
					formb.lblLocal.Text = ((Planta)comboBanco.SelectedItem).Oracle;
					rbtCadastroArmarioIncompleto.Enabled = false;
					rbtCadastroArmarioNumero.Enabled = false;
					rbtCadastroNumeroPessoa.Enabled = false;

					break;

				default:
					break;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{


				OdbcConnection conn;
				string edvResultado = txtEDV.Text;
				//string date_inicial = dtaInicial.Value.ToString("DD/MM/YYYY HH24:MI:SS");
				//string date_final = dtaFinal.Value.ToString("DD/MM/YYYY HH24:MI:SS");
				Planta planta = (Planta)comboBanco.SelectedItem;


				conn = new OdbcConnection(planta.Banco);

				DataSet ds = new DataSet();
				DataTable dt = new DataTable();
				OdbcDataAdapter ada = new OdbcDataAdapter();
				OdbcCommand cmd = new OdbcCommand();

				conn.ConnectionTimeout = 3600;
				cmd.CommandTimeout = 3600;


				if (rbtAcessoGarantidoEdv.Checked)
				{

					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format(" SELECT TO_CHAR(EVENTS.event_time_utc - interval '3' hour, 'DD/MM/YYYY HH24:MI:SS') as DATA_HORA_EVENTO, " +
													" EVENTS.EMPID as ID_EMP, " +
													" EVENTS.CARDNUM as NUMERO_CRACHA, " +
													" EMP.SSNO as EDV, " +
													" EMP.LASTNAME as ULTIMO_NOME, " +
													" EMP.FIRSTNAME as NOME, " +
													" EVENTS.EVENTTYPE as TIPO_EVENTO, " +
													" EVENT.EVDESCR as EVENTO, " +
													" EVENTS.SERIALNUM, " +
													" EVENTS.DEVID as ID_LEITOR, " +
													" EVENTS.MACHINE as ID_CONTROLADORA, " +
													" READER.READERDESC as LEITOR " +
													" FROM((((EVENTS EVENTS " +
													" INNER JOIN EVENT EVENT ON(EVENTS.EVENTTYPE = EVENT.EVTYPEID) AND(EVENTS.EVENTID = EVENT.EVID))" +
													" LEFT OUTER JOIN READER READER ON(EVENTS.MACHINE = READER.PANELID) AND(EVENTS.DEVID = READER.READERID))" +
													" LEFT OUTER JOIN EMP EMP ON EVENTS.EMPID = EMP.ID)" +
													" INNER JOIN ACCESSPANE ACCESSPANE ON EVENTS.MACHINE = ACCESSPANE.PANELID)" +
													" LEFT OUTER JOIN ACCOUNT_ZONE ACCOUNT_ZONE ON(EVENTS.MACHINE= ACCOUNT_ZONE.PANELID) AND(EVENTS.DEVID = ACCOUNT_ZONE.ZONENUM)" +
													" WHERE(EVENTS.EVENTTYPE = 0 OR EVENTS.EVENTTYPE = 2 AND" +
													" (EVENTS.EVENTID = 0 OR EVENTS.EVENTID = 2) OR EVENTS.EVENTTYPE = 3 AND(EVENTS.EVENTID = 4 OR EVENTS.EVENTID = 5))" +
													" and EVENTS.event_time_utc - interval '3' hour between to_date('{0} 00:00:00', 'DD/MM/YYYY HH24:MI:SS') and to_date('{1} 23:59:59', 'DD/MM/YYYY HH24:MI:SS')",
													dtaInicial.Value.ToString("dd/MM/yyyy"), dtaFinal.Value.ToString("dd/MM/yyyy"));

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" and SSNO like '%{0}%'", txtEDV.Text);
							}

						

							cmd.CommandText = sqlRelatorio;
							break;

						case "0":
							string sqlRelatorioAT = string.Format("SELECT DATEADD(hh,-3,EVENTS.EVENT_TIME_UTC) as DATA_HORA_EVENTO, " +
													 "EVENTS.EMPID as ID_EMP, " +
													 "EVENTS.CARDNUM as NUMERO_CRACHA, " +
													 "EMP.SSNO as EDV, " +
													 "EMP.LASTNAME as ULTIMO_NOME, " +
													 "EMP.FIRSTNAME as NOME, " +
													 "EVENTS.EVENTTYPE as TIPO_EVENTO, " +
													 "EVENT.EVDESCR as EVENTO, " +
													 "EVENTS.SERIALNUM, " +
													 "EVENTS.DEVID as ID_LEITOR, " +
													 "EVENTS.MACHINE as ID_CONTROLADORA, " +
													 "READER.READERDESC as LEITOR " +
													 "FROM((((EVENTS EVENTS " +
													 "INNER JOIN EVENT EVENT ON(EVENTS.EVENTTYPE = EVENT.EVTYPEID) AND(EVENTS.EVENTID = EVENT.EVID)) " +
													 "LEFT OUTER JOIN READER READER ON(EVENTS.MACHINE = READER.PANELID) AND(EVENTS.DEVID = READER.READERID)) " +
													 "LEFT OUTER JOIN EMP EMP ON EVENTS.EMPID = EMP.ID) " +
													 "INNER JOIN ACCESSPANE ACCESSPANE ON EVENTS.MACHINE = ACCESSPANE.PANELID) " +
													 "LEFT OUTER JOIN ACCOUNT_ZONE ACCOUNT_ZONE ON(EVENTS.MACHINE= ACCOUNT_ZONE.PANELID) AND(EVENTS.DEVID = ACCOUNT_ZONE.ZONENUM) " +
													 "WHERE(EVENTS.EVENTTYPE = 0 OR EVENTS.EVENTTYPE = 2 AND " +
													 "(EVENTS.EVENTID = 0 OR EVENTS.EVENTID = 2) OR EVENTS.EVENTTYPE = 3 AND(EVENTS.EVENTID = 4 OR EVENTS.EVENTID = 5)) and EVENTS.event_time_utc between '{0} 00:00:00' and '{1} 23:59:59' ", dtaInicial.Value.ToString("yyyy/MM/dd"), dtaFinal.Value.ToString("yyyy/MM/dd"));

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorioAT += String.Format(" and SSNO like '%{0}%'", txtEDV.Text);
							}

							cmd.CommandText = sqlRelatorioAT;
							break;

						default:
							break;
					}

				}
				else if (rbtAcessoNegadoEGarantido.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT to_char(EVENTS.EVENT_TIME_utc - interval '3' hour, 'DD/MM/YYYY HH24:MI:SS') as DATA_HORA_EVENTO, " +
															"EMP.LASTNAME as ULTIMO_NOME, " +
															"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
															"EVENT.EVDESCR as EVENTsOS, " +
															"EVENTS.EMPID as ID_EMP, " +
															"EVENTS.EVENTTYPE as TIPO_EVENTO, " +
															"EVENTS.MACHINE as ID_CONTROLADORA, " +
															"EVENTS.CARDNUM as NUMERO_CRACHA," +
															"EVENTS.SERIALNUM, " +
															"EVENTS.EVENTID as ID_EVENTO," +
															"EMP.SSNO as EDV " +
															"FROM((EVENTS EVENTS INNER JOIN EVENT EVENT ON(EVENTS.EVENTTYPE = EVENT.EVTYPEID) AND(EVENTS.EVENTID = EVENT.EVID)) LEFT OUTER JOIN EMP EMP ON EVENTS.EMPID = EMP.ID)" +
															"INNER JOIN ACCESSPANE ACCESSPANE ON EVENTS.MACHINE = ACCESSPANE.PANELID " +
															"WHERE(EVENTS.EVENTTYPE >= 0 AND EVENTS.EVENTTYPE < 4)" +
															"and EVENTS.event_time_utc - interval '3' hour between to_date('{0} 00:00:00', 'DD/MM/YYYY HH24:MI:SS') and to_date('{1} 23:59:59', 'DD/MM/YYYY HH24:MI:SS') ", dtaInicial.Value.ToString("dd / MM / yyyy"), dtaFinal.Value.ToString("dd / MM / yyyy"));

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" and SSNO like '%{0}%'", txtEDV.Text);
							}

							cmd.CommandText = sqlRelatorio;
							break;

						case "0":
							string sqlRelatorioAT = string.Format("SELECT DATEADD(hh,-3,EVENTS.EVENT_TIME_UTC) as DATA_HORA_EVENTO, " +
														"EMP.LASTNAME as ULTIMO_NOME, " +
														"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
														"EVENT.EVDESCR as EVENTOS, " +
														"EVENTS.EMPID as ID_EMP, " +
														"EVENTS.EVENTTYPE as TIPO_EVENTO, " +
														"EVENTS.MACHINE as ID_CONTROLADORA, " +
														"EVENTS.CARDNUM as NUMERO_CRACHA, " +
														"EVENTS.SERIALNUM, " +
														"EVENTS.EVENTID as ID_EVENTO, " +
														"EMP.SSNO as EDV " +
														"FROM((EVENTS EVENTS INNER JOIN EVENT EVENT ON(EVENTS.EVENTTYPE = EVENT.EVTYPEID) AND(EVENTS.EVENTID = EVENT.EVID)) LEFT OUTER JOIN EMP EMP ON EVENTS.EMPID = EMP.ID) " +
														"INNER JOIN ACCESSPANE ACCESSPANE ON EVENTS.MACHINE = ACCESSPANE.PANELID " +
														"WHERE(EVENTS.EVENTTYPE >= 0 AND EVENTS.EVENTTYPE < 4) " +
														"and EVENTS.event_time_utc between '{0} 00:00:00' and '{1} 23:59:59' ", dtaInicial.Value.ToString("yyyy / MM / dd"), dtaFinal.Value.ToString("yyyy / MM / dd"));

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorioAT += String.Format(" and SSNO like '%{0}%'", txtEDV.Text);
							}

							cmd.CommandText = sqlRelatorioAT;
							break;
					}

				}
				else if (rbtAcessoNegadoEGarantidoCC.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT to_char(EVENTS.EVENT_TIME_utc - interval '3' hour, 'DD/MM/YYYY HH24:MI:SS') as DATA_HORA_EVENTO, " +
													"EMP.LASTNAME as ULTIMO_NOME, " +
													"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
													"EVENT.EVDESCR as EVENTOS, " +
													"EVENTS.CARDNUM as NUMERO_CRACHA, " +
													"EVENTS.EMPID as ID_EMP, " +
													"ACCESSPANE.PANELID, " +
													"EVENTS.EVENTTYPE as TIPO_EVENTO, " +
													"EVENTS.MACHINE as ID_CONTROLADORA, " +
													"EVENTS.SERIALNUM, " +
													"EVENTS.ISSUECODE, " +
													"EVENTS.EVENTID as ID_EVENTO, " +
													"EMP.SSNO as EDV, BUILDING.NAME " +
													"FROM((EVENTS EVENTS INNER JOIN EVENT EVENT ON(EVENTS.EVENTTYPE = EVENT.EVTYPEID) AND(EVENTS.EVENTID = EVENT.EVID)) " +
													"LEFT OUTER JOIN((UDFEMP UDFEMP INNER JOIN BUILDING BUILDING ON UDFEMP.BUILDING = BUILDING.ID) INNER JOIN EMP EMP ON UDFEMP.ID = EMP.ID) ON EVENTS.EMPID = EMP.ID) " +
													"INNER JOIN ACCESSPANE ACCESSPANE ON EVENTS.MACHINE = ACCESSPANE.PANELID " +
													"WHERE(EVENTS.EVENTTYPE >= 0 AND EVENTS.EVENTTYPE < 4) " +
													"and EVENTS.event_time_utc - interval '3' hour between to_date('{0} 00:00:00', 'DD/MM/YYYY HH24:MI:SS') and to_date('{1} 23:59:59', 'DD/MM/YYYY HH24:MI:SS') "
													, dtaInicial.Value.ToString("dd / MM / yyyy"), dtaFinal.Value.ToString("dd / MM / yyyy"));

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" AND SSNO like '%{0}%' ORDER BY BUILDING.NAME ", txtEDV.Text);
							}
							else
							{
								sqlRelatorio += String.Format(" ORDER BY BUILDING.NAME ");
							}

							cmd.CommandText = sqlRelatorio;
							break;
						case "0":
							string sqlRelatorioAT = string.Format("SELECT DATEADD(hh,-3,EVENTS.EVENT_TIME_UTC) as DATA_HORA_EVENTO, " +
																"EMP.LASTNAME as ULTIMO_NOME, " +
																"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
																"EVENT.EVDESCR as EVENTOS, " +
																"EVENTS.CARDNUM as NUMERO_CRACHA, " +
																"EVENTS.EMPID as ID_EMP, " +
																"ACCESSPANE.PANELID, " +
																"EVENTS.EVENTTYPE as TIPO_EVENTO, " +
																"EVENTS.MACHINE as ID_CONTROLADORA, " +
																"EVENTS.SERIALNUM, " +
																"EVENTS.ISSUECODE, " +
																"EVENTS.EVENTID as ID_EVENTO, " +
																"EMP.SSNO as EDV, " +
																"BUILDING.NAME FROM((EVENTS EVENTS INNER JOIN EVENT EVENT ON(EVENTS.EVENTTYPE = EVENT.EVTYPEID) AND(EVENTS.EVENTID = EVENT.EVID)) " +
																"LEFT OUTER JOIN((UDFEMP UDFEMP INNER JOIN BUILDING BUILDING ON UDFEMP.BUILDING = BUILDING.ID) " +
																"INNER JOIN EMP EMP ON UDFEMP.ID = EMP.ID) ON EVENTS.EMPID = EMP.ID) " +
																"INNER JOIN ACCESSPANE ACCESSPANE ON EVENTS.MACHINE = ACCESSPANE.PANELID " +
																"WHERE(EVENTS.EVENTTYPE >= 0 AND EVENTS.EVENTTYPE < 4) " +
																"and EVENTS.event_time_utc between '{0}  00:00:00' and ' {1}  23:59:59' ", dtaInicial.Value.ToString("yyyy/MM/dd"), dtaFinal.Value.ToString("yyyy/MM/dd"));

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorioAT += String.Format(" and SSNO like '%{0}%' ORDER BY BUILDING.NAME ", txtEDV.Text);
							}
							else
							{
								sqlRelatorioAT += String.Format(" ORDER BY BUILDING.NAME; ");
							}

							cmd.CommandText = sqlRelatorioAT;
							break;
					}

				}
				else if (rbtAcessoNegadoPorEDV.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT to_char(EVENTS.EVENT_TIME_utc - interval '3' hour, 'DD/MM/YYYY HH24:MI:SS') as DATA_HORA_EVENTO, " +
														"EMP.LASTNAME as ULTIMO_NOME, " +
														"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
														"EVENT.EVDESCR as EVENTOS, " +
														"EVENTS.CARDNUM as NUMERO_CRACHA, " +
														"EVENTS.EMPID as ID_EMP, " +
														"EVENTS.EVENTTYPE as TIPO_EVENTO, " +
														"EVENTS.SERIALNUM, " +
														"EVENTS.MACHINE as ID_CONTROLADORA, " +
														"EVENTS.ISSUECODE, " +
														"EVENTS.EVENTID as ID_EVENTO, " +
														"ACCESSPANE.PANELID, " +
														"EMP.SSNO as EDV " +
														"FROM((EVENTS EVENTS INNER JOIN EVENT EVENT ON(EVENTS.EVENTTYPE = EVENT.EVTYPEID) AND(EVENTS.EVENTID = EVENT.EVID)) LEFT OUTER JOIN EMP EMP ON EVENTS.EMPID = EMP.ID) " +
														"INNER JOIN ACCESSPANE ACCESSPANE ON EVENTS.MACHINE = ACCESSPANE.PANELID " +
														"WHERE(EVENTS.EVENTTYPE = 1 OR EVENTS.EVENTTYPE = 2 AND EVENTS.EVENTID = 1 OR EVENTS.EVENTTYPE = 3 AND(EVENTS.EVENTID = 0 OR EVENTS.EVENTID = 1 OR EVENTS.EVENTID = 2 OR EVENTS.EVENTID = 3 OR EVENTS.EVENTID = 6 OR EVENTS.EVENTID = 7)) " +
														"and EVENTS.event_time_utc - interval '3' hour between to_date('{0} 00:00:00', 'DD/MM/YYYY HH24:MI:SS') and to_date('{1} 23:59:59', 'DD/MM/YYYY HH24:MI:SS') ", dtaInicial.Value.ToString("dd / MM / yyyy"), dtaFinal.Value.ToString("dd / MM / yyyy"));

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" and SSNO like '%{0}%'", txtEDV.Text);
							}


							cmd.CommandText = sqlRelatorio;
							break;

						case "0":
							string sqlRelatorioAT = string.Format("SELECT DATEADD(hh,-3,EVENTS.EVENT_TIME_UTC) as DATA_HORA_EVENTO, " +
																  "EMP.LASTNAME as ULTIMO_NOME, " +
																  "EMP.FIRSTNAME as PRIMEIRO_NOME, " +
																  "EVENT.EVDESCR as EVENTOS, " +
																  "EVENTS.CARDNUM as NUMERO_CRACHA, " +
																  "EVENTS.EMPID as ID_EMP, " +
																  "EVENTS.EVENTTYPE as TIPO_EVENTO, " +
																  "EVENTS.SERIALNUM, " +
																  "EVENTS.MACHINE as ID_CONTROLADORA, " +
																  "EVENTS.ISSUECODE, " +
																  "EVENTS.EVENTID as ID_EVENTO, " +
																  "ACCESSPANE.PANELID, " +
																  "EMP.SSNO as EDV " +
																  "FROM((EVENTS EVENTS INNER JOIN EVENT EVENT ON(EVENTS.EVENTTYPE = EVENT.EVTYPEID) " +
																  "AND(EVENTS.EVENTID = EVENT.EVID)) " +
																  "LEFT OUTER JOIN EMP EMP ON EVENTS.EMPID = EMP.ID) " +
																  "INNER JOIN ACCESSPANE ACCESSPANE ON EVENTS.MACHINE = ACCESSPANE.PANELID " +
																  "WHERE(EVENTS.EVENTTYPE = 1 OR EVENTS.EVENTTYPE = 2 AND EVENTS.EVENTID = 1 OR EVENTS.EVENTTYPE = 3 AND " +
																  "(EVENTS.EVENTID = 0 OR EVENTS.EVENTID = 1 OR EVENTS.EVENTID = 2 OR EVENTS.EVENTID = 3 OR EVENTS.EVENTID = 6 OR EVENTS.EVENTID = 7)) " +
																  "and EVENTS.event_time_utc between '{0} 00:00:00' and '{1} 23:59:59' ", dtaInicial.Value.ToString("yyyy/MM/dd"), dtaFinal.Value.ToString("yyyy/MM/dd"));

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorioAT += String.Format(" and SSNO like '%{0}%'", txtEDV.Text);
							}


							cmd.CommandText = sqlRelatorioAT;
							break;
					}



				}
				else if (rbtIdentificacaoAtivadas.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("Select EMP.LASTNAME as ULTIMO_NOME, " +
													"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
													"EMP.MIDNAME, " +
													"EMP.ID as EMP_ID, " +
													"BADGE.ID as Crachá_ID, " +
													"BADGETYP.NAME as Identificação, " +
													"to_char(BADGE.DEACTIVATE, 'DD/MM/YYYY HH24:MI:SS') as Crachá_Desativado, " +
													"BADGE.STATUS, " +
													"BADGE.BADGEKEY as Identificação_Senha, " +
													"BADGE.ISSUECODE, " +
													"to_char(BADGE.ACTIVATE, 'DD/MM/YYYY HH24:MI:SS') as Crachá_Ativo, " +
													"EMP.SSNO as EDV " +
													"FROM(EMP EMP INNER JOIN BADGE BADGE ON EMP.ID = BADGE.EMPID) INNER JOIN BADGETYP BADGETYP ON BADGE.TYPE = BADGETYP.ID " +
													"WHERE  BADGE.STATUS = 1 AND BADGE.ID > 0");

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" and SSNO like '%{0}%' ORDER BY BADGE.DEACTIVATE, BADGE.ID, EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID ", txtEDV.Text);
							}
							else
							{
								sqlRelatorio += String.Format(" ORDER BY BADGE.DEACTIVATE, BADGE.ID, EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID ");
							}

							cmd.CommandText = sqlRelatorio;
							break;

						case "0":
							string sqlRelatorioAT = string.Format("Select EMP.LASTNAME as ULTIMO_NOME, " +
																 "EMP.FIRSTNAME as PRIMEIRO_NOME, " +
																 "EMP.MIDNAME, " +
																 "EMP.ID as EMP_ID, " +
																 "BADGE.ID as Crachá_ID, " +
																 "BADGETYP.NAME as Identificação, " +
																 "BADGE.DEACTIVATE as Crachá_Desativado, " +
																 "BADGE.STATUS, " +
																 "BADGE.BADGEKEY as Identificação_Senha, " +
																 "BADGE.ISSUECODE, " +
																 "BADGE.ACTIVATE as Crachá_Ativo, " +
																 "EMP.SSNO as EDV " +
																 "FROM(EMP EMP INNER JOIN BADGE BADGE ON EMP.ID = BADGE.EMPID) " +
																 "INNER JOIN BADGETYP BADGETYP ON BADGE.TYPE = BADGETYP.ID " +
																 "WHERE  BADGE.STATUS = 1 AND BADGE.ID > 0 ");
							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorioAT += String.Format(" and SSNO like '%{0}%' ORDER BY BADGE.DEACTIVATE, BADGE.ID, EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID ", txtEDV.Text);
							}
							else
							{
								sqlRelatorioAT += String.Format(" ORDER BY BADGE.DEACTIVATE, BADGE.ID, EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID ");
							}

							cmd.CommandText = sqlRelatorioAT;
							break;
					}




				}
				else if (rbtCrachaSemAcesso.Checked)
				{

					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT EMP.LASTNAME as ULTIMO_NOME, " +
													"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
													"EMP.MIDNAME, EMP.ID as EMP_ID, " +
													"BADGE.ID as Identificador_ID, " +
													"BADGETYP.NAME Nome_de_Identificação, " +
													"BADGE.ACTIVATE as Crachá_Ativado, " +
													"BADGE.DEACTIVATE as Crachá_Desativado, " +
													"BADGE.BADGEKEY Identificação_Senha, " +
													"BADGE.ISSUECODE, " +
													"BADGE.STATUS, " +
													"EMP.SSNO as EDV " +
													"FROM(EMP EMP INNER JOIN BADGE BADGE ON EMP.ID = BADGE.EMPID) INNER JOIN BADGETYP BADGETYP ON BADGE.TYPE = BADGETYP.ID " +
													"WHERE  BADGE.ID > 0 AND BADGE.STATUS = 1 ");


							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" and SSNO like '%{0}%' ORDER BY BADGE.ID, EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID ", txtEDV.Text);
							}
							else
							{
								sqlRelatorio += String.Format(" ORDER BY BADGE.ID, EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID ");
							}

							cmd.CommandText = sqlRelatorio;
							break;

						case "0":
							string sqlRelatorioAT = string.Format("SELECT EMP.LASTNAME as ULTIMO_NOME, " +
												"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
												"EMP.MIDNAME, EMP.ID as EMP_ID, " +
												"BADGE.ID as Identificador_ID, " +
												"BADGETYP.NAME Nome_de_Identificação, " +
												"BADGE.ACTIVATE as Crachá_Ativado, " +
												"BADGE.DEACTIVATE as Crachá_Desativado, " +
												"BADGE.BADGEKEY Identificação_Senha, " +
												"BADGE.ISSUECODE, " +
												"BADGE.STATUS, " +
												"EMP.SSNO as EDV " +
												"FROM(EMP EMP INNER JOIN BADGE BADGE ON EMP.ID = BADGE.EMPID) INNER JOIN BADGETYP BADGETYP ON BADGE.TYPE = BADGETYP.ID " +
												"WHERE  BADGE.ID > 0 AND BADGE.STATUS = 1 ");


							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorioAT += String.Format(" and SSNO like '%{0}%' ORDER BY BADGE.ID, EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID ", txtEDV.Text);
							}
							else
							{
								sqlRelatorioAT += String.Format(" ORDER BY BADGE.ID, EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID ");
							}

							cmd.CommandText = sqlRelatorioAT;

							break;
					}

				}
				else if (rbtCadastroArmarioIncompleto.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT EMP.LASTNAME as ULTIMO_NOME, " +
														"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
														"EMP.ID, " +
														"EMP.SSNO as EDV, " +
														"UDFEMP.ARMARIONUMERO1, " +
														"UDFEMP.ARMARIONUMERO2, " +
														"UDFEMP.ARMARIOCA1, " +
														"UDFEMP.ARMARIOCA2, " +
														"UDFEMP.ARMARIOANDAR1, " +
														"UDFEMP.ARMARIOANDAR2, " +
														"UDFEMP.OPHONE as TELEFONE, " +
														"LOCATION.NAME LOCAL_NAME, " +
														"TERCSETOR.NAME, " +
														"UDFEMP.TERCRAMALTOMADO as RAMAL, " +
														"UDFEMP.TERCSETORTOMADO as SETOR_TOMADOR " +
														"FROM((EMP EMP INNER JOIN LENEL.UDFEMP UDFEMP ON EMP.ID = UDFEMP.ID) INNER JOIN LENEL.LOCATION LOCATION ON UDFEMP.LOCATION = LOCATION.ID) INNER JOIN LENEL.TERCSETOR TERCSETOR ON UDFEMP.TERCSETOR = TERCSETOR.ID " +
														"WHERE EMP.ID <> 0 AND(UDFEMP.ARMARIONUMERO1 <> ' ' OR UDFEMP.ARMARIONUMERO2 <> ' ') AND(UDFEMP.ARMARIOCA1 = 0 OR UDFEMP.ARMARIOANDAR1 = 0) AND(UDFEMP.ARMARIOCA2 = 0 OR UDFEMP.ARMARIOANDAR2 = 0) ");

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" AND SSNO like '%{0}%' ORDER BY EMP.SSNO ", txtEDV.Text);
							}
							else
							{
								sqlRelatorio += String.Format(" ORDER BY EMP.SSNO ");
							}


							cmd.CommandText = sqlRelatorio;
							break;

						case "0":

							break;
					}

				}
				else if (rbtCadastroArmarioNumero.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT EMP.LASTNAME as ULTIMO_NOME, " +
													"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
													"EMP.ID, EMP.SSNO as EDV, " +
													"UDFEMP.ARMARIONUMERO1, " +
													"UDFEMP.ARMARIONUMERO2, " +
													"UDFEMP.OPHONE as TELEFONE, " +
													"UDFEMP.TERCRAMALTOMADO as RAMAL, " +
													"ARMARIOCA1.NAME, " +
													"ARMARIOANDAR1.NAME as ARMARIO_ANDAR_1, " +
													"ARMARIOCA2.NAME as GRUPO_ARMARIO, " +
													"ARMARIOANDAR2.NAME as ARMARIO_ANDAR_3 " +
													"FROM((((EMP EMP INNER JOIN LENEL.UDFEMP UDFEMP ON EMP.ID = UDFEMP.ID) INNER JOIN LENEL.ARMARIOCA1 ARMARIOCA1 ON UDFEMP.ARMARIOCA1 = ARMARIOCA1.ID) INNER JOIN LENEL.ARMARIOANDAR1 ARMARIOANDAR1 ON UDFEMP.ARMARIOANDAR1 = ARMARIOANDAR1.ID) INNER JOIN LENEL.ARMARIOANDAR2 ARMARIOANDAR2 ON UDFEMP.ARMARIOANDAR2 = ARMARIOANDAR2.ID) INNER JOIN LENEL.ARMARIOCA2 ARMARIOCA2 ON UDFEMP.ARMARIOCA2 = ARMARIOCA2.ID " +
													"WHERE EMP.ID <> 0 AND(UDFEMP.ARMARIONUMERO1 <> ' ' OR UDFEMP.ARMARIONUMERO2 <> ' ') ");


							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" AND SSNO like '%{0}%' ORDER BY UDFEMP.ARMARIONUMERO1 ", txtEDV.Text);
							}
							else
							{
								sqlRelatorio += String.Format(" ORDER BY UDFEMP.ARMARIONUMERO1 ");
							}



							cmd.CommandText = sqlRelatorio;
							break;

						case "0":

							break;
					}

				}
				else if (rbtCadastroNumeroPessoa.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT EMP.LASTNAME as ULTIMO_NOME, " +
													"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
													"EMP.ID, " +
													"EMP.SSNO as EDV, " +
													"UDFEMP.ARMARIONUMERO1, " +
													"UDFEMP.ARMARIONUMERO2, " +
													"ARMARIOCA1.NAME as CA_1, " +
													"ARMARIOANDAR1.NAME as ARMARIO_ANDAR_1, " +
													"ARMARIOANDAR2.NAME as ARMARIO_ANDAR_2, " +
													"ARMARIOCA2.NAME as CA_2, " +
													"UDFEMP.OPHONE as TELEFONE, " +
													"UDFEMP.TERCRAMALTOMADO as RAMAL, " +
													"TERCSETOR.NAME as EMPRESA_TERCEIRA, " +
													"UDFEMP.TERCSETORTOMADO as SETOR_TOMADOR, " +
													"LOCATION.NAME as UNIDADE_ORGANIZACIONAL, " +
													"UDFEMP.EXT, " +
													"UDFEMP.TERCCCUSTO as CENTRO_CUSTO " +
													"FROM((((((EMP EMP INNER JOIN LENEL.UDFEMP UDFEMP ON EMP.ID = UDFEMP.ID) INNER JOIN LENEL.ARMARIOCA1 ARMARIOCA1 ON UDFEMP.ARMARIOCA1 = ARMARIOCA1.ID) INNER JOIN LENEL.ARMARIOANDAR1 ARMARIOANDAR1 ON UDFEMP.ARMARIOANDAR1 = ARMARIOANDAR1.ID) INNER JOIN LENEL.ARMARIOANDAR2 ARMARIOANDAR2 ON UDFEMP.ARMARIOANDAR2 = ARMARIOANDAR2.ID) INNER JOIN LENEL.ARMARIOCA2 ARMARIOCA2 ON UDFEMP.ARMARIOCA2 = ARMARIOCA2.ID) INNER JOIN LENEL.LOCATION LOCATION ON UDFEMP.LOCATION = LOCATION.ID) INNER JOIN LENEL.TERCSETOR TERCSETOR ON UDFEMP.TERCSETOR = TERCSETOR.ID " +
													"WHERE EMP.ID <> 0 AND(UDFEMP.ARMARIONUMERO2 <> ' ' OR UDFEMP.ARMARIONUMERO1 <> ' ') ");


							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" AND SSNO like '%{0}%' ORDER BY EMP.SSNO ", txtEDV.Text);
							}
							else
							{
								sqlRelatorio += String.Format(" ORDER BY EMP.SSNO ");
							}

							cmd.CommandText = sqlRelatorio;
							break;

						case "0":


							break;
					}

				}
				else if (rbtCartaoSobrenome.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT EMP.LASTNAME as ULTIMO_NOME, " +
													"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
													"EMP.MIDNAME, " +
													"EMP.ID as ID_FUNCIONARIO, " +
													"BADGE.ID as CARDNUM, " +
													"BADGSTAT.NAME, " +
													"BADGETYP.NAME, " +
													"BADGE.ACTIVATE as CARTAO_ATIVO, " +
													"BADGE.DEACTIVATE as CARTAO_DESATIVADO, " +
													"BADGE.STATUS as STATUS_CARTAO, " +
													"EMP.VISITOR, " +
													"BADGE.BADGEKEY, " +
													"EMP.SSNO as EDV " +
													"FROM((EMP EMP INNER JOIN BADGE BADGE ON EMP.ID = BADGE.EMPID) INNER JOIN BADGSTAT BADGSTAT ON BADGE.STATUS = BADGSTAT.ID) INNER JOIN BADGETYP BADGETYP ON BADGE.TYPE = BADGETYP.ID " +
													"WHERE EMP.ID <> 0 ");


							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" and SSNO like '%{0}%' ORDER BY EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID, BADGE.ID, BADGE.ISSUECODE ", txtEDV.Text);
							}
							else
							{
								sqlRelatorio += String.Format(" ORDER BY EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID, BADGE.ID, BADGE.ISSUECODE ");
							}

							cmd.CommandText = sqlRelatorio;

							break;

						case "0":
							string sqlRelatorioAT = string.Format("SELECT EMP.LASTNAME as ULTIMO_NOME, " +
													"EMP.FIRSTNAME as PRIMEIRO_NOME, " +
													"EMP.MIDNAME, " +
													"EMP.ID as ID_FUNCIONARIO, " +
													"BADGE.ID as CARDNUM, " +
													"BADGSTAT.NAME, " +
													"BADGETYP.NAME, " +
													"BADGE.ACTIVATE as CARTAO_ATIVO, " +
													"BADGE.DEACTIVATE as CARTAO_DESATIVADO, " +
													"BADGE.STATUS as STATUS_CARTAO, " +
													"EMP.VISITOR, " +
													"BADGE.BADGEKEY, " +
													"EMP.SSNO as EDV " +
													"FROM((EMP EMP INNER JOIN BADGE BADGE ON EMP.ID = BADGE.EMPID) INNER JOIN BADGSTAT BADGSTAT ON BADGE.STATUS = BADGSTAT.ID) INNER JOIN BADGETYP BADGETYP ON BADGE.TYPE = BADGETYP.ID " +
													"WHERE EMP.ID <> 0 ");



							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorioAT += String.Format(" AND SSNO like '%{0}%' ORDER BY EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID, BADGE.ID, BADGE.ISSUECODE ", txtEDV.Text);
							}
							else
							{
								sqlRelatorioAT += String.Format(" ORDER BY EMP.LASTNAME, EMP.FIRSTNAME, EMP.MIDNAME, EMP.ID, BADGE.ID, BADGE.ISSUECODE ");
							}

							cmd.CommandText = sqlRelatorioAT;
							break;
					}

				}
				else if (rbtHistoricoVisitas.Checked)
				{
					switch ((((Planta)comboBanco.SelectedItem).Oracle))
					{
						case "1":
							string sqlRelatorio = string.Format("SELECT EMP.LASTNAME as FUNCIONARIO_ULTIMO_NOME, " +
													"EMP.FIRSTNAME as FUNCIONARIO_PRIMEIRO_NOME, " +
													"EMP.MIDNAME, " +
													"VISIT.TIMEIN as TEMPO_REAL_ENTRADA, " +
													"VISIT.TIMEOUT as TEMPO_REAL_SAIDA, " +
													"VISIT.PURPOSE as FINALIDADE, " +
													"EMP_VISITOR.LASTNAME as VISITANTE_ULTIMO_NOME, " +
													"EMP_VISITOR.FIRSTNAME as VISITANTE_PRIMEIRO_NOME, " +
													"EMP_VISITOR.MIDNAME, " +
													"VISIT.VISITID, " +
													"VISIT_TYPE.NAME, " +
													"VISIT.SCHEDULED_TIMEOUT as TEMPO_LIMITE_AGENDADO, " +
													"VISIT.SCHEDULED_TIMEIN as HORARIO_AGENDADO, " +
													"EMP.SSNO as EDV " +
													"FROM((EMP EMP INNER JOIN VISIT VISIT ON EMP.ID = VISIT.EMPID) INNER JOIN EMP EMP_VISITOR ON VISIT.VISITORID = EMP_VISITOR.ID) " +
													"LEFT OUTER JOIN VISIT_TYPE VISIT_TYPE ON VISIT.TYPE = VISIT_TYPE.ID ");


							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorio += String.Format(" WHERE EMP.SSNO like '%{0}%' ORDER BY EMP_VISITOR.LASTNAME, EMP_VISITOR.FIRSTNAME, EMP.LASTNAME, EMP.FIRSTNAME, VISIT.SCHEDULED_TIMEIN DESC, VISIT.TIMEIN DESC ", txtEDV.Text);
							}
							else
							{
								sqlRelatorio += String.Format(" ORDER BY EMP_VISITOR.LASTNAME, EMP_VISITOR.FIRSTNAME, EMP.LASTNAME, EMP.FIRSTNAME, VISIT.SCHEDULED_TIMEIN DESC, VISIT.TIMEIN DESC ");
							}

							cmd.CommandText = sqlRelatorio;
							break;

						case "0":
							string sqlRelatorioAT = string.Format("SELECT EMP.LASTNAME as FUNCIONARIO_ULTIMO_NOME, " +
													"EMP.FIRSTNAME as FUNCIONARIO_PRIMEIRO_NOME, " +
													"EMP.MIDNAME, " +
													"VISIT.TIMEIN as TEMPO_REAL_ENTRADA, " +
													"VISIT.TIMEOUT as TEMPO_REAL_SAIDA, " +
													"VISIT.PURPOSE as FINALIDADE, " +
													"EMP_VISITOR.LASTNAME as VISITANTE_ULTIMO_NOME, " +
													"EMP_VISITOR.FIRSTNAME as VISITANTE_PRIMEIRO_NOME, " +
													"EMP_VISITOR.MIDNAME, " +
													"VISIT.VISITID, " +
													"VISIT_TYPE.NAME, " +
													"VISIT.SCHEDULED_TIMEOUT as TEMPO_LIMITE_AGENDADO, " +
													"VISIT.SCHEDULED_TIMEIN as HORARIO_AGENDADO, " +
													"EMP.SSNO as EDV " +
													"FROM((EMP EMP INNER JOIN VISIT VISIT ON EMP.ID = VISIT.EMPID) INNER JOIN EMP EMP_VISITOR ON VISIT.VISITORID = EMP_VISITOR.ID) " +
													"LEFT OUTER JOIN VISIT_TYPE VISIT_TYPE ON VISIT.TYPE = VISIT_TYPE.ID ");

							if (txtEDV.Text.Length > 0)
							{
								sqlRelatorioAT += String.Format(" WHERE EMP.SSNO like '%{0}%' ORDER BY EMP_VISITOR.LASTNAME, EMP_VISITOR.FIRSTNAME, EMP.LASTNAME, EMP.FIRSTNAME, VISIT.SCHEDULED_TIMEIN DESC, VISIT.TIMEIN DESC ", txtEDV.Text);
							}
							else
							{
								sqlRelatorioAT += String.Format(" ORDER BY EMP_VISITOR.LASTNAME, EMP_VISITOR.FIRSTNAME, EMP.LASTNAME, EMP.FIRSTNAME, VISIT.SCHEDULED_TIMEIN DESC, VISIT.TIMEIN DESC ");
							}

							cmd.CommandText = sqlRelatorioAT;
							break;
					}

				}



				//string sql = String.Format("SELECT * from emp where SSNO like '%%' and LASTCHANGED between to_date('29/08/2018 00:00:00', 'DD/MM/YYYY HH24:MI:SS') and to_date('29/08/2018 23:59:59', 'DD/MM/YYYY HH24:MI:SS') ",edvResultado,dtaInicial.Value,dtaFinal.Value);


				//cmd.CommandText = sql;

				conn.Open();

				cmd.Connection = conn;

				ada = new OdbcDataAdapter(cmd);
				ada.Fill(dt);

				if (dtaInicial.Value > dtaFinal.Value)
				{
					MessageBox.Show("Não se pode atribuir um valor superio a data final na data inicial");
				}
				else if (dt.Rows.Count <= 0)
				{
					MessageBox.Show("Essa Pesquisa não trouxe resultado");
				}
				else
				{
					if (dtaInicial.Enabled == true && dtaFinal.Enabled == true)
					{
						switch ((((Planta)comboBanco.SelectedItem).Oracle))
						{
							case "1":
								DataTable DtOrdenada = dt.AsEnumerable().OrderBy(r => DateTime.Parse(r.Field<string>("DATA_HORA_EVENTO"))).CopyToDataTable();
								dt = DtOrdenada;
								break;

							case "0":
								DataTable DtOrdenadaAT = dt.AsEnumerable().OrderBy(r => (r.Field<DateTime>("DATA_HORA_EVENTO"))).CopyToDataTable();
								dt = DtOrdenadaAT;
								break;

						}

						foreach (DataRow item in dt.Rows)
						{

							DateTime dataEvento = Convert.ToDateTime(item["DATA_HORA_EVENTO"].ToString());

							bool intervaloDeDatasValido = ConfereIntervaloData(dataEvento);


							if (CheckHorarioDeVerao(dataEvento) && (intervaloDeDatasValido == true))
							{
								item["DATA_HORA_EVENTO"] = dataEvento.AddHours(1);
							}
							else
							{
								item.Delete();
							}

						}
					}

					frmGrid c = new frmGrid();
					c.lblLocal.Text = ((Planta)comboBanco.SelectedItem).Local;
					c.lblConexao.Text = groupBox1.Controls.OfType<RadioButton>().SingleOrDefault(rad => rad.Checked == true).Text;
					c.Text = c.lblLocal.Text + " " + c.lblConexao.Text;
					c.grdRelatorio.DataSource = dt;
					c.grdRelatorio.Refresh();
					c.ShowDialog();
					
					conn.Close();
				}
			}
			catch (System.Data.Odbc.OdbcException)
			{
				MessageBox.Show("A conexão com o Banco de dados correto não esta sendo atribuida ");
			}
			catch (System.ArgumentException)
			{
				MessageBox.Show("Não existe uma conexão valida ");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());

			} 
		}

		private void rbtIdentificacaoAtivadas_CheckedChanged(object sender, EventArgs e)
		{

			ConfereRelatorioData();
		}

		private void rbtCrachaSemAcesso_CheckedChanged(object sender, EventArgs e)
		{

			ConfereRelatorioData();
		}

		private void rbtCadastroArmarioIncompleto_CheckedChanged(object sender, EventArgs e)
		{
			ConfereRelatorioData();
		}

		private void rbtCadastroArmarioNumero_CheckedChanged(object sender, EventArgs e)
		{

			ConfereRelatorioData();
		}

		private void rbtCadastroNumeroPessoa_CheckedChanged(object sender, EventArgs e)
		{

			ConfereRelatorioData();
		}

		private void rbtCartaoSobrenome_CheckedChanged(object sender, EventArgs e)
		{
			ConfereRelatorioData();
		}

		private void rbtHistoricoVisitas_CheckedChanged(object sender, EventArgs e)
		{
			ConfereRelatorioData();
		}

		public bool CheckHorarioDeVerao(DateTime dataEvento)
		{
			int MesInicial = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MesInicial"]);
			int SemanaInicial = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SemanaInicial"]);

			//int MesInicial = 10;
			//int SemanaInicial = 3;

			int MesFinal = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["MesFinal"]);
			int SemanaFinal = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SemanaFinal"]);


            //int MesFinal = 2;
            //int SemanaFinal = 3;

            //SemanaInicial++;
            //SemanaFinal++;

            int SemanaEvento = GetWeekInMonth(dataEvento);
            int AnoEvento = dataEvento.Year;
			int AnoEventoInicial = AnoEvento;
			int AnoEventoFinal = AnoEvento;

			if (dataEvento.Month >= MesInicial)
				AnoEventoFinal = AnoEvento + 1;

			if (dataEvento.Month <= MesFinal)
				AnoEventoInicial = AnoEvento - 1;

			DateTime Inicio = new DateTime(AnoEventoInicial, MesInicial, 01, 0, 0, 0);
            DateTime DataInicioHorarioDeVerao = LastDayOfWeek(Inicio, SemanaInicial);

            DateTime Final = new DateTime(AnoEventoFinal, MesFinal, 01, 0, 0, 0);
            DateTime DataFinalHorarioDeVerao = LastDayOfWeek(Final, SemanaFinal);

            //MessageBox.Show(DataInicioHorarioDeVerao.ToString());
            //MessageBox.Show(DataFinalHorarioDeVerao.ToString());

            if ((dataEvento >= DataInicioHorarioDeVerao) && (dataEvento <= DataFinalHorarioDeVerao))
            {
                //MessageBox.Show("true");
                return true;
            }

            //MessageBox.Show("false");
            return false;
        }

        private static DateTime FirstDayOfWeek(DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            var diff = dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
            if (diff < 0)
                diff += 7;
            return dt.AddDays(-diff).Date;
        }

        private static DateTime LastDayOfWeek(DateTime dt, int Semanas)
        {
			int count = 0;
			for (int i = 0; i < 31; i++)
			{
				DateTime result = dt.AddDays(i);
				
				if (result.DayOfWeek == DayOfWeek.Sunday)
					count++;
				if (count == Semanas)
					return result;
			}
			return dt;

			//DateTime result = FirstDayOfWeek(dt).AddDays(7 * Semanas);
			//if (GetWeekInMonth(result) == Semanas)
			//	return result;
			//else if (GetWeekInMonth(result) > Semanas)
			//	return result.AddDays(-7);
			//else 
			//	return result.AddDays(+7);
		}

        public static int GetWeekInMonth(DateTime date)
		{
			DateTime tempdate = date.AddDays(-date.Day + 1);
			CultureInfo ciCurr = CultureInfo.CurrentCulture;
			int weekNumStart = ciCurr.Calendar.GetWeekOfYear(tempdate, CalendarWeekRule.FirstFourDayWeek, ciCurr.DateTimeFormat.FirstDayOfWeek);
			int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, ciCurr.DateTimeFormat.FirstDayOfWeek);
			return weekNum - weekNumStart + 1;
		
		}

		public bool ConfereIntervaloData(DateTime dataEvento)
		{
		
			var evento = dataEvento.Date;
			var inicioDoIntervaloData = dtaInicial.Value;
			var finalDoIntervaloData = dtaFinal.Value;
		
			if((evento >= inicioDoIntervaloData) && (evento <= finalDoIntervaloData)){
				return true;
			}

			return false;

		}

	


	}

	}

