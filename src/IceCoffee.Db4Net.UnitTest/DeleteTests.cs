using IceCoffee.Db4Net.Extensions;
using IceCoffee.Db4Net.UnitTest.Entities;

namespace IceCoffee.Db4Net.UnitTest
{
    public class DeleteTests : TestFixtureBase
    {
        [Fact]
        public async Task Delete_SingleEntity_ShouldSucceed()
        {
            var entity = await InsertTestData<Country>();
            
            var affectedRows = await Db.Delete(entity).ExecuteAsync();
            
            Assert.Equal(1, affectedRows);
            
            var deletedEntity = await Db.Query<Country>(entity.Id).GetSingleOrDefaultAsync();
            
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task Delete_MultipleEntities_ShouldSucceed()
        {
            var entities = await InsertTestData<Country>(5);
            
            var affectedRows = await Db.DeleteMany(entities).ExecuteAsync();
            
            Assert.Equal(5, affectedRows);
            
            var deletedEntities = await Db.Query<Country>().GetListAsync();
            
            Assert.Empty(deletedEntities);
        }

        [Fact]
        public async Task Delete_WithCondition_ShouldDeleteMatchingEntities()
        {
            var entities = await InsertTestData<Country>(5);
            var entityToDelete = entities.First();
            
            var affectedRows = await Db.Delete<Country>()
                .WhereEq(i => i.Id, entityToDelete.Id)
                .ExecuteAsync();
            
            Assert.Equal(1, affectedRows);
            
            var deletedEntity = await Db.Query<Country>(entityToDelete.Id).GetSingleOrDefaultAsync();
            
            Assert.Null(deletedEntity);
        }

        [Fact]
        public async Task Delete_WhereIn_ShouldDeleteMatchingEntities()
        {
            var entities = await InsertTestData<Country>(5);
            var idsToDelete = entities.Take(3).Select(i => i.Id).ToList();
            
            var affectedRows = await Db.Delete<Country>()
                .WhereIn(i => i.Id, idsToDelete)
                .ExecuteAsync();
            
            Assert.Equal(3, affectedRows);
            
            var remainingEntities = await Db.Query<Country>().GetListAsync();
            
            Assert.Equal(2, remainingEntities.Count());
            Assert.DoesNotContain(remainingEntities, e => idsToDelete.Contains(e.Id));
        }

        [Fact]
        public async Task Delete_AllEntities_ShouldFailIfNoCondition()
        {
            var deleteAll = () => Db.Delete<Country>().ExecuteAsync();

            await Assert.ThrowsAsync<ArgumentNullException>(deleteAll);
        }
    }
}
