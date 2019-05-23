﻿using AppFiplasto.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppFiplasto.ViewModels
{
    public class MainViewModel
    {
        //Principal clase del proyecto
        #region Atributos

        private static MainViewModel instance;

        private ApiService apiService;

        private NavigationService navigationService;

        #endregion

        #region Properties

        public string UsuarioLogueado { get; set; }

        public LoginViewModel Login { get; set; }

        public AddBioPendienteViewModel AddRegistroBiomasa { get; set; }
        public BioPendienteViewModel DescarteBiomasaVM { get; set; }
        public BioCargadosViewModel CargadosBiomasaVM { get; set; }
        public BioInformeViewModel InformeBiomasaVM { get; set; }



        public ObservableCollection<MenuItemViewModel> Menus { get; set; }

        public StockMadViewModel StockVM { get; set; }
        public DetailStockMadViewModel DetailStockMadVM { get; set; }


        public AutorizacionViewModel AutorizacionesVM { get; set; }
        public AutorizacionDetailViewModel AutorizacionDetallesVM { get; set; }
        public AutorizacionConfirmacionViewModel ConfirmarRQAutorizacion { get; set; }

        public ProduccionViewModel ProduccionVM { get; set; }

        #endregion



        #region Constructor
        public MainViewModel()
        {
            instance = this;
            this.LoadMenus();
            apiService = new ApiService();
            navigationService = new NavigationService();

        }
        #endregion


        #region Metodos

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }

        private void LoadMenus()
        {
            var menus = new List<Models.Menu>
            {
                new Models.Menu
                {
                    Icon="ic_info",
                    PageName="StockMadPage",
                    Title="Stock Madera"
                },
                 new Models.Menu
                {
                    Icon="ic_info",
                    PageName="AutorizacionPage",
                    Title="Autorizaciones"
                },
                 new Models.Menu
                {
                    Icon="ic_info",
                    PageName="BioTabPage",
                    Title="Biomasa"
                },
                 new Models.Menu
                {
                    Icon = "ic_info",
                    PageName = "ProduccionPage",
                    Title = "Producción"
                },
                 new Models.Menu
                {
                    Icon="ic_info",
                    PageName="AboutPage",
                    Title="About"
                },
                new Models.Menu
                 {
                    Icon = "ic_setup",
                    PageName = "SetupPage",
                    Title = "Setup"
                 },
                 new Models.Menu
                {
                    Icon = "ic_exit",
                    PageName = "ExitPage",
                    Title = "Cerrar sesión"
                }



            };

            this.Menus = new ObservableCollection<MenuItemViewModel>(
                menus.Select(n => new MenuItemViewModel
                {
                    Icon = n.Icon,
                    PageName = n.PageName,
                    Title = n.Title
                }).ToList());
        }

        #endregion


    }
}