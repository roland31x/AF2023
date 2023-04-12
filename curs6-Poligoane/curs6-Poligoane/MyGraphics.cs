using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curs6_Poligoane
{
    public class MyGraphics
    {
        Bitmap bmp;
        public Graphics gfx;
        PictureBox pb;
        public int ResX { get { return pb.Width; } }
        public int ResY { get { return pb.Height; } }
        public MyGraphics(PictureBox p)
        {
            pb = p;
            bmp = new Bitmap(p.Width, p.Height);
            gfx = Graphics.FromImage(bmp);
        }
        public void Refresh()
        {
            pb.Image = bmp;
        }
        public void Clear(Color c)
        {
            gfx.Clear(c);
        }
    }
}
