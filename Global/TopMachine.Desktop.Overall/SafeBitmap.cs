using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TopMachine.Desktop.Overall;

namespace TopMachine.Desktop.Overall
{
    public class SafeBitmap : IDisposable
    {
        public Bitmap bmp;
        public Graphics g;
        Guid MemoryCheckId;

        public SafeBitmap(Bitmap bb, Graphics gg, object parent)
        {
            bmp = bb;
            g = gg;
            MemoryCheckId = MemoryCheck.Register(this, "SafeBitmap" + parent.GetType().Name);
        }

        public SafeBitmap(Bitmap bb, object parent)
        {
            bmp = bb;
            g = Graphics.FromImage(bmp); 
            MemoryCheckId = MemoryCheck.Register(this, "SafeBitmap : " + parent.GetType().Name );
        }        

        public void Dispose()
        {
            MemoryCheck.Unregister(MemoryCheckId);
            if (g == null)
            {
                g.Dispose();
                g = null;
            }

            if (bmp != null)
            {
                bmp.Dispose();
                bmp = null; 
            }

        }
    }

}
