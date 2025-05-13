using static AzureSearchDemo.ClientProvider;

namespace AzureSearchDemo;

public class CreateIndexDemo
{
    public static async Task RunAsync(CancellationToken cancellationToken = default)
    {
        var searchIndex = new SearchIndex("index01");
        searchIndex.Fields.Add(new SimpleField(nameof(ProductIndexData.ExternalId), SearchFieldDataType.String) { IsKey = true, IsHidden = false, IsFilterable = true }); // musí být string
        searchIndex.Fields.Add(new SearchField(nameof(ProductIndexData.Name), SearchFieldDataType.String) { IsHidden = true, AnalyzerName = "cs.lucene" });
        searchIndex.Fields.Add(new SearchField(nameof(ProductIndexData.Code), SearchFieldDataType.String) { IsHidden = false, AnalyzerName = "cs.lucene" });
        searchIndex.Fields.Add(new SimpleField(nameof(ProductIndexData.Tags), SearchFieldDataType.Collection(SearchFieldDataType.Int32)) { IsFilterable = true, IsHidden = true, IsFacetable = true });
        searchIndex.Fields.Add(new SimpleField(nameof(ProductIndexData.EnabledForWeb), SearchFieldDataType.Boolean) { IsHidden = true, IsFilterable = true });
        searchIndex.Fields.Add(new SimpleField(nameof(ProductIndexData.RetailPriceIncludingVat), SearchFieldDataType.Double) { IsHidden = true, IsFilterable = true, IsSortable = true, IsFacetable = true });
        searchIndex.Fields.Add(new SimpleField(nameof(ProductIndexData.InStock), SearchFieldDataType.Boolean) { IsHidden = true, IsFilterable = true, IsSortable = true });
        searchIndex.Fields.Add(new SimpleField(nameof(ProductIndexData.ExpectedStockDate), SearchFieldDataType.DateTimeOffset) { IsHidden = true, IsFilterable = true });

        #region MyDemoAnalyzers

        #region MyDemoAnalyzer1 (Lowercase, Ascifolding)
        searchIndex.Analyzers.Add(new CustomAnalyzer(name: "MyDemoAnalyzer1" , tokenizerName: LexicalTokenizerName.Whitespace)
        {
            TokenFilters =
            {
                TokenFilterName.Lowercase,
				TokenFilterName.AsciiFolding,
			}
        });
        #endregion

        #region MyDemoAnalyzer2 (Lowercase, Ascifolding, MyDemoNGramTokenFilter)
        searchIndex.TokenFilters.Add(new EdgeNGramTokenFilter("MyDemoNGramTokenFilter") { MinGram = 1, MaxGram = 300, Side = EdgeNGramTokenFilterSide.Front });
        searchIndex.Analyzers.Add(new CustomAnalyzer(name: "MyDemoAnalyzer2", tokenizerName: LexicalTokenizerName.Whitespace)
        {
            TokenFilters =
            {
                TokenFilterName.Lowercase, // převedeme tokeny na malá písmena (potřebujeme zajistit, aby hledání STUL, stul, Stul, ... bylo rovnocenné)
				TokenFilterName.AsciiFolding, // z tokenů odstraníme diakritiku (potřebujeme zajistit, aby hledání "židle" a "zidle" bylo rovnocenné)
				new TokenFilterName("MyDemoNGramTokenFilter"), // z každého tokenu uděláme ngramy (židle -> z, zi, zid, zidl, zidle)
                TokenFilterName.Unique  // odstraníme duplicity
			}
        });
        #endregion

        #region MyDemoAnalyzer3 (Lowercase, Ascifolding, Unique)
        searchIndex.Analyzers.Add(new CustomAnalyzer(name: "MyDemoAnalyzer3", tokenizerName: LexicalTokenizerName.Whitespace)
        {
            TokenFilters =
            {
                TokenFilterName.Lowercase, // převedeme tokeny na malá písmena (potřebujeme zajistit, aby hledání STUL, stul, Stul, ... bylo rovnocenné)
				TokenFilterName.AsciiFolding, // z tokenů odstraníme diakritiku (potřebujeme zajistit, aby hledání "židle" a "zidle" bylo rovnocenné)
				TokenFilterName.Unique // odstraníme duplicity
			}
        });
        #endregion

        #endregion

        SearchIndexClient searchIndexClient = GetSearchIndexClient();
        try
        {
            await searchIndexClient.CreateOrUpdateIndexAsync(searchIndex, allowIndexDowntime: true, cancellationToken: cancellationToken);
        }
        catch (RequestFailedException rfe) when (rfe.Status == 400)
        {
            await searchIndexClient.DeleteIndexAsync(searchIndex.Name, cancellationToken);
            await searchIndexClient.CreateOrUpdateIndexAsync(searchIndex, allowIndexDowntime: true, cancellationToken: cancellationToken);
        }
    }
}
