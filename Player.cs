using System;
using System.Collections.Generic;
using System.Timers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    class Player
    {
        int CoordInicialMapX;
        int CoordInicialMapY;
        Random r = new Random();
        public bool bando;
        private List<Aldeano> aldeanos;
        private List<UnidadMilitar> milicia;
        private List<UnidadMilitar> jedis;
        // lista de soldados
        //lista de edificios
        public int material;
        public int food;
        Aldeano al;

        public Player()
        {
            aldeanos = new List<Aldeano>();
            CoordInicialMapX = r.Next(0,3001); //random para aparecer al azar
            CoordInicialMapY = r.Next(0, 3001);// random para aparecer al azar
            for(int i = 0; i < 3; i++)
            {
                al = new Aldeano(CoordInicialMapX + r.Next(50), CoordInicialMapY + r.Next(50)); //coordenadas de inicio
                aldeanos.Add(al);
            }
            //ageregar un soldado
            // agregar un edicio centro urbano
            food = 200;
            material = 150;
        }

        public void AddAldeano(Aldeano al)
        {
            aldeanos.Add(al);                 
        }
        public List<Aldeano> getListAldeanos()
        {
            return aldeanos;
        }
        public int getCoordInicalMapX()
        {
            return CoordInicialMapX;
        }
        public int getCoordInicalMapY()
        {
            return CoordInicialMapY;
        }
        public void revisaRecogidaAldeanos(object obj, ElapsedEventHandler arg)
        {

        }
    }
}
