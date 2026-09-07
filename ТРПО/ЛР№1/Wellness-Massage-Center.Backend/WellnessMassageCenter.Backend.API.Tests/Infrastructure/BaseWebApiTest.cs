using Microsoft.Extensions.DependencyInjection;
using WellnessMassageCenter.Backend.API.Tests.Infrastructure.Fixtures;

namespace WellnessMassageCenter.Backend.API.Tests.Infrastructure;

public abstract class BaseWebApiTest: IClassFixture<WebApiTestFixture>
{
    protected readonly HttpClient Client;
    protected readonly WebApiTestFixture Fixture;
    protected readonly IServiceScope Scope;
    protected readonly string ControllerUrl = string.Empty;

    protected BaseWebApiTest(WebApiTestFixture fixture)
    {
        Client = fixture.CreateClient();
        Client.BaseAddress = new("http://localhost:5000");
        Fixture = fixture;
        Scope = fixture.Services.CreateScope();
    }

    protected BaseWebApiTest(WebApiTestFixture fixture, string controllerUrl) : this(fixture)
    {
        ControllerUrl = controllerUrl;
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
