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
    public partial class PageListar : ContentPage
    {
        public PageListar()
        {
            InitializeComponent();

            AtualizaLista();
        }

        public void AtualizaLista()
        {
            String titulo = entNota.Text != null ? entNota.Text : "";
            
            SerDbNotas dbNotas = new SerDbNotas(App.DbPath);

            if (swtFavorito.IsToggled)
            {
                lvwNotas.ItemsSource = dbNotas.Localizar(titulo, true);
            }
            else
            {
                lvwNotas.ItemsSource = dbNotas.Localizar(titulo);
            }
        }

        private void swtFavorito_Toggled(object sender, ToggledEventArgs e)
        {
            AtualizaLista();
        }

        private void btnLocalizar_Clicked(object sender, EventArgs e)
        {
            AtualizaLista();
        }

        private void lvwNotas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ModNotas nota = (ModNotas)lvwNotas.SelectedItem;
            
            MasterDetailPage p = (MasterDetailPage)Application.Current.MainPage;
            p.Detail = new NavigationPage(new PageCadastrar(nota));
        }
    }
}