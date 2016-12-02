using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    class UnidadMilitar
    {
        string NameUnidad;
        public Bitmap img;
        private List<Bitmap> animacionCamina = new List<Bitmap>();
        private List<Bitmap> Ataca = new List<Bitmap>();

        private int mapX, mapY;
        private bool selected;
        private int ancho;
        private int alto;
        private bool painted;
        private int lifeSoldado;
        private int DrawPercLife;
        private int ataque;
        public UnidadMilitar(int x1, int y1, string name)
        {
            NameUnidad = name;
            img = (Bitmap)Image.FromFile(name+".png");
            painted = false;
            EscogeAtaqueUnidadMilitar();
            mapX = x1;
            mapY = y1;
            ancho = 29;
            alto = 51;
            selected = false;
            DrawPercLife = lifeSoldado / 3;
        }
        private void EscogeAtaqueUnidadMilitar()
        {
            if(NameUnidad == "Jedi")
            {
                ataque = 15;
                lifeSoldado = 100;
                //ancho
                //alto
            }else
            {
                if (NameUnidad == "Sith") { ataque = 10; lifeSoldado = 150; /*ancho alto*/ }
                else
                {
                    if (NameUnidad == "storm" || NameUnidad == "robot") { ataque = 5; lifeSoldado = 80;/*ancho alto*/ }
                }
            }
        }
        public int getPercentLifeDraw()
        {
            return DrawPercLife;
        }
        public int getLifeSoldado()
        {
            return lifeSoldado;
        }
        public void recibeDaño(int damage)
        { 
            lifeSoldado -= damage;
            DrawPercLife = lifeSoldado / 3;
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
