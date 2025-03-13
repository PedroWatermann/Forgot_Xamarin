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
            try
            {
                ModNotas notas = new ModNotas();
                notas.titulo = txtTitulo.Text;
                notas.dados = txtDados.Text;
                notas.favorito = swtFavorito.IsToggled;

                SerDbNotas dbNotas = new SerDbNotas(App.DbPath);
                if (btnInserir.Text == "Inserir")
                {
                    dbNotas.Inserir(notas);
                    DisplayAlert("Cadastro", dbNotas.StatusMessage, "OK");
                }
                else
                {
                    notas.id = Convert.ToInt32(txtCodigo.Text);
                    dbNotas.Alterar(notas);
                    DisplayAlert("Atualização", dbNotas.StatusMessage, "OK");
                }

                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        private async void btnExcluir_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Excluir", "Deseja realmente excluir essa anotação?", "Sim", "Não");
            if (response)
            {
                SerDbNotas dbNotas = new SerDbNotas(App.DbPath);

                int id = Convert.ToInt32(txtCodigo.Text);
                dbNotas.Excluir(id);

                await DisplayAlert("Exclusão", dbNotas.StatusMessage, "OK");

                MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
                p.Detail = new NavigationPage(new PageHome());
            }
        }

        private void btnCancelar_Clicked(object sender, EventArgs e)
        {
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageHome());
        }
    }
}