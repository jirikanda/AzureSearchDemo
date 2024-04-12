using static AzureSearchDemo.ClientProvider;

namespace AzureSearchDemo;

public class FacetsDataDemo
{
    public static async Task RunAsync(CancellationToken cancellationToken = default)
    {
        SearchClient client = GetSearchClient("index01");

        var searchOptions = new SearchOptions
        {
            SearchMode = Azure.Search.Documents.Models.SearchMode.All,
            SearchFields = { nameof(ProductIndexData.Name) },
            Filter = "not InStock and RetailPriceIncludingVat lt 1000",
            Facets = { "Tags", "RetailPriceIncludingVat,interval:100" },
            Size = 0,
            IncludeTotalCount = true
        };

        var searchResponse = await client.SearchAsync<ProductIndexData>("", searchOptions, cancellationToken);
        var searchResults = searchResponse.Value;        

        Console.WriteLine("Total count: " + searchResults.TotalCount);

        var tagsFacet = searchResults.Facets["Tags"];
        foreach (var item in tagsFacet.OrderBy(item => item.Value))
        {
            Console.WriteLine($"Tag {item.Value} found {item.Count}x.");
        }

        var priceFacet = searchResults.Facets["RetailPriceIncludingVat"];
        foreach (var item in priceFacet.OrderBy(item => item.From))
        {
            Console.WriteLine($"Price interval starting {item.Value} found {item.Count}x.");
        }
    }
}