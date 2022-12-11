using ApiIntegratedWithDocker.Infrastructure;
using IntegrationTests.APITests;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Respawn.Graph;

namespace IntegrationTests;

public class IntegrationTestsBase
{
    public IServiceProvider ServiceProvider;
    protected TestApplication TestApi;
    private IServiceScope _databaseScope;
    public DbContextEf ContextForAsserts;
    private readonly Checkpoint _checkpoint = new()
    {
        TablesToIgnore = new Table[] { "VersionInfo", "transaction_type", "authorization_status", "idempotency_status" },
        SchemasToInclude = new[] { "public" },
        DbAdapter = DbAdapter.Postgres
    };

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        TestApi = new TestApplication();
        ServiceProvider = TestApi.Services;
    }

    [SetUp]
    public async Task SetUpBase()
    {
        _databaseScope = ServiceProvider.CreateScope();
        ContextForAsserts = _databaseScope.ServiceProvider.GetService<DbContextEf>()!;
        await DatabaseTestFixture.PostgresTestFixture.ResetDatabase(_checkpoint);
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        ContextForAsserts?.Dispose();
        TestApi?.Dispose();
        _databaseScope?.Dispose();
    }
}
