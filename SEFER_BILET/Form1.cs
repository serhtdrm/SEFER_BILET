using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Cryptography;

namespace SEFER_BILET
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        void temizle()
        {
            txtad.Text = "";
            txtsoyad.Text = "";
            msktel.Text = "";
            msktc.Text = "";
            cmbcinsiyet.Text = "";
            mskmaıl.Text = "";
            mskkaptanno.Text = "";
            txtkaptanad.Text = "";
            mskkaptantel.Text = "";
            txtkalkış.Text = "";
            txtvarıs.Text = "";
            msktarıh.Text = "";
            msksaat.Text = "";
            mskkaptan.Text = "";
            txtfiyat.Text = "";
            txtsefernumara.Text = "";
            mskyolcutc.Text = "";
            txtkoltukno.Text = "";
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=SerhatDemir\SQLEXPRESS;Initial Catalog=BiletOto;Integrated Security=True;");
        private void btnkaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLYOLCU(AD,SOYAD,TELEFON,TC,CINSIYET,MAIL) values(@P1,@P2,@P3,@P4,@P5,@P6)", baglanti);
            komut.Parameters.AddWithValue("@P1", txtad.Text);
            komut.Parameters.AddWithValue("@P2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@P3", msktel.Text);
            komut.Parameters.AddWithValue("@P4", msktc.Text);
            komut.Parameters.AddWithValue("@P5", cmbcinsiyet.Text);
            komut.Parameters.AddWithValue("@P6", mskmaıl.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("YOLCU BİLGİLERİ EKLENMİŞTİR!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void btnkaptan_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLKAPTAN(KAPTANNO,ADSOYAD,TELEFON) VALUES(@P1,@P2,@P3) ", baglanti);
            komut.Parameters.AddWithValue("@p1", mskkaptanno.Text);
            komut.Parameters.AddWithValue("@p2", txtkaptanad.Text);
            komut.Parameters.AddWithValue("@p3", mskkaptantel.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KAPTAN BİLGİLERİ EKLENMİŞTİR!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void btnseferolus_Click(object sender, EventArgs e)
        {


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        void seferlistesi()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from TBLSEFERBİL ", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;


        }
        void koltuk()
        {
            baglanti.Open();



            SqlCommand komut = new SqlCommand("select *from TBLSEFERDETA inner join TBLYOLCU ON TBLSEFERDETA.YOLCUTC=TBLYOLCU.TC WHERE SEFERNO=@P1", baglanti);

            komut.Parameters.AddWithValue("@P1", txtsefernumara.Text);

            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {

                foreach (Control btn in groupBox1.Controls)
                {
                    if (btn.Name == "btn" + dr[3].ToString())
                    {
                        if (dr[9].ToString() == "ERKEK")
                        {
                            btn.Enabled = false;
                            btn.BackColor = Color.Blue;
                        }
                        else
                        {
                            btn.Enabled = false;
                            btn.BackColor = Color.Purple;
                        }
                    }
                }
            }
            baglanti.Close();
        }

        void yolculistesi()
        {
            SqlDataAdapter da1 = new SqlDataAdapter("Select * from TBLYOLCU ", baglanti);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            dataGridView2.DataSource = dt1;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'biletOtoDataSet.TBLYOLCU' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.tBLYOLCUTableAdapter.Fill(this.biletOtoDataSet.TBLYOLCU);
            seferlistesi();
            yolculistesi();
        }

        private void btnseferolustur_Click_1(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLSEFERBİL(KALKIS,VARIS,TARIH,SAAT,KAPTAN,FIYAT) VALUES(@P1,@P2,@P3,@P4,@P5,@P6)", baglanti);
            komut.Parameters.AddWithValue("@p1", txtkalkış.Text);
            komut.Parameters.AddWithValue("@p2", txtvarıs.Text);
            komut.Parameters.AddWithValue("@p3", msktarıh.Text);
            komut.Parameters.AddWithValue("@p4", msksaat.Text);
            komut.Parameters.AddWithValue("@p5", mskkaptan.Text);
            komut.Parameters.AddWithValue("@p6", txtfiyat.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("KAPTAN BİLGİLERİ EKLENMİŞTİR!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtkoltukno.Text = "";
            foreach (Control btn in groupBox1.Controls)
            {
                btn.Enabled = true;
                btn.BackColor = SystemColors.ScrollBar;
            }
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtsefernumara.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            koltuk();
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "5";

        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtkoltukno.Text = "9";
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            mskyolcutc.Text = dataGridView2.Rows[secilen].Cells[4].Value.ToString();
        }
        bool tumkoltukdurum;
        private void btnbilet_Click(object sender, EventArgs e)
        {
            /*baglanti.Open();/*
            SqlCommand komut = new SqlCommand("insert into TBLSEFERDETA(SEFERNO,YOLCUTC,KOLTUK) VALUES(@P1,@P2,@P3) ",baglanti);
            komut.Parameters.AddWithValue("@p1", txtsefernumara.Text);
            komut.Parameters.AddWithValue("@p2",mskyolcutc.Text);
            komut.Parameters.AddWithValue("@p3", txtkoltukno.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("BİLET KESİLMİŞTİR!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();*/
            //Yolcunun kayıtlı olup olmadığını kontrol et
            foreach (Control cntrl2 in groupBox1.Controls)
            {
                if (cntrl2 is Button)
                {
                    if (cntrl2.Enabled == false)
                    {
                        tumkoltukdurum = true;
                    }
                    else
                    {
                        tumkoltukdurum = false;
                        break;
                    }
                }
            }
            if (tumkoltukdurum == false)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into TBLSEFERDETA(SEFERNO,YOLCUTC,KOLTUK) VALUES(@P1,@P2,@P3) ", baglanti);
                komut.Parameters.AddWithValue("@p1", txtsefernumara.Text);
                komut.Parameters.AddWithValue("@p2", mskyolcutc.Text);
                komut.Parameters.AddWithValue("@p3", txtkoltukno.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("BİLET KESİLMİŞTİR!", "BİLGİ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
            else
            {
                MessageBox.Show("tüm koltuklar dolu");
            }
            koltuk();
            txtkoltukno.Text = "";
    
            
            
           
        }
    }
}
