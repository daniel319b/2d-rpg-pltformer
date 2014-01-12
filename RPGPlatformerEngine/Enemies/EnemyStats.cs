using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPlatformerEngine
{
    /// <summary>
    /// A class that holds the stats of enemies.
    /// </summary>
    public class EnemyStats
    {
        /// <summary>
        /// The current health of this enemy.
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// The max health this enemy can have.
        /// </summary>
        public int MaxHealth { get; set; }

        /// <summary>
        /// The exp points the user will get upon killing this enemy.
        /// </summary>
        public int ExpPointsBonus { get; set; }

        /// <summary>
        /// The level of this enemy, will affect damage, health etc..
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The attack rate of the enemy in seconds.
        /// </summary>
        public float AttackRate { get; set; }
    }
}
