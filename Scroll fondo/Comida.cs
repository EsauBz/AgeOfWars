using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    class Comida
    {
        public Bitmap img = (Bitmap)Image.FromFile("food1.png");
        private int mapX, mapY;
        private bool selected;
        private int ancho;
        private int alto;
        private bool painted;
        private int recurso;
        private int DrawPercRec;
        public Comida(int x, int y)
        {
            recurso = 100;
            mapX = x;
            mapY = y;
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
