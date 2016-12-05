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
        private int costoComida;
        private int costoMineral;
        public UnidadMilitar(int x1, int y1, string name)
        {
            NameUnidad = name;
            img = (Bitmap)Image.FromFile(name+".png");
            painted = false;
            EscogeAtaqueUnidadMilitar();
            mapX = x1;
            mapY = y1;
            ancho = img.Size.Width;
            alto = img.Size.Height +25;
            selected = false;
            DrawPercLife = lifeSoldado / 3;
        }
        private void EscogeAtaqueUnidadMilitar()
        {
            if(NameUnidad == "jediW (1)") { ataque = 20; lifeSoldado = 125; costoComida = 50; costoMineral = 50;  /*ancho alto*/ }
            else
            {
                if (NameUnidad == "sithW (1)") { ataque = 10; lifeSoldado = 175; costoComida = 50; costoMineral = 50;  /*ancho alto*/ }
                else
                {
                    if (NameUnidad == "str1" || NameUnidad == "robo1") { ataque = 5; lifeSoldado = 100; costoComida = 30; costoMineral = 30;/*ancho alto*/ }
                    else
                    {
                        if (NameUnidad == "navejedi (1)" || NameUnidad == "navesith (1)") { ataque = 25; lifeSoldado = 80; costoComida = 100; costoMineral = 100; }
                    }
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
