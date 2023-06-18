using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Piggy_Bank_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }



        public static List<double> kumbara_paraMiktarlari = new List<double>();

        public static double bank_volume = 50000;//mm

        public static bool repair = false;
        public static bool broken = false;
        public static bool again = false;

        public Dictionary<double, (double, double, double)> Banknotes = new Dictionary<double, (double, double, double)>
        {

                  { 5.00, (64, 130, 0.25) },
                  { 10.00, (64, 136, 0.25) },
                  { 20.00, (68, 142, 0.25) },
                  { 50.00, (68, 148, 0.25) },
                  { 100.00, (72, 154, 0.25) },
                  { 200.00, (72, 160, 0.25) }
        };
        public Dictionary<double, (double, double)> Coins = new Dictionary<double, (double, double)>
        {

                  {0.01, (16.50,0.25)},
                  {0.05, (17.50,0.25)},
                  {0.10, (18.50,0.25)},
                  {0.25, (20.50,0.25)},
                  {0.50, (23.85,0.25)},
                  {1.00, (26.15,0.25)},
        };
        Dictionary<double, string> Names = new Dictionary<double, string>
        {

                  {0.01, "1 Kuruş"},
                  {0.05, "5 Kuruş"},
                  {0.10, "10 Kuruş"},
                  {0.25, "25 Kuruş"},
                  {0.50, "50 Kuruş"},
                  {1.00, "1 Lira"},
                  { 5.00, "5 Lira"},
                  { 10.00, "10 Lira" },
                  { 20.00, "20 Lira" },
                  { 50.00,"50 Lira" },
                  { 100.00, "100 Lira" },
                  { 200.00, "200 Lira" }
        };



        /*TL ÖZELLİKLERİ

        5TL -	64 x 130 mm

        10 TL - 64 x 136 mm

        20 TL - 68 x 142 mm

        50 TL - 68 x 148 mm

        100 TL - 72 x 154 mm

        200 TL - 72 x 160 mm

        TCMB'sının 1.tertip banknotları esas alınmıştır.
        Yükseklik Değerleri sabit 0,25 mm olarak alınmıştır.
        https://www.tcmb.gov.tr/wps/wcm/connect/TR/TCMB+TR/Main+Menu/Banknotlar/Dolasimdaki+Banknotlar/1.Tertip+Banknotlar/
        */
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Put_money_form ikinciForm = new Put_money_form();
            form_olustur(ikinciForm);


        }

        public void form_olustur(Form form)
        {


            form.MdiParent = this;
            panel1.Controls.Add(form);

            form.Show();

        }


        public double calc(List<double> list, double percentage = 0)
        {

            double total_volume = 0;
            double pi = 3.14;
            foreach (var item in list)
            {
                if (percentage == 0)
                {
                    percentage = (new Random().NextDouble() * 50 + 25) / 100;
                }
                if (Banknotes.ContainsKey(item))
                {
                    (double width, double height, double depth) = Banknotes[item];

                    double volume = (width) * (height) * (depth);

                    total_volume += volume + (volume * percentage);
                }
                else if (Coins.ContainsKey(item))
                {
                    (double radius, double height) = Coins[item];

                    double volume = pi * (radius * radius) * height;

                    total_volume += volume + (volume * percentage);
                }


            }

            return total_volume;
        }

        private void shake_button_Click(object sender, EventArgs e)
        {

            if (broken == true && repair == false || again)
            {
                MessageBox.Show("Kırılmış bir kumbaraya işlem yapılamaz.");
                return;
            }



            double total_volume = calc(kumbara_paraMiktarlari, 0.25);

            bank_volume = 50000 - total_volume;

            MessageBox.Show("Kumbarayı Salladınız.Kumbaranızın boş hacmi artmıştır.");
        }

        private void break_func()
        {
            List<double> units = new List<double> { 0.01, 0.05, 0.10, 0.25, 0.50, 1.00, 5.00, 10.00, 20.00, 50.00, 100.00, 200.00 };
            string text = "Kumbaranızdan; \n";

            foreach (var item in units)
            {
                int count = kumbara_paraMiktarlari.Count(x => x.Equals(item));
                if (count != 0)
                {
                    text += count.ToString() + " Tane " + Names[item] + "\n";
                }


            }

            text += "Olmak Üzere \nToplamda " + kumbara_paraMiktarlari.Sum().ToString() + " TL Çıkmıştır.";

            MessageBox.Show(text);

            kumbara_paraMiktarlari.Clear();
            bank_volume = 50000;

        }

        private void break_button_Click(object sender, EventArgs e)
        {
            if (again)
            {
                MessageBox.Show("Bu Kumbara Onarılamaz");
                return;
            }
            if (broken == false && repair == false)
            {
                DialogResult result = MessageBox.Show("Bu işlem ile birlikte Kumbaranız kırılacaktır. İşlemi Onaylıyor musunuz?", "Onay", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    if (kumbara_paraMiktarlari.Count() <= 0)
                    {
                        MessageBox.Show("Kumbaranızda hiç para bulunmamaktadır.Kırmak için önce para eklemelisiniz.");
                        return;
                    }
                    break_func();

                    broken = true;

                    break_button.Text = "Kumbarayı Onar";
                }


            }
            else if (broken == true && repair == false)
            {
                DialogResult result = MessageBox.Show("Kırılmış Kumbaranızı onarmak istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    repair = true;
                    break_button.Text = "Kumbarayı Parçala";
                    
                }


            }
            else if(broken == true && repair == true)
            {
                break_func();
                MessageBox.Show("Kumbara Artık Onarılamaz.");
                break_button.Text = "Kumbara Onarılamaz";
                break_button.Enabled = false;
                again = true;


            }
        }



       
    }
}
