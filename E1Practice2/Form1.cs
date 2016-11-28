using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E1Practice2
{
    public partial class Form1 : Form
    {
        string imagepath;
        Image image;

        public Form1()
        {
            InitializeComponent();
        }

        private void openImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                imagepath = openFileDialog1.FileName;
                try
                {
                    image = Image.FromFile(imagepath);
                    pictureBox1.Image = image;
                }
                catch (IOException)
                {
                }
            }
        }

        private void changeLightingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
                MessageBox.Show("No image loaded. Please load and image first.", "No Image Loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Form2 form2 = new Form2(imagepath);
                form2.FormClosed += new FormClosedEventHandler(FormClosed);
                form2.ShowDialog();
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
            {
                MessageBox.Show("No image loaded. Please load and image first.", "No Image Loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            saveFileDialog1.Filter = "Png Image|*.png|JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif";
            DialogResult result = saveFileDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string savepath = saveFileDialog1.FileName;
                ImageFormat format = ImageFormat.Jpeg; //set default
                //saveFileDialog1.DefaultExt = ".jpg";
                try
                {
                    string ext = System.IO.Path.GetExtension(saveFileDialog1.FileName);
                    switch (ext)
                    {
                        case ".jpg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".bmp":
                            format = ImageFormat.Bmp;
                            break;
                        case ".png":
                            format = ImageFormat.Png;
                            break;
                        case ".gif":
                            format = ImageFormat.Gif;
                            break;
                    }
                    pictureBox1.Image.Save(savepath, format);
                }
                catch (IOException)
                {
                }
            }
        }

        private void skewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (image == null)
                MessageBox.Show("No image loaded. Please load and image first.", "No Image Loaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                Form3 form3 = new Form3(imagepath);
                form3.FormClosed += new FormClosedEventHandler(FormClosed);
                form3.ShowDialog();
            }
        }

        private void FormClosed(object sender, EventArgs e)
        {
            IImageForm form = (IImageForm)sender;
            if(form.DestImage != null)
            {
                image = form.DestImage.Image;
                pictureBox1.Image = image;
                pictureBox1.Refresh();
            }

        }
    }
}
