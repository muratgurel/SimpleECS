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
                system.GetEntities();
            });
        }

        [Test]
        public void GetEntitiesShouldUseInnerWorld()
        {
            world.AddSystem(system);
            Entity entity = world.CreateEntity();

            Assert.Contains(entity, system.GetEntities());
        }

        [Test]
        public void GetEntitiesShouldNeverReturnNullList()
        {
            world.AddSystem(system);

            Assert.IsNotNull(system.GetEntities());

            world.CreateEntity();

            Assert.IsNotNull(system.GetEntities());
        }

        private class ExampleSystem : System
        {
            public override IPredicate<Entity> entityPredicate
            {
                get { return EntityPredicate.New; }
            }

            public void Update(float deltaTime)
            {
                foreach (var entity in GetEntities())
                {
                    
                }
            }
        }
    }
}
