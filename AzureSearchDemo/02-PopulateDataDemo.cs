using static AzureSearchDemo.ClientProvider;

namespace AzureSearchDemo;

public class PopulateDataDemo
{
    public static async Task RunAsync(CancellationToken cancellationToken = default)
    {
        IEnumerable<ProductIndexData> products =
        [
            new ProductIndexData { ExternalId = "1", Code="JIDELNIZIDLE", Name = "Jídelní židle, potah žlutá látka, kovová čtyřnohá podnož, černý mat", EnabledForWeb = true, InStock = true, RetailPriceIncludingVat = 10, Tags = [1, 3] },
            new ProductIndexData { ExternalId = "2", Code="JIDELNISTUL", Name = "Jídelní stůl 120x75 cm, barva buk", EnabledForWeb = true, InStock = true, RetailPriceIncludingVat = 11, Tags = [1] },
            new ProductIndexData { ExternalId = "3", Code="ANDELKERAMICKY", Name = "Anděl keramický, šedivá barva.", EnabledForWeb = true, InStock = true, RetailPriceIncludingVat = 12, Tags = [2, 4] },
            new ProductIndexData { ExternalId = "4", Code="PRODUCT4", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 30, Tags = [] },
            new ProductIndexData { ExternalId = "5", Code="PRODUCT5", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 75, Tags = [1] },
            new ProductIndexData { ExternalId = "6", Code="PRODUCT6", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 111, Tags = [1, 3] },
            new ProductIndexData { ExternalId = "7", Code="PRODUCT7", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 240, Tags = [1] },
            new ProductIndexData { ExternalId = "8", Code="PRODUCT8", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 241, Tags = [2, 4] },
            new ProductIndexData { ExternalId = "9", Code="PRODUCT9", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 255, Tags = [2, 4] },
            new ProductIndexData { ExternalId = "10", Code="PRODUCT10", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 360, Tags = [3] },
            new ProductIndexData { ExternalId = "11", Code="PRODUCT11", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 360, Tags = [3, 4] },
            new ProductIndexData { ExternalId = "12", Code="PRODUCT12", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 360, Tags = [1] },
            new ProductIndexData { ExternalId = "13", Code="PRODUCT13", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 360, Tags = [1, 4] },
            new ProductIndexData { ExternalId = "14", Code="PRODUCT14", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 455, Tags = [2, 4] },
            new ProductIndexData { ExternalId = "15", Code="PRODUCT15", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 460, Tags = [3, 4] },
            new ProductIndexData { ExternalId = "16", Code="PRODUCT16", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 800, Tags = [4] },
            new ProductIndexData { ExternalId = "17", Code="PRODUCT17", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 1000, Tags = [5, 6] },
            new ProductIndexData { ExternalId = "18", Code="PRODUCT18", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 1000, Tags = [5, 6] },
            new ProductIndexData { ExternalId = "19", Code="PRODUCT19", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 1000, Tags = [5, 6] },
            new ProductIndexData { ExternalId = "20", Code="PRODUCT20", Name = "", EnabledForWeb = true, InStock = false, RetailPriceIncludingVat = 5555, Tags = [5, 6] },
        ];

        SearchClient client = GetSearchClient("index01");
        await client.UploadDocumentsAsync(products, new IndexDocumentsOptions { ThrowOnAnyError = true }, cancellationToken);

        await Task.Delay(1000, cancellationToken); // chvilku počkáme na zaindexování dat
    }
}