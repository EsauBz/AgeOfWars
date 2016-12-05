using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    /*
     * 
     La clase mundo contiene a dos jugadores, uno es el jugador humano, el otro es el jugador maquina representado por
     procesamiento realizado por Timers. El mundo tiene una lista de minerales y comida que son los recursos del mundo
     y un bitmap llamado barra que representa la barra de herramientas del juego.
         */
    class Mundo 
    {
        Random r = new Random(); 
        private List<Mineral> RecursoMineral; //Lista de recurso de Mineral
        private List<Comida> RecursoComida; //Lista de recurso de Comida 
        private Bitmap escenario; // Bitmap que tiene uno de los dos fondos elaborados para el juego.
        private Player player1; //Primer Jugador: Humano
        private Player Player2; //Segundo Jugador 
        Bitmap barra = (Bitmap)Image.FromFile("barr1.png");
        int coordmapX;
        int coordmapY;
        public Mundo(int i,bool band)
        {
            player1 = new Player(band);
            if (band) { Player2 = new Player(false, player1.getCoordInicalMapY(), player1.getCoordInicalMapX()); } else { Player2 = new Player(true, player1.getCoordInicalMapY(), player1.getCoordInicalMapX()); }
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
        }//Fin de constructor

        public Player getPlayer2()
        {
            return Player2;
        }

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
                        if (a.getIrecoge() == false && player1.getListFarms().Count > 0) { a.Recoge(); a.setIrecoge(true); }
                        player1.setMineral(player1.getListFarms().Count);
                        b.disminuyeRecurso(player1.getListFarms().Count);
                    }
                    else
                    {
                        a.setIrecoge(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                }
                a.Update();
            }
            for (int j = 0; j < RecursoMineral.Count; j++)
            {
                if (RecursoMineral[j].getRecurso() <= 0)
                {
                    foreach (Aldeano a in player1.getListAldeanos())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    RecursoMineral.RemoveAt(j);
                    j--;
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
                        if (a.getIrecoge() == false && player1.getListFarms().Count > 0) { a.Recoge(); a.setIrecoge(true); }
                        player1.setComida(player1.getListFarms().Count);
                        b.disminuyeRecurso(player1.getListFarms().Count);
                    }
                    else
                    {
                        a.setIrecoge(false);
                    }
                }
                a.Update();
            }
            for (int j = 0; j < RecursoComida.Count; j++)
            {
                if (RecursoComida[j].getRecurso() <= 0)
                {
                    foreach (Aldeano a in player1.getListAldeanos())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    RecursoComida.RemoveAt(j);
                    j--;
                }
            }
        }//Fin de metodo

        public void RevisaAtacarEnemigo()
        {
            //Revisar las listas de unidades para saber si estan peleando
            //COMPARACIONES
        }
    }
}
