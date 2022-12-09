using BlogWebsite.Common.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;
using NSubstitute;

namespace BlogWebsite.UnitTests.Helpers;

public static class TestHelper
{

    public static IConfiguration GetConfig()
    {
        //TODO: Figure out a better way to mock IConfiguration
        var inMemorySettings = new Dictionary<string, string> {
            {"ApiKey", "Key"},
            {"Authentication:Audience", "bmwadforth.com"},
            {"Authentication:Issuer", "bmwadforth.com"},
            {"Authentication:Key", "key"},
            {"ConnectionStrings.Database", "User ID=admin;Password=password;Host=127.0.0.1;Port=5432;Database=bmwadforth;"},
            {"ConnectionStrings.ContentDeliveryNetwork", "https://cdn.bmwadforth.com/development"},
            {"Blob:Folder", "test"},
            {"Blob:Bucket", "bmwadforth"},
            {"AllowedHosts", "*"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        return configuration;
    }
}