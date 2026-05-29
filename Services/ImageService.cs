namespace AlbumCopa2026.Services
{
    public static class ImageService
    {
        public static async Task<string> SelecionarImagem()
        {
            try
            {
                var resultado = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Selecionar foto da figurinha"
                });

                if (resultado != null)
                {
                    return resultado.FullPath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao selecionar imagem: {ex.Message}");
            }

            return string.Empty;
        }
    }
}
