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
namespace TestTrigger
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=r1se\SQLEXPRESS;Initial Catalog=DbYeni;Integrated Security=True");
        void listele()
        {
            
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_Kitaplar",baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
           
        }
        void sayac()
        {
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("select * from tbl_Sayac",baglanti);
            SqlDataReader dr = komut1.ExecuteReader();
            while (dr.Read())
            {
                lblKitapAdet.Text = dr[0].ToString();
            }
            baglanti.Close();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
            sayac();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Kitaplar (Ad,Yazar,Sayfa,Yayınevi,Tur) values (@p1,@p2,@p3,@p4,@p5)",baglanti);
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtYazar.Text);
            komut.Parameters.AddWithValue("@p3",txtSayfa.Text);
            komut.Parameters.AddWithValue("@p4",txtYayınEvi.Text);
            komut.Parameters.AddWithValue("@p5",txtTur.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
            sayac();
            MessageBox.Show("Kitap Eklendi","Bilgi",MessageBoxButtons.OK);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtYayınEvi.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtTur.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut4 = new SqlCommand("delete from Tbl_Kitaplar where Id=@p0",baglanti);
            komut4.Parameters.AddWithValue("@p0",txtId.Text);
            komut4.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Silindi");
            listele();
            sayac();
        }
    }
}
