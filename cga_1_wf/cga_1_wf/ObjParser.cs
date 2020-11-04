using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Globalization;
using System.Windows;
using System.Drawing;
using MyExtensions;

namespace cga_1_wf
{

    public class ObjParser
    {
       

        public List<double[]> vertexes = new List<double[]>();
        public List<double[]> textures = new List<double[]>();
        public List<List<List<int>>> faces = new List<List<List<int>>>();


        public int WINDOW_WIDTH;
        public int WINDOW_HEIGHT;

        public int fov = 60;
        //public double aspect = WINDOW_WIDTH/WINDOW_HEIGHT;
        public double zFar = 1000, zNear = 0.3;

        

        public Vector3 camera = new Vector3(0, 0, 1000);
        public Vector3 up = new Vector3(0, 1, 0);
        public Vector3 target = new Vector3(0, 0, 0);
        public Matrix4x4 worldToView;
        public Matrix4x4 origin = new Matrix4x4(
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            );

        public Matrix4x4 scaleMatrix = new Matrix4x4(
                300, 0, 0, 0,
                0, 300, 0, 0,
                0, 0, 300, 0,
                0, 0, 0, 1
            );


        public ObjParser(int width, int height)
        {
            this.WINDOW_WIDTH = width;
            this.WINDOW_HEIGHT = height;
            this.zNear = WINDOW_WIDTH / 2;
        }

        private void translateToOrigin()
        {
            foreach (double[] verts in vertexes)
            {

            }
        }

        private Bitmap SetUpCamera()
        {
            //aspect = (float)WINDOW_WIDTH / (float)WINDOW_HEIGHT;
            //Vector4 testVector = new Vector4(3, 10, 12, 6);
            //testVector = Vector4.Transform(testVector, scaleMatrix);
            Vector3 target = new Vector3(0, 0, 0);
            Vector3 axisZ = Vector3.Normalize(Vector3.Subtract(camera, target));
            Vector3 axisX = Vector3.Normalize(Vector3.Cross(up, axisZ));
            Vector3 axisY = up;
            worldToView = new Matrix4x4(
                    axisX.X, axisX.Y, axisX.Z, -Vector3.Dot(axisX, camera),
                    axisY.X, axisY.Y, axisY.Z, -Vector3.Dot(axisY, camera),
                    axisZ.X, axisZ.Y, axisZ.Z, -Vector3.Dot(axisZ, camera),
                    0, 0, 0, 1
                    );
            Vector4 temp;

            List<Vector4> vertexes_2 = new List<Vector4>();
            
           foreach (double[] vertex in vertexes)
            {
                if (vertex.Length == 3)
                    temp = new Vector4((float)vertex[0], (float)vertex[1], (float)vertex[2], 1);
                else
                    temp = new Vector4((float)vertex[0], (float)vertex[1], (float)vertex[2], (float)vertex[3]);
                //temp = Vector4.Transform(temp, scaleMatrix);
                
                vertexes_2.Add(temp);
            }

           /* for (int i = 0; i < vertexes_2.Count; i++)
            {
                vertexes_2[i] = Vector4.Transform(vertexes_2[i], worldToView);
            }*/

            ///Добавить деление на w для кривых линий
            Matrix4x4 viewToProjection = new Matrix4x4(
                    (float)(2f * zNear/ WINDOW_WIDTH), 0, 0, 0,
                    0, (float)(2f * zNear / WINDOW_HEIGHT), 0, 0,
                    0, 0, (float)(zFar / (zNear - zFar)), (float)(zNear * zFar / (zNear - zFar)),
                    0, 0, -1, 0
                );

            /*Matrix4x4 viewToProjection = new Matrix4x4(
                     (float)(1f / ( Math.Tan((75f * Math.PI )/ 360f))), 0, 0, 0,
                     0, (float)(1f / Math.Tan((45f * Math.PI) / 360f)), 0, 0,
                     0, 0, -(float)((zFar + zNear )/ (zFar - zNear)), -(float)(2f* zNear * zFar / (zFar - zNear)),
                     0, 0, -1, 0
                 );*/

/*            float xMin = vertexes_2[0].X, yMin = vertexes_2[0].Y;
            for (int i = 0; i < vertexes_2.Count; i++)
            {
                vertexes_2[i] = Vector4.Transform(vertexes_2[i], viewToProjection);
                vertexes_2[i] = Vector4.Divide(vertexes_2[i], vertexes_2[i].W);
            }*/




            Matrix4x4 projectionToViewport = new Matrix4x4(
                    (float)(WINDOW_WIDTH / 2f), 0, 0, (float)(WINDOW_WIDTH / 2f),
                    0, -(float)(WINDOW_HEIGHT / 2f), 0,(float)(WINDOW_HEIGHT / 2f),
                    0, 0, 1, 0,
                    0, 0, 0, 1

                );
            /* for (int i = 0; i < vertexes_2.Count; i++)
             {
                 vertexes_2[i] = Vector4.Transform(vertexes_2[i], projectionToViewport);
             }*/

            //Matrix4x4 resultMatrix = Matrix4x4.Multiply(viewToProjection, worldToView);
            //resultMatrix = Matrix4x4.Multiply(resultMatrix, worldToView);
            //resultMatrix = Matrix4x4.Multiply(resultMatrix, scaleMatrix);


            /*for (int i = 0; i < vertexes_2.Count; i++)
            {
                vertexes_2[i] = Vector4.Transform(vertexes_2[i], resultMatrix);
                vertexes_2[i] = Vector4.Divide(vertexes_2[i], vertexes_2[i].W);
            }*/
            /*
             * 
             * 
            resultMatrix = Matrix4x4.Multiply(projectionToViewport, viewToProjection);
            resultMatrix = Matrix4x4.Multiply(resultMatrix, worldToView);
            resultMatrix = Matrix4x4.Multiply(resultMatrix, scaleMatrix);
            //vertexes_2.Clear();
            foreach (double[] vertex in vertexes)
            {
                Vector4 temp2 = new Vector4((float)vertex[0], (float)vertex[1], (float)vertex[2], 1);
                temp2 = Vector4.Transform(temp2, resultMatrix);
                temp2 = Vector4.Divide(temp2, temp2.W);
                vertexes_2.Add(temp2);
            }*/
            for (int i = 0; i < vertexes_2.Count; i++)
            {
                //vertexes_2[i] = Vector4.Transform(vertexes_2[i], scaleMatrix);
                vertexes_2[i] = vertexes_2[i].ApplyMatrix(scaleMatrix);
                vertexes_2[i] = vertexes_2[i].ApplyMatrix(worldToView);
                vertexes_2[i] = vertexes_2[i].ApplyMatrix(viewToProjection);
                vertexes_2[i] = Vector4.Divide(vertexes_2[i], vertexes_2[i].W);
                vertexes_2[i] = vertexes_2[i].ApplyMatrix(projectionToViewport);
                

            }
            Bitmap bmp = new Bitmap(WINDOW_WIDTH, WINDOW_HEIGHT);
            Graphics gfx = Graphics.FromImage(bmp);
            SolidBrush brush = new SolidBrush(System.Drawing.Color.White);

            gfx.FillRectangle(brush, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            /*for (int i = 0; i < vertexes_2.Count; i++)
            {
                
                bmp.SetPixel((int)vertexes_2[i].X, (int)vertexes_2[i].Y, Color.Red);
                
            }*/
            //bmp.Save("myfile.bmp");
            return DrawEdges(vertexes_2, bmp);
            //DrawEdges(vertexes_2);
        }

        public Bitmap parseFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            var fmt = new NumberFormatInfo
            {
                NegativeSign = "-"
            };
            foreach (string line in lines)
            {
                
                string[] literals = line.Trim().Split(' ');
                switch (literals[0])
                {
                    case "v":
                        if (literals.Length == 4)
                        {
                            vertexes.Add(new double[] { double.Parse(literals[1], fmt), double.Parse(literals[2],fmt),
                                                     double.Parse(literals[3], fmt)});
                        }
                        else
                        {
                            vertexes.Add(new double[] { double.Parse(literals[1]), double.Parse(literals[2]),
                                                     double.Parse(literals[3]), double.Parse(literals[4])});
                        }
                        break;
                    case "f":
                        if (line.Contains('/'))
                        {
                            List<List<int>> topLevelList = new List<List<int>>();
                            for (int i = 1; i < literals.Length; i++)
                            {
                                string[] numbers = literals[i].Split('/');
                                List<int> tempList = new List<int>();
                                foreach (string numb in numbers)
                                {
                                    if (numb.Length != 0)
                                        tempList.Add(int.Parse(numb));
                                    else
                                        tempList.Add(0);
                                }
                                topLevelList.Add(tempList);
                            }
                            faces.Add(topLevelList);
                        }
                        else
                        {
                            List<List<int>> topLevelList = new List<List<int>>();
                            for (int i = 1; i < literals.Length; i++)
                            {
                                List<int> tempList = new List<int>
                                {
                                    int.Parse(literals[i])
                                };
                                topLevelList.Add(tempList);
                            }
                            faces.Add(topLevelList);
                        }
                        break;
                    case "vt":

                        break;
                    case "vn":

                        break;
                }
            }

            return SetUpCamera();
        }

        private Bitmap DrawEdges(List<Vector4> vertexes_2, Bitmap bmp)
        {
            //Bitmap bmp = new Bitmap(WINDOW_WIDTH, WINDOW_HEIGHT);
            //Graphics gfx = Graphics.FromImage(bmp);
            //SolidBrush brush = new SolidBrush(System.Drawing.Color.White);
            
             //   gfx.FillRectangle(brush, 0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            
            //foreach (List<List<int>> array in faces)
            for (int j = 0; j < faces.Count; j++)
            {
                List<List<int>> array = faces[j];
                for (int i = 0; i < array.Count; i++)
                {
                    List<int> temp = array[i];
                    if (i == array.Count - 1)
                    {
                        try
                        {
                            //UseBresenhahm((int)vertexes_2[temp[0]].X, (int)vertexes_2[temp[0]].Y, (int)vertexes_2[array[0][0]].X, (int)vertexes_2[array[0][0]].X, bmp);
                            line((int)vertexes_2[temp[0]].X, (int)vertexes_2[temp[0]].Y, (int)vertexes_2[array[0][0]].X, (int)vertexes_2[array[0][0]].Y, bmp);
                        }
                        catch(ArgumentOutOfRangeException e)
                        {

                        }
                    }
                    else
                    {
                        line((int)vertexes_2[temp[0] - 1].X, (int)vertexes_2[temp[0] - 1].Y, (int)vertexes_2[array[i + 1][0] - 1].X, (int)vertexes_2[array[i + 1][0] - 1].Y,bmp);
                    }
                }
                //проход по вершинам полигона
                /*foreach(List<double> face in array)
                {
                    Console.WriteLine(face.Count);
                }*/
            }
            //System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            //image.Source = bitmap;
            bmp.Save("myfile.bmp");

            return bmp;
            //MainWindow.RedrawCanvas(image);
        }


        private void UseBresenhahm(int x0, int y0, int x1, int y1, Bitmap bmp)
        {

            int dx = Math.Abs(x1 - x0), sx = x0 < x1 ? 1 : -1;
            int dy = Math.Abs(y1 - y0), sy = y0 < y1 ? 1 : -1;
            int err = (dx > dy ? dx : -dy) / 2, e2;
            for (; ; )
            {
                if (Math.Abs(x0) < WINDOW_WIDTH && Math.Abs(y0) < WINDOW_HEIGHT)
                    bmp.SetPixel(Math.Abs(x0), Math.Abs(y0), System.Drawing.Color.IndianRed);
                if (x0 == x1 && y0 == y1) break;
                e2 = err;
                if (e2 > -dx) { err -= dy; x0 += sx; }
                if (e2 < dy) { err += dx; y0 += sy; }
            }
        }

        public void line(int x, int y, int x2, int y2, Bitmap bmp)
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                if ((x < WINDOW_WIDTH)&& (x > 0) && (y < WINDOW_HEIGHT) && (y > 0))
                    bmp.SetPixel(x, y, Color.Red);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        private void PutPixel(int x, int y, Bitmap bmp)
        {
            
            try
            {
                bmp.SetPixel(x, y, System.Drawing.Color.Orange);
            }
            catch(Exception e)
            {

            }

           /* try
            {
                bitmap.Lock();
                unsafe
                {
                    IntPtr pBackBuffer = bitmap.BackBuffer;
                    pBackBuffer += y * bitmap.BackBufferStride;
                    pBackBuffer += x  * 4;

                    int colorData = 255 << 16;
                    colorData |= 128 << 8;
                    colorData |= 255 << 0;

                    Console.WriteLine(bitmap.PixelHeight);
                    Console.WriteLine(bitmap.PixelWidth);
                    *((int*)pBackBuffer) = colorData;
                    

                }
                bitmap.AddDirtyRect(new Int32Rect(x, y, 1, 1));
            }
            finally
            {
                bitmap.Unlock();
            }*/
        }
    }
}
