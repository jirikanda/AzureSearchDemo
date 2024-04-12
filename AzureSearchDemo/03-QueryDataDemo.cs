using static AzureSearchDemo.ClientProvider;

namespace AzureSearchDemo;

public class QueryDataDemo
{
    public static async Task RunAsync(CancellationToken cancellationToken = default)
    {
        // 1
        await RunAsync("jídelní");
        await RunAsync("jídelní stůl");
        await RunAsync("JÍDELNÍ STŮL"); // kapitálky

        await RunAsync("jidelni"); // bez diakritiky

        // 2
        //await RunAsync("jídelní s");
        //await RunAsync("jídelní st");
        //await RunAsync("jídelní stů");
        //await RunAsync("jídelní stůl");

        //await RunAsync("jídelní s*");
        //await RunAsync("jídelní st*");
        //await RunAsync("jídelní stů*");
        //await RunAsync("jídelní stůl*");

        // 3
        //await RunAsync("jíde* stů*");
        //await RunAsync("jíde stů");

        // 4
        //await RunAsync("jedlá soda");

    }

    public static async Task RunAsync(string searchQuery, CancellationToken cancellationToken = default)
    {
        SearchClient client = GetSearchClient("index01");
        var searchOptions = new SearchOptions
        {
            SearchMode = Azure.Search.Documents.Models.SearchMode.All,
            SearchFields = { nameof(ProductIndexData.Name) },
            Select = { nameof(ProductIndexData.Code) },
            Filter = "EnabledForWeb"
        };
        var searchResponse = await client.SearchAsync<ProductIndexData>(searchQuery, searchOptions, cancellationToken);
        var searchResults = searchResponse.Value.GetResults();

        Console.WriteLine(searchQuery + ": " + String.Join(", ", searchResults.Select(searchResult => searchResult.Document.Code)));
    }
}