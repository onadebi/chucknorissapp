namespace chuck_swapi.ApplicationLib.Config
{
    public class AppSettings
    {
        public string BaseNoris { get; set; } = default!;
        public string NorisSearch { get; set; } = default!;
        public string NorisJokeCategories { get; set; } = default!;
        public string ChuckRandomResource { get; set; }

        public string BaseSwapi { get; set; } = default!;

        public string SwapiPeopleResource { get; set; } = default!;
        public string SwapiSearchResource { get; set; } = default!;


        public string ChuckMetaData { get; set; }
        public string SwapiMetaData { get; set; }
    }
}
