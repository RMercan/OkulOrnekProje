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
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-B4L6VS5\SQLEXPRESS;Initial Catalog=OkulDb;Integrated Security=True");

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_KULUPLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbKulup.DisplayMember = "KULUPAD";
            CmbKulup.ValueMember = "KULUPID";
            CmbKulup.DataSource = dt;
            baglanti.Close();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        string c = "";
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            ds.OgrenciEkle(TxtAd.Text, TxtSoyad.Text, byte.Parse(CmbKulup.SelectedValue.ToString()), c);
            MessageBox.Show("Öğrenci ekleme yapıldı.");
        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }

        private void CmbKulup_SelectedIndexChanged(object sender, EventArgs e)
        {
           // TxtOgrenciID.Text = CmbKulup.SelectedValue.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(TxtOgrenciID.Text));
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            TxtOgrenciID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            c = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            if (c == "KIZ")
            {
                radioButton1.Checked = true;
            }
            if (c == "ERKEK")
            {
                radioButton2.Checked = true;
            }
            CmbKulup.Text= dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                c = "KIZ";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                c = "ERKEK";
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(TxtAd.Text, TxtSoyad.Text, byte.Parse(CmbKulup.SelectedValue.ToString()),c , int.Parse(TxtOgrenciID.Text));
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciGetir(TxtAra.Text);
        }
    }
}
