using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Media;

namespace Scroll_fondo
{
    class Menu
    {
        /************Variables Globales de Graficos ******************/
        Bitmap fondo;
        /**********************Variables de Sonido ***************************************/
        SoundPlayer cancion = new SoundPlayer("wars.wav");
        List<boton> botones; //Lista que se actualiza con los botones de los Menus
        boton bot;
        public Menu(string name)
        {
            fondo = (Bitmap)Image.FromFile(name); //Variable fondo contiene la imagen de Background
            botones = new List<boton>(); //Se crea lista de botones.
            cancion.PlayLooping(); //Inicia la reproduccion de Sonido
        }

        public void GeneraBotonesMenu()
        {
            bot = new boton(100, 300, "partnu.jpg", 40, 100);
            botones.Add(bot);
            bot = new boton(100, 400, "ayuda.jpg", 40, 100);
            botones.Add(bot);
            bot = new boton(300, 300, "cred.jpg", 40, 100);
            botones.Add(bot);
            bot = new boton(300, 400, "salir.jpg", 40, 100);
            botones.Add(bot);
        }

        public void GeneraBotonesBando()
        {
            bot = new boton(80, 200, "jedi.jpg", 40, 100);
            botones.Add(bot);
            bot = new boton(910, 200, "sith.jpg", 40, 100);
            botones.Add(bot);
            bot = new boton(470, 450, "volver.jpg", 40, 100);
            botones.Add(bot);
        }

        public Bitmap getImgfondo()
        {
            return fondo;
        }

        public List<boton> getBotones()
        {
            return botones;
        }

        public SoundPlayer getCancion()
        {
            return cancion;
        }
    }
}
