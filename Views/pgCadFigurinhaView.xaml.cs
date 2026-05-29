using AlbumCopa2026.Controllers;
using AlbumCopa2026.Models;
using AlbumCopa2026.Services;

namespace AlbumCopa2026.Views
{
    public partial class pgCadFigurinhaView : ContentPage
    {
        FigurinhaController _controller;
        string _imgSelecionada = "";
        string _tipoSelecionado = ""; // "Comum" ou "Especial"

        public pgCadFigurinhaView()
        {
            InitializeComponent();
            _controller = new FigurinhaController();
        }

        private void tapComum_Tapped(object sender, TappedEventArgs e)
        {
            _tipoSelecionado = "Comum";
            // Destaque visual: frmComum ativo
            frmComum.BackgroundColor = Color.FromArgb("#2E7D52");
            frmComum.BorderColor = Color.FromArgb("#FFD700");
            frmEspecial.BackgroundColor = Color.FromArgb("#1A5C3E");
            frmEspecial.BorderColor = Color.FromArgb("#2E7D52");
            lblTipoSelecionado.Text = "Tipo selecionado: ⚪ Comum";
            lblTipoSelecionado.TextColor = Color.FromArgb("#FFFFFF");
        }

        private void tapEspecial_Tapped(object sender, TappedEventArgs e)
        {
            _tipoSelecionado = "Especial";
            frmEspecial.BackgroundColor = Color.FromArgb("#2E7D52");
            frmEspecial.BorderColor = Color.FromArgb("#FFD700");
            frmComum.BackgroundColor = Color.FromArgb("#1A5C3E");
            frmComum.BorderColor = Color.FromArgb("#2E7D52");
            lblTipoSelecionado.Text = "Tipo selecionado: ⭐ Especial";
            lblTipoSelecionado.TextColor = Color.FromArgb("#FFD700");
        }

        private async void btnAdicionarImagem_Clicked(object sender, EventArgs e)
        {
            _imgSelecionada = await ImageService.SelecionarImagem();

            if (!string.IsNullOrEmpty(_imgSelecionada))
            {
                imgFigurinha.Source = _imgSelecionada;
                imgFigurinha.IsVisible = true;
                btnRemoverImagem.IsVisible = true;
            }
        }

        void RemoverImagem()
        {
            imgFigurinha.Source = "";
            imgFigurinha.IsVisible = false;
            _imgSelecionada = "";
            btnRemoverImagem.IsVisible = false;
        }

        private void btnRemoverImagem_Clicked(object sender, EventArgs e)
        {
            RemoverImagem();
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            string nome = txtNomeJogador.Text;
            string selecao = txtSelecao.Text;

            // Validação: todos os campos obrigatórios
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(selecao) ||
                string.IsNullOrWhiteSpace(_tipoSelecionado) ||
                string.IsNullOrWhiteSpace(_imgSelecionada))
            {
                await DisplayAlert("⚠️ Atenção",
                    "Preencha todos os campos obrigatórios:\n• Foto da figurinha\n• Nome do jogador\n• Seleção\n• Tipo da figurinha",
                    "OK");
                return;
            }

            Figurinha fig = new Figurinha
            {
                NomeJogador = nome.Trim(),
                Selecao = selecao.Trim(),
                TipoFigurinha = _tipoSelecionado,
                Obtido = swObtido.IsToggled,
                Desejado = swDesejado.IsToggled,
                DirImagem = _imgSelecionada
            };

            bool sucesso = _controller.Insert(fig);

            if (sucesso)
            {
                await DisplayAlert("✅ Sucesso", "Figurinha cadastrada com sucesso!", "OK");
                LimparFormulario();
            }
            else
            {
                await DisplayAlert("❌ Erro", "Não foi possível salvar a figurinha. Tente novamente.", "OK");
            }
        }

        void LimparFormulario()
        {
            txtNomeJogador.Text = "";
            txtSelecao.Text = "";
            _tipoSelecionado = "";
            swObtido.IsToggled = false;
            swDesejado.IsToggled = false;
            RemoverImagem();

            // Resetar seleção de tipo
            frmComum.BackgroundColor = Color.FromArgb("#1A5C3E");
            frmComum.BorderColor = Color.FromArgb("#2E7D52");
            frmEspecial.BackgroundColor = Color.FromArgb("#1A5C3E");
            frmEspecial.BorderColor = Color.FromArgb("#2E7D52");
            lblTipoSelecionado.Text = "Nenhum tipo selecionado";
            lblTipoSelecionado.TextColor = Color.FromArgb("#6B9E7A");
        }

        private void btnVoltar_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
