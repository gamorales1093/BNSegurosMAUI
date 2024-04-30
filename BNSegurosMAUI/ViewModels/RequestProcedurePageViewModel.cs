using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.WebServices.Client;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BNSegurosMAUI.ViewModels
{
    public class RequestProcedurePageViewModel : BasePageViewModel
    {
        #region Constructor
        public RequestProcedurePageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            Title = "Solicitud de trámite";
            displayList();
            ItemSelectedCommand = new DelegateCommand<object>(OnItemSelected);
            CloseCommand = new DelegateCommand(ClosePage);
            Debug.WriteLine("Constructor: RequestProcedurePageViewModel");
        }
        #endregion

        #region Variables
        private ObservableCollection<Procedures> _displayedItems;
        public ObservableCollection<Procedures> DisplayedItems
        {
            get => _displayedItems;
            set => SetProperty(ref _displayedItems, value);
        }
        public DelegateCommand<object> ItemSelectedCommand { get; }
        public DelegateCommand CloseCommand { get; private set; }
        #endregion

        #region Methods
        private async void displayList()
        {
            await Task.Delay(100);
            FillProcedures();
        }

        private void FillProcedures()
        {
            DisplayedItems = new ObservableCollection<Procedures>
            {
                new Procedures {
                    Id = 1,
                    Title = "Duplicados",
                    Icon = "icopaduplicate.png",
                    Name = "Solicitud de duplicado",
                    Description = "Solicite una copia de las condiciones particulares del seguro de su vehículo, incendio, vida, desempleo, riesgos del trabajo, entre otros.",
                    Url = ApiConstants.BaseOpaUrl + "Duplicados",
                    IsCellExpanded = false },
                new Procedures {
                    Id = 2,
                    Title = "Cotización",
                    Icon = "icopaemit.png",
                    Name = "Solicitud de cotización",
                    Description = "Solicite la cotización del seguro de automóviles, gastos médicos, incendio, responsabilidad civil, equipo electrónico, entre otros.",
                    Url = ApiConstants.BaseOpaUrl + "Emisiones",
                    IsCellExpanded = false },
                new Procedures {
                    Id = 3,
                    Title = "Variaciones",
                    Icon = "icopavariation.png",
                    Name = "Solicitud de variación",
                    Description = "Solicite la disminución o aumento del monto asegurado, cambios de beneficiarios, placa o cualquier cambio que desee realizar a su póliza.",
                    Url = ApiConstants.BaseOpaUrl + "Variaciones",
                    IsCellExpanded = false },
                new Procedures {
                    Id = 4,
                    Title = "No Cotizante",
                    Icon = "icopaconstancy.png",
                    Name = "Constancia no cotizante",
                    Description = "Realice el envío de la constancia de no cotizante que le remite la CCSS para su trámite de seguro de desempleo.",
                    Url = ApiConstants.BaseOpaUrl + "Nocotizante",
                    IsCellExpanded = false },
                new Procedures {
                    Id = 5,
                    Title = "Cobros",
                    Icon = "icopavoucher.png",
                    Name = "Comprobante de pago",
                    Description = "Gestione aquí el envío del comprobante de pago de su seguro.",
                    Url = ApiConstants.BaseOpaUrl + "Cobros",
                    IsCellExpanded = false }
            };
        }

        private async void OnItemSelected(object items)
        {
            if (items == null)
                return;

            var procedures = (Procedures)items;
            var navigationParams = new NavigationParameters {
                { "Procedures", procedures },
            };

            await Navigation.NavigateAsync("ProcedureFormPage", navigationParams);
            Debug.WriteLine("Go to: ProcedureFormPage");
        }

        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }
        #endregion
    }
}
