using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KonhoidNikomeda
{
    public partial class Form1 : Form
    {
        Pen pen1, pen2, pen_ax;
        double x0, y0, x1, y1,x2,y2, t, a, l, a_mult=1,l_mult=1;

        Bitmap canvas;

        Graphics user_Graphics;
        Color user_color;
        int w, h;

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
        }

        Image shrek, osel, shrek_run;
        public Form1()
        {
            InitializeComponent();
            canvas = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            user_Graphics = Graphics.FromImage(canvas);
            user_Graphics.FillRectangle(Brushes.White, 0, 0, ClientRectangle.Width, ClientRectangle.Height);
            shrek = pictureBox1.Image;
            shrek_run = Properties.Resources.pngwing_com;
            osel = Properties.Resources.pngwing_com__2_;
           // pictureBox1.Image = canvas;
            w = pictureBox1.Width;
            h = pictureBox1.Height;
            x0 = w / 2;
            y0 = h / 2;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            user_color = Color.Red;
            pen_ax = new Pen(Color.Blue, 1);
            pen1 = new Pen(user_color, 3);
            pen2 = new Pen(user_color, 3);
            //pen2.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            
        }
        private void KH_button_Click(object sender, EventArgs e)
        {
            System.Drawing.Graphics graph = this.pictureBox1.CreateGraphics();
            System.Drawing.Rectangle rect = ClientRectangle;
            System.Windows.Forms.PaintEventArgs e1 = new System.Windows.Forms.PaintEventArgs(graph, rect);
            pictureBox1_Paint(sender, e1);
            a = Convert.ToDouble(A_Box.Text);
            l = Convert.ToDouble(L_Box.Text);
            String sum_a_l =Convert.ToString(a + l);
            double a_res = a * a_mult;
            double l_res = l * a_mult;
            user_Graphics.Clear(BackColor);
            user_Graphics.DrawLine(pen_ax, (float)x0, 5, (float)x0, h);
            user_Graphics.DrawLine(pen_ax, 0, (float)y0, w - 5, (float)y0);
            user_Graphics.DrawLine(pen_ax, w - 10, (float)y0 + 5, w - 5, (float)y0);
            user_Graphics.DrawLine(pen_ax, w - 10, (float)y0 - 5, w - 5, (float)y0);
            user_Graphics.DrawLine(pen_ax, (float)x0 - 5, 10, (float)x0, 5);
            user_Graphics.DrawLine(pen_ax, (float)x0 + 5, 10, (float)x0, 5);
            string xAxisLabel = "X";
            string yAxisLabel = "Y";
            Font labelFont = new Font("Arial", 10, FontStyle.Regular);
            Brush labelBrush = Brushes.Black;
            PointF xAxisLabelPosition = new PointF(w - 10, (float)y0+5);
            PointF yAxisLabelPosition = new PointF((float)x0 - 15, 10);
            PointF aPosition= new PointF((float)x0 +(float)a_res, (float)y0 + 5);
            PointF a_plus_l_Position = new PointF((float)x0 + (float)(a_res+l_res), (float)y0 + 5);
            user_Graphics.DrawString(xAxisLabel, labelFont, labelBrush, xAxisLabelPosition);
            user_Graphics.DrawString(yAxisLabel, labelFont, labelBrush, yAxisLabelPosition);
            user_Graphics.DrawString(A_Box.Text, labelFont, labelBrush, aPosition);
            user_Graphics.DrawString(sum_a_l, labelFont, labelBrush, a_plus_l_Position);

            pictureBox1.Image = canvas;
            x2 = x1;
            y2 = y1;
            t = -Math.PI / 2+0.1;
            x1 =x0 + a_res + l_res * Math.Cos(t);
            y1 =y0 + a_res * Math.Tan(t) + l_res * Math.Sin(t);
            while (t<= 1.57 && x2 <= pictureBox1.Width && y2 > -1e+10)
            {
                x2 =x0+ a_res + l_res * Math.Cos(t);
                y2 =y0+ a_res * Math.Tan(t) + l_res * Math.Sin(t);
                if (y2 < -1e+10) break;
                user_Graphics.DrawLine(pen1, (float)x1, (float)y1, (float)x2, (float)y2);
                x1 = x2;
                y1 = y2;
                t += Math.PI / 180;
            }
            t = Math.PI / 2-0.1;
            x1 =x0+ a_res + l_res * Math.Cos(t);
            y1 =y0+ a_res * Math.Tan(t) + l_res * Math.Sin(t);
            while (t <= 4.7&&x2 >= 0 && x2 <= pictureBox1.Width && y2 < 1e+10)
            {
                x2 =x0+ a_res + l_res * Math.Cos(t);
                y2 =y0+ a_res * Math.Tan(t) + l_res * Math.Sin(t);
                user_Graphics.DrawLine(pen2, (float)x1, (float)y1, (float)x2, (float)y2);
                x1 = x2;
                y1 = y2;
                t += Math.PI / 180;
            }
            pictureBox1.Refresh();
            pictureBox1.Image = canvas;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if ((x0 - 50) > 0)
                x0 = x0 - 50;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if ((x0+50)<w)
            x0 = x0+ 50;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if ((y0 - 50) >0)
                y0 = y0 - 50;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if((y0+50)<h)
            y0 = y0 + 50;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            x0 = w / 2;
            y0 = h / 2;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            a_mult /= 1.2;
            l_mult /= 1.2;
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            a_mult *= 1.2;
            l_mult *= 1.2;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = shrek;
        }
        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
        }
    }
}
