using SQLite;

namespace AlbumCopa2026.Models
{
    public class Figurinha
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string NomeJogador { get; set; }

        public string Selecao { get; set; }

        // "Comum" ou "Especial"
        public string TipoFigurinha { get; set; }

        // true = figurinha obtida (na coleção)
        public bool Obtido { get; set; }

        // true = figurinha desejada (na lista de desejos)
        public bool Desejado { get; set; }

        // Caminho local da imagem
        public string DirImagem { get; set; }

        // Ícone de obtido
        public string ObtidoIcon
        {
            get
            {
                return Obtido ? "✅" : "⬜";
            }
        }

        // Ícone de desejado
        public string DesejadoIcon
        {
            get
            {
                return Desejado ? "❤️" : "🤍";
            }
        }
    }
}
