using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;

namespace MVCiHealth.Utils
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public class ValidateCodeCreater
    {
        #region 内部字段

        /// <summary>表示图像清晰度，数字越大越清晰</summary>
        const float clarity = 20f;

        /// <summary>
        /// 可选背景色
        /// </summary>
        Color[] backColors =
        {
            Color.Silver,
            Color.LightBlue,
            Color.LightGoldenrodYellow,
            Color.PapayaWhip,
            Color.AntiqueWhite,
            Color.LightYellow,
            Color.Thistle,
            Color.Wheat
        };
        /// <summary>
        /// 可选前景色
        /// </summary>
        Color[] foreColors =
        {
            Color.Black,
            Color.DarkBlue,
            Color.Navy,
            Color.Black,
            Color.Indigo,
            Color.Purple,
            Color.DarkRed,
            Color.DarkSlateGray
        };
        /// <summary>
        /// 可选干扰色
        /// </summary>
        Color[] noiseColors =
        {
            Color.Chartreuse,
            Color.LimeGreen,
            Color.MediumSpringGreen,
            Color.GreenYellow,
            Color.Gold,
            Color.Orange,
            Color.HotPink,
            Color.Yellow,
        };
        /// <summary>
        /// 可选字体
        /// </summary>
        Font[] fonts =
        {
            new Font("Arial", clarity, FontStyle.Bold),
            new Font("Arial", clarity, FontStyle.Bold|FontStyle.Italic),

            new Font("Calibri", clarity, FontStyle.Bold),
            new Font("Calibri", clarity, FontStyle.Bold|FontStyle.Italic),

            new Font("SimSun", clarity, FontStyle.Bold),
            new Font("SimSun", clarity, FontStyle.Bold|FontStyle.Italic),

            new Font("Times New Roman", clarity, FontStyle.Bold),
            new Font("Times New Roman", clarity, FontStyle.Bold|FontStyle.Italic),

            new Font("SimKai", clarity, FontStyle.Bold),
            new Font("SimKai", clarity, FontStyle.Bold|FontStyle.Italic),

            new Font("SimHei", clarity, FontStyle.Bold),
            new Font("SimHei", clarity, FontStyle.Bold|FontStyle.Italic),
        };

        /// <summary>
        /// 验证码的最大长度
        /// </summary>
        public static int MaxLength
        {
            get { return 10; }
        }

        /// <summary>
        /// 验证码的最小长度
        /// </summary>
        public static int MinLength
        {
            get { return 1; }
        }

        #endregion

        #region 公开方法

        /// <summary>
        /// 生成验证码字符串
        /// </summary>
        /// <param name="length">指定验证码的长度</param>
        /// <returns></returns>
        public string CreateValidateCode(uint length = 4)
        {
            if (length < MinLength || length > MaxLength)
                throw new ArgumentOutOfRangeException("验证码长度应该在"
                    + MinLength + "和" + MaxLength + "之间，当前指定长度为" + length);
            int[] randMembers = new int[length];
            int[] validateNums = new int[length];
            string validateNumberStr = "";
            //生成起始序列值
            int seekSeek = unchecked((int)DateTime.Now.Ticks);
            Random seekRand = new Random(seekSeek);
            int beginSeek = (int)seekRand.Next(0, Int32.MaxValue - (int)length * 10000);
            int[] seeks = new int[length];
            for (int i = 0; i < length; i++)
            {
                beginSeek += 10000;
                seeks[i] = beginSeek;
            }
            //生成随机数字
            for (int i = 0; i < length; i++)
            {
                Random rand = new Random(seeks[i]);
                int pownum = 1 * (int)Math.Pow(10, length);
                randMembers[i] = rand.Next(pownum, Int32.MaxValue);
            }
            //抽取随机数字
            for (int i = 0; i < length; i++)
            {
                string numStr = randMembers[i].ToString();
                int numLength = numStr.Length;
                Random rand = new Random();
                int numPosition = rand.Next(0, numLength - 1);
                validateNums[i] = Int32.Parse(numStr.Substring(numPosition, 1));
            }
            //生成验证码
            for (int i = 0; i < length; i++)
            {
                validateNumberStr += validateNums[i].ToString();
            }
            return validateNumberStr;
        }

        /// <summary>
        /// 返回包含验证码图片的数据流
        /// </summary>
        /// <param name="validateCode">验证码</param>
        public Stream CreateValidatePicture(string validateCode)
        {

            Bitmap image = new Bitmap(GetImageWidth(validateCode.Length), GetImageHeight(validateCode.Length));
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                Random random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的背景干扰线
                var count = random.Next(30, 60);
                for (int i = 0; i < count; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(backColors[random.Next(backColors.Length)]), x1, y1, x2, y2);
                }
                //画验证码主体文字
                var pos = new PointF(random.Next(0, 3), random.Next(5, 10));
                for (int i = 0; i < validateCode.Length; i++)
                {
                    var font = fonts[random.Next(fonts.Length)];
                    var brush = new SolidBrush(foreColors[random.Next(foreColors.Length)]);
                    g.DrawString(validateCode[i].ToString(), font, brush, pos);
                    var size = g.MeasureString(validateCode[i].ToString(), font, image.Width / (validateCode.Length + 1));
                    pos += size;
                    pos.Y += random.Next(-3, 3);
                    pos.Y = Math.Min(image.Height - size.Height, pos.Y);
                    pos.Y = Math.Max(0, pos.Y);
                }
                //画图片的前景干扰线
                count = random.Next(3, 6);
                var poss = new Point[count];
                for (int i = 0; i < count / 2; i++)
                {
                    for (int j = 0; j < poss.Length; j++)
                    {
                        if (j < poss.Length / 2)
                        {
                            poss[j].X = random.Next(image.Width / 2);
                            poss[j].Y = random.Next(image.Height / 2);
                        }
                        else
                        {
                            poss[j].X = random.Next(image.Width / 2, image.Width);
                            poss[j].Y = random.Next(image.Height / 2, image.Height);
                        }
                    }
                    var c = noiseColors[random.Next(noiseColors.Length)];
                    c = Color.FromArgb(192, c);
                    g.DrawCurve(new Pen(c, clarity / random.Next(10, 15)), poss);
                }
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                //保存图片数据
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 得到验证码图片的长度
        /// </summary>
        /// <param name="validateNumLength">验证码的长度</param>
        /// <returns></returns>
        public static int GetImageWidth(int validateNumLength)
        {
            return (int)Math.Ceiling(validateNumLength * (double)clarity);
        }

        /// <summary>
        /// 得到验证码的高度
        /// </summary>
        /// <returns></returns>
        public static int GetImageHeight(int validateNumLength)
        {
            return (int)(validateNumLength / 2.0 * clarity);
        }

        #endregion
    }
}
