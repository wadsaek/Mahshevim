using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Web;

/// <summary>
/// Summary description for UserInfo
/// </summary>
public class UserInfo
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public DateTime Birth { get; set; }

    /// <summary>
    /// Crops an Image to a circlified version of itself (hopefully)
    /// </summary>
    /// <param name="srcImage">Image to be cropped</param>
    /// <returns>The circlified Image</returns>
    public static Image ClipToCircle(Image srcImage)
    {
        Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);
        int h = srcImage.Height;
        int w = srcImage.Width;
        PointF center = new PointF(h / 2, w / 2);
        Color backGround = Color.FromArgb(0, 0, 0, 0); 

        float radius = h>w ? w/2 :h/2;

        using (Graphics g = Graphics.FromImage(dstImage))
        {
            RectangleF r = new RectangleF(center.X - radius, center.Y - radius,
                                                     radius * 2, radius * 2);

            // enables smoothing of the edge of the circle (less pixelated)
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // fills background color
            using (Brush br = new SolidBrush(backGround))
            {
                g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
            }

            // adds the new ellipse & draws the image again 
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(r);
            g.SetClip(path);
            g.DrawImage(srcImage, 0, 0);

            return dstImage;
        }
    }

    public UserInfo(int id, string usern, string fulln, DateTime datetime, string mail)
    {
        Id = id;
        UserName = usern;
        FullName = fulln;
        Birth=datetime;
        Email=mail;
    }
    public UserInfo(int id, string usern, string mail)
    {
        Id = id;
        UserName = usern;
        Email = mail;
    }
    public UserInfo(int id, string usern, string fulln, string mail)
    {
        Id = id;
        UserName = usern;
        FullName = fulln;
        Email = mail;
    }
    public UserInfo(int id, string usern, DateTime datetime, string mail)
    {
        Id = id;
        UserName = usern;
        Birth = datetime;
        Email = mail;
    }
    public UserInfo()
    {
        Id = -1;
        UserName = "";
        FullName = "";
        Birth = new DateTime();
        Email = "";
    }
}