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

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                //y axis
                case Keys.W:
                    //mesh.Rotation = new Vector3(mesh.Rotation.X - deltaAngle, mesh.Rotation.Y, mesh.Rotation.Z);
                    objParser.Rotate(-0.1d, 0, 0, pictureBox1);
                    break;
                case Keys.S:
                    //mesh.Rotation = new Vector3(mesh.Rotation.X + deltaAngle, mesh.Rotation.Y, mesh.Rotation.Z);
                    objParser.Rotate(0.1d, 0, 0, pictureBox1);
                    break;

                //x axis
                case Keys.A:
                    //mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y - deltaAngle, mesh.Rotation.Z);
                    objParser.Rotate(0, -0.1d, 0, pictureBox1);
                    break;
                case Keys.D:
                    //mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y + deltaAngle, mesh.Rotation.Z);
                    objParser.Rotate(0, 0.1d, 0, pictureBox1);
                    break;

                //z axis
                case Keys.Q:
                    //mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y, mesh.Rotation.Z + deltaAngle);
                    objParser.Rotate(0, 0, 0.1d, pictureBox1);
                    break;
                case Keys.E:
                    //mesh.Rotation = new Vector3(mesh.Rotation.X, mesh.Rotation.Y, mesh.Rotation.Z - deltaAngle);
                    objParser.Rotate(0, 0, -0.1d, pictureBox1);
                    break;
            }
        }
    }
}
