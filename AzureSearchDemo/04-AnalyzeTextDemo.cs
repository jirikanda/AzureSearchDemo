using static AzureSearchDemo.ClientProvider;

namespace AzureSearchDemo;

public class AnalyzeTextDataDemo
{
    public static async Task RunAsync(CancellationToken cancellationToken = default)
    {
        await AnalyzeText("jídelní stůl", cancellationToken);
        await AnalyzeText("jídelnímu stolu", cancellationToken);
        await AnalyzeText("jídelních stolů", cancellationToken);
    }

    public static async Task AnalyzeText(string text, CancellationToken cancellationToken = default)
    {
        SearchIndexClient client = GetSearchIndexClient();
        var analyzeTextResponse = await client.AnalyzeTextAsync("index01", new AnalyzeTextOptions(text, (LexicalAnalyzerName)"cs.lucene"), cancellationToken);
        var analyzedTokenInfos = analyzeTextResponse.Value;

        Console.WriteLine(text + ": " + String.Join(", ", analyzedTokenInfos.Select(ati => ati.Token)));
    }
}