using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace BNSegurosMAUI.ViewModels
{
    public class InsurancesPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public InsurancesPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            Title = TextResources.Insurance_TitleLabel;
            // Go to
            CloseCommand = new DelegateCommand(ClosePage);
            ItemSelectedCommand = new DelegateCommand<object>(OnItemSelected);
            Debug.WriteLine("Constructor: InsurancesPageViewModel");
        }
        #endregion

        #region Fields & Properties
        public DelegateCommand CloseCommand { get; private set; }
        private List<Insurance> _insurances;
        private string _termsAndConditions;

        public DelegateCommand<object> ItemSelectedCommand { get; }

        public List<Insurance> _displayedInsurances;
        public List<Insurance> DisplayedInsurances
        {
            get => _displayedInsurances;
            set => SetProperty(ref _displayedInsurances, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterInsurancesByText();
            }
        }
        #endregion

        #region Methods

        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }

        private void FilterInsurancesByText()
        {
            var results = new List<Insurance>(_insurances);
            if (!string.IsNullOrEmpty(SearchText))
            {
                var text = SearchText.ToLowerInvariant();
                results = results.FindAll(x => x.Name.ToLowerInvariant().Contains(text)
                                            || x.Description.ToLowerInvariant().Contains(text));
            }
            //DisplayedInsurances = new List<Insurance>(results);
            displayList(results);
        }

        private async void displayList(List<Insurance> results)
        {
            await Task.Delay(100);
            DisplayedInsurances = new List<Insurance>(results);
        }

        private async void OnItemSelected(object items)
        {
            if (items == null)
                return;

            var insurance = (Insurance)items;
            var navigationParams = new NavigationParameters {
                { "Insurance", insurance },
                { "TermsAndConditions", _termsAndConditions }
            };

            await Navigation.NavigateAsync("InsuranceDetailPage", navigationParams);
            Debug.WriteLine("Go to: InsuranceDetailPage");
        }



        public  void Initialize(INavigationParameters parameters)
        {
            ShowProgressDialog("Cargando");
            if (parameters.ContainsKey("Insurances") && parameters.ContainsKey("TermsAndConditions"))
            {
                _insurances = parameters.GetValue<List<Insurance>>("Insurances");
                _termsAndConditions = parameters.GetValue<string>("TermsAndConditions");

                foreach (var insurance in _insurances)
                {
                    insurance.Image = ImageSource.FromFile(Insurance.ImageNameFromInsuranceType(insurance.Type));
                }

                FilterInsurancesByText();
            }
            DismissProgressDialog();

        }
        #endregion
    }
}
