using SQLite;
using PCLExt.FileStorage.Folders;

namespace AlbumCopa2026.Services
{
    public class DatabaseService
    {
        public SQLiteConnection GetConnection()
        {
            var pasta = new LocalRootFolder();

            var arquivo = pasta.CreateFile("albumcopa2026",
                PCLExt.FileStorage.CreationCollisionOption.OpenIfExists);

            return new SQLiteConnection(arquivo.Path);
        }
    }
}
