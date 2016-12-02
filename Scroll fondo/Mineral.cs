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
        private int DrawPercLife;
        public Mineral()
        {

        }
        public void disminuyeRecurso(int quita)
        {
            recurso -= quita;
        }
    }
}
