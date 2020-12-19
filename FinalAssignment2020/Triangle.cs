using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment2020
{
    class Triangle : Shape
    {

        int side;
        double x1, y1, x2, y2 = 0;
        public override void draw(Color c, Graphics g, Form1 form)
        {
            Pen p = new Pen(c, 1);
            if (form.rotateValue.Text != "0")
            {
                float rotateValue = float.Parse(form.rotateValue.Text.Trim());


                g.RotateTransform((rotateValue), MatrixOrder.Append);
            }
            x1 = x + side * Math.Cos(AngleToRadians(30));
            y1 = y + side * Math.Sin(AngleToRadians(30));

            x2 = x + side * Math.Cos(AngleToRadians(330));
            y2 = y + side * Math.Sin(AngleToRadians(330));


            PointF point1 = new PointF(x, y);
            PointF point2 = new PointF(Convert.ToSingle(x1), Convert.ToSingle(y1));
            PointF point3 = new PointF(Convert.ToSingle(x2), Convert.ToSingle(y2));
            PointF[] vertices = { point1, point2, point3 };

            foreach (PointF pa in vertices)
            {
                Console.WriteLine(pa);
            }

            if (form.fill.Checked)
            {
                SolidBrush b = new SolidBrush(c);
                g.FillPolygon(b, vertices);
            }

            g.DrawPolygon(p, vertices);
        }

        private double AngleToRadians(double angle)
        {
            return (float)(Math.PI / 180) * angle;
        }


        public override void set(params int[] list)
        {
            base.set(list[1], list[2]);
            this.side = list[0];

        }


    }
}
