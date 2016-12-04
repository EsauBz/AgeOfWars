using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    class boton
    {
        string img;
        int x, y;
        int ancho;
        int alto;
        private bool visible;
        public boton(int x1, int y1, string img1, int alto1, int ancho1)
        {
            visible = true;
            img = img1;
            x = x1;
            y = y1;
            alto = alto1;
            ancho = ancho1;
        }

        public bool getVisible()
        {
            return visible;
        }

        public void setVisible(bool s)
        {
            visible = s;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public string getImg()
        {
            return img;
        }

        public int getAncho()
        {
            return ancho;
        }

        public int getAlto()
        {
            return alto;
        }
    }
}
