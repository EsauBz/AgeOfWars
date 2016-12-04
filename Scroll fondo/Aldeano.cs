using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Scroll_fondo
{
    class Aldeano
    {
        public Bitmap img = (Bitmap)Image.FromFile("aldeanoW (1).png");
        private List<Bitmap> animacion = new List<Bitmap>();
        private List<Bitmap> Construye = new List<Bitmap>();
        //array lis bitmaps
        private int mapX, mapY;
        private bool selected;
        private int ancho;
        private int alto;
        private bool painted;
        private int lifeAldeano;
        private int DrawPercLife;
        public Aldeano(int x1 , int y1)
        {
            painted = false;            
            mapX = x1;
            mapY = y1;
            ancho = img.Size.Width;
            alto = img.Size.Height + 25;
            selected = false;
            lifeAldeano = 50;
            DrawPercLife = lifeAldeano / 4;
        }
        public int getPercentLifeDraw()
        {
            return DrawPercLife;
        }
        public int getLifeAldeano()
        {
            return lifeAldeano;
        }
        public void recibeDaño(int damage)
        {
            lifeAldeano -= damage;
            DrawPercLife = lifeAldeano / 4;
        }
        public int getMapX()
        {
            return mapX;
        }
        public void SetMapX(int a)
        {
            mapX = a;
        }
        public int getAncho()
        {
            return ancho;
        }
        public int getAlto()
        {
            return alto;
        }
        public int getMapY()
        {
            return mapY;
        }
        public void SetMapY(int a)
        {
            mapY = a;
        }
        public bool GetSpriteSelected()
        {
            return selected;
        }
        public void SetSpriteSelected(bool a)
        {
            selected = a;
        }
        public bool SpritePainted()
        {
            return painted;
        }
        public void SetPaintedSprite(bool a)
        {
            painted = a;
        }
    }
}
