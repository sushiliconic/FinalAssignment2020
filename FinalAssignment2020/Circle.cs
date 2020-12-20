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
    /// This class is for circle shape.
    /// </summary>
    class Circle : Shape
    {

        int radius;

        /// <summary>
        /// This is methord to darw the circle.
        /// </summary>
        /// <param name="c">C is for color</param>
        
        /// <param name="g">g is for graphics</param>
        /// <param name="form">form</param>
        public override void draw(Color c, Graphics g, Form1 form)
        {
            if (form.rotateValue.Text != "0")
            {
                float rotateValue = float.Parse(form.rotateValue.Text.Trim());


                g.RotateTransform((rotateValue), MatrixOrder.Append);
            }
            Pen p = new Pen(c, 1);
            SolidBrush b = new SolidBrush(c);
            if (form.fill.Checked)
            {
                g.FillEllipse(b, x, y, radius, radius);
            }
            g.DrawEllipse(p, x, y, radius, radius);
        } 



        public override void set(params int[] list)
        {
            base.set(list[1], list[2]);
            this.radius = list[0];

        }


    }
}
