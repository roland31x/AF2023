using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace curs6_Poligoane
{
    public class Poligon
    {
        public static Random rng = new Random();


        PointF[] points;
        public Poligon(string FileName)
        {
            StreamReader streamReader = new StreamReader(FileName);
            List<string> lines = new List<string>();
            string buffer;
            while(!streamReader.EndOfStream)
            {
                lines.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            points = new PointF[lines.Count];
            for(int i = 0; i < lines.Count; i++)
            {
                float x = float.Parse(lines[i].Split(' ')[0]);
                float y = float.Parse(lines[i].Split(' ')[1]);
                points[i] = new PointF(x, y);
            }
        }
        public Poligon(int n, int w, int h)
        {
            points = new PointF[n];
            for(int i = 0; i < n; i++)
            {
                points[i] = new PointF(rng.Next(0, w), rng.Next(0, h));
            }
        }
        public PointF Gpoint()
        {
            float x = 0;
            float y = 0;
            for(int i = 0; i < points.Length; i++)
            {
                x += points[i].X;
                y += points[i].Y;
            }
            x = x / points.Length;
            y = y / points.Length;
            return new PointF(x, y);
        }
        public float Perimeter()
        {
            float perimeter = 0;
            for(int i = 0; i <  points.Length - 1; i++)
            {
                perimeter += MyMath.Dist(points[i], points[(i + 1) % points.Length]);
            }
            return perimeter;
        }
        public float Area()
        {
            float area = 0;
            for(int i = 0; i < points.Length - 1; i++)
            {
                area += points[i].X * points[(i + 1) % points.Length].Y - points[i].Y * points[(i + 1) % points.Length].X;
            }
            return Math.Abs(area) / 2;
        }
        public void Draw(Graphics g)
        {
            g.DrawPolygon(Pens.Black, points);
        }
    }
}
