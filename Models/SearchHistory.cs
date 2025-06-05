namespace CatGifApi.Models
{
    public class SearchHistory
    {
        public int Id { get; set; }                            // Clave primaria
        public DateTime Date { get; set; } = DateTime.Now;    // Fecha de la búsqueda
        public string CatFact { get; set; } = "";              // Hecho del gato
        public string QueryWords { get; set; } = "";           // Palabras de búsqueda ingresadas por el usuario
        public string GifUrl { get; set; } = "";               // URL del GIF de Giphy
    }
}
