using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment2020
{
    /// <summary>
    /// Polygon class inherits class shape
    /// </summary>
    class Polygon : Shape
    {

        int[] points;
        /// <summary>
        /// This method is for shape polygon
        /// </summary>
        /// <param name="c">c is for color</param>
        /// <param name="g">g is for graphics</param>
        /// <param name="form"> form1 is graphics </param>
        public override void draw(Color c, Graphics g, Form1 form)
        {

            Pen p = new Pen(c, 1);
            SolidBrush b = new SolidBrush(c);
            Point[] point = new Point[points.Length];
            point[0] = new Point(x, y);
            int point_position = 1;
            // a b c d x y
            for (int i = 0; i < points.Length - 2; i += 2)
            {
                point[point_position] = new Point(points[i], points[i + 1]);
                point_position++;
            }
            if (form.rotateValue.Text != "0")
            {
                float rotateValue = float.Parse(form.rotateValue.Text.Trim());


                g.RotateTransform((rotateValue), MatrixOrder.Append);
            }
            if (form.fill.Checked)
            {
                g.FillPolygon(b, point);
            }
            g.DrawPolygon(p, point);
        }



        public override void set(params int[] list)
        {
            base.set(list[list.Length - 1], list[list.Length]);
            this.points = list;

        }


    }
}
