namespace AzureSearchDemo;

public static class ClientProvider
{
    private static Uri s_searchEndpoint = new Uri("https://globalazuredemosearch.search.windows.net");
    private static string s_manageAdminKey = "4zgWANJGIT7CdyRg2bU5qZwujMsPPsyRLnbwJ7WHNCAzSeAgzpTk";
    //private static string s_manageQueryKey = "...";

    public static SearchClient GetSearchClient(string indexName)
    {
        return new SearchClient(s_searchEndpoint, indexName, new AzureKeyCredential(s_manageAdminKey));
        // RBAC: return new SearchClient(s_SearchEndpoint, new DefaultAzureCredential());
    }

    public static SearchIndexClient GetSearchIndexClient()
    {
        return new SearchIndexClient(s_searchEndpoint, new AzureKeyCredential(s_manageAdminKey));
        // RBAC: return new SearchIndexClient(s_SearchEndpoint, new DefaultAzureCredential());
    }
}
