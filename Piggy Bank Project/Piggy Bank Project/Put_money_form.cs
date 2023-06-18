using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Piggy_Bank_Project
{
    public partial class Put_money_form : Form
    {
        public Put_money_form()
        {
            InitializeComponent();
        }


    
        List<double> paraMiktarlari = new List<double>();

        bool increase = true;

       

        private void Put_money_form_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            manage(5.00, bes_tl_count);

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            manage(10.00, on_tl_count);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            manage(20.00, yirmi_tl_count);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

            manage(50.00, elli_tl_count);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

            manage(100.00, yuz_tl_count);
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            manage(200.00, ikiyuz_tl_count);


        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

            manage(0.01, bir_krs_count);
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            manage(0.05, bes_krs_count);
        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {

            manage(0.10, on_krs_count);
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

            manage(0.25, yirmi_bes_krs_count);

        }

        private void pictureBox11_Click(object sender, EventArgs e)
        {
            manage(0.50, elli_krs_count);
        }

        private void pictureBox12_Click(object sender, EventArgs e)
        {

            manage(1.00, bir_tl_count);
        }


        public void manage(double value, System.Windows.Forms.TextBox textBox)
        {
            if (increase)
            {
               
                paraMiktarlari.Add(value);
                textBox.Text = (int.Parse(textBox.Text) + 1).ToString();
            }
            else
            {
             
                bool delete_value = paraMiktarlari.Remove(value);
                if (!delete_value)
                {
                    MessageBox.Show("Seçtiğiniz para seçili olmadığından dolayı eksiltme işlemi gerçekleşmedi.Lütfen seçmiş olduğunuz paralar için eksiltme işlemi uygulayınız.");
                }
                else
                {
                    textBox.Text = (int.Parse(textBox.Text) - 1).ToString();

                }

            }
            textBox1.Text = Sum(paraMiktarlari).ToString();
        }

        public double Sum(List<double> list)
        {
            double total = 0;
            foreach (double number in list)
            {
                total += number;
            }
            return total;
        }

        private void increase_button_Click(object sender, EventArgs e)
        {
            if (increase)
            {
                increase = false;
                increase_button.Text = "Arttırma Modu";
            }
            else
            {
                increase = true;
                increase_button.Text = "Azaltma Modu";

            }
        }

        private void put_money_button_Click(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            if (Form1.broken  && !Form1.repair  || Form1.again)
            {
                MessageBox.Show("Kırılmış bir kumbaraya para eklenemez.");
                return;
            }


            if (paraMiktarlari.Count()<=0)
            {
                MessageBox.Show("Hiç Para Seçilmedi");
                return;
            }
            double total_volume = form1.calc(paraMiktarlari);
        
            if (Form1.bank_volume - total_volume > 0)
            {
                Form1.bank_volume = Form1.bank_volume - total_volume;              
                Form1.kumbara_paraMiktarlari.AddRange(paraMiktarlari);
                MessageBox.Show("Ekleme işlemi Başarılı");
                ResetTextBoxValues("0");            
                paraMiktarlari.Clear();
            }
            else
            {
                MessageBox.Show("Seçtiğiniz paralar kumbaraya sığmamaktadır.Kumbaranızı parçalayabilir yada daha fazla hacim elde etmek için sallayabilirsiniz");           

            }
          

        }

        private void ResetTextBoxValues(string text)
        {
            foreach (Control control in this.Controls)
            {
                if (control is System.Windows.Forms.TextBox textBox)
                {
                    textBox.Text = text;
                }
            }
        }


    }



}