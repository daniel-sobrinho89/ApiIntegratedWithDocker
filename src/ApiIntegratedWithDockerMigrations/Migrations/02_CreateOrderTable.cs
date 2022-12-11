using FluentMigrator;

namespace ApiIntegratedWithDockerMigrations.Migrations;

[Migration(2)]
public class CreateOrderTable : Migration
{
    const string tableName = "order";
    public override void Up()
    {
        Create.Table(tableName)
            .WithColumn("id")
                .AsGuid()
                .PrimaryKey()
            .WithColumn("description")
                .AsString(20)
                .NotNullable()
            .WithColumn("created_at")
                .AsDateTime()
                .NotNullable()
            .WithColumn("amount")
                .AsDecimal(19, 6)
                .NotNullable();
    }

    public override void Down() => Delete.Table(tableName);
}