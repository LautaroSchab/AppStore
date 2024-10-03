using AppStore.mvvm.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;

namespace AppStore.mvvm.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    public LoginViewModel()
    {
        Title = "Login";
    }

    [ObservableProperty] private string username = string.Empty;
    [ObservableProperty] private string contrasena = string.Empty;
    [ObservableProperty] private string message = string.Empty;

    [RelayCommand]
    public async Task LoginAsync2()
    {
        // await Application.Current.MainPage.DisplayAlert("Login", "Login", "Ok");

        await Application.Current.MainPage.Navigation.PushAsync(new ProductoListaPage(new ProductoListaViewModel()));
    }

    [RelayCommand]
    public async Task LoginAsync()
    {
        if (IsBusy)
        {
            IsBusy = true;

            // asignamos objeto con datos del usuario-establecimiento logueados
            if (Username != string.Empty && Contrasena!= String.Empty)
            {
                var apiClient = new ApiService();

                var login = await apiClient.ValidarLogin(Username, Contrasena);

                if (login != null)
                {                    
                    Application.Current.MainPage = new NavigationPage(new ProductoListaPage(new ProductoListaViewModel()));
                   
                   
                    Transport.IdUsuario = 1;
                            
                    
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Atención", "Las credenciales ingresadas no son válidas", "Aceptar");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Atención", "Las credenciales son Obligatorias. Verifique!", "Aceptar");
            }

            IsBusy = true;
        }

    }
}