using AlbumCopa2026.Controllers;
using AlbumCopa2026.Models;

namespace AlbumCopa2026.Views
{
    public partial class pgListaFigurinhasView : ContentPage
    {
        FigurinhaController _controller;

        bool? _filtroObtido = null;
        bool? _filtroDesejado = null;

        public pgListaFigurinhasView()
        {
            InitializeComponent();
            _controller = new FigurinhaController();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AtualizarEstiloFiltros("todos");
            AtualizarLista();
        }

        void AtualizarLista()
        {
            string nome = txtFiltroNome?.Text ?? "";

            var lista = _controller.GetFiltrado(nome, _filtroObtido, _filtroDesejado);
            var linhas = MontarLinhasAlbum(lista);

            lsvFigurinhas.ItemsSource = linhas;

            lblContador.Text = $"{lista.Count} figurinha(s) encontrada(s)";

            bool temResultado = lista.Count > 0;

            lsvFigurinhas.IsVisible = temResultado;
            lblListaVazia.IsVisible = !temResultado;
        }

        List<LinhaAlbum> MontarLinhasAlbum(List<Figurinha> lista)
        {
            List<LinhaAlbum> linhas = new List<LinhaAlbum>();

            for (int i = 0; i < lista.Count; i += 2)
            {
                linhas.Add(new LinhaAlbum
                {
                    Item1 = lista[i],
                    Item2 = i + 1 < lista.Count ? lista[i + 1] : null
                });
            }

            return linhas;
        }

        private void OnFiltroAlterado(object sender, TextChangedEventArgs e)
        {
            AtualizarLista();
        }

        private void tapFiltroTodos_Tapped(object sender, TappedEventArgs e)
        {
            _filtroObtido = null;
            _filtroDesejado = null;

            AtualizarEstiloFiltros("todos");
            AtualizarLista();
        }

        private void tapFiltroObtidas_Tapped(object sender, TappedEventArgs e)
        {
            _filtroObtido = true;
            _filtroDesejado = null;

            AtualizarEstiloFiltros("obtidas");
            AtualizarLista();
        }

        private void tapFiltroDesejadas_Tapped(object sender, TappedEventArgs e)
        {
            _filtroObtido = null;
            _filtroDesejado = true;

            AtualizarEstiloFiltros("desejadas");
            AtualizarLista();
        }

        private void btnLimparFiltros_Clicked(object sender, EventArgs e)
        {
            txtFiltroNome.Text = "";
            _filtroObtido = null;
            _filtroDesejado = null;

            AtualizarEstiloFiltros("todos");
            AtualizarLista();
        }

        void AtualizarEstiloFiltros(string ativo)
        {
            Color corAtivo = Color.FromArgb("#2E7D52");
            Color corInativo = Color.FromArgb("#10271F");

            Color bordaAtiva = Color.FromArgb("#FFD700");
            Color bordaInativa = Color.FromArgb("#2E7D52");

            frmFiltroTodos.BackgroundColor = ativo == "todos" ? corAtivo : corInativo;
            frmFiltroTodos.BorderColor = ativo == "todos" ? bordaAtiva : bordaInativa;

            frmFiltroObtidas.BackgroundColor = ativo == "obtidas" ? corAtivo : corInativo;
            frmFiltroObtidas.BorderColor = ativo == "obtidas" ? bordaAtiva : bordaInativa;

            frmFiltroDesejadas.BackgroundColor = ativo == "desejadas" ? corAtivo : corInativo;
            frmFiltroDesejadas.BorderColor = ativo == "desejadas" ? bordaAtiva : bordaInativa;
        }

        private void tapToggleObtido_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is Figurinha fig)
            {
                _controller.ToggleObtido(fig);
                AtualizarLista();
            }
        }

        private void tapToggleDesejado_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is Figurinha fig)
            {
                _controller.ToggleDesejado(fig);
                AtualizarLista();
            }
        }

        private async void tapDeletar_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is Figurinha fig)
            {
                bool confirmar = await DisplayAlert(
                    "🗑️ Excluir Figurinha",
                    $"Deseja realmente excluir a figurinha de {fig.NomeJogador}?",
                    "Sim, excluir",
                    "Cancelar");

                if (confirmar)
                {
                    _controller.Delete(fig);
                    AtualizarLista();
                }
            }
        }
    }

    public class LinhaAlbum
    {
        public Figurinha Item1 { get; set; }
        public Figurinha Item2 { get; set; }

        public bool HasItem2
        {
            get { return Item2 != null; }
        }
    }
}