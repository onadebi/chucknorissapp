namespace chuck_swapi.DomainLib.Search
{
    public class SearchResult
    {
        public string Content { get; set; }
        public string Metadata { get; set; }
    }

    public class SearchResultsList
    {
        public List<SearchResult> Results { get; set; }
    }
}
