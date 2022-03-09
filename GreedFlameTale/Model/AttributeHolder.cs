﻿namespace GreedFlameTale.Model
{
    /// <summary>
    /// This record is The attributes collection.
    /// </summary>
    public class AttributeHolder
    {
        /// <summary>
        /// If the character can attack
        /// </summary>
        public bool CanAttack => this.Stamina.CompareTo(this.NormalCost) >= 0;

        /// <summary>
        /// If the character can cast a special attack
        /// </summary>
        public bool CanSpecialAttack => this.Stamina.CompareTo(this.SpecialCost) >= 0;

        /// <summary>
        /// If the character is alive
        /// </summary>
        public bool IsAlive => !this.HitPoints.IsEmpty;

        /// <summary>
        /// Hit Points, aka HP
        /// </summary>
        public Measure HitPoints { get; init; }
        /// <summary>
        /// Stamina, how much habilities can be cast
        /// </summary>
        public Measure Stamina { get; init; }
        /// <summary>
        /// Magic Power, aka MP
        /// </summary>
        public Measure MagicPower { get; init; }
        /// <summary>
        /// AttackPower, aka Strength
        /// </summary>
        public Measure AttackPower { get; init; }
        /// <summary>
        /// Armor Points, aka Defense
        /// </summary>
        public Measure Armor { get; init; }
        /// <summary>
        /// Cost of normal habilities
        /// </summary>
        public Measure NormalCost { get; init; }
        /// <summary>
        /// Cost of Special Habilities
        /// </summary>
        public Measure SpecialCost { get; init; }
        /// <summary>
        /// Heal Points, how much <see cref="HitPoints"/> are recovered on healing
        /// </summary>
        public Measure HealPoints { get; init; }
        /// <summary>
        /// How much <see cref="Stamina"/> is recovered on resting
        /// </summary>
        public Measure RestPoints { get; init; }
    }
}