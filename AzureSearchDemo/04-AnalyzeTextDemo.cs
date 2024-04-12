using static AzureSearchDemo.ClientProvider;

namespace AzureSearchDemo;

public class AnalyzeTextDataDemo
{
    public static async Task RunAsync(CancellationToken cancellationToken = default)
    {
        await RunAsync("jídelní stů");
        await RunAsync("jídelní stůl");
    }

    public static async Task RunAsync(string searchQuery, CancellationToken cancellationToken = default)
    {
        SearchIndexClient client = GetSearchIndexClient();
        var analyzeTextResponse = await client.AnalyzeTextAsync("index01", new AnalyzeTextOptions(searchQuery, (LexicalAnalyzerName)"MyDemoAnalyzer1"), cancellationToken);
        var analyzedTokenInfos = analyzeTextResponse.Value;

        Console.WriteLine(searchQuery + ": " + String.Join(", ", analyzedTokenInfos.Select(ati => ati.Token)));
    }
}