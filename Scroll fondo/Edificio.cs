using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    class Edificio
    {
        public Bitmap img;

        private string EdificioName;
        private int mapX, mapY;
        private bool selected;
        private int ancho;
        private int alto;
        private bool painted;
        private int lifeEdificio;
        private int DrawPercLife;
        public Edificio(int x1, int y1, string name)
        {
            img = (Bitmap)Image.FromFile(name+".png");
            EdificioName = name;
            EligeAtributosEdificio();
            painted = false;
            mapX = x1;
            mapY = y1;
            selected = false;
            lifeEdificio = 150;
            DrawPercLife = lifeEdificio / 3;
        }
        private void EligeAtributosEdificio()
        {
            if (EdificioName == "CUJ") { lifeEdificio = 300; ancho = 597; alto = 368; }
            else
            {
                if (EdificioName == "Farm") { lifeEdificio = 150; ancho = 399; alto = 282; }
                    {
                    if (EdificioName == "cuartel") { lifeEdificio = 200; ancho = 532; alto = 368; }
                    else
                    {
                        if (EdificioName == "CUS") { lifeEdificio = 300; ancho = 597; alto = 368; }
                    }
                }
            }
        }
        public int getPercentLifeDraw()
        {
            return DrawPercLife;
        }
        public int getLifeEdificio()
        {
            return lifeEdificio;
        }
        public void recibeDaño(int damage)
        {
            lifeEdificio -= damage;
            DrawPercLife = lifeEdificio / 2;
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
