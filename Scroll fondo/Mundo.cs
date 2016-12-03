using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    class Mundo
    {
        private List<Mineral> RecursoMineral;
        private List<Comida> RecursoComida;
        private Bitmap escenario;
        private Player player1;
        Bitmap barra = (Bitmap)Image.FromFile("barr1.png");
        int coordmapX;
        int coordmapY;
        public Mundo(int i)
        {
            player1 = new Player();
            RecursoMineral = new List<Mineral>();
            RecursoComida = new List<Comida>();
            if (i == 1) { escenario = (Bitmap)Image.FromFile("fondo.png"); }
            else
            {
                escenario = (Bitmap)Image.FromFile("fondo2.png");
            }
            coordmapX = -player1.getCoordInicalMapX() + 350;
            coordmapY = -player1.getCoordInicalMapY() + 150;
            startAñadeComida();
            startAñadeMinerales();
        }//Fin de constructor

        public void pluscoordmapX()
        {
            coordmapX += 20;
        }

        public void pluscoordmapY()
        {
            coordmapY += 20;
        }

        public void lesscoordmapX()
        {
            coordmapX -= 20;
        }

        public void lesscoordmapY()
        {
            coordmapY -= 20;
        }

        public Bitmap getImgBarra()
        {
            return barra;
        }

        public int getcoordmapX()
        {
            return coordmapX;
        }
        
        public void setcoordmapX(int a)
        {
            coordmapX = a;
        }

        public int getcoordmapY()
        {
            return coordmapY;
        }

        public void setcoordmapY(int a)
        {
            coordmapY = a;
        }

        public Player getPlayer()
        {
            return player1;
        }

        private void startAñadeMinerales()
        {

        }
        private void startAñadeComida()
        {

        }
        public Bitmap getImgMundo()
        {
            return escenario;
        }
        public List<Mineral> getRecursoMinMundo()
        {
            return RecursoMineral;
        }
        public List<Comida> getRecursoComMundo()
        {
            return RecursoComida;
        }
    }
}
