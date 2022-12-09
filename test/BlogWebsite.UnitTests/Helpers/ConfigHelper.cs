using Microsoft.Extensions.Configuration;

namespace BlogWebsite.UnitTests.Helpers;

public static class ConfigHelper
{

    public static IConfiguration GetConfig()
    {
        string projectPath =
            Path.GetFullPath(
                $"{AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0]}../../src");
        
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(projectPath)
            .AddJsonFile("appsettings.json")
            .Build();

        return config;
    }
}