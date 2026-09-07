using Microsoft.Extensions.DependencyInjection;
using WellnessMassageCenter.Backend.Services.Tests.Infrastructure.Fixtures;

namespace WellnessMassageCenter.Backend.Services.Tests.Infrastructure;

public abstract class BaseServiceTest : IClassFixture<ServicesTestFixture>
{
    protected readonly ServicesTestFixture Fixture;
    protected readonly IServiceScope Scope;

    protected BaseServiceTest(ServicesTestFixture fixture)
    {
        Fixture = fixture;
        Scope = Fixture.ServiceProvider.CreateScope();
    }

    protected T GetService<T>() where T : notnull
    {
        return Scope.ServiceProvider.GetRequiredService<T>();
    }

    public void Dispose()
    {
        Scope.Dispose();
    }
}
