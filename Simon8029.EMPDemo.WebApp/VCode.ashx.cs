using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Simon8029.EMPDemo.WebApp
{
    /// <summary>
    /// VCode 的摘要说明
    /// </summary>
    public class VCode : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        /// 验证码 存入Session 的 键
        /// </summary>
        public const string vCodeName = "vcode";

        HttpContext context;

        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            //1.0 生成验证码字符串
            string strVCode = GenerateCheckCode();
            //1.1保存到 Session 中
            context.Session[vCodeName] = strVCode;
            //2.0 绘制验证码图片 输出到浏览器
            CreateCheckCodeImage(strVCode);
        }

        #region 1.0 生成随机验证码方法 -string GenerateCheckCode()
        /// <summary>
        /// 生成随机验证码方法
        /// </summary>
        /// <returns></returns>
        private string GenerateCheckCode()
        {
            int number;
            char code;
            string checkCode = String.Empty;

            System.Random random = new Random();

            for (int i = 0; i < 5; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('0' + (char)(number % 10));
                //code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }
            return checkCode;
        } 
        #endregion

        #region 2.0 生成验证吗图片，并返回浏览器 -void CreateCheckCodeImage(string checkCode)
        /// <summary>
        /// 2.0 生成验证吗图片，并返回浏览器
        /// </summary>
        /// <param name="checkCode"></param>
        private void CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap((int)Math.Ceiling((checkCode.Length * 12.5)), 22);
            Graphics g = Graphics.FromImage(image);

            try
            {
                //生成随机生成器
                Random random = new Random();

                //清空图片背景色
                g.Clear(Color.White);

                //画图片的背景噪音线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);

                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }

                Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic));
                System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(checkCode, font, brush, 2, 2);

                //画图片的前景噪音点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);

                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }

                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
                context.Response.ClearContent();
                context.Response.ContentType = "image/Gif";
                context.Response.BinaryWrite(ms.ToArray());
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        } 
        #endregion
    


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}