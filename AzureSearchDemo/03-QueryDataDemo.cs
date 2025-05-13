using static AzureSearchDemo.ClientProvider;

namespace AzureSearchDemo;

public class QueryDataDemo
{
    public static async Task RunAsync(CancellationToken cancellationToken = default)
    {
        // 1
        await QueryAsync("jídelní", cancellationToken);
        await QueryAsync("jídelní stůl", cancellationToken);
        await QueryAsync("JÍDELNÍ STŮL", cancellationToken); // kapitálky

        await QueryAsync("jídelnímu stolu", cancellationToken); // další tvary
        await QueryAsync("jídelních stolů", cancellationToken); // další tvary

        await QueryAsync("jidelni", cancellationToken); // bez diakritiky

        // 2
        //await QueryAsync("s", cancellationToken);
        //await QueryAsync("st", cancellationToken);
        //await QueryAsync("stů", cancellationToken);
        //await QueryAsync("stůl", cancellationToken);

        //await QueryAsync("s*", cancellationToken);
        //await QueryAsync("st*", cancellationToken);
        //await QueryAsync("stů*", cancellationToken);
        //await QueryAsync("stůl*", cancellationToken);

        // 3
        //await QueryAsync("jíde* stů*", cancellationToken);
        //await QueryAsync("jíde stů", cancellationToken);

        // 4
        //await QueryAsync("jedlá soda", cancellationToken);

    }

    public static async Task QueryAsync(string searchQuery, CancellationToken cancellationToken = default)
    {
        SearchClient client = GetSearchClient("index01");
        var searchOptions = new SearchOptions
        {
            SearchMode = Azure.Search.Documents.Models.SearchMode.All,
            SearchFields = { nameof(ProductIndexData.Name) },
            Select = { nameof(ProductIndexData.Code) },
            Filter = "EnabledForWeb and InStock"
        };
        var searchResponse = await client.SearchAsync<ProductIndexData>(searchQuery, searchOptions, cancellationToken);
        var searchResults = searchResponse.Value.GetResults();

        Console.WriteLine(searchQuery + ": " + String.Join(", ", searchResults.Select(searchResult => searchResult.Document.Code)));
    }
}