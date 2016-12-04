﻿using System;
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
        private List<UnidadMilitar> Uespecial;
        private List<UnidadMilitar> Naves;
        private List<Edificio> CUs;
        private List<Edificio> Farms;
        private List<Edificio> Cuarteles;
        // lista de soldados
        //lista de edificios
        private int material;
        private int food;
        Aldeano al;

        public Player(bool bnd)
        {
            bando = bnd;
            CUs = new List<Edificio>();
            Uespecial = new List<UnidadMilitar>();
            Naves = new List<UnidadMilitar>();
            milicia = new List<UnidadMilitar>();
            aldeanos = new List<Aldeano>();
            Farms = new List<Edificio>();
            CoordInicialMapX = r.Next(100,2901); //random para aparecer al azar
            CoordInicialMapY = r.Next(100, 2901);// random para aparecer al azar
            for(int i = 0; i < 3; i++)
            {
                al = new Aldeano(CoordInicialMapX + r.Next(50,71), CoordInicialMapY); //coordenadas de inicio
                aldeanos.Add(al);
            }
            Edificio cu;
            if (bando == true) {  cu = new Edificio(CoordInicialMapX, CoordInicialMapY, "CUJ"); }
            else
            {
                 cu = new Edificio(CoordInicialMapX, CoordInicialMapY, "CUS");
            }
            CUs.Add(cu);
            UnidadMilitar umil ;
            if (bando == true) { umil = new UnidadMilitar(CoordInicialMapX + r.Next(5), CoordInicialMapY + r.Next(5), "str1"); }
            else
            {
                umil = new UnidadMilitar(CoordInicialMapX + r.Next(5), CoordInicialMapY + r.Next(5), "robo1");
            }
            milicia.Add(umil);         
            food = 200;
            material = 150;
        }

        public int getMineral()
        {
            return material;
        }

        public int getComida()
        {
            return food;
        }

        public void AddAldeano(Aldeano al)
        {
            aldeanos.Add(al);                 
        }

        public List<UnidadMilitar> getListNaves()
        {
            return Naves;
        }

        public List<UnidadMilitar> getListUespecial()
        {
            return Uespecial;
        }

        public List<Aldeano> getListAldeanos()
        {
            return aldeanos;
        }

        public List<UnidadMilitar> getlistMilicia()
        {
            return milicia;
        }

        public List<Edificio> getlistCUs()
        {
            return CUs;
        }

        public List<Edificio> getListFarms()
        {
            return Farms;
        }

        public int getCoordInicalMapX()
        {
            return CoordInicialMapX;
        }
        public int getCoordInicalMapY()
        {
            return CoordInicialMapY;
        }
        public void setMineral(int addRecurso)
        {
            material += addRecurso;
        }

        public void setComida(int addRecurso)
        {
            food += addRecurso;
        }
    }
}
