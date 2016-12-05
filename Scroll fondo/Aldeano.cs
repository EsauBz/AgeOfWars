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
        private Bitmap estatico = (Bitmap)Image.FromFile("aldeanoW (3).png");
        private List<Bitmap> animacion = new List<Bitmap>();
        private int currentIndex;
        private int upbeetFrames;
        private int frameCount;
        //array lis bitmaps
        private bool isMoving;
        private int ProvX, ProvY;
        private int mapX, mapY;
        private bool selected;
        private int ancho;
        private int alto;
        private bool painted;
        private int lifeAldeano;
        private int DrawPercLife;
        private int costoComida;
        private bool Icamina;
        private bool Irecoge;
        public Aldeano(int x1 , int y1)
        {
            Icamina = false;
            Irecoge = false;
            isMoving = false;
            currentIndex = -1;
            upbeetFrames = 6;
            frameCount = 0;
            painted = false;            
            mapX = x1;
            mapY = y1;
            ProvX = mapX;
            ProvY = mapY;
            ancho = estatico.Size.Width;
            alto = estatico.Size.Height + 25;
            selected = false;
            lifeAldeano = 50;
            DrawPercLife = lifeAldeano / 4;
            costoComida = 50;
            parado();
        }

        public Bitmap getEstatico()
        {
            return estatico;
        }

        public bool getIcamina()
        {
            return Icamina;
        }

        public void SetCamina(bool a)
        {
            Icamina = a;
        }

        public bool getIrecoge()
        {
            return Irecoge;
        }

        public void setIrecoge(bool a)
        {
            Irecoge = a;
        }

        public List<Bitmap> getListAnimacion()
        {
            return animacion;
        }

        public int getProvX()
        {
            return ProvX;
        }

        public int getProvY()
        {
            return ProvY;
        }

        public void setProvX(int a)
        {
            ProvX = a;
        }

        public void setProvY(int a)
        {
            ProvY = a;
        }

        public bool getIsMoving()
        {
            return isMoving;
        }

        public void setIsMoving(bool a)
        {
            isMoving = a;
        }

        public void Update()
        {
            if (animacion.Count == 0) return;
            frameCount++;
            if (frameCount >= upbeetFrames)
            {
                currentIndex++;
                if(currentIndex >= animacion.Count) { currentIndex = 0; }
                frameCount = 0;
            }
        }

        public void addFrame(Bitmap aux)
        {
            animacion.Add(aux);
        }

        public Bitmap currentFrameActual()
        {
            if (animacion.Count == 0) return null;
            if(currentIndex == -1) { return null; }
            return animacion[currentIndex];
        }

        public void Camina()
        {
            animacion.Clear();
            currentIndex = 0;
            //addFrame((Bitmap)Image.FromFile("aldeanoW (1).png"));
            //addFrame((Bitmap)Image.FromFile("aldeanoW (2).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoW (3).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoW (4).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoW (5).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoW (6).png"));
        }

        public void Recoge()
        {
            animacion.Clear();
            currentIndex = 0;
            addFrame((Bitmap)Image.FromFile("aldeanoR (1).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoR (2).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoR (3).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoR (4).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoR (5).png"));
            addFrame((Bitmap)Image.FromFile("aldeanoR (6).png"));
        }

        public void parado()
        {
            animacion.Clear();
            currentIndex = 0;
            addFrame((Bitmap)Image.FromFile("aldeanoW (3).png"));
        }

        public int getCostoComida()
        {
            return costoComida;
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
