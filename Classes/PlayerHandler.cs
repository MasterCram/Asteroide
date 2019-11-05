using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cstj.Sim.ES.MSimard.Classes
{
    public static class PlayerHandler
    {
        public static int money = 0;
        public static int level = 1;
        public static int levelSelected = 1;
        public static int difficulty = 1;
        public static int vaisseauID = 1;
        public static int skillPoints = 0;
        public static int attaque = 3;
        public static int defense = 3;
        public static int tir = 3;
        public static int vitesse = 3;
        public static int vie = 5;
        public static int attaqueAjoute = 0;
        public static int defenseAjoute = 0;
        public static int tirAjoute = 0;
        public static int vitesseAjoute = 0;
        public static int vieAjoute = 0;
        public static int shootSkill = 0;

        public static void setStats(int a, int d, int t, int vit, int v)
        {
            attaque = a;
            defense = d;
            tir = t;
            vitesse = vit;
            vie = v;
        }
    }
}
