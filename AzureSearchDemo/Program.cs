namespace AzureSearchDemo
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {            
            await CreateIndexDemo.RunAsync();
            await PopulateDataDemo.RunAsync();
            await QueryDataDemo.RunAsync();
           // await AnalyzeTextDataDemo.RunAsync();
        }
    }
}
