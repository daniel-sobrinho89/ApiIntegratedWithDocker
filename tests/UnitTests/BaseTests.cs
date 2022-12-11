using Autofac.Extras.FakeItEasy;
using Bogus;

namespace UnitTests;

public class BaseTests
{
    protected Faker Faker { get; private set; }
    protected AutoFake AutoFake;

    [SetUp]
    public void BaseSetUp()
    {
        Faker = new Faker("pt_BR");
        AutoFake = new();
    }
}
