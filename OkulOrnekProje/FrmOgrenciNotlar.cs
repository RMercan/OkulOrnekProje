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

namespace OkulOrnekProje
{
    public partial class FrmOgrenciNotlar : Form
    {
        public FrmOgrenciNotlar()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-B4L6VS5\SQLEXPRESS;Initial Catalog=OkulDb;Integrated Security=True");

        public string numara;
        private void FrmOgrenciNotlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT DERSAD, SINAV1,SINAV2,SINAV3, PROJE, ORTALAMA, DURUM FROM TBL_NOTLAR INNER JOIN TBL_DERSLER ON TBL_NOTLAR.DERSID=TBL_DERSLER.DERSID WHERE OGRID=@P1", baglanti);
            komut.Parameters.AddWithValue("@P1", numara);
            //this.Text = numara.ToString();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

            //SAYFA BAŞLIĞI ÖĞRENCİ ADI OLSUN
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select OGRAD, OGRSOYAD from TBL_OGRENCILER WHERE OGRID=@F1", baglanti);
            komut1.Parameters.AddWithValue("@F1", numara);
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read())
            {
                this.Text = dr1[0] + " " + dr1[1].ToString();
            }
            baglanti.Close();
        }
    }
}
