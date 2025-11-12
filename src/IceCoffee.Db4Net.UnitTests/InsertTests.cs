using AutoFixture;
using IceCoffee.Db4Net.Extensions;
using IceCoffee.Db4Net.UnitTests.Entities;

namespace IceCoffee.Db4Net.UnitTests
{
    public class InsertTests : TestFixtureBase
    {
        [Fact]
        public async Task Insert_SingleEntity_ShouldSucceed()
        {
            var fixture = new Fixture();
            var entity = fixture.Create<User>();

            var affectedRows = await Db.Insert(entity).ExecuteAsync();

            Assert.Equal(1, affectedRows);
        }

        [Fact]
        public async Task Insert_MultipleEntities_ShouldSucceed()
        {
            var fixture = new Fixture();
            var entities = fixture.CreateMany<User>(10);

            var affectedRows = await Db.InsertMany(entities).ExecuteAsync();

            Assert.Equal(10, affectedRows);
        }

        [Fact]
        public async Task InsertAndGetId_ShouldReturnGeneratedId()
        {
            var fixture = new Fixture();
            var entity = fixture.Create<User>();

            int id = await Db.InsertAndGetId(entity).ExecuteAsync<int>();

            Assert.True(id > 0, "Generated ID should be greater than 0.");

            var insertedEntity = await Db.Query<User>(id).GetSingleAsync();

            Assert.NotNull(insertedEntity);
            Assert.Equal(entity.Name, insertedEntity.Name);
            Assert.Equal(entity.Age, insertedEntity.Age);
        }

        [Fact]
        public async Task InsertOrIgnore_ShouldInsertIfNotExists()
        {
            var fixture = new Fixture();
            var entity = fixture.Create<Country>();

            // First insert should succeed
            int affectedRows = await Db.InsertOrIgnore(entity).ExecuteAsync();

            Assert.Equal(1, affectedRows);

            // Second insert should be ignored
            affectedRows = await Db.InsertOrIgnore(entity).ExecuteAsync();

            Assert.Equal(0, affectedRows);

            // Verify that the entity exists in the database
            var insertedEntity = await Db.Query<Country>(entity.Id).GetSingleAsync();

            Assert.NotNull(insertedEntity);
            Assert.Equal(entity.Id, insertedEntity.Id);
            Assert.Equal(entity.Name, insertedEntity.Name);
        }

        [Fact]
        public async Task InsertOrReplace_ShouldReplaceExistingEntity()
        {
            var fixture = new Fixture();
            var entity = fixture.Create<Country>();

            // First insert should succeed
            int affectedRows = await Db.InsertOrReplace(entity).ExecuteAsync();

            Assert.Equal(1, affectedRows);

            // Modify the entity and insert again
            entity.Name += " Updated";
            affectedRows = await Db.InsertOrReplace(entity).ExecuteAsync();

            Assert.Equal(1, affectedRows);

            // Verify that the entity was updated in the database
            var updatedEntity = await Db.Query<Country>(entity.Id).GetSingleAsync();

            Assert.NotNull(updatedEntity);
            Assert.Equal(entity.Id, updatedEntity.Id);
            Assert.Equal(entity.Name, updatedEntity.Name);
        }

        [Fact]
        public async Task InsertSpecificColumns_ShouldInsertOnlySpecifiedColumns()
        {
            var fixture = new Fixture();
            var entity = fixture.Create<Country>();

            // Insert only Name column
            int affectedRows = await Db.Insert<Country>()
                .Set(i => i.Id, entity.Id)
                .Set(i => i.Name, entity.Name)
                .ExecuteAsync();

            Assert.Equal(1, affectedRows);

            // Verify that the entity was inserted with only Name
            var insertedEntity = await Db.Query<Country>(entity.Id).GetSingleAsync();

            Assert.NotNull(insertedEntity);
            Assert.Equal(entity.Name, insertedEntity.Name);
            Assert.Equal(0, insertedEntity.Sort); // Sort should be 0 since it was not set
        }
    }
}
