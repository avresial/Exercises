namespace Confab.Modules.Conferences.Api;

internal class ConferencesModule //: IModule
{
    public const string BasePath = "conferences-module";
    public string Name { get; } = "Conferences";
    public string Path => BasePath;

    public IEnumerable<string> Policies { get; } = ["conferences", "hosts"];

    //public void Register(IServiceCollection services)
    //{
    //    services.AddCore();
    //}

    //public void Use(IApplicationBuilder app)
    //{
    //}
}