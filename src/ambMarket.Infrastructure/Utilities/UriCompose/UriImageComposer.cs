using ambMarket.Application.Interfaces.UriComposer;
using ambMarket.Infrastructure.Settings;

namespace ambMarket.Infrastructure.Utilities.UriCompose;

public class UriImageComposer : IUriImageComposer
{
    private ApplicationSettings ApplicationSettings { get; }
    public UriImageComposer(ApplicationSettings applicationSettings)
    {
        ApplicationSettings = applicationSettings;
    }


    public string UriComposer(string src)
    {
        var concatStr = Path.Combine(ApplicationSettings.CatalogImageUriSrc, src);
        return concatStr.Replace(@"\", @"/");
    }
}