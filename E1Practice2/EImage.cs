using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;

namespace E1Practice2
{
    public class EImage
    {
        public Bitmap SrcBitmap { get; set; }
        public Image Image { get; set; }
        
        public EImage()
        {

        }
        /*
        public EImage(Bitmap bitmap)
        {
            OriginalBitmap = bitmap;
            OriginalImage = OriginalBitmap;
        }
         * */

        public EImage(Image img)
        {
            Image = img;
        }

        public EImage Deblur(int angle, int length, int combination)
        {
            //do deblur
            return new EImage();
        }

        public EImage Skew(int width, int height, Point[] parallelogram)
        {
            Image tempCanvas = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(tempCanvas))
            {
                graph.DrawImage(Image, parallelogram);
            }

            return new EImage(tempCanvas);
        }

        public EImage Brightness(int adjustedBrightness)
        {
            
            ImageAttributes imageAttributes = new ImageAttributes();
            int width = Image.Width;
            int height = Image.Height;

            float[][] colorMatrixElements = {
                                                new float[] {2, 0, 0, 0, 0},
                                                new float[] {0, 1, 0, 0, 0},
                                                new float[] {0, 0, 1, 0, 0},
                                                new float[] {0, 0, 0, 1, 0},
                                                new float[] {.2f, .2f, .2f, 0, 1}
                                            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            Image tempCanvas = new Bitmap(width, height);
            using (Graphics graph = Graphics.FromImage(tempCanvas))
            {
                graph.DrawImage(
                    Image, 
                    new Rectangle(0, 0, width, height), 
                    0, 0, 
                    width, 
                    height, 
                    GraphicsUnit.Pixel, 
                    imageAttributes);
            }

            return new EImage(tempCanvas);
            
            /*
            Bitmap copiedBitmap = (Bitmap)Image.Clone();
            float percent = adjustedBrightness / 100.0f;
            int adjustedPixelValue = (int)(255 * percent);
            //byte adjustedPixelValue = (byte)(255 * ((float)adjustedBrightness / 100));
            try
            {
                int x, y;
                int r, g, b;

                for(x = 0; x < copiedBitmap.Width; x++)
                {
                    for(y = 0; y < copiedBitmap.Height; y++)
                    {
                        Color origColor = copiedBitmap.GetPixel(x, y);
                        r = origColor.R + adjustedPixelValue;
                        if(r < 0) { r = 0; }
                        else if(r > 255) { r = 255; }

                        g = origColor.G + adjustedPixelValue;
                        if(g < 0) { g = 0; }
                        else if(g > 255) { g = 255; }
                        
                        b = origColor.B + adjustedPixelValue;
                        if(b < 0) { b = 0; }
                        else if(b > 255) { b = 255; }

                        Color newColor = Color.FromArgb(r, g, b);
                        copiedBitmap.SetPixel(x, y, newColor);
                    }
                }
            }
            catch
            {
            }

            Image copiedImage = (Image)copiedBitmap;            
            return new EImage(copiedImage);
            */
        }
    }
}
