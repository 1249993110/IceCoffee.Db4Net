using AutoFixture;
using Dapper;
using IceCoffee.Db4Net.Extensions;
using Microsoft.Data.Sqlite;
using System.Data;

namespace IceCoffee.Db4Net.UnitTest
{
    public abstract class TestFixtureBase : IDisposable
    {
        private const string _connectionString = "Data Source=InMemorySample;Mode=Memory;Cache=Shared";
        static TestFixtureBase()
        {
            Db.Register(Core.DatabaseProvider.SQLite, _connectionString);
        }

        private readonly IDbConnection _connection;
        protected TestFixtureBase()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open(); // Open the in-memory database connection, keep it alive for the lifetime of the test fixture

            InitializeTestData();
        }

        private void InitializeTestData()
        {
            _connection.Execute(@"
                CREATE TABLE user (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name TEXT,
                    age INTEGER NOT NULL,
                    created_at TEXT NOT NULL DEFAULT (DATETIME('now', 'localtime'))
                )");

            _connection.Execute(@"
                CREATE TABLE role (
                    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                    name TEXT,
                    created_at TEXT NOT NULL DEFAULT (DATETIME('now', 'localtime'))
                )");

            _connection.Execute(@"
                CREATE TABLE user_role (
                    user_id INTEGER NOT NULL,
                    role_id INTEGER NOT NULL,
                    created_at TEXT NOT NULL DEFAULT (DATETIME('now', 'localtime')),
                    PRIMARY KEY (user_id, role_id)
                )");

            _connection.Execute(@"
                CREATE TABLE country (
                    Code INTEGER NOT NULL PRIMARY KEY,
                    Name TEXT NOT NULL
                )");
        }

        protected async Task<TEntity> InsertTestData<TEntity>()
        {
            var fixture = new Fixture();
            var entity = fixture.Create<TEntity>();
            await Db.Insert(entity).ExecuteAsync();
            return entity;
        }
        protected async Task<IEnumerable<TEntity>> InsertTestData<TEntity>(int count)
        {
            var fixture = new Fixture();
            var entities = fixture.CreateMany<TEntity>(count);
            await Db.InsertMany(entities).ExecuteAsync();
            return entities;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
