using SQLite;
using AlbumCopa2026.Models;
using AlbumCopa2026.Services;

namespace AlbumCopa2026.Controllers
{
    public class FigurinhaController
    {
        DatabaseService _database;
        SQLiteConnection _connection;

        public FigurinhaController()
        {
            _database = new DatabaseService();
            _connection = _database.GetConnection();
            _connection.CreateTable<Figurinha>();
        }

        public bool Insert(Figurinha value)
        {
            return _connection.Insert(value) > 0;
        }

        public bool Update(Figurinha value)
        {
            return _connection.Update(value) > 0;
        }

        public bool Delete(Figurinha value)
        {
            return _connection.Delete(value) > 0;
        }

        public Figurinha GetById(int id)
        {
            return _connection.Find<Figurinha>(id);
        }

        public List<Figurinha> GetAll()
        {
            return _connection.Table<Figurinha>().ToList();
        }

        // Busca combinada: por nome, obtido e/ou desejado
        public List<Figurinha> GetFiltrado(string nome, bool? obtido, bool? desejado)
        {
            var query = _connection.Table<Figurinha>();

            if (!string.IsNullOrEmpty(nome))
                query = query.Where(x => x.NomeJogador.Contains(nome));

            if (obtido.HasValue)
                query = query.Where(x => x.Obtido == obtido.Value);

            if (desejado.HasValue)
                query = query.Where(x => x.Desejado == desejado.Value);

            return query.ToList();
        }

        public bool ToggleObtido(Figurinha value)
        {
            value.Obtido = !value.Obtido;
            return Update(value);
        }

        public bool ToggleDesejado(Figurinha value)
        {
            value.Desejado = !value.Desejado;
            return Update(value);
        }
    }
}
