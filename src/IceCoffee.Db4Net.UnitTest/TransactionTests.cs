using AutoFixture;
using IceCoffee.Db4Net.Extensions;
using IceCoffee.Db4Net.UnitTest.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace IceCoffee.Db4Net.UnitTest
{
    public class TransactionTests : TestFixtureBase
    {
        [Fact]
        public void Sync_Transaction_ShouldCommitFailure()
        {
            var fixture = new Fixture();
            var entity = fixture.Create<Country>();

            using (var dbConnection = Db.CreateDbConnection())
            {
                dbConnection.Open();
                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        Db.Insert(entity).Execute(transaction);
                        Db.Insert(entity).Execute(transaction);

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
            
            var insertedEntity = Db.Query<Country>(entity.Id).GetSingleOrDefault();

            Assert.Null(insertedEntity);
        }

#if NETCOREAPP
        [Fact]
        public async Task Async_Transaction_ShouldCommitFailure()
        {
            var fixture = new Fixture();
            var entity = fixture.Create<Country>();

            using (var dbConnection = Db.CreateDbConnection())
            {
                dbConnection.Open();
                using (var transaction = await dbConnection.BeginTransactionAsync())
                {
                    try
                    {
                        await Db.Insert(entity).ExecuteAsync(transaction);
                        await Db.Insert(entity).ExecuteAsync(transaction);

                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                    }
                }
            }

            var insertedEntity = await Db.Query<Country>(entity.Id).GetSingleOrDefaultAsync();

            Assert.Null(insertedEntity);
        }
#endif

    }
}
