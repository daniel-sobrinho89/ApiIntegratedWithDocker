using FluentMigrator;

namespace ApiIntegratedWithDockerMigrations.Migrations;

[Migration(1)]
public class CreateCitusExtension : Migration
{
    public override void Up() => Execute.Sql(@$"CREATE EXTENSION Citus;");

    public override void Down() => Execute.Sql("DROP EXTENSION Citus");
}