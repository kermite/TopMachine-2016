using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TopMachine.Desktop.Controls 
{
    public class Shapes
    {
        public  System.Drawing.Drawing2D.GraphicsPath path = new GraphicsPath();

        ~Shapes()
        {
            path.Dispose(); 
        }

        public Shapes()
        {
        }

        public Shapes(string t, FontFamily f, int style, float size, Rectangle r)
        {
            AddText(t, f, style, size, r); 
        }

        public Shapes(Rectangle r, int round)
        {
            path.AddRectangle(r);
            RoundPath(round); 
        }

        public void AddText(string t, FontFamily f, int style, float size, RectangleF r)
        {
            StringFormat sf = StringFormat.GenericDefault;
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center; 
            path.AddString(t, f, 1, size, r, sf);
        }

        public GraphicsPath GetPath()
        {
            return path;
        }

        public RectangleF GetRectangle()
        {
            return path.GetBounds();
        }

        public void Rotate(float angle)
        {
            Matrix m = new Matrix();
            RectangleF rf = GetRectangle(); 
            m.RotateAt(angle, new PointF(rf.Left + rf.Width / 2, rf.Top + rf.Height / 2));
            path.Transform(m); 
        }

        public void RoundPath(int round)
        {
          //  round = 15;
            PointF[] pp = path.PathPoints;
            PointF[] Targetp = new PointF[(pp.Length) * 6 + 1];


            Array.Resize(ref pp, pp.Length + 1);

            pp[pp.Length-1] = pp[0];

            int nbPath = 0;
            double distx = 0;
            double disty = 0;

            int nbcotes = (pp.Length - 1) * 2;
            for (int x = 0; x < nbcotes; x += 2)
            {


                PointF p0 = pp[x / 2];
                PointF p1 = pp[x / 2 + 1];

                double dx = pp[x / 2 + 1].X - pp[x / 2].X;
                double dy = pp[x / 2 + 1].Y - pp[x / 2].Y;
                double d = Math.Sqrt(dx * dx + dy * dy);
                double t = round / d;
                PointF B1 = interpolate(0.5 * t, p0, p1);
                PointF B2 = interpolate(1.0 - 0.5 * t, p0, p1);

                Targetp[x * 3] = B1;
                Targetp[x * 3 + 1] = B1;
                Targetp[x * 3 + 2] = B1;

                if (x != 0)
                {
                    Targetp[x * 3 - 2].X = Targetp[x * 3 - 3].X + (pp[x / 2].X - Targetp[x * 3 - 3].X) / 2;
                    Targetp[x * 3 - 2].Y = Targetp[x * 3 - 3].Y + (pp[x / 2].Y - Targetp[x * 3 - 3].Y) / 2;
                    Targetp[x * 3 - 1].X = Targetp[x * 3].X + (pp[x / 2].X - Targetp[x * 3].X) / 2;
                    Targetp[x * 3 - 1].Y = Targetp[x * 3].Y + (pp[x / 2].Y - Targetp[x * 3].Y) / 2;
                }

                Targetp[(x + 1) * 3] = B2;

            }

            int last = nbcotes * 3 - 2;
            Targetp[last].X = Targetp[last - 1].X + (pp[0].X - Targetp[last - 1].X) / 2;
            Targetp[last].Y = Targetp[last - 1].Y + (pp[0].Y - Targetp[last - 1].Y) / 2;
            Targetp[last + 1].X = Targetp[0].X + (pp[0].X - Targetp[0].X) / 2;
            Targetp[last + 1].Y = Targetp[0].Y + (pp[0].Y - Targetp[0].Y) / 2;
            Targetp[last + 2].X = Targetp[0].X;
            Targetp[last + 2].Y = Targetp[0].Y;

            path.Reset();
            path.AddBeziers(Targetp);
            path.CloseFigure();

        }

        PointF interpolate(double t, PointF p0, PointF p1)
	    {
		    PointF	rv = new PointF();
		    rv.X = (float) (p0.X + t * (p1.X - p0.X));
		    rv.Y = (float) (p0.Y + t * (p1.Y - p0.Y));
		    return rv;
	    }

    }
}
