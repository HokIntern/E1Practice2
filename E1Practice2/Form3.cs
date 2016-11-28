using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E1Practice2
{
    public partial class Form3 : Form, IImageForm
    {
        private Point startPoint;
        private Point destPoint;
        private EImage _srcImage;
        private EImage _destImage;

        Point[] prevDestPoints;
        Point[] currDestPoints;

        public EImage SrcImage
        {
            get { return _srcImage; }
            set { _srcImage = value; }
        }

        public EImage DestImage
        {
            get { return _destImage; }
            set { _destImage = value; }
        }

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string imagepath)
        {
            InitializeComponent();

            try
            {
                Image newImage = Image.FromFile(imagepath);
                SrcImage = new EImage(newImage);
                DestImage = new EImage((Image)newImage.Clone());
            }
            catch (IOException)
            {
            }

            pictureBox2Initialize();
            pictureBox2.MouseDown += new MouseEventHandler(pictureBox2_MouseDown);
            pictureBox2.MouseUp += new MouseEventHandler(pictureBox2_MouseUp);
        }

        private void SkewPicture()
        {
            Point[] destinationPoints = calculateParallelogram();
            DestImage = SrcImage.Skew(pictureBox2.Width, pictureBox2.Height, destinationPoints);
            pictureBox2.Image = DestImage.Image;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            startPoint = e.Location;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            destPoint = e.Location;
            SkewPicture();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DestImage = SrcImage;
            this.Close();
        }

        private void pictureBox2Initialize()
        {
            Image tempCanvas = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            using (Graphics graph = Graphics.FromImage(tempCanvas))
            {
                graph.DrawImage(SrcImage.Image, pictureBox2.Width / 3, pictureBox2.Height / 3, pictureBox2.Width / 3, pictureBox2.Height / 3);
                currDestPoints = new Point[3];
                currDestPoints[0] = new Point(pictureBox2.Width / 3, pictureBox2.Height / 3);
                currDestPoints[1] = new Point(pictureBox2.Width / 3 * 2, pictureBox2.Height / 3);
                currDestPoints[2] = new Point(pictureBox2.Width / 3, pictureBox2.Height / 3 * 2);

                pictureBox2.Image = tempCanvas;
                DestImage.Image = tempCanvas;
            }
        }
        private Point[] calculateParallelogram()
        {
            int deltaX = destPoint.X - startPoint.X;
            int deltaY = destPoint.Y - startPoint.Y;

            Point[] destinationPoints = new Point[3];
            currDestPoints.CopyTo(destinationPoints, 0);

            if (deltaX >= 0 && deltaY <= 0) //top right moves
            {
                destinationPoints[1] = new Point(currDestPoints[1].X + deltaX,
                                                currDestPoints[1].Y + deltaY); //top right
            }
            else if (deltaX < 0 && deltaY < 0) //top left moves
            {
                destinationPoints[0] = new Point(currDestPoints[0].X + deltaX,
                                                currDestPoints[0].Y + deltaY); //top left
            }
            else //bottom left moves
            {
                destinationPoints[2] = new Point(currDestPoints[2].X + deltaX,
                                                currDestPoints[2].Y + deltaY); //bottom left
            }

            prevDestPoints = currDestPoints;
            currDestPoints = destinationPoints;
            return destinationPoints;
        }
    }
}
