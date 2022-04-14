using System;
using System.Diagnostics;

namespace Core
{
    public interface IHealthService
    {
        /// <summary>
        /// Read current health
        /// </summary>
        int CurrentHealth { get; }

        /// <summary>
        /// Read maximum possibly health
        /// </summary>
        int MaxHealth { get; }

        /// <summary>
        /// Health status
        /// </summary>
        bool IsAlive { get; }

        /// <summary>
        /// Interface for receiving damage
        /// </summary>
        /// <param name="dmg"></param>
        void TakeDamage(int dmg);

        /// <summary>
        /// Interface for healing damage
        /// </summary>
        /// <param name="health"></param>
        void Heal(int health);
    }

    [DebuggerDisplay("CurrentHealth:{CurrentHealth}. MaxHealth:{MaxHealth}")]
    public partial class HealthService : IHealthService
    {
        private int health;

        private Action? OnDeath = null;

        /// <inheritdoc/>
        public int CurrentHealth
        {
            get { return health; }
            private set
            {
                health = value;
                if (health < 0)
                    health = 0;

                if (health > MaxHealth)
                    health = MaxHealth;

                if (health == 0 && OnDeath != null)
                {
                    OnDeath.Invoke();
                    OnDeath = null;
                }
            }
        }

        /// <inheritdoc/>
        public bool IsAlive
        {
            get
            {
                return health > 0;
            }
        }

        private int maxHealth;

        public HealthService(int health, int maxHealth)
        {
            this.health = health;
            this.maxHealth = maxHealth;
        }
        public HealthService(int health, int maxHealth, Action onDeath) : this(health, maxHealth)
        {
            this.OnDeath = onDeath;
        }
        public HealthService(int maxHealth) : this(maxHealth, maxHealth) { }
        public HealthService(int maxHealth, Action onDeath) : this(maxHealth, maxHealth, onDeath) { }

        /// <inheritdoc/>
        public int MaxHealth
        {
            get { return maxHealth; }
            private set { maxHealth = value; }
        }


        /// <inheritdoc/>
        public void Heal(int healing)
        {
            if (healing < 0)
                throw new ArgumentOutOfRangeException(nameof(healing) + " cannot be negative!");

            if (!IsAlive)
                return;

            CurrentHealth += healing;
        }

        /// <inheritdoc/>
        public void TakeDamage(int dmg)
        {
            if (dmg < 0)
                throw new ArgumentOutOfRangeException(nameof(dmg) + " cannot be negative!");
            CurrentHealth -= dmg;
        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
    }
}
