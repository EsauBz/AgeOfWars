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
        private List<Bitmap> animacion = new List<Bitmap>();
        private int currentIndex;
        private int upbeetFrames;
        private int frameCount;
        private bool isMoving;
        private int ProvX, ProvY;
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
        private bool Icamina;
        private bool Iataca;
        public UnidadMilitar(int x1, int y1, string name)
        {
            Icamina = false;
            Iataca = false;
            isMoving = false;
            currentIndex = -1;
            upbeetFrames = 6;
            frameCount = 0;
            NameUnidad = name;
            ProvX = mapX;
            ProvY = mapY;
            img = (Bitmap)Image.FromFile(name+".png");
            painted = false;
            EscogeAtaqueUnidadMilitar();
            mapX = x1;
            mapY = y1;
            ancho = img.Size.Width;
            alto = img.Size.Height +25;
            selected = false;
            DrawPercLife = lifeSoldado / 3;
            parado();
        }
        private void EscogeAtaqueUnidadMilitar()
        {
            if(NameUnidad == "jediW (3)") { ataque = 20; lifeSoldado = 125; costoComida = 50; costoMineral = 50;  /*ancho alto*/ }
            else
            {
                if (NameUnidad == "sithW (3)") { ataque = 10; lifeSoldado = 175; costoComida = 50; costoMineral = 50;  /*ancho alto*/ }
                else
                {
                    if (NameUnidad == "str1" || NameUnidad == "robo1") { ataque = 5; lifeSoldado = 100; costoComida = 30; costoMineral = 30;/*ancho alto*/ }
                    else
                    {
                        if (NameUnidad == "navejedi (1)" || NameUnidad == "navesith (1)") { ataque = 25; lifeSoldado = 250; costoComida = 100; costoMineral = 100; }
                    }
                }
            }
        }

        public bool getIcamina()
        {
            return Icamina;
        }

        public void SetCamina(bool a)
        {
            Icamina = a;
        }

        public bool getIataca()
        {
            return Iataca;
        }

        public void setIataca(bool a)
        {
            Iataca = a;
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
                if (currentIndex >= animacion.Count) { currentIndex = 0; }
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
            if (currentIndex == -1) { return null; }
            return animacion[currentIndex];
        }

        public void Camina()
        {
            animacion.Clear();
            currentIndex = 0;
            if (NameUnidad == "jediW (1)")
            {
                //addFrame((Bitmap)Image.FromFile("jediW (1).png"));
                //addFrame((Bitmap)Image.FromFile("jediW (2).png"));
                addFrame((Bitmap)Image.FromFile("jediW (3).png"));
                addFrame((Bitmap)Image.FromFile("jediW (4).png"));
                addFrame((Bitmap)Image.FromFile("jediW (5).png"));
                addFrame((Bitmap)Image.FromFile("jediW (6).png"));
            }
            else
            {
                if (NameUnidad == "sithW (1)")
                {
                    //addFrame((Bitmap)Image.FromFile("sithW (1).png"));
                    //addFrame((Bitmap)Image.FromFile("sithW (2).png"));
                    addFrame((Bitmap)Image.FromFile("sithW (3).png"));
                    addFrame((Bitmap)Image.FromFile("sithW (4).png"));
                    addFrame((Bitmap)Image.FromFile("sithW (5).png"));
                    addFrame((Bitmap)Image.FromFile("sithW (6).png"));
                }
                else
                {
                    if (NameUnidad == "str1")
                    {
                        addFrame((Bitmap)Image.FromFile("str2.png"));
                        addFrame((Bitmap)Image.FromFile("str3.png"));
                        addFrame((Bitmap)Image.FromFile("str4.png"));
                        addFrame((Bitmap)Image.FromFile("str5.png"));
                        addFrame((Bitmap)Image.FromFile("str6.png"));
                        addFrame((Bitmap)Image.FromFile("str7.png"));
                    }
                    else
                    {
                        if (NameUnidad == "navejedi (1)")
                        {
                            addFrame((Bitmap)Image.FromFile("navejedi (1).png"));
                            addFrame((Bitmap)Image.FromFile("navejedi (2).png"));
                            addFrame((Bitmap)Image.FromFile("navejedi (3).png"));
                            addFrame((Bitmap)Image.FromFile("navejedi (4).png"));                            
                        }
                        else
                        {
                            if(NameUnidad == "robo1")
                            {
                                addFrame((Bitmap)Image.FromFile("robo2.png"));
                                addFrame((Bitmap)Image.FromFile("robo3.png"));
                                addFrame((Bitmap)Image.FromFile("robo6.png"));
                                addFrame((Bitmap)Image.FromFile("robo7.png"));
                                addFrame((Bitmap)Image.FromFile("robo8.png"));
                            }
                            else
                            {
                                if(NameUnidad == "navesith (1)")
                                {
                                    addFrame((Bitmap)Image.FromFile("navesith (1).png"));
                                    addFrame((Bitmap)Image.FromFile("navesith (2).png"));
                                    addFrame((Bitmap)Image.FromFile("navesith (3).png"));
                                    addFrame((Bitmap)Image.FromFile("navesith (4).png"));
                                    addFrame((Bitmap)Image.FromFile("navesith (5).png"));
                                    addFrame((Bitmap)Image.FromFile("navesith (6).png"));
                                    addFrame((Bitmap)Image.FromFile("navesith (7).png"));
                                    addFrame((Bitmap)Image.FromFile("navesith (8).png"));
                                }
                            }
                        }
                    }
                }
            }           
        }

        public void ataca()
        {
            animacion.Clear();
            currentIndex = 0;
            if (NameUnidad == "jediW (1)")
            {
                addFrame((Bitmap)Image.FromFile("jediA (1).png"));
                addFrame((Bitmap)Image.FromFile("jediA (2).png"));
                addFrame((Bitmap)Image.FromFile("jediA (3).png"));
                addFrame((Bitmap)Image.FromFile("jediA (4).png"));
                addFrame((Bitmap)Image.FromFile("jediA (5).png"));
                addFrame((Bitmap)Image.FromFile("jediA (6).png"));
                addFrame((Bitmap)Image.FromFile("jediA (7).png"));
                addFrame((Bitmap)Image.FromFile("jediA (8).png"));
                addFrame((Bitmap)Image.FromFile("jediA (9).png"));
            }
            else
            {
                if (NameUnidad == "sithW (1)")
                {
                    addFrame((Bitmap)Image.FromFile("sithA (1).png"));
                    addFrame((Bitmap)Image.FromFile("sithA (2).png"));
                    addFrame((Bitmap)Image.FromFile("sithA (3).png"));
                    addFrame((Bitmap)Image.FromFile("sithA (4).png"));
                    addFrame((Bitmap)Image.FromFile("sithA (5).png"));
                    addFrame((Bitmap)Image.FromFile("sithA (6).png"));
                    addFrame((Bitmap)Image.FromFile("sithA (7).png"));
                    addFrame((Bitmap)Image.FromFile("sithA (8).png"));
                    addFrame((Bitmap)Image.FromFile("sithA (9).png"));
                }
                else
                {
                    if (NameUnidad == "str1")
                    {
                        addFrame((Bitmap)Image.FromFile("stra1.png"));
                        addFrame((Bitmap)Image.FromFile("stra2.png"));
                        addFrame((Bitmap)Image.FromFile("stra3.png"));
                        addFrame((Bitmap)Image.FromFile("stra4.png"));
                        addFrame((Bitmap)Image.FromFile("stra5.png"));
                    }
                    else
                    {
                        if (NameUnidad == "navejedi (1)")
                        {
                            addFrame((Bitmap)Image.FromFile("navejedi (1).png"));
                            addFrame((Bitmap)Image.FromFile("navejedi (2).png"));
                        }
                        else
                        {
                            if (NameUnidad == "robo1")
                            {
                                addFrame((Bitmap)Image.FromFile("roboa1.png"));
                                addFrame((Bitmap)Image.FromFile("roboa2.png"));
                                addFrame((Bitmap)Image.FromFile("roboa3.png"));
                                addFrame((Bitmap)Image.FromFile("roboa4.png"));
                                addFrame((Bitmap)Image.FromFile("roboa5.png"));
                            }
                            else
                            {
                                if (NameUnidad == "navesith (1)")
                                {
                                    addFrame((Bitmap)Image.FromFile("navesith (1).png"));
                                    addFrame((Bitmap)Image.FromFile("navesith (2).png"));                                    
                                }
                            }
                        }
                    }
                }
            }
        }

        public void parado()
        {
            animacion.Clear();
            currentIndex = 0;
            if (NameUnidad == "jediW (1)")
            {
                addFrame((Bitmap)Image.FromFile("jediA (3).png"));
            }
            else
            {
                if (NameUnidad == "sithW (1)")
                {
                    addFrame((Bitmap)Image.FromFile("sithA (3).png"));                    
                }
                else
                {
                    if (NameUnidad == "str1")
                    {
                        addFrame((Bitmap)Image.FromFile("str1.png"));
                    }
                    else
                    {
                        if (NameUnidad == "navejedi (1)")
                        {
                            addFrame((Bitmap)Image.FromFile("navejedi (1).png"));
                        }
                        else
                        {
                            if (NameUnidad == "robo1")
                            {
                                addFrame((Bitmap)Image.FromFile("robo1.png"));
                            }
                            else
                            {
                                if (NameUnidad == "navesith (1)")
                                {
                                    addFrame((Bitmap)Image.FromFile("navesith (1).png"));
                                }
                            }
                        }
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
