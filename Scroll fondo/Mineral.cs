using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    class Mineral
    {
        public Bitmap img = (Bitmap)Image.FromFile("mineral.png");

        private int mapX, mapY;
        private bool selected;
        private int ancho;
        private int alto;
        private bool painted;
        private int recurso;
        private int DrawPercRec;
        public Mineral(int x, int y)
        {
            recurso = 100;
            mapX = x;
            mapY = y;
            ancho = img.Size.Width;
            alto = img.Size.Height;
        }
        public int getRecurso()
        {
            return recurso;
        }
        public void SetPaintedSprite(bool b)
        {
            painted = b;
        }
        public void disminuyeRecurso(int quita)
        {
            recurso -= quita;
        }

        public int getMapX()
        {
            return mapX;
        }

        public int getMapY()
        {
            return mapY;
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
