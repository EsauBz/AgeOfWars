using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scroll_fondo
{
    class Mundo
    {
        private List<Mineral> RecursoMineral;
        private List<Comida> RecursoComida;
        private Bitmap escenario;
        public Mundo(int i)
        {
            RecursoMineral = new List<Mineral>();
            RecursoComida = new List<Comida>();
            if (i == 1) { escenario = (Bitmap)Image.FromFile("fondo2.png"); }
            else
            {
                escenario = (Bitmap)Image.FromFile("fondo.png");
            }
            startAñadeComida();
            startAñadeMinerales();
        }//Fin de constructor
        private void startAñadeMinerales()
        {

        }
        private void startAñadeComida()
        {

        }
        public Bitmap getImgMundo()
        {
            return escenario;
        }
        public List<Mineral> getRecursoMinMundo()
        {
            return RecursoMineral;
        }
        public List<Comida> getRecursoComMundo()
        {
            return RecursoComida;
        }
    }
}
