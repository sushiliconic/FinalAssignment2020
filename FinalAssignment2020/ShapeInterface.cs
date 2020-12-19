using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalAssignment2020
{
    interface Shapes
    {
        void set(params int[] list);
        void draw(Color c, Graphics g, Form1 form);


    }
}
