using IntegrationTests.Infrastructure;

namespace IntegrationTests;

[SetUpFixture]
public class DatabaseTestFixture
{
    public static PostgresTestFixture PostgresTestFixture;

    [OneTimeSetUp]
    public void OneTimeSetUp()
        => PostgresTestFixture = new PostgresTestFixture();

    [OneTimeTearDown]
    public void OneTimeTearDown()
        => PostgresTestFixture?.Dispose();
}