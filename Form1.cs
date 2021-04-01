using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Flight
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        const double dt = 0.01;
        const double g = 9.81;

        double a;
        double v0;
        double y0;

        double t;
        double x;
        double y;

        private void btStart_Click(object sender, EventArgs e)
        {
            a = (double)edAngle.Value;
            v0 = (double)edSpeed.Value;
            y0 = (double)edHeight.Value;

            var sizes = GetSize();
            var size1 = Convert.ToInt32(sizes.Item1) + 1;
            var size2 = Convert.ToInt32(sizes.Item2) + 1;
            chart1.ChartAreas[0].AxisX.Maximum = size1;
            chart1.ChartAreas[0].AxisY.Maximum = size2;

            t = 0;
            x = 0;
            y = y0;
            chart1.Series[0].Points.Clear();
            chart1.Series[0].Points.AddXY(x, y);

            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t += dt;
            x = v0 * Math.Cos(a * Math.PI / 180) * t;
            y = y0 + v0 * Math.Sin(a * Math.PI / 180) * t - g * t * t / 2;
            chart1.Series[0].Points.AddXY(x, y);
            time.Text = "Время: " + Convert.ToString(t);
            if (y <= 0) timer1.Stop();
        }

        private void stop_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled) timer1.Stop();
            else timer1.Start();
        }
        private Tuple<double, double, double> GetSize()
        {
            double sina = Math.Sin(a * Math.PI / 180);
            double ymax = y0 + (v0 * v0 * sina * sina / 2 / g);
            double tpol = (v0 * sina + Math.Sqrt(v0 * v0 * sina * sina + 2 * g * y0)) / g;
            double xmax = v0 * tpol * Math.Cos(a * Math.PI / 180);
            return Tuple.Create(xmax, ymax, tpol);
        }
    }
}
