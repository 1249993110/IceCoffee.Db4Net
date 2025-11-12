using IceCoffee.Db4Net.Extensions;
using IceCoffee.Db4Net.UnitTests.Entities;

namespace IceCoffee.Db4Net.UnitTests
{
    public class UpdateTests : TestFixtureBase
    {
        [Fact]
        public async Task Update_SingleEntity_ShouldSucceed()
        {
            var entity = await InsertTestData<Country>();
            entity.Name = "Updated Name";

            var affectedRows = await Db.Update(entity).ExecuteAsync();

            Assert.Equal(1, affectedRows);

            var updatedEntity = await Db.Query<Country>(entity.Id).GetSingleAsync();

            Assert.Equal("Updated Name", updatedEntity.Name);
        }

        [Fact]
        public async Task Update_MultipleEntities_ShouldSucceed()
        {
            var entities = await InsertTestData<Country>(5);
            foreach (var entity in entities)
            {
                entity.Name = "Updated Name " + entity.Id;
            }

            var affectedRows = await Db.UpdateMany(entities).ExecuteAsync();

            Assert.Equal(5, affectedRows);

            foreach (var entity in entities)
            {
                var updatedEntity = await Db.Query<Country>(entity.Id).GetSingleAsync();

                Assert.Equal("Updated Name " + entity.Id, updatedEntity.Name);
            }
        }

        [Fact]
        public async Task Update_WithCondition_ShouldUpdateMatchingEntities()
        {
            var entities = await InsertTestData<Country>(2);
            var entity1 = entities.First();
            var entity2 = entities.Last();

            var affectedRows = await Db.Update<Country>()
                .Set(i => i.Name, "Updated")
                .WhereEq(i => i.Name, entity1.Name)
                .ExecuteAsync();

            Assert.Equal(1, affectedRows);

            var updatedEntity = await Db.Query<Country>(entity1.Id).GetSingleAsync();

            Assert.NotNull(updatedEntity);
            Assert.Equal("Updated", updatedEntity.Name);

            var notUpdatedEntity = await Db.Query<Country>(entity2.Id).GetSingleAsync();
            Assert.NotNull(notUpdatedEntity);
            Assert.Equal(entity2.Name, notUpdatedEntity.Name);
        }

        [Fact]
        public async Task Update_WhereIn_ShouldUpdateMatchingEntities()
        {
            var entities = await InsertTestData<Country>(5);
            var idsToUpdate = entities.Take(3).Select(i => i.Id).ToList();

            var affectedRows = await Db.Update<Country>()
                .Set(i => i.Name, "Updated Name")
                .WhereIn(i => i.Id, idsToUpdate)
                .ExecuteAsync();

            Assert.Equal(3, affectedRows);

            foreach (var entity in entities.Where(i => idsToUpdate.Contains(i.Id)))
            {
                var updatedEntity = await Db.Query<Country>(entity.Id).GetSingleAsync();

                Assert.Equal("Updated Name", updatedEntity.Name);
            }

            foreach (var entity in entities.Where(i => !idsToUpdate.Contains(i.Id)))
            {
                var notUpdatedEntity = await Db.Query<Country>(entity.Id).GetSingleAsync();

                Assert.Equal(entity.Name, notUpdatedEntity.Name);
            }
        }

        [Fact]
        public async Task Update_AllEntities_ShouldFailIfNoCondition()
        {
            var updateAll = () => Db.Update<Country>().Set(i => i.Name, "Updated Name").ExecuteAsync();

            await Assert.ThrowsAsync<ArgumentNullException>(updateAll);
        }
    }
}
