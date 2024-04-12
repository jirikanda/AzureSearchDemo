using static AzureSearchDemo.ClientProvider;

namespace AzureSearchDemo;

public class PopulateDataDemo
{
    public static async Task RunAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<ProductIndexData> products =
        [
            new ProductIndexData { ExternalId = "1", Code="JIDELNIZIDLE", Name = "Jídelní židle, potah žlutá látka, kovová čtyřnohá podnož, černý mat", EnabledForWeb = true, InStock = true, RetailPriceIncludingVat = 1000, Tags = [1, 3] },
            new ProductIndexData { ExternalId = "2", Code="JIDELNISTUL", Name = "Jídelní stůl 120x75 cm, barva buk", EnabledForWeb = true, InStock = true, RetailPriceIncludingVat = 1000, Tags = [1] },
            new ProductIndexData { ExternalId = "3", Code="ANDELKERAMICKY", Name = "Anděl keramický, šedivá barva.", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 5555, Tags = [2, 4] }
        ];

        SearchClient client = GetSearchClient("index01");
        await client.UploadDocumentsAsync(products, new IndexDocumentsOptions { ThrowOnAnyError = true }, cancellationToken);

        await Task.Delay(1000); // chvilku počkáme na zaindexování dat
    }
}