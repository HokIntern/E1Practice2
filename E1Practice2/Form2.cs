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
    public partial class Form2 : Form, IImageForm
    {
        private EImage _srcImage;
        private EImage _destImage;

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

        public Form2()
        {
            InitializeComponent();
        }
        public Form2(string imagepath)
        {
            InitializeComponent();

            try
            {
                Image newImage = Image.FromFile(imagepath);
                SrcImage = new EImage(newImage);
                pictureBox1.Image = SrcImage.Image;
            }
            catch (IOException)
            {
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int brightness = int.Parse(textBox1.Text);
            DestImage = SrcImage.Brightness(brightness);
            pictureBox2.Image = DestImage.Image;
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
    }
}
