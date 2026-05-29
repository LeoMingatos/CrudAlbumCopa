using AlbumCopa2026.Controllers;
using AlbumCopa2026.Views;

namespace AlbumCopa2026
{
    public partial class MainPage : ContentPage
    {
        FigurinhaController _controller = new FigurinhaController();

        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AtualizarEstatisticas();
        }

        void AtualizarEstatisticas()
        {
            var todas = _controller.GetAll();
            lblTotalFigurinhas.Text = todas.Count.ToString();
            lblObtidas.Text = todas.Count(f => f.Obtido).ToString();
            lblDesejadas.Text = todas.Count(f => f.Desejado).ToString();
        }

        private void btnCadastrar_Tapped(object sender, TappedEventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new pgCadFigurinhaView());
        }

        private void btnLista_Tapped(object sender, TappedEventArgs e)
        {
            Application.Current.MainPage.Navigation.PushAsync(new pgListaFigurinhasView());
        }
    }
}
