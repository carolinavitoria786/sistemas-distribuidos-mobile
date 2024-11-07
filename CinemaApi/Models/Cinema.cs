using System.Text.Json.Serialization;

namespace CinemaApi.Models {

    public class Cinema {

        public int cinemaId { get; set; }

        public string? nome { get; set; }

        public string? cnpj { get; set; }
        [JsonIgnore]
    public Filme Filme { get; set; } = null!;
    }
}