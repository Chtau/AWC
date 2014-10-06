using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AWC.WindowHandle
{
    public class DWMThumbnail
    {
        public static void Update(IntPtr iptrThumb, AWC.WindowHandle.NativStructs.RECT _rec)
        {
            if (iptrThumb != IntPtr.Zero)
            {
                System.Drawing.Size size = new System.Drawing.Size();
                Nativ.DwmQueryThumbnailSourceSize(iptrThumb, ref size);

                NativStructs.ThumbnailProperties props = new NativStructs.ThumbnailProperties();
                props.Visible = true;
                props.TNP = NativStructs.Dwm_TNP.Visible | NativStructs.Dwm_TNP.Recdestination | NativStructs.Dwm_TNP.Opacity;
                props.opacity = 255;
                props.rcDestination = _rec;

                if (size.Height < _rec.Height)
                    props.rcDestination.Bottom = props.rcDestination.Top + size.Height;

                if (size.Width < _rec.Width)
                    props.rcDestination.Right = props.rcDestination.Left + size.Width;

                //calculate scalingfor the Thumb to center it
                double radio = (size.Height / _rec.Height);
                int nWidth = (int)(size.Width / radio);
                int nHeight = (int)(size.Height / radio);

                if (nHeight > _rec.Height || nWidth < 0 || nHeight < 0)
                {
                    radio = (size.Width / _rec.Width);
                    nWidth = (int)(size.Width / radio);
                    nHeight = (int)(size.Height / radio);
                }

                if (_rec.Width > nWidth)
                    props.rcDestination.Left += (_rec.Width - nWidth) / 2;

                if (_rec.Height > nHeight)
                    props.rcDestination.Top += (_rec.Height - nHeight) / 2;

                props.rcDestination.Width = _rec.Width;
                props.rcDestination.Height = _rec.Height;

                AWC.WindowHandle.Nativ.DwmUpdateThumbnailProperties(iptrThumb, ref props);
            } else
            {
                throw new ArgumentNullException("iptrThumb, ctrl", "Can't Update Thumbnail");
            }
        }

        public static int Registry(System.Windows.Forms.Form frm, IntPtr handle, ref IntPtr iprThumb)
        {
             return AWC.WindowHandle.Nativ.DwmRegisterThumbnail(frm.Handle, handle, ref iprThumb);
        }


        public static int Unregistry(IntPtr iprThumb)
        {
            return AWC.WindowHandle.Nativ.DwmUnregisterThumbnail(iprThumb);
        }

    }
}
