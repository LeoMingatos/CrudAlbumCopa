using AlbumCopa2026.Views;

namespace AlbumCopa2026
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new pgCadFigurinhaView());
        }

        private void btnLista_Tapped(object sender, TappedEventArgs e)
        {
            Navigation.PushAsync(new pgListaFigurinhasView());
        }
    }
}