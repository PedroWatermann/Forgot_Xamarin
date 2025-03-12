using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Forgot.Models;
using Forgot.Services;

namespace Forgot.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageCadastrar : ContentPage
    {
        public PageCadastrar()
        {
            InitializeComponent();
        }

        public PageCadastrar(ModNotas nota)
        {
            InitializeComponent();

            btnInserir.Text = "Atualizar";
            txtCodigo.IsVisible = true;
            btnExcluir.IsVisible = true;

            txtCodigo.Text = nota.id.ToString();
            txtTitulo.Text = nota.titulo.ToString();
            txtDados.Text = nota.dados.ToString();
            swtFavorito.IsToggled = nota.favorito;
        }

        private void btnInserir_Clicked(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Clicked(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {

        }
    }
}