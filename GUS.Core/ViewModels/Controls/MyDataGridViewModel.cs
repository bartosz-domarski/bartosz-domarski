using Newtonsoft.Json;

namespace GUS.Core
{
    public class MyDataGridViewModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("nazwa")]
        public string Name { get; set; } = default!;

        [JsonProperty("id-nadrzedny-element")]
        public int PrecedentElementId { get; set; }

        [JsonProperty("id-poziom")]
        public int LevelId { get; set; }

        [JsonProperty("nazwa-poziom")]
        public string LevelName { get; set; } = default!;

        [JsonProperty("czy-zmienne")]
        public bool isChangable { get; set; }
    }
}
