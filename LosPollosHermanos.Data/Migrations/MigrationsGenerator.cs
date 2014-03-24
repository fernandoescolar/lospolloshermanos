using System.Data.Entity.Migrations.Model;
using System.Data.Entity.SqlServer;
using System.Linq;

namespace LosPollosHermanos.Data.Migrations
{
    public class MigrationsGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(CreateTableOperation createTableOperation)
        {
            if (createTableOperation.Name.Contains("__MigrationHistory") && !createTableOperation.Name.StartsWith("dbo."))
            {
                var newCreateTableOperation = new CreateTableOperation("dbo." + createTableOperation.Name);
                createTableOperation.Columns.ToList().ForEach(newCreateTableOperation.Columns.Add);
                base.Generate(newCreateTableOperation);
            }
            else
                base.Generate(createTableOperation);
        }
    }
}
