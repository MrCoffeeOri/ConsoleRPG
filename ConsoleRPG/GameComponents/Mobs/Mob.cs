﻿namespace ConsoleRPG.Mobs
{
    internal abstract class Mob
    {
        #region Properties

        public int level, coins = 0, strengthPoints, resistencePoints, speedPoints, difficultyFactor;
        public float life;
        public string name;
        protected float NewLife => level * 30 + Resistence;
        public int AttackDamage => Strength + 10 * level;
        public int Resistence => resistencePoints * 5;
        public int Speed => speedPoints * 10;
        public int Strength => strengthPoints * 10;
        protected float xp;
        public abstract float Xp { get; set; }

        #endregion
        
        public Mob(int level, string name, int strengthPoints, int resistencePoints, int speedPoints)
        {
            this.strengthPoints += strengthPoints;
            this.resistencePoints += resistencePoints;
            this.speedPoints += speedPoints;
            this.name = name;
            this.level = level;
            life = NewLife;
        }

        protected virtual void Attack(Mob deffenser, int AttackDamage) => deffenser.life -= GameComponents.GameManager.rand.Next(AttackDamage, AttackDamage + 5) - deffenser.Resistence;
    }
}