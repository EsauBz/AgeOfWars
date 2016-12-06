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

        public void RevisaColisionCampal1()
        {
            checkMilicia();
            checkAldeano();
            checkUespecial();
            checkNaves();
            checkCU();
            checkFarms();
            checkCuarteles();
        }
        public void checkMilicia()
        {
            foreach (UnidadMilitar a in player1.getlistMilicia())
            {
                foreach (UnidadMilitar b in Player2.getlistMilicia())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(10);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(10);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getlistMilicia().Count; j++)
            {
                if (player1.getlistMilicia()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getlistMilicia().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getlistMilicia().Count; j++)
            {
                if (Player2.getlistMilicia()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getlistMilicia().RemoveAt(j);
                    j--;
                }
            }
            /********** Aqui comienza el Segundo Foreach del metodo para revisar contra aldeanos*****/
            foreach (UnidadMilitar a in player1.getlistMilicia())
            {
                foreach (Aldeano b in Player2.getListAldeanos())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(10);
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListAldeanos().Count; j++)
            {
                if (Player2.getListAldeanos()[j].getLifeAldeano() <= 0)
                {
                    Player2.getListAldeanos().RemoveAt(j);
                    j--;
                }
            }
            /****** Aqui comienza el tercer foreach del metodo aqui se compara contra Uespecial****/
            foreach (UnidadMilitar a in player1.getlistMilicia())
            {
                foreach (UnidadMilitar b in Player2.getListUespecial())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(20);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(10);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getlistMilicia().Count; j++)
            {
                if (player1.getlistMilicia()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getlistMilicia().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getListUespecial().Count; j++)
            {
                if (Player2.getListUespecial()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListUespecial().RemoveAt(j);
                    j--;
                }
            }
            /*******  Aqui comienza el cuarto foreach del metodo aqui se compara contra naves******/
            foreach (UnidadMilitar a in player1.getlistMilicia())
            {
                foreach (UnidadMilitar b in Player2.getListNaves())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(25);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(10);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getlistMilicia().Count; j++)
            {
                if (player1.getlistMilicia()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getlistMilicia().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getListNaves().Count; j++)
            {
                if (Player2.getListNaves()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListNaves().RemoveAt(j);
                    j--;
                }
            }
            /****** Aqui comienza el quinto if del metodo, aqui se compara contra eficios******/
            foreach (UnidadMilitar a in player1.getlistMilicia())
            {
                foreach (Edificio b in Player2.getlistCUs())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(10);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getlistCUs().Count; j++)
            {
                if (Player2.getlistCUs()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getlistCUs().RemoveAt(j);
                    j--;
                }
            }
            /****************************************************************************************/
            foreach (UnidadMilitar a in player1.getlistMilicia())
            {
                foreach (Edificio b in Player2.getListFarms())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(10);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListFarms().Count; j++)
            {
                if (Player2.getListFarms()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListFarms().RemoveAt(j);
                    j--;
                }
            }
            /********************************************************************************************/
            foreach (UnidadMilitar a in player1.getlistMilicia())
            {
                foreach (Edificio b in Player2.getListCuarteles())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(10);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListCuarteles().Count; j++)
            {
                if (Player2.getListCuarteles()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListCuarteles().RemoveAt(j);
                    j--;
                }
            }
            /***********************************************************************************************/
        }
        public void checkAldeano()
        {
            foreach (Aldeano a in player1.getListAldeanos())
            {
                foreach (UnidadMilitar b in Player2.getlistMilicia())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(10);                    
                    }
                    else
                    {
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListAldeanos().Count; j++)
            {
                if (player1.getListAldeanos()[j].getLifeAldeano() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListAldeanos().RemoveAt(j);
                    j--;
                }
            }                        
            /****** Aqui comienza el tercer foreach del metodo aqui se compara contra Uespecial****/
            foreach (Aldeano a in player1.getListAldeanos())
            {
                foreach (UnidadMilitar b in Player2.getListUespecial())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(20);                       
                    }
                    else
                    {
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListAldeanos().Count; j++)
            {
                if (player1.getListAldeanos()[j].getLifeAldeano() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListAldeanos().RemoveAt(j);
                    j--;
                }
            }            
            /*******  Aqui comienza el cuarto foreach del metodo aqui se compara contra naves******/
            foreach (Aldeano a in player1.getListAldeanos())
            {
                foreach (UnidadMilitar b in Player2.getListNaves())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(25);                       
                    }
                    else
                    {
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListAldeanos().Count; j++)
            {
                if (player1.getListAldeanos()[j].getLifeAldeano() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListAldeanos().RemoveAt(j);
                    j--;
                }
            }                       
            /***********************************************************************************************/
        }
        public void checkUespecial()
        {
            foreach (UnidadMilitar a in player1.getListUespecial())
            {
                foreach (UnidadMilitar b in Player2.getlistMilicia())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(10);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(20);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListUespecial().Count; j++)
            {
                if (player1.getListUespecial()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListUespecial().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getlistMilicia().Count; j++)
            {
                if (Player2.getlistMilicia()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getlistMilicia().RemoveAt(j);
                    j--;
                }
            }
            /********** Aqui comienza el Segundo Foreach del metodo para revisar contra aldeanos*****/
            foreach (UnidadMilitar a in player1.getListUespecial())
            {
                foreach (Aldeano b in Player2.getListAldeanos())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(20);
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListAldeanos().Count; j++)
            {
                if (Player2.getListAldeanos()[j].getLifeAldeano() <= 0)
                {
                    Player2.getListAldeanos().RemoveAt(j);
                    j--;
                }
            }
            /****** Aqui comienza el tercer foreach del metodo aqui se compara contra Uespecial****/
            foreach (UnidadMilitar a in player1.getListUespecial())
            {
                foreach (UnidadMilitar b in Player2.getListUespecial())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(20);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(20);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListUespecial().Count; j++)
            {
                if (player1.getListUespecial()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListUespecial().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getListUespecial().Count; j++)
            {
                if (Player2.getListUespecial()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListUespecial().RemoveAt(j);
                    j--;
                }
            }
            /*******  Aqui comienza el cuarto foreach del metodo aqui se compara contra naves******/
            foreach (UnidadMilitar a in player1.getListUespecial())
            {
                foreach (UnidadMilitar b in Player2.getListNaves())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(25);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(20);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListUespecial().Count; j++)
            {
                if (player1.getListUespecial()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListUespecial().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getListNaves().Count; j++)
            {
                if (Player2.getListNaves()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListNaves().RemoveAt(j);
                    j--;
                }
            }
            /****** Aqui comienza el quinto if del metodo, aqui se compara contra eficios******/
            foreach (UnidadMilitar a in player1.getListUespecial())
            {
                foreach (Edificio b in Player2.getlistCUs())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(20);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getlistCUs().Count; j++)
            {
                if (Player2.getlistCUs()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getlistCUs().RemoveAt(j);
                    j--;
                }
            }
            /****************************************************************************************/
            foreach (UnidadMilitar a in player1.getListUespecial())
            {
                foreach (Edificio b in Player2.getListFarms())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(20);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListFarms().Count; j++)
            {
                if (Player2.getListFarms()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListFarms().RemoveAt(j);
                    j--;
                }
            }
            /********************************************************************************************/
            foreach (UnidadMilitar a in player1.getListUespecial())
            {
                foreach (Edificio b in Player2.getListCuarteles())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(20);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListCuarteles().Count; j++)
            {
                if (Player2.getListCuarteles()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListCuarteles().RemoveAt(j);
                    j--;
                }
            }
            /***********************************************************************************************/

        }
        public void checkNaves()
        {
            foreach (UnidadMilitar a in player1.getListNaves())
            {
                foreach (UnidadMilitar b in Player2.getlistMilicia())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(10);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(25);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListNaves().Count; j++)
            {
                if (player1.getListNaves()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListNaves().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getlistMilicia().Count; j++)
            {
                if (Player2.getlistMilicia()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getlistMilicia().RemoveAt(j);
                    j--;
                }
            }
            /********** Aqui comienza el Segundo Foreach del metodo para revisar contra aldeanos*****/
            foreach (UnidadMilitar a in player1.getListNaves())
            {
                foreach (Aldeano b in Player2.getListAldeanos())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(25);
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListAldeanos().Count; j++)
            {
                if (Player2.getListAldeanos()[j].getLifeAldeano() <= 0)
                {
                    Player2.getListAldeanos().RemoveAt(j);
                    j--;
                }
            }
            /****** Aqui comienza el tercer foreach del metodo aqui se compara contra Uespecial****/
            foreach (UnidadMilitar a in player1.getListNaves())
            {
                foreach (UnidadMilitar b in Player2.getListUespecial())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(20);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(25);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListNaves().Count; j++)
            {
                if (player1.getListNaves()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListNaves().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getListUespecial().Count; j++)
            {
                if (Player2.getListUespecial()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListUespecial().RemoveAt(j);
                    j--;
                }
            }
            /*******  Aqui comienza el cuarto foreach del metodo aqui se compara contra naves******/
            foreach (UnidadMilitar a in player1.getListNaves())
            {
                foreach (UnidadMilitar b in Player2.getListNaves())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(25);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                        b.recibeDaño(25);
                        if (b.getIataca() == false) { b.ataca(); b.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListNaves().Count; j++)
            {
                if (player1.getListNaves()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListNaves().RemoveAt(j);
                    j--;
                }
            }
            for (int j = 0; j < Player2.getListNaves().Count; j++)
            {
                if (Player2.getListNaves()[j].getLifeSoldado() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListNaves().RemoveAt(j);
                    j--;
                }
            }
            /****** Aqui comienza el quinto if del metodo, aqui se compara contra eficios******/
            foreach (UnidadMilitar a in player1.getListNaves())
            {
                foreach (Edificio b in Player2.getlistCUs())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(25);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getlistCUs().Count; j++)
            {
                if (Player2.getlistCUs()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getlistCUs().RemoveAt(j);
                    j--;
                }
            }
            /****************************************************************************************/
            foreach (UnidadMilitar a in player1.getListNaves())
            {
                foreach (Edificio b in Player2.getListFarms())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(25);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListFarms().Count; j++)
            {
                if (Player2.getListFarms()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListFarms().RemoveAt(j);
                    j--;
                }
            }
            /********************************************************************************************/
            foreach (UnidadMilitar a in player1.getListNaves())
            {
                foreach (Edificio b in Player2.getListCuarteles())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        b.recibeDaño(25);
                        if (a.getIataca() == false) { a.ataca(); a.setIataca(true); }
                    }
                    else
                    {
                        a.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    a.Update();
                }
            }
            for (int j = 0; j < Player2.getListCuarteles().Count; j++)
            {
                if (Player2.getListCuarteles()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in player1.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    Player2.getListCuarteles().RemoveAt(j);
                    j--;
                }
            }
            /***********************************************************************************************/

        }
        public void checkCU()
        {
            foreach (Edificio a in player1.getlistCUs())
            {
                foreach (UnidadMilitar b in Player2.getlistMilicia())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(10);                        
                    }
                    else
                    {
                        b.setIataca(false);
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getlistCUs().Count; j++)
            {
                if (player1.getlistCUs()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getlistCUs().RemoveAt(j);
                    j--;
                }
            }                        
            /****** Aqui comienza el tercer foreach del metodo aqui se compara contra Uespecial****/
            foreach (Edificio a in player1.getlistCUs())
            {
                foreach (UnidadMilitar b in Player2.getListUespecial())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(20);                       
                    }
                    else
                    {
                        b.setIataca(false);
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getlistCUs().Count; j++)
            {
                if (player1.getlistCUs()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getlistCUs().RemoveAt(j);
                    j--;
                }
            }                       
            /*******  Aqui comienza el cuarto foreach del metodo aqui se compara contra naves******/
            foreach (Edificio a in player1.getlistCUs())
            {
                foreach (UnidadMilitar b in Player2.getListNaves())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(25);                       
                    }
                    else
                    {
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getlistCUs().Count; j++)
            {
                if (player1.getlistCUs()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getlistCUs().RemoveAt(j);
                    j--;
                }
            }                       
        }

        public void checkFarms()
        {
            foreach (Edificio a in player1.getListFarms())
            {
                foreach (UnidadMilitar b in Player2.getlistMilicia())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(10);
                    }
                    else
                    {
                        b.setIataca(false);
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListFarms().Count; j++)
            {
                if (player1.getListFarms()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListFarms().RemoveAt(j);
                    j--;
                }
            }
            /****** Aqui comienza el tercer foreach del metodo aqui se compara contra Uespecial****/
            foreach (Edificio a in player1.getListFarms())
            {
                foreach (UnidadMilitar b in Player2.getListUespecial())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(20);
                    }
                    else
                    {
                        b.setIataca(false);
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListFarms().Count; j++)
            {
                if (player1.getListFarms()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListFarms().RemoveAt(j);
                    j--;
                }
            }
            /*******  Aqui comienza el cuarto foreach del metodo aqui se compara contra naves******/
            foreach (Edificio a in player1.getListFarms())
            {
                foreach (UnidadMilitar b in Player2.getListNaves())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(25);
                    }
                    else
                    {
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListFarms().Count; j++)
            {
                if (player1.getListFarms()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListFarms().RemoveAt(j);
                    j--;
                }
            }
        }
        public void checkCuarteles()
        {
            foreach (Edificio a in player1.getListCuarteles())
            {
                foreach (UnidadMilitar b in Player2.getlistMilicia())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(10);
                    }
                    else
                    {
                        b.setIataca(false);
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListCuarteles().Count; j++)
            {
                if (player1.getListCuarteles()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getlistMilicia())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListCuarteles().RemoveAt(j);
                    j--;
                }
            }
            /****** Aqui comienza el tercer foreach del metodo aqui se compara contra Uespecial****/
            foreach (Edificio a in player1.getListCuarteles())
            {
                foreach (UnidadMilitar b in Player2.getListUespecial())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(20);
                    }
                    else
                    {
                        b.setIataca(false);
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListCuarteles().Count; j++)
            {
                if (player1.getListCuarteles()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListUespecial())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListCuarteles().RemoveAt(j);
                    j--;
                }
            }
            /*******  Aqui comienza el cuarto foreach del metodo aqui se compara contra naves******/
            foreach (Edificio a in player1.getListCuarteles())
            {
                foreach (UnidadMilitar b in Player2.getListNaves())
                {
                    if (a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() >= b.getMapY() && a.getMapY() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() >= b.getMapX() && a.getMapX() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto() ||
                        a.getMapX() + a.getAncho() >= b.getMapX() && a.getMapX() + a.getAncho() <= b.getMapX() + b.getAncho() && a.getMapY() + a.getAlto() >= b.getMapY() && a.getMapY() + a.getAlto() <= b.getMapY() + b.getAlto())
                    {
                        a.recibeDaño(25);
                    }
                    else
                    {
                        b.setIataca(false);
                        //(a.getIcamina() == false) { a.parado(); }
                    }
                    b.Update();
                }
            }
            for (int j = 0; j < player1.getListCuarteles().Count; j++)
            {
                if (player1.getListCuarteles()[j].getLifeEdificio() <= 0)
                {
                    foreach (UnidadMilitar a in Player2.getListNaves())
                    {
                        if (a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() >= RecursoComida[j].getMapY() && a.getMapY() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() >= RecursoComida[j].getMapX() && a.getMapX() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto() ||
                        a.getMapX() + a.getAncho() >= RecursoComida[j].getMapX() && a.getMapX() + a.getAncho() <= RecursoComida[j].getMapX() + RecursoComida[j].getAncho() && a.getMapY() + a.getAlto() >= RecursoComida[j].getMapY() && a.getMapY() + a.getAlto() <= RecursoComida[j].getMapY() + RecursoComida[j].getAlto())
                        {
                            a.parado();
                        }
                    }
                    player1.getListCuarteles().RemoveAt(j);
                    j--;
                }
            }
        }
    }
}
