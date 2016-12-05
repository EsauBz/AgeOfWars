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
        Random r = new Random();
        private List<Mineral> RecursoMineral;
        private List<Comida> RecursoComida;
        private Bitmap escenario;
        private Player player1;
        Bitmap barra = (Bitmap)Image.FromFile("barr1.png");
        int coordmapX;
        int coordmapY;
        public Mundo(int i,bool band)
        {
            player1 = new Player(band);
            RecursoMineral = new List<Mineral>();
            RecursoComida = new List<Comida>();
            if (i == 1) { escenario = (Bitmap)Image.FromFile("fondo.png"); }
            else
            {
                escenario = (Bitmap)Image.FromFile("fondo2.png");
            }
            Mineral min;
            for(int cont =0; cont<15;cont++)
            {
                min = new Mineral(r.Next(100,2901), r.Next(100, 2901));
                RecursoMineral.Add(min);
            }
            Comida Com;
            for (int cont = 0; cont < 15; cont++)
            {
                Com = new Comida(r.Next(100, 2901), r.Next(100, 2901));
                RecursoComida.Add(Com);
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

        public void revisaRecogidaAldeanos()
        {
            foreach (Aldeano a in player1.getListAldeanos())// Revisar Recogida Aldeanos
            {
                foreach (Mineral b in RecursoMineral)// Revisar Recogida Aldeanos
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        player1.setMineral(player1.getListFarms().Count);
                        b.disminuyeRecurso(player1.getListFarms().Count);
                    }
                }
            }
            for (int cont = 0; cont < RecursoMineral.Count; cont++)
            {
                if (RecursoMineral[cont].getRecurso() <= 0)
                {
                    RecursoMineral.RemoveAt(cont);
                    cont--;
                }
            }
        }
        public void RevisaRecogidaComida()
        { 
            foreach (Aldeano a in player1.getListAldeanos())// Revisar Recogida Aldeanos
            {
                foreach (Comida b in RecursoComida)// Revisar Recogida Aldeanos
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        player1.setComida(player1.getListFarms().Count);
                        b.disminuyeRecurso(player1.getListFarms().Count);
                    }
                }
            }
            for (int j = 0; j < RecursoComida.Count; j++)
            {
                if (RecursoComida[j].getRecurso() <= 0)
                {
                    RecursoMineral.RemoveAt(j);
                    j--;
                }
            }
        }//Fin de metodo
    }
}
