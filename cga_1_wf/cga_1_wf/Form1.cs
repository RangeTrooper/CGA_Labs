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
        ObjParser objParser;
        bool mousePressed = false;
        Point mouseInitPos;
        Point movement;

        public Form1()
        {
            InitializeComponent();
            objParser = new ObjParser(pictureBox1.Width, pictureBox1.Height);
            this.pictureBox1.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseWheel);
            pictureBox1.Image = objParser.parseFile("ModelHead.obj");
        }

        private void pictureBox1_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta > 0)
                objParser.ZoomIn(pictureBox1);
            else
                objParser.ZoomOut(pictureBox1);
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;
            mouseInitPos = e.Location;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed)
            {
                Point path = new Point(e.Location.X - mouseInitPos.X, e.Location.Y - mouseInitPos.Y);
                objParser.Rotate(path, pictureBox1);
            }
            mouseInitPos = e.Location;
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            mousePressed = false;
        }
    }
}
