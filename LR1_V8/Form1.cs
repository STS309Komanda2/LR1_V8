using System;
using System.Data;
using System.Linq;
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            double Xn, Xv;
            double[] r1 = new double[] { 0.006, 0.040, 0.185, 0.43, 0.75, 1.13, 1.56, 2.03, 2.53, 3.06, 3.6, 4.2, 4.8, 5.4, 6.0, 6.6, 7.3, 7.9, 8.6, 9.2 };
            double[] r2 = new double[] { 2.7, 4.6, 6.3, 7.8, 9.2, 10.6, 12.0, 13.4, 14.7, 16.0, 17.3, 18.5 };
            double N = Convert.ToDouble(textBox1.Text);
            string[] y = (from object item in listBox1.Items select item.ToString()).ToArray<string>();
            double m = Convert.ToDouble(textBox6.Text);
            double[] s = new double[Convert.ToInt32(m)];
            double xj, xj1, l;
            for (int i = 0; i < m; i++)
            {
                s[i] = 0;
            }
            for (int i = 0; i < N - 1; i++)
            {
                xj = 0;
                for (int j = 0; i < m; i++)
                {
                    if (Convert.ToDouble(y[i]) >= xj & Convert.ToDouble(y[i]) <= xj + 1 / m)
                    {
                        s[j] = s[j] + 1;
                    }
                    xj = xj + 1 / m;
                }
            }
            double Y = 0;
            xj = 1 / m;
            xj1 = 0;
            for (int i = 0; i < m; i++)
            {
                Console.WriteLine(xj);
                l = (xj - xj1) * N;
                Y = Y + (Math.Pow(s[i] - l, 2) / l);
                xj1 = xj;
                xj = xj + 1 / m;
                l = 0;
            }
            Xn = r1[Convert.ToInt32(m) - 1 - 1];
            Xv = r2[Convert.ToInt32(m) - 1 - 1];
            if (Y >= Xn & Y <= Xv)
            {
                textBox5.Text = "X Пирсона =" + Convert.ToString(Y) + (char)13 + (char)10 + "Нижняя граница доверительного интервала " + Convert.ToString(Xn) + (char)13 + (char)10 + "Верхняя граница доверительного интервала" + Convert.ToString(Xv) + (char)13 + (char)10 + "Гипотеза о равномерном законе распределения СВ не отвергается";
            }
            else
            {
                textBox5.Text = "X Пирсона =" + Convert.ToString(Y) + (char)13 + (char)10 + "Нижняя граница доверительного интервала " + Convert.ToString(Xn) + (char)13 + (char)10 + "Верхняя граница доверительного интервала" + Convert.ToString(Xv) + (char)13 + (char)10 + "Гипотеза о равномерном законе распределения СВ отвергается";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
