namespace AzureSearchDemo;

public class ProductIndexData
{
    public string ExternalId { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public List<int> Tags { get; set; }
    public bool EnabledForWeb { get; set; }
    public decimal? RetailPriceIncludingVat { get; set; }
    public bool InStock { get; set; }
    public DateTime? ExpectedStockDate { get; set; }
}