using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Scroll_fondo
{
    public partial class Form1 : Form
    {
        /*Inicio  de Variables del Programa* ********/
        Random rnd = new Random();
        Font fnt;
        Brush bsh;
        Menu menu;
        char opc;
        Mundo World;
        Graphics grap;      
        int bordeX1;
        int bordeY1;
        int bordeX2;
        int bordeY2;
        int LimitePantX1 = 0;
        int LimitePantY1 = 0;
        int LimitePantX2 = 4003;
        int LimitePantY2 = 4123;
        Pen p;
        System.Timers.Timer Trecurso = new System.Timers.Timer();
        System.Timers.Timer Tcomida = new System.Timers.Timer();
        System.Timers.Timer Tcuarteles = new System.Timers.Timer();
        System.Timers.Timer TCuD = new System.Timers.Timer();
        System.Timers.Timer TFrames = new System.Timers.Timer();
        System.Timers.Timer TUMFrames = new System.Timers.Timer();
        System.Timers.Timer TVerificaAtaque = new System.Timers.Timer();
        System.Timers.Timer TEdificioEnem = new System.Timers.Timer();
        System.Timers.Timer TMovimientoEnem = new System.Timers.Timer();
        System.Timers.Timer TFramesEnem = new System.Timers.Timer();
        System.Timers.Timer Timer1BatallaCampal = new System.Timers.Timer();
        System.Timers.Timer TimerGanaPierde = new System.Timers.Timer();
        /*Inicio de Constructor ************/
        public Form1()
        {
            InitializeComponent();
            fnt = new Font(this.Font, FontStyle.Italic);
            bsh = new SolidBrush(Color.Black);
            p = new Pen(Color.Green);
            p.Width = 10;
            Start();
            InicializaTimers();
            /*******Fin de Constructor**************/
        }

        public void startJuego(bool a)///se ejecuta despues de elegir bando
        {
            Random r = new Random();
            World = new Mundo(r.Next(0, 2),a);
            menu = new Menu("bando5.jpg");
            menu.getBotones().Clear();
            menu.GeneraBotonesGame();
            bordeX1 = World.getPlayer().getCoordInicalMapX() - 350;
            bordeY1 = World.getPlayer().getCoordInicalMapY() - 150;
            bordeX2 = bordeX1 + this.ClientSize.Width;
            bordeY2 = bordeY1 + this.ClientSize.Height;
            /****************************/
            Trecurso.Start();
            /****************************/
            Tcomida.Start();
            /****************************/
            Tcuarteles.Start();
            /****************************/
            TCuD.Start();
            /****************************/
            TFrames.Start();
            /****************************/
            TUMFrames.Start();
            /****************************/
            TVerificaAtaque.Start();
            /****************************/
            TEdificioEnem.Start();
            /****************************/
            TMovimientoEnem.Start();
            /*****************************/
            TFramesEnem.Start();
            /*****************************/
            Timer1BatallaCampal.Start();
            /******************************/
            TimerGanaPierde.Start();
        }
        private void InicializaTimers()
        {
            /*Variables Timers Game*/
            Trecurso.Interval = 1000;
            Trecurso.Elapsed += RevisaRecogidaPlayer;
            /**********************/
            Tcomida.Interval = 1000;
            Tcomida.Elapsed += RevisaComidaPlayer;
            /****************************/
            Tcuarteles.Interval = 1000;
            Tcuarteles.Elapsed += RevisaCuartelesPlayer;
            /****************************/
            TCuD.Interval = 1000;
            TCuD.Elapsed += RevisaCUDestruidos;
            /****************************/
            TFrames.Interval = 15;
            TFrames.Elapsed += UpdateAnimaFrames;
            /****************************/
            TUMFrames.Interval = 15;
            TUMFrames.Elapsed += UpdateAnimaMil;
            /****************************/
            TEdificioEnem.Interval = 30000;
            TEdificioEnem.Elapsed += GeneraEdificioEnem;
            /****************************/
            TMovimientoEnem.Interval = 15000;
            TMovimientoEnem.Elapsed += GenerMovEnem;
            /****************************/
            TFramesEnem.Interval = 15;
            TFramesEnem.Elapsed += MueveEnem;
            /****************************/
            Timer1BatallaCampal.Interval = 1000;
            Timer1BatallaCampal.Elapsed += ColisionPeleaCampal1;
            /****************************/
            TimerGanaPierde.Interval = 15;
            TimerGanaPierde.Elapsed += RevisaGanaPierde;
        }
        public void Start()
        {
            this.Cursor = Cursors.Default; // Mientras este video, sea cursor normal.
            opc = '0'; //Cero es para que no corra ninguna opcion la desicion de Paint()
                       /************ Variables para carga de Video *************************/
            axWindowsMediaPlayer1.Visible = true;
            axWindowsMediaPlayer1.URL = ("intro2.wmv");
            button1.Enabled = true;
            button1.Visible = true;
            axWindowsMediaPlayer1.Ctlcontrols.play();
            /******************************************************************/
        }

        public void ColisionPeleaCampal1(object obj, ElapsedEventArgs args)
        {
            try
            {
                World.RevisaColisionCampal1();
            }
            catch
            {

            }
            Invalidate();
        }

        public void GeneraEdificioEnem(object obj, ElapsedEventArgs args)
        {
            Edificio ed;
            Random r = new Random();
            switch(r.Next(0,3))
            {
                case 0:
                    ed = new Edificio(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900), "Farm");
                    World.getPlayer2().getListFarms().Add(ed);
                    break;
                case 1:
                    ed = new Edificio(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900), "Cuartel");
                    World.getPlayer2().getListCuarteles().Add(ed);
                    break;
            }
            UnidadMilitar um;
            Aldeano al;
            switch (r.Next(0, 4))
            {
                case 0:
                    al = new Aldeano(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900));
                    World.getPlayer2().getListAldeanos().Add(al);
                    break;
                case 1:
                    if(World.getPlayer().getBando() == true)
                    {
                        um = new UnidadMilitar(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900), "robo1");
                    }else
                    {
                        um = new UnidadMilitar(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900), "str1");
                    }
                    World.getPlayer2().getlistMilicia().Add(um);
                    break;
                case 2:
                    if (World.getPlayer().getBando() == true)
                    {
                        um = new UnidadMilitar(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900), "sithW (3)");
                    }
                    else
                    {
                        um = new UnidadMilitar(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900), "jediW (3)");
                    }
                    World.getPlayer2().getListUespecial().Add(um);
                    break;
                case 3:
                    if (World.getPlayer().getBando() == true)
                    {
                        um = new UnidadMilitar(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900), "navesith (1)");
                    }
                    else
                    {
                        um = new UnidadMilitar(World.getPlayer2().getCoordInicalMapX() + r.Next(200, 2900), World.getPlayer2().getCoordInicalMapY() + r.Next(200, 2900), "navejedi (1)");
                    }
                    World.getPlayer2().getListNaves().Add(um);
                    break;
            }
            Invalidate();
        }

        public void GenerMovEnem(object obj, ElapsedEventArgs args)
        {
            try
            {
                foreach (Aldeano a in World.getPlayer2().getListAldeanos())
                {
                    a.setProvX(World.getPlayer2().getCoordInicalMapX() + rnd.Next(100, 1000));
                    a.setProvY(World.getPlayer2().getCoordInicalMapY() + rnd.Next(100, 1000));
                    a.setIsMoving(true);
                    a.Camina();
                }
                foreach (UnidadMilitar a in World.getPlayer2().getlistMilicia())
                {
                    a.setProvX(World.getPlayer().getCoordInicalMapX() + rnd.Next(200, 1000));
                    a.setProvY(World.getPlayer().getCoordInicalMapY() + rnd.Next(200, 1000));
                    a.setIsMoving(true);
                    a.Camina();
                }
                foreach (UnidadMilitar a in World.getPlayer2().getListUespecial())
                {
                    a.setProvX(World.getPlayer2().getCoordInicalMapX() + rnd.Next(100, 1000));
                    a.setProvY(World.getPlayer2().getCoordInicalMapY() + rnd.Next(100, 1000));
                    a.setIsMoving(true);
                    a.Camina();
                }
                foreach (UnidadMilitar a in World.getPlayer2().getListNaves())
                {
                    a.setProvX(World.getPlayer2().getCoordInicalMapX() + rnd.Next(100, 1000));
                    a.setProvY(World.getPlayer2().getCoordInicalMapY() + rnd.Next(100, 1000));
                    a.setIsMoving(true);
                    a.Camina();
                }
            }
            catch { }
        }

        public void RevisaRecogidaPlayer(object obj, ElapsedEventArgs arg)
        {
            try
            {
                World.revisaRecogidaAldeanos();
            }
            catch
            {

            }
            Invalidate();
        }

        public void RevisaCUDestruidos(object obj, ElapsedEventArgs arg)
        {
            try
            {
                if(World.getPlayer().getlistCUs().Count < 1) { }
            }
            catch
            {

            }
            Invalidate();
        }

        public void RevisaComidaPlayer(object obj, ElapsedEventArgs arg)
        {
            try
            {
                World.RevisaRecogidaComida();
            }
            catch
            {

            }
            Invalidate();
        }
        public void RevisaCuartelesPlayer(object obj, ElapsedEventArgs arg)
        {
            try
            {
                if (World.getPlayer().getListCuarteles().Count < 1)
                {
                    menu.getBotones()[1].setVisible(false);
                    menu.getBotones()[2].setVisible(false);
                    menu.getBotones()[3].setVisible(false);
                }
            }
            catch
            {

            }
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            grap = e.Graphics;
            switch (opc)
            {
                case '4':
                    //Jugar
                    try
                    {
                        grap.DrawImage(World.getImgMundo(), World.getcoordmapX(), World.getcoordmapY());//escenario  
                        printRec();
                        printCUs(World.getPlayer());
                        printCUs(World.getPlayer2()); //P2
                        printCuarteles(World.getPlayer());
                        printCuarteles(World.getPlayer2());//P2
                        printFarms(World.getPlayer());
                        printFarms(World.getPlayer2());//P2
                        printAldeanos(World.getPlayer());
                        printAldeanos(World.getPlayer2());//P2
                        printUespecial(World.getPlayer());
                        printUespecial(World.getPlayer2());//P2
                        printNaves(World.getPlayer());
                        printNaves(World.getPlayer2());//P2
                        printMilicia(World.getPlayer());
                        printMilicia(World.getPlayer2());//P2
                        grap.DrawImage(World.getImgBarra(), 0, this.ClientSize.Height - World.getImgBarra().Size.Height); //416
                        PintaBotones();
                        PrintStatus();
                    }
                    catch { }
                    break;               
                case '2':
                    grap.DrawImage(menu.getImgfondo(), 0, 0);
                    PintaBotones();
                    break;
                case '3':
                    //Ayuda
                    grap.DrawImage(menu.getImgfondo(), 0, 0);
                    PintaBotones();
                    break;
                case '1':
                    grap.DrawImage(menu.getImgfondo(), 0, 0);
                    PintaBotones();
                    break;
            }


        }

        private void printCuarteles(Player p)
        {
            foreach (Edificio ed in p.getListCuarteles())// metodo de acceso
            {
                if (ed.getMapX() + ed.getAncho() >= bordeX1 && ed.getMapX() <= bordeX2 && ed.getMapY() + ed.getAlto() >= bordeY1 && ed.getMapY() <= bordeY2)
                {
                    ed.SetPaintedSprite(true);
                    grap.DrawImage(ed.img, ed.getMapX() + World.getcoordmapX(), ed.getMapY() + World.getcoordmapY());
                }
            }
        }

        private void printCUs(Player p)
        {
            foreach (Edificio ed in p.getlistCUs())// metodo de acceso
            {
                if (ed.getMapX() + ed.getAncho() >= bordeX1 && ed.getMapX() <= bordeX2 && ed.getMapY() + ed.getAlto() >= bordeY1 && ed.getMapY() <= bordeY2)
                {
                    ed.SetPaintedSprite(true);
                    grap.DrawImage(ed.img, ed.getMapX() + World.getcoordmapX(), ed.getMapY() + World.getcoordmapY());
                }
            }
        }
        private void printFarms(Player p)
        {
            foreach (Edificio ed in p.getListFarms())// metodo de acceso
            {
                if (ed.getMapX() + ed.getAncho() >= bordeX1 && ed.getMapX() <= bordeX2 && ed.getMapY() + ed.getAlto() >= bordeY1 && ed.getMapY() <= bordeY2)
                {
                    ed.SetPaintedSprite(true);
                    grap.DrawImage(ed.img, ed.getMapX() + World.getcoordmapX(), ed.getMapY() + World.getcoordmapY());
                }
            }
        }

        private void printRec()
        {            
            foreach (Mineral ed in World.getRecursoMinMundo())// metodo de acceso
            {
                if (ed.getMapX() + ed.getAncho() >= bordeX1 && ed.getMapX() <= bordeX2 && ed.getMapY() + ed.getAlto() >= bordeY1 && ed.getMapY() <= bordeY2)
                {
                    ed.SetPaintedSprite(true);
                    grap.DrawImage(ed.img, ed.getMapX() + World.getcoordmapX(), ed.getMapY() + World.getcoordmapY());
                }
            }
            foreach (Comida ed in World.getRecursoComMundo())// metodo de acceso
            {
                if (ed.getMapX() + ed.getAncho() >= bordeX1 && ed.getMapX() <= bordeX2 && ed.getMapY() + ed.getAlto() >= bordeY1 && ed.getMapY() <= bordeY2)
                {
                    ed.SetPaintedSprite(true);
                    grap.DrawImage(ed.img, ed.getMapX() + World.getcoordmapX(), ed.getMapY() + World.getcoordmapY());
                }
            }
        }

        private void printAldeanos(Player p)
        {
            Bitmap aux;
            foreach (Aldeano a in p.getListAldeanos())// metodo de acceso
            {
                if (a.getMapX() + a.getAncho() >= bordeX1 && a.getMapX() <= bordeX2 && a.getMapY() + a.getAlto() >= bordeY1 && a.getMapY() <= bordeY2)
                {
                    a.SetPaintedSprite(true);
                    aux = a.currentFrameActual();
                    if(aux != null)
                    {
                        if(a.getMapX() > a.getProvX())
                        {
                            aux.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            grap.DrawImage(aux, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                            aux.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        else
                        {
                            grap.DrawImage(a.currentFrameActual(), a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                        }
                    }
                    /*else
                    {
                        grap.DrawImage(a.getEstatico(), a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                    }*/
                    if (a.GetSpriteSelected() == true)
                    {
                        grap.DrawLine(this.p, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY(), a.getMapX() + World.getcoordmapX() + a.getPercentLifeDraw(), a.getMapY() + World.getcoordmapY());
                    }
                }
            }
        }

        private void printMilicia(Player p)
        {
            Bitmap aux;
            foreach (UnidadMilitar a in p.getlistMilicia())// metodo de acceso
            {
                if (a.getMapX() + a.getAncho() >= bordeX1 && a.getMapX() <= bordeX2 && a.getMapY() + a.getAlto() >= bordeY1 && a.getMapY() <= bordeY2)
                {
                    a.SetPaintedSprite(true);
                    aux = a.currentFrameActual();
                    if (aux != null)
                    {
                        if (a.getMapX() > a.getProvX())
                        {
                            aux.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            grap.DrawImage(aux, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                            aux.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        else
                        {
                            grap.DrawImage(a.currentFrameActual(), a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                        }
                    }
                    if (a.GetSpriteSelected() == true)
                    {
                        grap.DrawLine(this.p, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY(), a.getMapX() + World.getcoordmapX() + a.getPercentLifeDraw(), a.getMapY() + World.getcoordmapY());
                    }
                }
            }
        }

        private void printUespecial(Player p)
        {
            Bitmap aux;
            foreach (UnidadMilitar a in p.getListUespecial())// metodo de acceso
            {
                if (a.getMapX() + a.getAncho() >= bordeX1 && a.getMapX() <= bordeX2 && a.getMapY() + a.getAlto() >= bordeY1 && a.getMapY() <= bordeY2)
                {
                    a.SetPaintedSprite(true);
                    aux = a.currentFrameActual();
                    if (aux != null)
                    {
                        if (a.getMapX() > a.getProvX())
                        {
                            aux.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            grap.DrawImage(aux, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                            aux.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        else
                        {
                            grap.DrawImage(a.currentFrameActual(), a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                        }
                    }
                    if (a.GetSpriteSelected() == true)
                    {
                        grap.DrawLine(this.p, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY(), a.getMapX() + World.getcoordmapX() + a.getPercentLifeDraw(), a.getMapY() + World.getcoordmapY());
                    }
                }
            }
        }
        private void printNaves(Player p)
        {
            Bitmap aux;
            foreach (UnidadMilitar a in p.getListNaves())// metodo de acceso
            {
                if (a.getMapX() + a.getAncho() >= bordeX1 && a.getMapX() <= bordeX2 && a.getMapY() + a.getAlto() >= bordeY1 && a.getMapY() <= bordeY2)
                {
                    a.SetPaintedSprite(true);
                    aux = a.currentFrameActual();
                    if (aux != null)
                    {
                        if (a.getMapX() > a.getProvX())
                        {
                            aux.RotateFlip(RotateFlipType.RotateNoneFlipX);
                            grap.DrawImage(aux, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                            aux.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        }
                        else
                        {
                            grap.DrawImage(a.currentFrameActual(), a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                        }
                    }
                    if (a.GetSpriteSelected() == true)
                    {
                        grap.DrawLine(this.p, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY(), a.getMapX() + World.getcoordmapX() + a.getPercentLifeDraw(), a.getMapY() + World.getcoordmapY());
                    }
                }
            }
        }

        private void PintaBotones() /****Este metodo solo se llama en los dos menus que tienen botones  *****/
        {
            Bitmap imagen;
            foreach (boton b in menu.getBotones())
            {
                if (b.getVisible() == true)
                {
                    imagen = (Bitmap)Image.FromFile(b.getImg());
                    grap.DrawImage(imagen, b.getX(), b.getY());
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int c = e.KeyValue;
            switch(c)
            {
                case 37://izquierda
                    if (bordeX1 - 20 >= LimitePantX1)
                    {
                        World.pluscoordmapX();
                        bordeX1 = bordeX1 - 20;
                        bordeX2 = bordeX2 - 20;
                    }               
                    break;
                case 38: //arriba
                    if (bordeY1 - 20 >= LimitePantY1)
                    {
                        World.pluscoordmapY();
                        bordeY1 = bordeY1 - 20;
                        bordeY2 = bordeY2 - 20;
                    }                  
                    break;
                case 39://Derecha
                    if (bordeX2 + 20 <= LimitePantX2)
                    {
                        World.lesscoordmapX();
                        bordeX1 = bordeX1 + 20;
                        bordeX2 = bordeX2 + 20;
                    }               
                    break;
                case 40://Abajo      
                    if (bordeY2 + 20 <= LimitePantY2)
                    {
                        World.lesscoordmapY();
                        bordeY1 = bordeY1 + 20;
                        bordeY2 = bordeY2 + 20;
                    }               
                    break;                
            }
            Invalidate();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = e.X;
            int y = e.Y;
            if (int.Parse(opc.ToString()) < 4)
            {
                switch (opc)
                {
                    case '1':
                        MouseMenu(x, y);
                        break;
                    case '2':
                        PartidaNuevaMenu(x, y);
                        break;
                    case '3':
                        AyudaMenu(x, y);
                        break;
                }
            }
            else
            {
                /*********************/                                    
                if (e.Button == MouseButtons.Left)
                {
                    checkLeftAldeanos(x, y);
                    checkLeftMilicia(x, y);
                    checkButtons(x, y);
                    checkLeftUespecial(x, y);
                    checkLeftNaves(x, y);
                }
                if (e.Button == MouseButtons.Right)
                {
                    checkRightAldeanos(x, y);
                    checkRightMilicia(x,y);
                    checkRightUespecial(x, y);
                    checkRightNaves(x, y);
                }
                Invalidate();
            }
        }

        private void UpdateAnimaFrames(object obj, ElapsedEventArgs arg)
        {
            foreach(Aldeano a in World.getPlayer().getListAldeanos())
            { 
                if(a.getIsMoving() == true)
                {
                    if (a.getMapX() != a.getProvX()) { if(a.getMapX() > a.getProvX()) { a.SetMapX(a.getMapX() - 1); }else { a.SetMapX(a.getMapX() + 1); } }
                    if (a.getMapY() != a.getProvY()) { if (a.getMapY() > a.getProvY()) { a.SetMapY(a.getMapY() - 1); } else { a.SetMapY(a.getMapY() + 1); } }
                    if(a.getMapX() == a.getProvX() && a.getMapY() == a.getProvY())
                    {
                        a.setIsMoving(false);
                        a.SetCamina(false);
                        a.parado();
                    }
                }
                a.Update();
            }
            Invalidate();
        }
        private void UpdateAnimaMil(object obj, ElapsedEventArgs arg)
        {
            foreach (UnidadMilitar a in World.getPlayer().getlistMilicia())
            {
                if (a.getIsMoving() == true)
                {
                    if (a.getMapX() != a.getProvX()) { if (a.getMapX() > a.getProvX()) { a.SetMapX(a.getMapX() - 1); } else { a.SetMapX(a.getMapX() + 1); } }
                    if (a.getMapY() != a.getProvY()) { if (a.getMapY() > a.getProvY()) { a.SetMapY(a.getMapY() - 1); } else { a.SetMapY(a.getMapY() + 1); } }
                    if (a.getMapX() == a.getProvX() && a.getMapY() == a.getProvY())
                    {
                        a.setIsMoving(false);
                        a.SetCamina(false);
                        a.parado();
                    }
                }
                a.Update();
            }
            Invalidate();
            foreach (UnidadMilitar a in World.getPlayer().getListUespecial())
            {
                if (a.getIsMoving() == true)
                {
                    if (a.getMapX() != a.getProvX()) { if (a.getMapX() > a.getProvX()) { a.SetMapX(a.getMapX() - 1); } else { a.SetMapX(a.getMapX() + 1); } }
                    if (a.getMapY() != a.getProvY()) { if (a.getMapY() > a.getProvY()) { a.SetMapY(a.getMapY() - 1); } else { a.SetMapY(a.getMapY() + 1); } }
                    if (a.getMapX() == a.getProvX() && a.getMapY() == a.getProvY())
                    {
                        a.setIsMoving(false);
                        a.SetCamina(false);
                        a.parado();
                    }
                }
                a.Update();
            }
            Invalidate();
            foreach (UnidadMilitar a in World.getPlayer().getListNaves())
            {
                if (a.getIsMoving() == true)
                {
                    if (a.getMapX() != a.getProvX()) { if (a.getMapX() > a.getProvX()) { a.SetMapX(a.getMapX() - 1); } else { a.SetMapX(a.getMapX() + 1); } }
                    if (a.getMapY() != a.getProvY()) { if (a.getMapY() > a.getProvY()) { a.SetMapY(a.getMapY() - 1); } else { a.SetMapY(a.getMapY() + 1); } }
                    if (a.getMapX() == a.getProvX() && a.getMapY() == a.getProvY())
                    {
                        a.setIsMoving(false);
                        a.SetCamina(false);
                        a.parado();
                    }
                }
                a.Update();
            }
            Invalidate();
        }

        private void MueveEnem(object obj, ElapsedEventArgs arg)
        {
            foreach (Aldeano a in World.getPlayer2().getListAldeanos())
            {
                if (a.getIsMoving() == true)
                {
                    if (a.getMapX() != a.getProvX()) { if (a.getMapX() > a.getProvX()) { a.SetMapX(a.getMapX() - 1); } else { a.SetMapX(a.getMapX() + 1); } }
                    if (a.getMapY() != a.getProvY()) { if (a.getMapY() > a.getProvY()) { a.SetMapY(a.getMapY() - 1); } else { a.SetMapY(a.getMapY() + 1); } }
                    if (a.getMapX() == a.getProvX() && a.getMapY() == a.getProvY())
                    {
                        a.setIsMoving(false);
                        a.SetCamina(false);
                        a.parado();
                    }
                }
                a.Update();
            }
            Invalidate();
            foreach (UnidadMilitar a in World.getPlayer2().getlistMilicia())
            {
                if (a.getIsMoving() == true)
                {
                    if (a.getMapX() != a.getProvX()) { if (a.getMapX() > a.getProvX()) { a.SetMapX(a.getMapX() - 1); } else { a.SetMapX(a.getMapX() + 1); } }
                    if (a.getMapY() != a.getProvY()) { if (a.getMapY() > a.getProvY()) { a.SetMapY(a.getMapY() - 1); } else { a.SetMapY(a.getMapY() + 1); } }
                    if (a.getMapX() == a.getProvX() && a.getMapY() == a.getProvY())
                    {
                        a.setIsMoving(false);
                        a.SetCamina(false);
                        a.parado();
                    }
                }
                a.Update();
            }
            Invalidate();
            foreach (UnidadMilitar a in World.getPlayer2().getListUespecial())
            {
                if (a.getIsMoving() == true)
                {
                    if (a.getMapX() != a.getProvX()) { if (a.getMapX() > a.getProvX()) { a.SetMapX(a.getMapX() - 1); } else { a.SetMapX(a.getMapX() + 1); } }
                    if (a.getMapY() != a.getProvY()) { if (a.getMapY() > a.getProvY()) { a.SetMapY(a.getMapY() - 1); } else { a.SetMapY(a.getMapY() + 1); } }
                    if (a.getMapX() == a.getProvX() && a.getMapY() == a.getProvY())
                    {
                        a.setIsMoving(false);
                        a.SetCamina(false);
                        a.parado();
                    }
                }
                a.Update();
            }
            Invalidate();
            foreach (UnidadMilitar a in World.getPlayer2().getListNaves())
            {
                if (a.getIsMoving() == true)
                {
                    if (a.getMapX() != a.getProvX()) { if (a.getMapX() > a.getProvX()) { a.SetMapX(a.getMapX() - 1); } else { a.SetMapX(a.getMapX() + 1); } }
                    if (a.getMapY() != a.getProvY()) { if (a.getMapY() > a.getProvY()) { a.SetMapY(a.getMapY() - 1); } else { a.SetMapY(a.getMapY() + 1); } }
                    if (a.getMapX() == a.getProvX() && a.getMapY() == a.getProvY())
                    {
                        a.setIsMoving(false);
                        a.SetCamina(false);
                        a.parado();
                    }
                }
                a.Update();
            }
            Invalidate();
        }

        private void checkLeftAldeanos(int x, int y)
        {
            int cont = 0;
            foreach (Aldeano a in World.getPlayer().getListAldeanos())
            {
                if (x >= a.getMapX() + World.getcoordmapX() && x <= a.getMapX() + World.getcoordmapX() + a.getAncho() && y >= a.getMapY() + World.getcoordmapY() && y <= a.getMapY() + World.getcoordmapY() + a.getAlto())
                {
                    if (a.GetSpriteSelected() == true)
                        a.SetSpriteSelected(false);
                    else
                    {
                        a.SetSpriteSelected(true);
                        cont++;
                    }
                }
            }
            if (cont == 0)
            {
                foreach (Aldeano a in World.getPlayer().getListAldeanos())
                {
                    if (y < this.ClientSize.Height - World.getImgBarra().Size.Height) { a.SetSpriteSelected(false); }
                }
            }
        }
        private void checkRightAldeanos(int x, int y)
        {
            Random rand = new Random();
            foreach (Aldeano a in World.getPlayer().getListAldeanos())
            {
                if (a.GetSpriteSelected() == true && y <= this.ClientSize.Height - World.getImgBarra().Size.Height)
                {
                    // mover coordenadas de personaje con animacion
                    a.setProvX(x + rand.Next(50) - World.getcoordmapX()); //sumar random
                    a.setProvY(y - World.getcoordmapY());  //sumar random   
                    a.setIsMoving(true);
                    a.Camina();                  
                }
            }
        }

        private void checkLeftMilicia(int x, int y)
        {
            int cont = 0;
            foreach (UnidadMilitar a in World.getPlayer().getlistMilicia())
            {
                if (x >= a.getMapX() + World.getcoordmapX() && x <= a.getMapX() + World.getcoordmapX() + a.getAncho() && y >= a.getMapY() + World.getcoordmapY() && y <= a.getMapY() + World.getcoordmapY() + a.getAlto())
                {
                    if (a.GetSpriteSelected() == true)
                        a.SetSpriteSelected(false);
                    else
                    {
                        a.SetSpriteSelected(true);
                        cont++;
                    }
                }
            }
            if (cont == 0)
            {
                foreach (UnidadMilitar a in World.getPlayer().getlistMilicia())
                {
                    a.SetSpriteSelected(false);
                }
            }
        }

        private void checkLeftUespecial(int x, int y)
        {
            int cont = 0;
            foreach (UnidadMilitar a in World.getPlayer().getListUespecial())
            {
                if (x >= a.getMapX() + World.getcoordmapX() && x <= a.getMapX() + World.getcoordmapX() + a.getAncho() && y >= a.getMapY() + World.getcoordmapY() && y <= a.getMapY() + World.getcoordmapY() + a.getAlto())
                {
                    if (a.GetSpriteSelected() == true)
                        a.SetSpriteSelected(false);
                    else
                    {
                        a.SetSpriteSelected(true);
                        cont++;
                    }
                }
            }
            if (cont == 0)
            {
                foreach (UnidadMilitar a in World.getPlayer().getListUespecial())
                {
                    a.SetSpriteSelected(false);
                }
            }
        }

        private void checkRightMilicia(int x, int y)
        {
            Random rand = new Random();
            foreach (UnidadMilitar a in World.getPlayer().getlistMilicia())
            {
                if (a.GetSpriteSelected() == true && y <= this.ClientSize.Height - World.getImgBarra().Size.Height)
                {
                    // mover coordenadas de personaje con animacion
                    a.setProvX(x + rand.Next(50) - World.getcoordmapX()); //sumar random
                    a.setProvY(y - World.getcoordmapY());  //sumar random   
                    a.setIsMoving(true);
                    a.Camina();                 //sumar random                        
                }
            }
        }

        private void checkLeftNaves(int x, int y)
        {
            int cont = 0;
            foreach (UnidadMilitar a in World.getPlayer().getListNaves())
            {
                if (x >= a.getMapX() + World.getcoordmapX() && x <= a.getMapX() + World.getcoordmapX() + a.getAncho() && y >= a.getMapY() + World.getcoordmapY() && y <= a.getMapY() + World.getcoordmapY() + a.getAlto())
                {
                    if (a.GetSpriteSelected() == true)
                        a.SetSpriteSelected(false);
                    else
                    {
                        a.SetSpriteSelected(true);
                        cont++;
                    }
                }
            }
            if (cont == 0)
            {
                foreach (UnidadMilitar a in World.getPlayer().getListNaves())
                {
                    a.SetSpriteSelected(false);
                }
            }
        }

        private void checkRightUespecial(int x, int y)
        {
            Random rand = new Random();
            foreach (UnidadMilitar a in World.getPlayer().getListUespecial())
            {
                if (a.GetSpriteSelected() == true && y <= this.ClientSize.Height - World.getImgBarra().Size.Height)
                {
                    // mover coordenadas de personaje con animacion
                    a.setProvX(x + rand.Next(50) - World.getcoordmapX()); //sumar random
                    a.setProvY(y - World.getcoordmapY());  //sumar random   
                    a.setIsMoving(true);
                    a.Camina();                  //sumar random                        
                }
            }
        }

        private void checkRightNaves(int x, int y)
        {
            Random rand = new Random();
            foreach (UnidadMilitar a in World.getPlayer().getListNaves())
            {
                if (a.GetSpriteSelected() == true && y <= this.ClientSize.Height - World.getImgBarra().Size.Height)
                {
                    // mover coordenadas de personaje con animacion
                    a.setProvX(x + rand.Next(50) - World.getcoordmapX()); //sumar random
                    a.setProvY(y - World.getcoordmapY());  //sumar random   
                    a.setIsMoving(true);
                    a.Camina();
                }
            }
        }

        private void MouseMenu(int x, int y) /****Este Metodo decide si se presiono un boton del menu principal *************/
        {
            if (x > menu.getBotones()[0].getX() && x < menu.getBotones()[0].getX() + menu.getBotones()[0].getAncho() && y > menu.getBotones()[0].getY() && y < menu.getBotones()[0].getY() + menu.getBotones()[0].getAlto())
            {
                //PARTIDA NUEVA
                StartBando();
            }
            else
            {
                if (x > menu.getBotones()[1].getX() && x < menu.getBotones()[1].getX() + menu.getBotones()[1].getAncho() && y > menu.getBotones()[1].getY() && y < menu.getBotones()[1].getY() + menu.getBotones()[1].getAlto())
                {
                    //AYUDA
                    StartAyudaMenu();
                }
                else
                {
                    if (x > menu.getBotones()[2].getX() && x < menu.getBotones()[2].getX() + menu.getBotones()[2].getAncho() && y > menu.getBotones()[2].getY() && y < menu.getBotones()[2].getY() + menu.getBotones()[2].getAlto())
                    {
                        //CREDITOS
                        menu.getCancion().Stop();
                        Start();
                    }
                    else
                    {
                        if (x > menu.getBotones()[3].getX() && x < menu.getBotones()[3].getX() + menu.getBotones()[3].getAncho() && y > menu.getBotones()[3].getY() && y < menu.getBotones()[3].getY() + menu.getBotones()[3].getAlto())
                        {
                            //SALIR
                            menu.getCancion().Stop();
                            this.Close();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Visible = false;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            button1.Enabled = false;
            button1.Visible = false;
            menuStart();
        }

        public void menuStart() //MenuStart() inicializa todo para cargar el menu principal del juego.
        {
            opc = '1'; //Opcion 1 del paint para pintar menu.  
            menu = new Menu("fondo3.jpg");
            menu.getBotones().Clear();
            this.Cursor = new Cursor("cursor2.cur"); //Se activa el cursor de espada.            
            menu.GeneraBotonesMenu(); //Carga Botones de Menu
            Invalidate(); //Pinta lo que ya se cargo
        }

        public void StartBando() /**Este metodo se manda llamar en MouseMenu **************/
        {
            opc = '2';
            menu = new Menu("bando5.jpg");
            menu.getBotones().Clear();
            menu.GeneraBotonesBando();
            Invalidate();
        }

        private void PartidaNuevaMenu(int x, int y) /*** Este Metodo decide si se presiono un boton del menu de equipo*******/
        {
            bool a;
            if (x > menu.getBotones()[0].getX() && x < menu.getBotones()[0].getX() + menu.getBotones()[0].getAncho() && y > menu.getBotones()[0].getY() && y < menu.getBotones()[0].getY() + menu.getBotones()[0].getAlto())
            {
                //ELEGIR JEDI
                opc = '4';
                a = true;
                startJuego(a);
            }
            else
            {
                if (x > menu.getBotones()[1].getX() && x < menu.getBotones()[1].getX() + menu.getBotones()[1].getAncho() && y > menu.getBotones()[1].getY() && y < menu.getBotones()[1].getY() + menu.getBotones()[1].getAlto())
                {
                    //ELEGIR SITH
                    opc = '4';
                    a = false;
                    startJuego(a);
                }
                else
                {
                    if (x > menu.getBotones()[2].getX() && x < menu.getBotones()[2].getX() + menu.getBotones()[2].getAncho() && y > menu.getBotones()[2].getY() && y < menu.getBotones()[2].getY() + menu.getBotones()[2].getAlto())
                    {
                        menuStart();
                    }
                }
            }
            Invalidate();
        }
        private void AyudaMenu(int x, int y)
        {
            if (x > menu.getBotones()[0].getX() && x < menu.getBotones()[0].getX() + menu.getBotones()[0].getAncho() && y > menu.getBotones()[0].getY() && y < menu.getBotones()[0].getY() + menu.getBotones()[0].getAlto())
            {
                menuStart();
            }
        }

        public void StartAyudaMenu()
        {
            opc = '3'; //Opcion 1 del paint para pintar menu.  
            menu = new Menu("ayudita.png");
            menu.getBotones().Clear();
            this.Cursor = new Cursor("cursor2.cur"); //Se activa el cursor de espada.            
            menu.GeneraBotonesAyudita(); //Carga Botones de Menu
            Invalidate(); //Pinta lo que ya se cargo    
        }
        public void startPG(string m)
        {
            opc = '3'; //Opcion 1 del paint para pintar menu. 
            menu = new Menu(m+".png");
            //menu.getBotones().Clear();
            //this.Cursor = new Cursor("cursor2.cur"); //Se activa el cursor de espada.            
            menu.GeneraBotonesPG(); //Carga Botones de Menu
            Invalidate(); //Pinta lo que ya se cargo    
        }
        private void checkButtons(int x, int y)
        {
            if (x > menu.getBotones()[0].getX() && x < menu.getBotones()[0].getX() + menu.getBotones()[0].getAncho() && y > menu.getBotones()[0].getY() && y < menu.getBotones()[0].getY() + menu.getBotones()[0].getAlto())
            {
                //Aldeano
                if(World.getPlayer().getComida() - 50 >= 0)
                {
                    Aldeano aux = new Aldeano(World.getPlayer().getCoordInicalMapX() + rnd.Next(30, 50), World.getPlayer().getCoordInicalMapY() + rnd.Next(30, 50));
                    World.getPlayer().AddAldeano(aux);
                    World.getPlayer().restaComida(aux.getCostoComida());
                }
            }
            else
            {
                if (x > menu.getBotones()[1].getX() && x < menu.getBotones()[1].getX() + menu.getBotones()[1].getAncho() && y > menu.getBotones()[1].getY() && y < menu.getBotones()[1].getY() + menu.getBotones()[1].getAlto())
                {
                    //Soldado
                    UnidadMilitar aux;
                    if (World.getPlayer().getComida() - 30 >= 0 && World.getPlayer().getMineral() - 30 >= 0 && World.getPlayer().getListCuarteles().Count > 0)
                    {
                        if (World.getPlayer().getBando() == true) {  aux = new UnidadMilitar(World.getPlayer().getListCuarteles()[0].getMapX() + rnd.Next(30, 50), World.getPlayer().getListCuarteles()[0].getMapY() + rnd.Next(30, 50), "str1"); }
                        else {  aux = new UnidadMilitar(World.getPlayer().getListCuarteles()[0].getMapX() + rnd.Next(30, 50), World.getPlayer().getListCuarteles()[0].getMapY() + rnd.Next(30, 50), "robo1"); }
                        World.getPlayer().getlistMilicia().Add(aux);
                        World.getPlayer().restaComida(30);
                        World.getPlayer().restaMineral(30);
                    }
                }
                else
                {
                    if (x > menu.getBotones()[2].getX() && x < menu.getBotones()[2].getX() + menu.getBotones()[2].getAncho() && y > menu.getBotones()[2].getY() && y < menu.getBotones()[2].getY() + menu.getBotones()[2].getAlto())
                    {
                        //SoldadoEspecial
                        UnidadMilitar aux;
                        if (World.getPlayer().getComida() - 50 >= 0 && World.getPlayer().getMineral() - 50 >= 0 && World.getPlayer().getListCuarteles().Count > 0)
                        {
                            if (World.getPlayer().getBando() == true) { aux = new UnidadMilitar(World.getPlayer().getListCuarteles()[0].getMapX() + rnd.Next(30, 50), World.getPlayer().getListCuarteles()[0].getMapY() + rnd.Next(30, 50), "jediW (3)"); }
                            else { aux = new UnidadMilitar(World.getPlayer().getListCuarteles()[0].getMapX() + rnd.Next(30, 50), World.getPlayer().getListCuarteles()[0].getMapY() + rnd.Next(30, 50), "sithW (3)"); }
                            World.getPlayer().getListUespecial().Add(aux);
                            World.getPlayer().restaComida(50);
                            World.getPlayer().restaMineral(50);
                        }
                    }
                    else
                    {
                        if (x > menu.getBotones()[3].getX() && x < menu.getBotones()[3].getX() + menu.getBotones()[3].getAncho() && y > menu.getBotones()[3].getY() && y < menu.getBotones()[3].getY() + menu.getBotones()[3].getAlto())
                        {
                            //Nave
                            UnidadMilitar aux;
                            if (World.getPlayer().getComida() - 100 >= 0 && World.getPlayer().getMineral() - 100 >= 0 && World.getPlayer().getListCuarteles().Count > 0)
                            {
                                if (World.getPlayer().getBando() == true) { aux = new UnidadMilitar(World.getPlayer().getListCuarteles()[0].getMapX() + rnd.Next(30, 50), World.getPlayer().getListCuarteles()[0].getMapY() + rnd.Next(30, 50), "navejedi (1)"); }
                                else { aux = new UnidadMilitar(World.getPlayer().getListCuarteles()[0].getMapX() + rnd.Next(30, 50), World.getPlayer().getListCuarteles()[0].getMapY() + rnd.Next(30, 50), "navesith (1)"); }
                                World.getPlayer().getListNaves().Add(aux);
                                World.getPlayer().restaComida(100);
                                World.getPlayer().restaMineral(100);
                            }
                        }
                        else
                        {
                            if (x > menu.getBotones()[4].getX() && x < menu.getBotones()[4].getX() + menu.getBotones()[4].getAncho() && y > menu.getBotones()[4].getY() && y < menu.getBotones()[4].getY() + menu.getBotones()[4].getAlto())
                            {
                                //Centro
                                Edificio aux;
                                for(int cont = 0; cont < World.getPlayer().getListAldeanos().Count; cont++)
                                {
                                    if(World.getPlayer().getListAldeanos()[cont].GetSpriteSelected() == true && World.getPlayer().getMineral() - 100 >= 0)
                                    {
                                        if (World.getPlayer().getBando() == true) { aux = new Edificio(World.getPlayer().getListAldeanos()[cont].getMapX() + rnd.Next(30, 50), World.getPlayer().getListAldeanos()[cont].getMapY() + rnd.Next(30, 50), "CUJ"); }
                                        else { aux = new Edificio(World.getPlayer().getListAldeanos()[cont].getMapX() + rnd.Next(30, 50), World.getPlayer().getListAldeanos()[cont].getMapY() + rnd.Next(30, 50), "CUS"); }
                                        World.getPlayer().getlistCUs().Add(aux);
                                        World.getPlayer().restaMineral(100);
                                    }
                                }
                            }
                            else
                            {
                                if (x > menu.getBotones()[5].getX() && x < menu.getBotones()[5].getX() + menu.getBotones()[5].getAncho() && y > menu.getBotones()[5].getY() && y < menu.getBotones()[5].getY() + menu.getBotones()[5].getAlto())
                                {
                                    //cuartel
                                    Edificio aux;
                                    for (int cont = 0; cont < World.getPlayer().getListAldeanos().Count; cont++)
                                    {
                                        if (World.getPlayer().getListAldeanos()[cont].GetSpriteSelected() == true && World.getPlayer().getMineral() - 100 >= 0)
                                        {
                                            menu.getBotones()[1].setVisible(true);
                                            menu.getBotones()[2].setVisible(true);
                                            menu.getBotones()[3].setVisible(true);
                                            aux = new Edificio(World.getPlayer().getListAldeanos()[cont].getMapX() + rnd.Next(30, 50), World.getPlayer().getListAldeanos()[cont].getMapY() + rnd.Next(30, 50), "cuartel");
                                            World.getPlayer().getListCuarteles().Add(aux);
                                            World.getPlayer().restaMineral(100);
                                        }
                                    }
                                }
                                else
                                {
                                    if (x > menu.getBotones()[6].getX() && x < menu.getBotones()[6].getX() + menu.getBotones()[6].getAncho() && y > menu.getBotones()[6].getY() && y < menu.getBotones()[6].getY() + menu.getBotones()[6].getAlto())
                                    {
                                        //Granja
                                        Edificio aux;
                                        for (int cont = 0; cont < World.getPlayer().getListAldeanos().Count; cont++)
                                        {
                                            if (World.getPlayer().getListAldeanos()[cont].GetSpriteSelected() == true && World.getPlayer().getMineral() - 100 >= 0 && World.getPlayer().getComida() - 100 >= 0)
                                            {
                                                aux = new Edificio(World.getPlayer().getListAldeanos()[cont].getMapX() + rnd.Next(30, 50), World.getPlayer().getListAldeanos()[cont].getMapY() + rnd.Next(30, 50), "Farm");
                                                World.getPlayer().getListFarms().Add(aux);
                                                World.getPlayer().restaMineral(100);
                                                World.getPlayer().restaComida(100);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if(x > menu.getBotones()[7].getX() && x < menu.getBotones()[7].getX() + menu.getBotones()[7].getAncho() && y > menu.getBotones()[7].getY() && y < menu.getBotones()[7].getY() + menu.getBotones()[7].getAlto())
                                        {
                                            //Salir Juego
                                            Trecurso.Stop();
                                            Tcomida.Stop();
                                            Tcuarteles.Stop();
                                            TCuD.Stop();
                                            TFrames.Stop();
                                            TUMFrames.Stop();
                                            TVerificaAtaque.Stop();
                                            TEdificioEnem.Stop();
                                            TMovimientoEnem.Stop();
                                            TFramesEnem.Stop();
                                            Timer1BatallaCampal.Stop();
                                            menuStart();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Invalidate();
        }
        private void PrintStatus()
        {
            int UM = World.getPlayer().getlistMilicia().Count + World.getPlayer().getListNaves().Count + World.getPlayer().getListUespecial().Count;
            grap.DrawString("JUGADOR", fnt, bsh, 370, Form1.ActiveForm.ClientSize.Height - 105);
            grap.DrawString("Aldeanos: " + World.getPlayer().getListAldeanos().Count, fnt, bsh, 370, Form1.ActiveForm.ClientSize.Height - 90);
            grap.DrawString("Unidades Militares: " + UM, fnt, bsh, 370, Form1.ActiveForm.ClientSize.Height - 75);
            grap.DrawString("Mineral: " + World.getPlayer().getMineral(), fnt, bsh, 370, Form1.ActiveForm.ClientSize.Height - 60);
            grap.DrawString("Comida: " + World.getPlayer().getComida(), fnt, bsh, 370, Form1.ActiveForm.ClientSize.Height - 45);
            //Player 2
            int UM2 = World.getPlayer2().getlistMilicia().Count + World.getPlayer2().getListNaves().Count + World.getPlayer2().getListUespecial().Count;
            grap.DrawString("CPU", fnt, bsh, 660, Form1.ActiveForm.ClientSize.Height - 105);
            grap.DrawString("Aldeanos: " + World.getPlayer2().getListAldeanos().Count, fnt, bsh, 660, Form1.ActiveForm.ClientSize.Height - 90);
            grap.DrawString("Unidades Militares: " + UM2, fnt, bsh, 660, Form1.ActiveForm.ClientSize.Height - 75);
            grap.DrawString("Mineral: " + World.getPlayer2().getMineral(), fnt, bsh, 660, Form1.ActiveForm.ClientSize.Height - 60);
            grap.DrawString("Comida: " + World.getPlayer2().getComida(), fnt, bsh, 660, Form1.ActiveForm.ClientSize.Height - 45);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Salir Juego
            Trecurso.Stop();
            Tcomida.Stop();
            Tcuarteles.Stop();
            TCuD.Stop();
            TFrames.Stop();
            TUMFrames.Stop();
            TVerificaAtaque.Stop();
            TEdificioEnem.Stop();
            TMovimientoEnem.Stop();
            TFramesEnem.Stop();
            Timer1BatallaCampal.Stop();
        }
        private void RevisaGanaPierde(object obj, ElapsedEventArgs arg)
        {
            try {
                if (World.getPlayer().getlistCUs().Count < 1)
                {
                    //Salir Juego
                    Trecurso.Stop();
                    Tcomida.Stop();
                    Tcuarteles.Stop();
                    TCuD.Stop();
                    TFrames.Stop();
                    TUMFrames.Stop();
                    TVerificaAtaque.Stop();
                    TEdificioEnem.Stop();
                    TMovimientoEnem.Stop();
                    TFramesEnem.Stop();
                    Timer1BatallaCampal.Stop();
                    TimerGanaPierde.Stop();
                    startPG("perdido");
                }
                else
                {
                    if (World.getPlayer2().getlistCUs().Count < 1)
                    {
                        //Salir Juego
                        Trecurso.Stop();
                        Tcomida.Stop();
                        Tcuarteles.Stop();
                        TCuD.Stop();
                        TFrames.Stop();
                        TUMFrames.Stop();
                        TVerificaAtaque.Stop();
                        TEdificioEnem.Stop();
                        TMovimientoEnem.Stop();
                        TFramesEnem.Stop();
                        Timer1BatallaCampal.Stop();
                        TimerGanaPierde.Stop();
                        startPG("ganado");
                    }
                }
            }
            catch { }
        }
    }
}
