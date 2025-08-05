using IceCoffee.Db4Net.Core.SqlBuilders;
using IceCoffee.Db4Net.Extensions;

namespace IceCoffee.Db4Net.UnitTests
{
    public class QueryTests : TestFixtureBase
    {
        [Fact]
        public async Task Query_SingleEntityById_ShouldReturnSingleEntity()
        {
            var entity = await InsertTestData<Country>();

            var result = await Db.Query<Country>(entity.Id).GetSingleAsync();

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public async Task Query_AllEntities_ShouldReturnAll()
        {
            var entities = await InsertTestData<Country>(5);

            var results = await Db.Query<Country>().GetListAsync();

            Assert.Equal(entities.Count(), results.Count());
        }

        [Fact]
        public async Task Query_Count_ShouldReturnCorrectCount()
        {
            await InsertTestData<Country>(5);

            int count = await Db.QueryCount<Country>().GetAsync<int>();

            Assert.Equal(5, count);
        }

        [Fact]
        public async Task QueryPaged_Entities_ShouldReturnPagedResults()
        {
            var entities = await InsertTestData<Country>(5);

            var result = await Db.QueryPaged<Country>(1, 1).OrderBy(i => i.Id).GetPagedResultAsync();

            Assert.NotNull(result);
            Assert.Single(result.Items);
            Assert.Equal(entities.OrderBy(i => i.Id).First().Id, result.Items.First().Id);
        }

        [Fact]
        public async Task QueryExists_ExistingEntity_ShouldReturnTrue()
        {
            var entity = await InsertTestData<Country>();

            bool exists = await Db.QueryExists<Country>(entity.Id).GetAsync();

            Assert.True(exists);
        }

        [Fact]
        public async Task QueryWithWhere_Condition_ShouldReturnFilteredResults()
        {
            var entities = await InsertTestData<Country>(5);
            var targetEntity = entities.First();

            var results = await Db.Query<Country>()
                .WhereEq(i => i.Id, targetEntity.Id)
                .GetListAsync();

            Assert.Single(results);
            Assert.Equal(targetEntity.Id, results.First().Id);
        }

        [Fact]
        public async Task QueryWithWhereOr_Condition_ShouldReturnFilteredResults()
        {
            var entities = (await InsertTestData<Country>(5)).OrderBy(i => i.Id);
            var first = entities.First();
            var last = entities.Last();

            var filter = SqlBuilder<Country>.Filter;
            var results = await Db.Query<Country>()
                .WhereOr(filter.Eq(i => i.Id, first.Id), filter.Eq(i => i.Id, last.Id))
                .OrderBy(i => i.Id)
                .GetListAsync();

            Assert.Equal(first.Id, results.First().Id);
            Assert.Equal(last.Id, results.Last().Id);
        }

        [Fact]
        public async Task Query_SelectProperties_ShouldReturnSelectedProperties()
        {
            var entity = await InsertTestData<Country>();

            var result = await Db.Query<Country>()
                .Select(i => i.Id)
                .GetSingleAsync();

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result.Id);
            Assert.Null(result.Name);

            result = await Db.Query<Country>()
                .Select(i => i.Name)
                .GetSingleAsync();

            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
            Assert.Equal(0, result.Id);
        }
    }
}
