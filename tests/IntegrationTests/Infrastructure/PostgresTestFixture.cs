using ApiIntegratedWithDockerMigrations;
using ApiIntegratedWithDockerMigrations.Migrations;
using Npgsql;
using Respawn;

namespace IntegrationTests.Infrastructure;

public class PostgresTestFixture : IDisposable
{
    private const string ConnectionString =
        "Server=localhost;Database=postgres;Port=5434;User Id=admin;Password=thisIsMyPass12345;Timeout=300;CommandTimeout=300;KeepAlive=300;Include Error Detail=true";

    private const string ImageName = "citusdata/citus";
    private const string DatabaseContainerName = "docker-postgres-tests";

    public PostgresTestFixture()
    {
        DockerManager.PullImageIfDoesNotExists(ImageName);
        DockerManager.KillContainer(DatabaseContainerName);
        DockerManager.KillVolume(DatabaseContainerName);
        DockerManager.RunContainerIfIsNotRunning(DatabaseContainerName,
            $"run --name {DatabaseContainerName} -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=thisIsMyPass12345 -p 5434:5432 -v {DatabaseContainerName}:/var/lib/postgresql/data -d {ImageName}");

        Thread.Sleep(5000);

        new MigrationService(ConnectionString).Up(typeof(CreateCitusExtension).Assembly, false);
    }

    public async Task ResetDatabase(Checkpoint checkpoint)
    {
        await using var conn = new NpgsqlConnection(ConnectionString);
        await conn.OpenAsync();
        await checkpoint.Reset(conn);
    }

    public void Dispose()
    {
        DockerManager.KillContainer(DatabaseContainerName);
        DockerManager.KillVolume(DatabaseContainerName);
    }
}
