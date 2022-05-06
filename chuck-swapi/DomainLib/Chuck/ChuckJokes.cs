namespace chuck_swapi.DomainLib.Chuck
{
    public class ChuckJokes
    {
        public List<string> Categories { get; set; }
        public string Created_at { get; set; }
        public string Icon_url { get; set; }
        public string Id { get; set; }
        public string Updated_at { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }
    }
    public class ChuckJokesList
    {
        public int Total { get; set; }
        public List<ChuckJokes> Result { get; set; }
    }
}
