using Bogus;
using Tests.Common.Infrastructure;
using OrderModel = ApiIntegratedWithDocker.Domain.Models.Order;

namespace Tests.Common.Builders.Domain.Models;

public class OrderBuilder : Faker<OrderModel>
{
    public OrderBuilder()
    {
        RuleFor(x => x.Id, faker => faker.Random.Guid());
        RuleFor(x => x.Description, faker => faker.Random.String2(1, 20));
        RuleFor(x => x.Amount, faker => faker.Finance.Amount(min: 0.01m));
        RuleFor(x => x.CreatedAt, faker => faker.Date.Recent(1, Clock.UtcNow));
    }
}
