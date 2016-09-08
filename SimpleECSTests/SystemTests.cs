using System;
using NUnit.Framework;

namespace SimpleECS.Test
{
    [TestFixture]
    public class SystemTests
    {
        private World world;
        private ExampleSystem system;

        [SetUp]
        public void SetUp()
        {
            world = new World();
            system = new ExampleSystem();
        }

        [Test]
        public void UninitializedSystemShouldHaveNullWorld()
        {
            Assert.IsNull(system.world);
        }

        [Test]
        public void UninitializedSystemShouldThrowProperExceptionInGetEntities()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                system.GetEntities(EntityPredicate.New);
            });
        }

        [Test]
        public void GetEntitiesShouldUseInnerWorld()
        {
            world.AddSystem(system);
            Entity entity = world.CreateEntity();

            Assert.Contains(entity, system.GetEntities(EntityPredicate.New));
        }

        private class ExampleSystem : System
        {
            public void Update(float deltaTime)
            {
                foreach (var entity in GetEntities(EntityPredicate.New))
                {
                    // Do something
                }
            }
        }
    }
}
