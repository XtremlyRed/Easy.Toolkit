using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Easy.Toolkit
{
    public static class ImageExtensions
    {
        private static readonly BitmapPalette bitmapPalette;
        static ImageExtensions()
        {
            using Bitmap bitmap = new Bitmap(1, 1);
            BitmapSource bitmapImage = bitmap.ToBitmapSource2();
            bitmapPalette = bitmapImage.Palette;
        }


        public static BitmapImage ToBitmapSource1(this Bitmap bitmap)
        {
            using MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);
            stream.Position = 0;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            return bitmapImage;
        }


        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);
        public static BitmapSource ToBitmapSource2(this Bitmap bitmap)
        {
            IntPtr ptr = bitmap.GetHbitmap(); //obtain the Hbitmap
            BitmapSource bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ptr); //release the HBitmap
            return bitmapSource;
        }


        public static BitmapSource ToBitmapSource3(this Bitmap bitmap)
        {

            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            BitmapSource bitmapSource = BitmapSource.Create(bitmap.Width, bitmap.Height, 96, 96, PixelFormats.Bgr24, bitmapPalette, bmpData.Scan0, bitmap.Width * bitmap.Height * 3, bitmap.Width * 3);
            bitmap.UnlockBits(bmpData);
            return bitmapSource;
        }



        public static WriteableBitmap ToWriteableBitmap1(this Bitmap bitmap, Action<Graphics> action = null, Action<WriteableBitmap> action1 = null)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmap.Width, bitmap.Height, 96.0, 96.0, PixelFormats.Pbgra32, null);
            writeableBitmap.Lock();
            using (Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, writeableBitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, writeableBitmap.BackBuffer))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap2))
                {
                    graphics.Clear(System.Drawing.Color.Black);
                    Rectangle rectangle = new Rectangle(default(System.Drawing.Point), new System.Drawing.Size(bitmap.Width, bitmap.Height));
                    graphics.DrawImage(bitmap, rectangle, rectangle, GraphicsUnit.Pixel);
                    action?.Invoke(graphics);
                }

            }

            action1?.Invoke(writeableBitmap);

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.Width, bitmap.Height));
            writeableBitmap.Unlock();

            return writeableBitmap;
        }


        public static WriteableBitmap ToWriteableBitmap2(this Bitmap bitmap, Action<Graphics> action = null, Action<Bitmap> action1 = null)
        {
            WriteableBitmap writeableBitmap = new WriteableBitmap(bitmap.Width, bitmap.Height, 96.0, 96.0, PixelFormats.Pbgra32, null);
            writeableBitmap.Lock();
            using (Bitmap bitmap2 = new Bitmap(bitmap.Width, bitmap.Height, writeableBitmap.BackBufferStride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, writeableBitmap.BackBuffer))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap2))
                {
                    graphics.Clear(System.Drawing.Color.Black);
                    Rectangle rectangle = new Rectangle(default(System.Drawing.Point), new System.Drawing.Size(bitmap.Width, bitmap.Height));
                    graphics.DrawImage(bitmap, rectangle, rectangle, GraphicsUnit.Pixel);
                    action?.Invoke(graphics);
                }
                action1?.Invoke(bitmap2);
            }

            writeableBitmap.AddDirtyRect(new Int32Rect(0, 0, bitmap.Width, bitmap.Height));
            writeableBitmap.Unlock();

            return writeableBitmap;
        }
    }
}
