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
        int LimitePantY2 = 4130;
        Pen p;
        System.Timers.Timer t1 = new System.Timers.Timer();
        /*Inicio de Constructor ************/
        public Form1()
        {
            InitializeComponent();
            Start();
            /*Variable Timer*****
            t1.Interval = 200;
            t1.Elapsed += RevisaRecogidaPlayer;
            t1.Start();
            /*******Fin de Constructor**************/
        }

        public void startJuego(char a)///se ejecuta despues de elegir bando
        {
            Random r = new Random();
            World = new Mundo(r.Next(0, 2));
            bordeX1 = World.getPlayer().getCoordInicalMapX() - 350;
            bordeY1 = World.getPlayer().getCoordInicalMapY() - 150;
            bordeX2 = bordeX1 + this.ClientSize.Width;
            bordeY2 = bordeY1 + this.ClientSize.Height;
            p = new Pen(Color.Green);
            p.Width = 10;
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

        public void RevisaRecogidaPlayer(object obj, ElapsedEventArgs arg)
        {
            World.getPlayer().revisaRecogidaAldeanos();
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            grap = e.Graphics;
            switch (opc)
            {
                case '4':
                    //Jugar
                    grap.DrawImage(World.getImgMundo(), World.getcoordmapX(), World.getcoordmapY());//escenario           
                    foreach (Aldeano a in World.getPlayer().getListAldeanos())// metodo de acceso
                    {
                        if (a.getMapX() >= bordeX1 && a.getMapX() <= bordeX2 && a.getMapY() >= bordeY1 && a.getMapY() <= bordeY2)
                        {
                            a.SetPaintedSprite(true);
                            grap.DrawImage(a.img, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY());
                            if (a.GetSpriteSelected() == true)
                            {
                                grap.DrawLine(p, a.getMapX() + World.getcoordmapX(), a.getMapY() + World.getcoordmapY(), a.getMapX() + World.getcoordmapX() + a.getPercentLifeDraw(), a.getMapY() + World.getcoordmapY());
                            }
                        }
                    }
                    grap.DrawImage(World.getImgBarra(), 0, 420);
                    break;               
                case '2':
                    grap.DrawImage(menu.getImgfondo(), 0, 0);
                    PintaBotones();
                    break;
                case '3':
                    //Ayuda
                    break;
                case '1':
                    grap.DrawImage(menu.getImgfondo(), 0, 0);
                    PintaBotones();
                    break;
            }


        }

        private void PintaBotones() /****Este metodo solo se llama en los dos menus que tienen botones  *****/
        {
            Bitmap imagen;
            foreach (boton b in menu.getBotones())
            {
                imagen = (Bitmap)Image.FromFile(b.getImg());
                grap.DrawImage(imagen, b.getX(), b.getY());
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
            Random r = new Random();
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
                int cont = 0;                
                if (e.Button == MouseButtons.Left)
                {
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
                            a.SetSpriteSelected(false);
                        }
                    }
                }
                if (e.Button == MouseButtons.Right)
                {
                    foreach (Aldeano a in World.getPlayer().getListAldeanos())
                    {
                        if (a.GetSpriteSelected() == true && y <= 420)
                        {
                            // mover coordenadas de personaje con animacion
                            a.SetMapX(x + r.Next(51) - World.getcoordmapX()); //sumar random
                            a.SetMapY(y - World.getcoordmapY());  //sumar random                        
                        }
                    }
                }
                Invalidate();
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
            menu = new Menu("bando4.jpg");
            menu.getBotones().Clear();
            menu.GeneraBotonesBando();
            Invalidate();
        }

        private void PartidaNuevaMenu(int x, int y) /*** Este Metodo decide si se presiono un boton del menu de equipo*******/
        {
            char a;
            if (x > menu.getBotones()[0].getX() && x < menu.getBotones()[0].getX() + menu.getBotones()[0].getAncho() && y > menu.getBotones()[0].getY() && y < menu.getBotones()[0].getY() + menu.getBotones()[0].getAlto())
            {
                //ELEGIR JEDI
                opc = '4';
                a = 'a';
                startJuego(a);
            }
            else
            {
                if (x > menu.getBotones()[1].getX() && x < menu.getBotones()[1].getX() + menu.getBotones()[1].getAncho() && y > menu.getBotones()[1].getY() && y < menu.getBotones()[1].getY() + menu.getBotones()[1].getAlto())
                {
                    //ELEGIR SITH
                    opc = '4';
                    a = 'r';
                    startJuego(a);
                }
                else
                {
                    if (x > menu.getBotones()[2].getX() && x < menu.getBotones()[2].getX() + menu.getBotones()[2].getAncho() && y > menu.getBotones()[2].getY() && y < menu.getBotones()[2].getY() + menu.getBotones()[2].getAlto())
                    {
                        menuStart();
                        Invalidate();
                    }
                }
            }
        }
        private void AyudaMenu(int x, int y)
        {

        }
    }
}
