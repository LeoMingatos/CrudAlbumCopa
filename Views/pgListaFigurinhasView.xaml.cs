using AlbumCopa2026.Controllers;
using AlbumCopa2026.Models;

namespace AlbumCopa2026.Views
{
    public partial class pgListaFigurinhasView : ContentPage
    {
        FigurinhaController _controller;

        // Controle de filtro de status ativo: null = todos, true = obtidas, false = desejadas
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
            AtualizarLista();
        }

        void AtualizarLista()
        {
            string nome = txtFiltroNome?.Text ?? "";
            var lista = _controller.GetFiltrado(nome, _filtroObtido, _filtroDesejado);
            lsvFigurinhas.ItemsSource = lista;
            lblContador.Text = $"{lista.Count} figurinha(s) encontrada(s)";
        }

        private void OnFiltroAlterado(object sender, TextChangedEventArgs e)
        {
            AtualizarLista();
        }

        // --- Filtros de status ---

        private void tapFiltroTodos_Tapped(object sender, TappedEventArgs e)
        {
            _filtroObtido = null;
            _filtroDesejado = null;
            AtualizarEstiloFiltros(ativo: "todos");
            AtualizarLista();
        }

        private void tapFiltroObtidas_Tapped(object sender, TappedEventArgs e)
        {
            _filtroObtido = true;
            _filtroDesejado = null;
            AtualizarEstiloFiltros(ativo: "obtidas");
            AtualizarLista();
        }

        private void tapFiltroDesejadas_Tapped(object sender, TappedEventArgs e)
        {
            _filtroObtido = null;
            _filtroDesejado = true;
            AtualizarEstiloFiltros(ativo: "desejadas");
            AtualizarLista();
        }

        void AtualizarEstiloFiltros(string ativo)
        {
            var corAtivo = Color.FromArgb("#2E7D52");
            var corInativo = Color.FromArgb("#1A5C3E");
            var bordaAtiva = Color.FromArgb("#FFD700");
            var bordaInativa = Color.FromArgb("#2E7D52");

            frmFiltroTodos.BackgroundColor = ativo == "todos" ? corAtivo : corInativo;
            frmFiltroTodos.BorderColor = ativo == "todos" ? bordaAtiva : bordaInativa;

            frmFiltroObtidas.BackgroundColor = ativo == "obtidas" ? corAtivo : corInativo;
            frmFiltroObtidas.BorderColor = ativo == "obtidas" ? bordaAtiva : bordaInativa;

            frmFiltroDesejadas.BackgroundColor = ativo == "desejadas" ? corAtivo : corInativo;
            frmFiltroDesejadas.BorderColor = ativo == "desejadas" ? bordaAtiva : bordaInativa;
        }

        // --- Ações da lista ---

        private async void tapToggleObtido_Tapped(object sender, TappedEventArgs e)
        {
            if (e.Parameter is Figurinha fig)
            {
                _controller.ToggleObtido(fig);
                AtualizarLista();
            }
        }

        private async void tapToggleDesejado_Tapped(object sender, TappedEventArgs e)
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
}
