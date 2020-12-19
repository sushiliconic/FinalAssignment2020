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
    /// This is the shape for rectangle.
    /// </summary>
    class Rectangle : Shape
    {

        int width, height;
        /// <summary>
        ///
        /// </summary>
        /// <param name="c">Color of pen</param>
        /// <param name="g"> graphic </param>
        /// <param name="form">form</param>
        public override void draw(Color c, Graphics g, Form1 form)
        {
            Pen p = new Pen(c, 1);
            if (form.rotateValue.Text != "0")
            {
                float rotateValue = float.Parse(form.rotateValue.Text.Trim());


                g.RotateTransform((rotateValue), MatrixOrder.Append);
            }
            if (form.fill.Checked)
            {
                SolidBrush b = new SolidBrush(c);
                g.FillRectangle(b, x, y, width, height);
            }

            g.DrawRectangle(p, x, y, width, height);
        }

        public override void set(params int[] list)
        {
            base.set(list[2], list[3]);
            this.width = list[0];
            this.height = list[1];
        }


    }
}
