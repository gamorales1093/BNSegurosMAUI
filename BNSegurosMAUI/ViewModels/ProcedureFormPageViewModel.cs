using System;
using System.Collections.Generic;
using System.Diagnostics;
using BNSegurosMAUI.Models;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BNSegurosMAUI.ViewModels
{
    public class ProcedureFormPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public ProcedureFormPageViewModel(INavigationService navigationService, IPageDialogService pageDialog)
            : base(navigationService, pageDialog)
        {
            CloseCommand = new DelegateCommand(ClosePage);

            Debug.WriteLine("Constructor: ProcedureFormPageViewModel");
        }
        #endregion

        #region Variables
        public DelegateCommand CloseCommand { get; private set; }

        private Procedures _procedures;
        public Procedures Procedures
        {
            get { return _procedures; }
            set { SetProperty(ref _procedures, value); }
        }
        #endregion

        #region Methods
        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Procedures"))
            {
                Procedures = parameters.GetValue<Procedures>("Procedures");
            }
        }
        #endregion
    }
}