using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPlatformerEngine
{
    public class PlayerStatistics
    {
        public int Health { get; set; }

        public int MaxHealth { get; set; }

        public int Level { get; set; }

        public int ExperiencePoints { get; set; }

        public int XPMultiplier { get; set; }

        public int UpgradePoints { get; set; }

        public int Money { get; set; }

        public int BaseDamage { get; set; }

        public int ExpPointsToNextLevel { get { return (Level * Level) * 3; } }

        public PlayerStatistics()
        {
            Level = 1;
            Health = MaxHealth = 100;
            ExperiencePoints = 0;
            XPMultiplier = 1;
            UpgradePoints = 0;
            Money = 10;
            BaseDamage = 5;
        }

       
    }
}
