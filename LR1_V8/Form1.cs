using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR1_V8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            int N = Convert.ToInt32(textBox1.Text);
            double x0 = Convert.ToDouble(textBox2.Text);
            double x1 = Convert.ToDouble(textBox3.Text);
            double[] y = new double[N];
            y[0] = x0;
            listBox1.Items.Add(y[0]);
            y[1] = x1;
            listBox1.Items.Add(y[1]);

            for (int i = 2; i < N; i++)
            {
                double x = y[i - 2] * y[i - 1];
                string str = Convert.ToString(x);
                string str1 = str.Remove(2, 2);
                y[i] = Convert.ToDouble(str1.Remove(7));
                listBox1.Items.Add(y[i]);
                str.Remove(0, str.Length); str1.Remove(0, str1.Length);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double sum1, sum2, sum3, z, ro_kol, ro_max, p;
            sum1 = 0; sum2 = 0; sum3 = 0;
            double N = Convert.ToDouble(textBox1.Text);
            z = 1.64;
            string[] y = (from object item in listBox1.Items select item.ToString()).ToArray<string>();
            for (int i = 0; i < N; i++)
            {
                sum1 = sum1 + i * Convert.ToDouble(y[i]);
                sum2 = sum2 + (Convert.ToDouble(y[i]) * ((N + 1) / 2.0));
                sum3 = sum3 + Convert.ToDouble(y[i]);
            }
            p = (Math.Pow(N, 2) - 1) / 12;
            ro_kol = ((1 / N * sum1) - (1 / N * sum2)) / Math.Sqrt(((1 / N * sum3) - Math.Pow(1 / N * sum3, 2)) * p);
            ro_max = z * ((1 - Math.Pow(ro_kol, 2)) / Math.Sqrt(N));
            if (ro_kol > ro_max)
            {
                textBox4.Text = "Существует корреляционная зависимость" + ", " + "ρ_kol = " + Convert.ToString(ro_kol) + "," + " " + "ρ_max =" + Convert.ToString(ro_max);
            }
            else textBox4.Text = "Корреляционная зависимость не прослеживается" + ", " + "ρ_kol =" + Convert.ToString(ro_kol) + "," + " " + "ρ_max =" + Convert.ToString(ro_max);
        }
    }
}
