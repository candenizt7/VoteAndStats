using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Veri_Tabanli_Parti_Secim_Grafik_Istatistik
{
    public partial class FrmGrafikler : Form
    {
        public FrmGrafikler()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=CANDENIZ\SQLEXPRESS;Initial Catalog=DbSecimProje;Integrated Security=True");
        private void FrmGrafikler_Load(object sender, EventArgs e)
        {
            //İLÇE ADLARINI COMBOBOXA ÇEKME
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select ILCEAD from TBLILCE", baglanti);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr[0]);
            }
            baglanti.Close();

            //GRAFİĞE TOPLAM SONUÇLARI GETİRME
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select SUM(APARTI),SUM(BPARTI),SUM(CPARTI),SUM(DPARTI),SUM(EPARTI) FROM TBLILCE",baglanti);
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read())
            {
                chart1.Series["Partiler"].Points.AddXY("A PARTI", dr2[0]);
                chart1.Series["Partiler"].Points.AddXY("B PARTI", dr2[1]);
                chart1.Series["Partiler"].Points.AddXY("C PARTI", dr2[2]);
                chart1.Series["Partiler"].Points.AddXY("D PARTI", dr2[3]);
                chart1.Series["Partiler"].Points.AddXY("E PARTI", dr2[4]);
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLILCE where ILCEAD=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", comboBox1.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read())
            {
                int s1, s2, s3, s4, s5;
                s1 = int.Parse(dr[2].ToString());
                s2 = int.Parse(dr[3].ToString());
                s3 = int.Parse(dr[4].ToString());
                s4 = int.Parse(dr[5].ToString());
                s5 = int.Parse(dr[6].ToString());

                progressBar1.Maximum = s1 + s2 + s3 + s4 + s5;
                progressBar2.Maximum = s1 + s2 + s3 + s4 + s5;
                progressBar3.Maximum = s1 + s2 + s3 + s4 + s5;
                progressBar4.Maximum = s1 + s2 + s3 + s4 + s5;
                progressBar5.Maximum = s1 + s2 + s3 + s4 + s5;

                progressBar1.Value = int.Parse(dr[2].ToString());
                progressBar2.Value = int.Parse(dr[3].ToString());
                progressBar3.Value = int.Parse(dr[4].ToString());
                progressBar4.Value = int.Parse(dr[5].ToString());
                progressBar5.Value = int.Parse(dr[6].ToString());

                lblA.Text = dr[2].ToString();
                lblB.Text = dr[3].ToString();
                lblC.Text = dr[4].ToString();
                lblD.Text = dr[5].ToString();
                lblE.Text = dr[6].ToString();
            }
            baglanti.Close();
        }
    }
}
