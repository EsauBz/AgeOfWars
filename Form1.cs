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

namespace Scroll_fondo
{
    public partial class Form1 : Form
    {
        Bitmap b = (Bitmap)Image.FromFile("fondo.png");
        Bitmap barra = (Bitmap)Image.FromFile("barr1.png");
        //Bitmap CU = (Bitmap)Image.FromFile("centrou2.png");
        Player player1;
        int coordmapX;
        int coordmapY;
        int centradoImagenX;
        int centradoImagenY;
        int bordeX1;
        int bordeY1;
        int bordeX2;
        int bordeY2;
        int LimitePantX1 = 0;
        int LimitePantY1 = 0;
        int LimitePantX2 = 4003;
        int LimitePantY2 = 4130;
        Pen p;
        System.Timers.Timer t1;
        public Form1()
        {
            InitializeComponent();
            centradoImagenX = 350;
            centradoImagenY = 150;
            player1 = new Player();
            bordeX1 = player1.getCoordInicalMapX() - centradoImagenX;
            bordeY1 = player1.getCoordInicalMapY() - centradoImagenY;
            bordeX2 = bordeX1 + this.ClientSize.Width;
            bordeY2 = bordeY1 + this.ClientSize.Height;
            coordmapX = -player1.getCoordInicalMapX() + centradoImagenX;
            coordmapY = -player1.getCoordInicalMapY() + centradoImagenY;
            p = new Pen(Color.Green);
            //t1.Interval = 200;
          //  t1.Elapsed += player1.revisaRecogidaAldeanos;
           // t1.Start();
            p.Width = 10;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(b, coordmapX,coordmapY);//tierrita           
            foreach (Aldeano a in player1.getListAldeanos())// metodo de acceso
            {
                if (a.getMapX() >= bordeX1 && a.getMapX() <= bordeX2 && a.getMapY() >= bordeY1 && a.getMapY() <= bordeY2)
                {
                    a.SetPaintedSprite(true);
                    e.Graphics.DrawImage(a.img, a.getMapX() + coordmapX, a.getMapY() + coordmapY);                    
                    if (a.GetSpriteSelected() == true)
                    {
                        e.Graphics.DrawLine(p, a.getMapX() + coordmapX, a.getMapY() + coordmapY, a.getMapX() + coordmapX + a.getPercentLifeDraw(), a.getMapY() + coordmapY);
                    }
                }                
            }
            //e.Graphics.DrawImage(CU, 400, 10);
            e.Graphics.DrawImage(barra, 0, 420);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int c = e.KeyValue;
            switch(c)
            {
                case 37://izquierda
                    if (bordeX1 - 20 >= LimitePantX1)
                    {
                        coordmapX = coordmapX + 20;
                        bordeX1 = bordeX1 - 20;
                        bordeX2 = bordeX2 - 20;
                    }               
                    break;
                case 38: //arriba
                    if (bordeY1 - 20 >= LimitePantY1)
                    {
                        coordmapY = coordmapY + 20;
                        bordeY1 = bordeY1 - 20;
                        bordeY2 = bordeY2 - 20;
                    }                  
                    break;
                case 39://Derecha
                    if (bordeX2 + 20 <= LimitePantX2)
                    {
                        coordmapX = coordmapX - 20;
                        bordeX1 = bordeX1 + 20;
                        bordeX2 = bordeX2 + 20;
                    }               
                    break;
                case 40://Abajo      
                    if (bordeY2 + 20 <= LimitePantY2)
                    {
                        coordmapY = coordmapY - 20;
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
            int cont = 0;
            Random r = new Random();
            if (e.Button == MouseButtons.Left)
            {
                foreach (Aldeano a in player1.getListAldeanos())
                {
                    if (x >= a.getMapX() + coordmapX && x <= a.getMapX() + coordmapX + a.getAncho() && y >= a.getMapY() + coordmapY && y <= a.getMapY() + coordmapY + a.getAlto())
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
                    foreach (Aldeano a in player1.getListAldeanos())
                    {
                        a.SetSpriteSelected(false);
                    }
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                foreach(Aldeano a in player1.getListAldeanos())
                {
                    if (a.GetSpriteSelected() == true && y <= 420)
                    {
                        // mover coordenadas de personaje con animacion
                        a.SetMapX(x  + r.Next(51) - coordmapX); //sumar random
                        a.SetMapY(y - coordmapY);  //sumar random                        
                    }
                }
            }
            Invalidate();
        }            
    }
}
