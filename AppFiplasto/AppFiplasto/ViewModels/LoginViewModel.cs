﻿namespace AppFiplasto.ViewModels
{
    using System.Windows.Input;
    using AppFiplasto.Services;
    using AppFiplasto.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;
    using AppFiplasto.Helpers;
    using Plugin.Connectivity;

    public class LoginViewModel : BaseViewModel
    {
        #region Atributos
        private ApiService apiService;

        private bool isRunning;
        
        private bool isEnabled;

      
        #endregion

        #region Properties
        public string Usuario { get; set; }

        public string Password { get; set; }

        public bool IsRemember { get; set; }

        public bool IsRunning
        {
            get => this.isRunning;
            set => this.SetValue(ref this.isRunning, value);
        }



        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetValue(ref this.isEnabled, value);
        }


        public ICommand LoginCommand => new RelayCommand(Login);

        #endregion

        #region Constructor
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRemember = true;

        }
        #endregion

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Usuario))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingrese usuario",
                    "Aceptar"
                    );
                return;
            }
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Ingrese contraseña",
                    "Aceptar"
                    );
                return;
            }


            this.IsRunning = true;
            this.IsEnabled = false;

          /*  var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;

                await Application.Current.MainPage.DisplayAlert(
                   "Error",
                   connection.Message,
                   "OK");
                return;
            }*/

            if (IsRunning)
            {
                var response = await apiService.Login(this.Usuario, this.Password);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        response.Message,
                        "Aceptar"
                        );
                    this.IsRunning = false;
                    this.IsEnabled = true;
                    return;
                }
            }

            var mainViewModel = MainViewModel.GetInstance();
            mainViewModel.DescarteBiomasaVM = new BioPendienteViewModel();
            mainViewModel.CargadosBiomasaVM = new BioCargadosViewModel();
            mainViewModel.InformeBiomasaVM = new BioInformeViewModel();
            mainViewModel.StockVM = new StockMadViewModel();
            mainViewModel.BioStockMadVM = new BioStockMadViewModel();
            mainViewModel.ProduccionVM = new ProduccionViewModel();
            mainViewModel.InfoVM = new InfoViewModel();
            mainViewModel.UsuarioLogueado = this.Usuario;
            this.IsRunning = false;
            this.IsEnabled = true;

            Settings.IsRemember = this.IsRemember;
            Settings.Usuario = this.Usuario;


            mainViewModel.AutorizacionesVM = new AutorizacionViewModel();

            Application.Current.MainPage = new MasterPage();




        }
    }
}
