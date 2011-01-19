using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Security.Cryptography;

namespace Tax666.Common.Plugin
{
    /// <summary>
    /// 验证码图片类
    /// </summary>
    public class VerifyImage : IVerifyImage
    {
        private static byte[] randb = new byte[4];
        private static RNGCryptoServiceProvider rand = new RNGCryptoServiceProvider();

        private static Font[] fontsize18 = {
                                        new Font(new FontFamily("Times New Roman"), 18 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Georgia"), 18 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Arial"), 18 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Comic Sans MS"), 18 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Tahoma"), 18 + Next(4), FontStyle.Bold)
                                     };
        private static Font[] fontsize16 = {
                                        new Font(new FontFamily("Times New Roman"), 16 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Georgia"), 16 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Arial"), 16 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Comic Sans MS"), 16 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Tahoma"), 16 + Next(4), FontStyle.Bold)
                                     };

        private static Font[] fontsize14 = {
                                        new Font(new FontFamily("Times New Roman"), 14 + Next(4), FontStyle.Bold),
                                        //new Font(new FontFamily("Georgia"), 14 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Tahoma"), 14 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Arial"), 14 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Comic Sans MS"), 14 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Verdana"), 14 + Next(4), FontStyle.Bold)
                                     };
        private static Font[] fontsize12 = {
                                        new Font(new FontFamily("Times New Roman"), 12 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Georgia"), 12 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Arial"), 12 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Comic Sans MS"), 12 + Next(4), FontStyle.Bold),
                                        new Font(new FontFamily("Tahoma"), 12 + Next(4), FontStyle.Bold)
                                     };
        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        private static int Next(int max)
        {
            rand.GetBytes(randb);
            int value = BitConverter.ToInt32(randb, 0);
            value = value % (max + 1);
            if (value < 0)
                value = -value;
            return value;
        }

        /// <summary>
        /// 获得下一个随机数
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        private static int Next(int min, int max)
        {
            int value = Next(max - min) + min;
            return value;
        }

        #region IVerifyImage 成员

        public VerifyImageInfo GenerateImage(string code, int width, int height, Color bgcolor, int textcolor, int fontSize)
        {
            VerifyImageInfo verifyimage = new VerifyImageInfo();
            verifyimage.ImageFormat = ImageFormat.Jpeg;
            verifyimage.ContentType = "image/pjpeg";

            Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);
            Rectangle rect = new Rectangle(0, 0, width, height);
            g.SmoothingMode = SmoothingMode.AntiAlias;

            g.Clear(bgcolor);

            int fixedNumber = textcolor == 2 ? 60 : 0;

            SolidBrush drawBrush = new SolidBrush(Color.FromArgb(Next(100), Next(100), Next(100)));
            for (int x = 0; x < 3; x++)
            {
                Pen linePen = new Pen(Color.FromArgb(Next(150) + fixedNumber, Next(150) + fixedNumber, Next(150) + fixedNumber), 1);
                g.DrawLine(linePen, new PointF(0.0F + Next(20), 0.0F + Next(height)), new PointF(0.0F + Next(width), 0.0F + Next(height)));
            }

            Matrix m = new Matrix();
            for (int x = 0; x < code.Length; x++)
            {
                m.Reset();
                m.RotateAt(Next(30) - 15, new PointF(Convert.ToInt64(width * (0.10 * x)), Convert.ToInt64(height * 0.5)));
                g.Transform = m;
                drawBrush.Color = Color.FromArgb(Next(150) + fixedNumber + 20, Next(150) + fixedNumber + 20, Next(150) + fixedNumber + 20);

                PointF drawPoint;
                switch (fontSize)
                {
                    case 18:
                        drawPoint = new PointF(0.0F + Next(4) + x * 24, 3.0F + Next(3));
                        break;
                    case 16:
                        drawPoint = new PointF(0.0F + Next(4) + x * 20, 3.0F + Next(3));
                        break;
                    case 14:
                        drawPoint = new PointF(0.0F + Next(4) + x * 16, 3.0F + Next(3));
                        break;
                    case 12:
                        drawPoint = new PointF(0.0F + Next(4) + x * 12, 3.0F + Next(3));
                        break;
                    default:
                        drawPoint = new PointF(0.0F + Next(4) + x * 20, 3.0F + Next(3));
                        break;
                }
                
                //PointF drawPoint = new PointF(0.0F + Next(4) + x * 20, 3.0F + Next(3));

                switch (fontSize)
                {
                    case 18:
                        g.DrawString(Next(1) == 1 ? code[x].ToString() : code[x].ToString().ToUpper(), fontsize18[Next(fontsize18.Length - 1)], drawBrush, drawPoint);
                        break;
                    case 16:
                        g.DrawString(Next(1) == 1 ? code[x].ToString() : code[x].ToString().ToUpper(), fontsize16[Next(fontsize16.Length - 1)], drawBrush, drawPoint);
                        break;
                    case 14:
                        g.DrawString(Next(1) == 1 ? code[x].ToString() : code[x].ToString().ToUpper(), fontsize14[Next(fontsize14.Length - 1)], drawBrush, drawPoint);
                        break;
                    case 12:
                        g.DrawString(Next(1) == 1 ? code[x].ToString() : code[x].ToString().ToUpper(), fontsize12[Next(fontsize12.Length - 1)], drawBrush, drawPoint);
                        break;
                    default:
                        g.DrawString(Next(1) == 1 ? code[x].ToString() : code[x].ToString().ToUpper(), fontsize16[Next(fontsize16.Length - 1)], drawBrush, drawPoint);
                        break;
                }

                g.ResetTransform();
            }

            double distort = Next(5, 10) * (Next(10) == 1 ? 1 : -1);

            using (Bitmap copy = (Bitmap)bitmap.Clone())
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int newX = (int)(x + (distort * Math.Sin(Math.PI * y / 84.5)));
                        int newY = (int)(y + (distort * Math.Cos(Math.PI * x / 54.5)));
                        if (newX < 0 || newX >= width)
                            newX = 0;
                        if (newY < 0 || newY >= height)
                            newY = 0;
                        bitmap.SetPixel(x, y, copy.GetPixel(newX, newY));
                    }
                }
            }

            drawBrush.Dispose();
            g.Dispose();

            verifyimage.Image = bitmap;

            return verifyimage;
        }

        #endregion
    }
}
