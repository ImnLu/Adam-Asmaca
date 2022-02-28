using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Adam_Asmaca
{
    public partial class AdamAsmaca : Form
    {
        public AdamAsmaca()
        {
            InitializeComponent();
        }

        int kalanhak = 8; //Kalan hak için değişken.
        int copadam = 0; //Çöp adam için değişken.
        int sonkontrol = 0; //Kazanıp kazanmamayı kontrol eden değişken.
        string secilenkelime; //Seçilen kelime değişkeni.
        char[] ayrilan; //Kelimeyi harflere ayırdığımız char dizisi.

        public void OyunuBaslat()
        {
            //Metin dosyasını okuyup diziye kaydediyor.
            TextReader tReader = new StreamReader("kelimeler.txt");
            string okunan = tReader.ReadToEnd();
            string[] satirlar = okunan.Split('\n');
            tReader.Close();

            //Metin dosyası içinden rastgele kelime çekiyor.
            Random rastgele = new Random();
            int rastgelesayi = rastgele.Next(1, satirlar.Length + 1);
            secilenkelime = satirlar[rastgelesayi].ToString();

            //kelimeyazdir metodunu çağırıyoruz.
            kelimeyazdir();

            //Kelimeyi ayırma.
            ayrilan = secilenkelime.ToCharArray();
        }

        public void AdamAsmaca_Load(object sender, EventArgs e)
        {
            //Eğer kullanıcı daha önceden pencere boyutunu ayarladıysa otomatik olarak önceki konumuna getiriyor.
            if (File.Exists("ayarlar.ini"))
            {
                TextReader tReader = new StreamReader("ayarlar.ini");
                string okunan = tReader.ReadToEnd();
                string[] satirlar = okunan.Split(' ');
                tReader.Close();

                if (satirlar[2] == "Tam")
                {
                    this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                }
            }

            //Eğer kelimeler dosyası yoksa internetten indirmesini sağlıyor.
            if (!File.Exists("kelimeler.txt"))
            {
                this.Hide();
                KelimeIndir klmindir = new KelimeIndir();
                klmindir.ShowDialog();
            }
            else
            {
                OyunuBaslat(); //Oyunu başlatıyor.
            }

            tabkapat(); //Tabla geçiş özelliğini kapatan metodu çağırıyoruz.
        }

        private void kelimeyazdir()
        {
            //Pointli label oluşturuyoruz.
            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                Label lbl = new Label();
                Point lblkonum = new Point(380 + (30 * i), 80);
                lbl.Location = lblkonum;
                lbl.Name = "labelHarf" + i;
                lbl.Text = "_";
                lbl.AutoSize = true;
                this.Controls.Add(lbl);
            }
        }

        private void kontrol()
        {
            //Kalan hakkı labela yazdırıyoruz.
            labelKalanHak.Text = kalanhak.ToString();

            //Elenmemizi ya da kazanmamızı kontrol eden sistem.
            if (copadam == 1)
            {
                pictureBox4.Visible = true;
            }
            if (copadam == 2)
            {
                pictureBox5.Visible = true;
            }
            if (copadam == 3)
            {
                pictureBox6.Visible = true;
            }
            if (copadam == 4)
            {
                pictureBox7.Visible = true;
            }
            if (copadam == 5)
            {
                pictureBox8.Visible = true;
            }
            if (copadam == 6)
            {
                pictureBox9.Visible = true;
            }
            if (copadam == 7)
            {
                pictureBox3.Visible = true;
                pictureBox2.Visible = true;
            }
            if (copadam == 8)
            {
                pictureBox15.Visible = true;
                MessageBox.Show("Oyunu kaybettiniz!" + "\n" + "Kelime: " + secilenkelime);
                Application.Restart();
            }
            if (sonkontrol == secilenkelime.Length - 1)
            {
                MessageBox.Show("Oyunu kazandınız!");
                Application.Restart();
            }
        }

        private void tabkapat()
        {
            //Butonların tabla geçişini engelliyorum.
            buttonA.TabStop = false;
            buttonB.TabStop = false;
            buttonC.TabStop = false;
            buttonÇ.TabStop = false;
            buttonD.TabStop = false;
            buttonE.TabStop = false;
            buttonF.TabStop = false;
            buttonG.TabStop = false;
            buttonĞ.TabStop = false;
            buttonH.TabStop = false;
            buttonI.TabStop = false;
            buttonİ.TabStop = false;
            buttonJ.TabStop = false;
            buttonK.TabStop = false;
            buttonL.TabStop = false;
            buttonM.TabStop = false;
            buttonN.TabStop = false;
            buttonO.TabStop = false;
            buttonÖ.TabStop = false;
            buttonP.TabStop = false;
            buttonR.TabStop = false;
            buttonS.TabStop = false;
            buttonŞ.TabStop = false;
            buttonT.TabStop = false;
            buttonU.TabStop = false;
            buttonÜ.TabStop = false;
            buttonV.TabStop = false;
            buttonY.TabStop = false;
            buttonZ.TabStop = false;
        }

        //Tüm butonlar için harf kontrol aracı. (Örnek olarak buttonA'da gösterildi.)
        public void buttonA_Click(object sender, EventArgs e)
        {
            buttonA.Enabled = false; //Butonu deaktive ediyor.
            int icindekontrol = 0; //Harf kontrol değişkeni.

            //Kelimenin tüm harflerini kontrol eden sistem.
            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "a")
                {
                    //Eğer harfi bulursa harfle ilgili labelı bulup Textini değiştiriyor.
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "A";
                    sonkontrol++;
                    buttonA.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }

            //Eğer içinde ilgili harfle eşleşen hiç harf yoksa çöp adamın bir kısmı çiziliyor.
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonA.BackColor = Color.Crimson;
            }
            kontrol(); //Kontrol metodu çağırılıyor.
        }

        private void buttonB_Click(object sender, EventArgs e)
        {
            buttonB.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "b")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "B";
                    sonkontrol++;
                    buttonB.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonB.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            buttonC.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "c")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "C";
                    sonkontrol++;
                    buttonC.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonC.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonÇ_Click(object sender, EventArgs e)
        {
            buttonÇ.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "ç")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "Ç";
                    sonkontrol++;
                    buttonÇ.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonÇ.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonD_Click(object sender, EventArgs e)
        {
            buttonD.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "d")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "D";
                    sonkontrol++;
                    buttonD.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonD.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonE_Click(object sender, EventArgs e)
        {
            buttonE.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "e")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "E";
                    sonkontrol++;
                    buttonE.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonE.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonF_Click(object sender, EventArgs e)
        {
            buttonF.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "f")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "F";
                    sonkontrol++;
                    buttonF.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonF.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonG_Click(object sender, EventArgs e)
        {
            buttonG.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "g")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "G";
                    sonkontrol++;
                    buttonG.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonG.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonĞ_Click(object sender, EventArgs e)
        {
            buttonĞ.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "ğ")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "Ğ";
                    sonkontrol++;
                    buttonĞ.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonĞ.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonH_Click(object sender, EventArgs e)
        {
            buttonH.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "h")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "H";
                    sonkontrol++;
                    buttonH.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonH.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonI_Click(object sender, EventArgs e)
        {
            buttonI.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "ı")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "I";
                    sonkontrol++;
                    buttonI.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonI.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonİ_Click(object sender, EventArgs e)
        {
            buttonİ.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "i")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "İ";
                    sonkontrol++;
                    buttonİ.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonİ.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonJ_Click(object sender, EventArgs e)
        {
            buttonJ.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "j")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "J";
                    sonkontrol++;
                    buttonJ.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonJ.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonK_Click(object sender, EventArgs e)
        {
            buttonK.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "k")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "K";
                    sonkontrol++;
                    buttonK.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonK.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonL_Click(object sender, EventArgs e)
        {
            buttonL.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "l")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "L";
                    sonkontrol++;
                    buttonL.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonL.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonM_Click(object sender, EventArgs e)
        {
            buttonM.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "m")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "M";
                    sonkontrol++;
                    buttonM.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonM.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonN_Click(object sender, EventArgs e)
        {
            buttonN.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "n")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "N";
                    sonkontrol++;
                    buttonN.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonN.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonO_Click(object sender, EventArgs e)
        {
            buttonO.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "o")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "O";
                    sonkontrol++;
                    buttonO.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonO.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonÖ_Click(object sender, EventArgs e)
        {
            buttonÖ.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "ö")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "Ö";
                    sonkontrol++;
                    buttonÖ.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonÖ.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonP_Click(object sender, EventArgs e)
        {
            buttonP.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "p")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "P";
                    sonkontrol++;
                    buttonP.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonP.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonR_Click(object sender, EventArgs e)
        {
            buttonR.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "r")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "R";
                    sonkontrol++;
                    buttonR.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonR.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonS_Click(object sender, EventArgs e)
        {
            buttonS.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "s")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "S";
                    sonkontrol++;
                    buttonS.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonS.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonŞ_Click(object sender, EventArgs e)
        {
            buttonŞ.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "ş")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "Ş";
                    sonkontrol++;
                    buttonŞ.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonŞ.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonT_Click(object sender, EventArgs e)
        {
            buttonT.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "t")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "T";
                    sonkontrol++;
                    buttonT.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonT.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonU_Click(object sender, EventArgs e)
        {
            buttonU.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "u")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "U";
                    sonkontrol++;
                    buttonU.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonU.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonÜ_Click(object sender, EventArgs e)
        {
            buttonÜ.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "ü")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "Ü";
                    sonkontrol++;
                    buttonÜ.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonÜ.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonV_Click(object sender, EventArgs e)
        {
            buttonV.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "v")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "V";
                    sonkontrol++;
                    buttonV.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonV.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonY_Click(object sender, EventArgs e)
        {
            buttonY.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "y")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "Y";
                    sonkontrol++;
                    buttonY.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonY.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void buttonZ_Click(object sender, EventArgs e)
        {
            buttonZ.Enabled = false;
            int icindekontrol = 0;

            for (int i = 0; i < secilenkelime.Length - 1; i++)
            {
                if (ayrilan[i].ToString() == "z")
                {
                    Label lbl = this.Controls.Find("labelHarf" + i, true).FirstOrDefault() as Label;
                    lbl.Text = "Z";
                    sonkontrol++;
                    buttonZ.BackColor = Color.LimeGreen;
                }
                else
                {
                    icindekontrol++;
                }
            }
            if (icindekontrol == secilenkelime.Length - 1)
            {
                copadam++;
                kalanhak--;
                buttonZ.BackColor = Color.Crimson;
            }
            kontrol();
        }

        private void AdamAsmaca_KeyDown(object sender, KeyEventArgs e)
        {
            //Tuşlara basılınca da butona basılmasını sağlıyor.
            if (e.KeyCode == Keys.A)
            {
                buttonA.PerformClick();
            }

            if (e.KeyCode == Keys.B)
            {
                buttonB.PerformClick();
            }

            if (e.KeyCode == Keys.C)
            {
                buttonC.PerformClick();
            }

            if (e.KeyCode == Keys.Oem5)
            {
                buttonÇ.PerformClick();
            }

            if (e.KeyCode == Keys.D)
            {
                buttonD.PerformClick();
            }

            if (e.KeyCode == Keys.E)
            {
                buttonE.PerformClick();
            }

            if (e.KeyCode == Keys.F)
            {
                buttonF.PerformClick();
            }

            if (e.KeyCode == Keys.G)
            {
                buttonG.PerformClick();
            }

            if (e.KeyCode == Keys.OemOpenBrackets)
            {
                buttonĞ.PerformClick();
            }

            if (e.KeyCode == Keys.H)
            {
                buttonH.PerformClick();
            }

            if (e.KeyCode == Keys.I)
            {
                buttonI.PerformClick();
            }

            if (e.KeyCode == Keys.Oem7)
            {
                buttonİ.PerformClick();
            }

            if (e.KeyCode == Keys.J)
            {
                buttonJ.PerformClick();
            }

            if (e.KeyCode == Keys.K)
            {
                buttonK.PerformClick();
            }

            if (e.KeyCode == Keys.L)
            {
                buttonL.PerformClick();
            }

            if (e.KeyCode == Keys.M)
            {
                buttonM.PerformClick();
            }

            if (e.KeyCode == Keys.N)
            {
                buttonN.PerformClick();
            }

            if (e.KeyCode == Keys.O)
            {
                buttonO.PerformClick();
            }

            if (e.KeyCode == Keys.OemQuestion)
            {
                buttonÖ.PerformClick();
            }

            if (e.KeyCode == Keys.P)
            {
                buttonP.PerformClick();
            }

            if (e.KeyCode == Keys.R)
            {
                buttonR.PerformClick();
            }

            if (e.KeyCode == Keys.S)
            {
                buttonS.PerformClick();
            }

            if (e.KeyCode == Keys.Oem1)
            {
                buttonŞ.PerformClick();
            }

            if (e.KeyCode == Keys.T)
            {
                buttonT.PerformClick();
            }

            if (e.KeyCode == Keys.U)
            {
                buttonU.PerformClick();
            }

            if (e.KeyCode == Keys.Oem6)
            {
                buttonÜ.PerformClick();
            }

            if (e.KeyCode == Keys.V)
            {
                buttonV.PerformClick();
            }

            if (e.KeyCode == Keys.Y)
            {
                buttonY.PerformClick();
            }

            if (e.KeyCode == Keys.Z)
            {
                buttonZ.PerformClick();
            }
        }

        private void AdamAsmaca_SizeChanged(object sender, EventArgs e)
        {
            //Pencere boyutunu dosyada saklıyor.
            if (this.WindowState.ToString() == "Normal")
            {
                StreamWriter yaz = new StreamWriter("ayarlar.ini");
                yaz.Write("Pencere Boyutu: Normal");
                yaz.Close();
            }
            if (this.WindowState.ToString() == "Maximized")
            {
                StreamWriter yaz = new StreamWriter("ayarlar.ini");
                yaz.Write("Pencere Boyutu: Tam Ekran");
                yaz.Close();
            }
        }
    }
}
