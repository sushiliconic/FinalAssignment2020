using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment2020
{
    abstract class Shape : Shapes
    {
        protected int x, y;
        protected bool fillshape = false;

        public Shape()
        {
            x = y = 0;
        }

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public abstract void draw(Color c, Graphics g, Form1 form);
        public virtual void set(params int[] list)
        {
            x = list[0];
            y = list[1];
        }




    }
}
