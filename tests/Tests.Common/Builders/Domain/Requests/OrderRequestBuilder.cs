using ApiIntegratedWithDocker.Domain.Requests;
using Bogus;

namespace Tests.Common.Builders.Domain.Requests;

public class OrderRequestBuilder : Faker<OrderRequest>
{
    public OrderRequestBuilder()
    {
        RuleFor(x => x.Description, faker => faker.Random.String2(1, 20));
        RuleFor(x => x.Amount, faker => faker.Finance.Amount(min: 0.01m));
    }
}
