using AlbumCopa2026.Controllers;
using AlbumCopa2026.Models;
using AlbumCopa2026.Services;

namespace AlbumCopa2026.Views
{
    public partial class pgCadFigurinhaView : ContentPage
    {
        FigurinhaController _controller;

        string _imgSelecionada = "";
        string _tipoSelecionado = "";
        bool _obtido = false;
        bool _desejado = false;

        public pgCadFigurinhaView()
        {
            InitializeComponent();
            _controller = new FigurinhaController();
            AtualizarBotaoSalvar();
        }

        private void AtualizarBotaoSalvar()
        {
            bool formularioOk =
                !string.IsNullOrWhiteSpace(txtNomeJogador.Text) &&
                pickerSelecao.SelectedIndex != -1 &&
                !string.IsNullOrWhiteSpace(_tipoSelecionado) &&
                !string.IsNullOrWhiteSpace(_imgSelecionada);

            btnSalvar.IsEnabled = formularioOk;
            btnSalvar.Opacity = formularioOk ? 1 : 0.45;
        }

        private void txtNomeJogador_TextChanged(object sender, TextChangedEventArgs e)
        {
            AtualizarBotaoSalvar();
        }

        private void pickerSelecao_SelectedIndexChanged(object sender, EventArgs e)
        {
            AtualizarBotaoSalvar();
        }

        private void tapComum_Tapped(object sender, TappedEventArgs e)
        {
            _tipoSelecionado = "Comum";

            frmComum.BackgroundColor = Color.FromArgb("#1B4D3A");
            frmComum.BorderColor = Color.FromArgb("#FFD700");

            frmEspecial.BackgroundColor = Color.FromArgb("#10271F");
            frmEspecial.BorderColor = Color.FromArgb("#2E7D52");

            lblTipoSelecionado.Text = "Tipo selecionado: ⚪ Comum";
            lblTipoSelecionado.TextColor = Color.FromArgb("#FFFFFF");

            AtualizarBotaoSalvar();
        }

        private void tapEspecial_Tapped(object sender, TappedEventArgs e)
        {
            _tipoSelecionado = "Especial";

            frmEspecial.BackgroundColor = Color.FromArgb("#2A3A16");
            frmEspecial.BorderColor = Color.FromArgb("#FFD700");

            frmComum.BackgroundColor = Color.FromArgb("#10271F");
            frmComum.BorderColor = Color.FromArgb("#2E7D52");

            lblTipoSelecionado.Text = "Tipo selecionado: ⭐ Especial";
            lblTipoSelecionado.TextColor = Color.FromArgb("#FFD700");

            AtualizarBotaoSalvar();
        }

        private void tapObtido_Tapped(object sender, TappedEventArgs e)
        {
            _obtido = !_obtido;

            frmObtido.BackgroundColor = _obtido ? Color.FromArgb("#145C32") : Color.FromArgb("#10271F");
            frmObtido.BorderColor = _obtido ? Color.FromArgb("#4CAF50") : Color.FromArgb("#2E7D52");

            lblObtidoEstado.Text = _obtido ? "Já faz parte da coleção" : "Não adquirida";
            lblObtidoEstado.TextColor = _obtido ? Colors.White : Color.FromArgb("#A8D8A8");
        }

        private void tapDesejado_Tapped(object sender, TappedEventArgs e)
        {
            _desejado = !_desejado;

            frmDesejado.BackgroundColor = _desejado ? Color.FromArgb("#4A1324") : Color.FromArgb("#10271F");
            frmDesejado.BorderColor = _desejado ? Color.FromArgb("#E91E63") : Color.FromArgb("#2E7D52");

            lblDesejadoEstado.Text = _desejado ? "Na lista de desejos" : "Fora da lista";
            lblDesejadoEstado.TextColor = _desejado ? Colors.White : Color.FromArgb("#A8D8A8");
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

            AtualizarBotaoSalvar();
        }

        private void btnRemoverImagem_Clicked(object sender, EventArgs e)
        {
            RemoverImagem();
            AtualizarBotaoSalvar();
        }

        void RemoverImagem()
        {
            imgFigurinha.Source = "";
            imgFigurinha.IsVisible = false;
            _imgSelecionada = "";
            btnRemoverImagem.IsVisible = false;
        }

        private async void btnSalvar_Clicked(object sender, EventArgs e)
        {
            string nome = txtNomeJogador.Text;
            string selecao = pickerSelecao.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(_imgSelecionada))
            {
                await DisplayAlert("⚠️ Atenção", "Selecione uma foto da figurinha.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                await DisplayAlert("⚠️ Atenção", "Digite o nome do jogador.", "OK");
                return;
            }

            if (pickerSelecao.SelectedIndex == -1)
            {
                await DisplayAlert("⚠️ Atenção", "Escolha uma seleção.", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(_tipoSelecionado))
            {
                await DisplayAlert("⚠️ Atenção", "Escolha o tipo da figurinha.", "OK");
                return;
            }

            Figurinha fig = new Figurinha
            {
                NomeJogador = nome.Trim(),
                Selecao = selecao,
                TipoFigurinha = _tipoSelecionado,
                Obtido = _obtido,
                Desejado = _desejado,
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
                await DisplayAlert("❌ Erro", "Não foi possível salvar a figurinha.", "OK");
            }
        }

        void LimparFormulario()
        {
            txtNomeJogador.Text = "";
            pickerSelecao.SelectedIndex = -1;

            _tipoSelecionado = "";
            _obtido = false;
            _desejado = false;

            RemoverImagem();

            frmComum.BackgroundColor = Color.FromArgb("#10271F");
            frmComum.BorderColor = Color.FromArgb("#2E7D52");

            frmEspecial.BackgroundColor = Color.FromArgb("#10271F");
            frmEspecial.BorderColor = Color.FromArgb("#2E7D52");

            lblTipoSelecionado.Text = "Nenhum tipo selecionado";
            lblTipoSelecionado.TextColor = Color.FromArgb("#7CB08A");

            frmObtido.BackgroundColor = Color.FromArgb("#10271F");
            frmObtido.BorderColor = Color.FromArgb("#2E7D52");
            lblObtidoEstado.Text = "Não adquirida";
            lblObtidoEstado.TextColor = Color.FromArgb("#A8D8A8");

            frmDesejado.BackgroundColor = Color.FromArgb("#10271F");
            frmDesejado.BorderColor = Color.FromArgb("#2E7D52");
            lblDesejadoEstado.Text = "Fora da lista";
            lblDesejadoEstado.TextColor = Color.FromArgb("#A8D8A8");

            AtualizarBotaoSalvar();
        }

        private void btnVoltar_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}