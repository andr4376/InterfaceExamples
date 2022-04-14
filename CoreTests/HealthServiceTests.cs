using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Core.Tests
{
    [TestClass()]
    public class HealthServiceTests
    {
        [TestMethod()]
        public void HealthService_props()
        {
            //arrange
            int health = 50;
            int maxHealth = 100;

            //act
            IHealthService svc = new HealthService(health, maxHealth);

            //assert
            Assert.AreEqual(health, svc.CurrentHealth);
            Assert.AreEqual(maxHealth, svc.MaxHealth);
        }

        [TestMethod()]
        public void HealthService_Heal()
        {
            //arrange
            int health = 50;
            int toHeal = 25;
            //act
            IHealthService svc = new HealthService(health, maxHealth: 10000);
            svc.Heal(toHeal);

            //assert
            Assert.AreEqual(health + toHeal, svc.CurrentHealth);
        }
        [TestMethod()]
        public void HealthServiceTest_Heal_cannotOverheal()
        {
            //arrange
            int health = 50;
            int toHeal = 9999;
            //act
            IHealthService svc = new HealthService(health);
            svc.Heal(toHeal);

            //assert
            Assert.AreEqual(svc.MaxHealth, svc.CurrentHealth);
        }

        [TestMethod()]
        public void HealthServiceTest_Heal_Negative_Exception()
        {
            //arrange
            int health = 50;
            int toHeal = -1;

            //act
            IHealthService svc = new HealthService(health);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => svc.Heal(toHeal));

        }

        [TestMethod()]
        public void HealthServiceTest_TakeDamage_Negative_Exception()
        {
            //arrange
            int health = 50;
            int dmg = -1;

            //act
            IHealthService svc = new HealthService(health);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => svc.TakeDamage(dmg));

        }

        [TestMethod()]
        public void HealthServiceTest_TakeDamage()
        {
            //arrange
            int health = 50;
            int dmg = 1;
            //act
            IHealthService svc = new HealthService(health);
            svc.TakeDamage(dmg);

            //assert
            Assert.AreEqual(svc.MaxHealth - dmg, svc.CurrentHealth);
        }

        [TestMethod()]
        public void HealthServiceTest_TakeDamage_cannotOverkill()
        {
            //arrange
            int health = 50;
            int dmg = 1000;
            //act
            IHealthService svc = new HealthService(health);
            svc.TakeDamage(dmg);

            //assert
            Assert.AreEqual(0, svc.CurrentHealth);
        }

        [TestMethod()]
        public void HealthServiceTest_TakeDamage_Killed_IsAliveFalse()
        {
            //arrange
            int health = 50;
            int dmg = 1000;
            //act
            IHealthService svc = new HealthService(health);
            svc.TakeDamage(dmg);

            //assert
            Assert.AreEqual(0, svc.CurrentHealth);
            Assert.IsFalse(svc.IsAlive);
        }

        [TestMethod()]
        public void HealthServiceTest_OnDeath()
        {
            //arrange
            int health = 50;
            int dmg = 1000;

            //act
            int deathCount = 0;
            IHealthService svc = new HealthService(health, () => { deathCount++; });
            svc.TakeDamage(dmg);

            //assert
            Assert.AreEqual(1, deathCount);
        }

        [TestMethod()]
        public void HealthServiceTest_OnDeath_DoesNotInvokeOnDeathMoreThanOnce()
        {
            //arrange
            int health = 50;
            int dmg = 1000;

            //act
            int deathCount = 0;
            IHealthService svc = new HealthService(health, () => { deathCount++; });
            svc.TakeDamage(dmg);
            svc.Heal(1);
            svc.TakeDamage(999999);

            //assert
            Assert.AreEqual(1, deathCount);
        }
    }
}