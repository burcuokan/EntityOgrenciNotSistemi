using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOgrenciNotSistemi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbSınavOgrenciEntities db = new DbSınavOgrenciEntities();
        private void BtnOgrenciListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.ToList();
        }

        private void BtnDersListesi_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLDERSLER.ToList();
        }

        private void BtnNotListesi_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.NOTLISTESI();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            TBLOGRENCI t = new TBLOGRENCI();
            t.AD = TxtAd.Text;
            t.SOYAD = TxtSoyad.Text;
            db.TBLOGRENCI.Add(t);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Listeye Eklenmiştir", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciıd.Text);
            var x = db.TBLOGRENCI.Find(id);
            db.TBLOGRENCI.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Sistemden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(TxtOgrenciıd.Text);
            var x = db.TBLOGRENCI.Find(id);
            x.AD = TxtAd.Text;
            x.SOYAD = TxtSoyad.Text;
            x.FOTOGRAF = TxtFotograf.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Bilgileri Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.TBLOGRENCI.Where(x => x.AD == TxtAd.Text).ToList();
        }

        private void TxtAd_TextChanged(object sender, EventArgs e)
        {
            string aranan = TxtAd.Text;
            var degerler = from item in db.TBLOGRENCI
                           where item.AD.Contains(aranan) // içinde
                           select item;
            dataGridView1.DataSource = degerler.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked==true)
            {
                List<TBLOGRENCI> liste1 = db.TBLOGRENCI.OrderBy(p => p.AD).ToList();
                dataGridView1.DataSource = liste1;
            }

            if (radioButton2.Checked == true)
            {
                List<TBLOGRENCI> liste2 = db.TBLOGRENCI.OrderByDescending(p => p.AD).ToList();
                dataGridView1.DataSource = liste2;
            }

            if (radioButton3.Checked==true)
            {
                List<TBLOGRENCI> liste3 = db.TBLOGRENCI.OrderBy(p => p.ID).Take(3).ToList();
                dataGridView1.DataSource = liste3;
            }

            if (radioButton4.Checked==true)
            {
                List<TBLOGRENCI> liste4 = db.TBLOGRENCI.Where(p => p.ID == 5).ToList();
                dataGridView1.DataSource = liste4;
            }

            if (radioButton5.Checked==true)
            {
                List<TBLOGRENCI> liste5 = db.TBLOGRENCI.Where(p => p.AD.StartsWith("a")).ToList();
                dataGridView1.DataSource = liste5;
            }

            if (radioButton6.Checked==true)
            {
                List<TBLOGRENCI> liste6 = db.TBLOGRENCI.Where(p => p.AD.EndsWith("a")).ToList();
                dataGridView1.DataSource = liste6;
            }

            if (radioButton7.Checked==true)
            {
                bool deger = db.TBLOGRENCI.Any();
                MessageBox.Show(deger.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (radioButton8.Checked==true)
            {
                int toplam = db.TBLOGRENCI.Count();
                MessageBox.Show(toplam.ToString(), "Toplam Öğrenci Sayısı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (radioButton9.Checked==true)
            {
                var toplam = db.TBLNOTLAR.Sum(p => p.SINAV1);
                MessageBox.Show("Toplam Sınav1 Puanı:" + toplam.ToString());
            }

            if (radioButton10.Checked==true)
            {
                var ortalama = db.TBLNOTLAR.Average(p => p.SINAV1);
                MessageBox.Show("1. Sınavın Ortalaması:" + ortalama.ToString());
            }

            if (radioButton11.Checked == true)
            {
                var enyuksek = db.TBLNOTLAR.Max(p => p.SINAV1);
                MessageBox.Show("1. Sınavın En Yüksek Notu:" + enyuksek.ToString());
            }

            if (radioButton12.Checked == true)
            {
                var endusuk = db.TBLNOTLAR.Min(p => p.SINAV1);
                MessageBox.Show("1. En Düşük Notu:" + endusuk.ToString());
            }
        }

        private void BtnSınavNotuGuncelle_Click(object sender, EventArgs e)
        {

            int id = Convert.ToInt32(TxtNotid.Text);
            var x = db.TBLNOTLAR.Find(id);
            x.SINAV1 =short.Parse(TxtSınav1.Text);
            x.SINAV2 = short.Parse(TxtSınav2.Text);
            x.SINAV3 = short.Parse(TxtSınav3.Text);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Notları Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

           
        }
    }
}
