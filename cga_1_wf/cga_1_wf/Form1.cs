using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cga_1_wf
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ObjParser objParser = new ObjParser(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = objParser.parseFile("ModelHead.obj");
        }
    }
}
