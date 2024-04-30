using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.WebServices;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Microsoft.Maui.ApplicationModel;
using BNSegurosMAUI.Views;
using CommunityToolkit.Maui.Core;

namespace BNSegurosMAUI.ViewModels
{
    public class FAQPageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public FAQPageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService,IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            Title = TextResources.FAQ_TitleLabel;

            SecondItemCommand = new DelegateCommand<FrequentlyAskedQuestion>((args) => OnSecondItemSelected(args));
            FirstItemCommand = new DelegateCommand<ItemGroupQuestion>((args) => OnPrimaryItemSelected(args));
            InsuranceRedirectCommand = new DelegateCommand(ShowInsurancesPage);
            CloseCommand = new DelegateCommand(ClosePage);
            DownloadPdfCommand = new DelegateCommand<string>((args) => DownloadPdf(args));
            GoToOpaFormCommand = new DelegateCommand<string>((args) => GoToOpaForm(args));
       
        }
        #endregion

        public DelegateCommand CloseCommand { get; private set; }
        public DelegateCommand<FrequentlyAskedQuestion> SecondItemCommand { get; }
        public DelegateCommand<ItemGroupQuestion> FirstItemCommand { get; }
        public DelegateCommand InsuranceRedirectCommand { get; }
        public DelegateCommand<string> DownloadPdfCommand { get; private set; }
        public DelegateCommand<string> GoToOpaFormCommand { get; private set; }

        private List<QuestionType> QuestionTypes { get; set; }
        private List<FrequentlyAskedQuestion> Questions { get; set; }

        private ObservableCollection<ItemGroupQuestion> _displayedGroupedQuestions;
        public ObservableCollection<ItemGroupQuestion> DisplayedGroupedQuestions
        {
            get => _displayedGroupedQuestions;
            set => SetProperty(ref _displayedGroupedQuestions, value);
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                SetProperty(ref _searchText, value);
                FilterQuestionsByText();
            }
        }

        private async void ClosePage()
        {
            //becaus
            Application.Current.MainPage = new AppShell();
            //await Shell.Current.GoToAsync("home");
            Debug.WriteLine("Return to: ClosePage");
        }

        public async void GetQuestionData()
        {
            List<FrequentlyAskedQuestion> questions = null;
            var types = await GetQuestionTypes();

            if (types != null)
            {
                questions = await GetFrequentlyAskedQuestions();
            }

            if (questions != null)
            {
                var navigationParams = new NavigationParameters {
                    { "QuestionTypes", types },
                    { "Questions", questions }
                };
                Initialize(navigationParams);
            }

        }

        private async Task<List<QuestionType>> GetQuestionTypes()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetQuestionTypes);
                var response = await service.Execute<GetQuestionTypesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    return response.Data;
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async Task<List<FrequentlyAskedQuestion>> GetFrequentlyAskedQuestions()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetFrequentlyAskedQuestions);
                var response = await service.Execute<GetFrequentlyAskedQuestionsResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    return response.Data;
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private ObservableCollection<ItemGroupQuestion> GroupQuestions(List<QuestionType> types, List<FrequentlyAskedQuestion> questions)
        {
            var groupedQuestions = new ObservableCollection<ItemGroupQuestion>();

            foreach (var type in types)
            {
                var subQuestions = questions.FindAll(x => x.QuestionType.Id == type.Id);

                if (subQuestions.Count > 0)
                {
                    groupedQuestions.Add(new ItemGroupQuestion
                    {
                        QuestionType = type,
                        ItemQuestions = subQuestions
                    });
                }
            }
            return groupedQuestions;
        }

        private void FilterQuestionsByText()
        {
            var results = new List<FrequentlyAskedQuestion>(Questions);
            if (!string.IsNullOrEmpty(SearchText))
            {
                var text = SearchText.ToLowerInvariant();
                results = results.FindAll(x => x.Question.ToLowerInvariant().Contains(text)
                                            || x.Answer.ToLowerInvariant().Contains(text)
                                            || x.QuestionType.Name.ToLowerInvariant().Contains(text)
                                            || x.QuestionType.Description.ToLowerInvariant().Contains(text));
            }
            DisplayedGroupedQuestions = GroupQuestions(QuestionTypes, results);
        }

        private void OnSecondItemSelected(FrequentlyAskedQuestion item)
        {
            item.IsCellExpanded = !item.IsCellExpanded;
        }

        private void OnPrimaryItemSelected(ItemGroupQuestion item)
        {
            item.IsCellExpanded = !item.IsCellExpanded;
        }

        private async void ShowInsurancesPage()
        {
            string termsAndConditions = null;
            var insurances = await GetInsurancesInformation();

            if (insurances != null)
            {
                termsAndConditions = await GetTermsAndConditions();
            }

            if (termsAndConditions != null)
            {
                var navigationParams = new NavigationParameters {
                    { "Insurances", insurances },
                    { "TermsAndConditions", termsAndConditions }
                };
                await Navigation.NavigateAsync("InsurancesPage", navigationParams);
                Debug.WriteLine("Go to: ShowInsurancePage");
            }
        }

        private async Task<List<Insurance>> GetInsurancesInformation()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetInsurances);
                var response = await service.Execute<GetInsurancesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    return response.Data.OrderBy(x => x.Id).ToList();
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async Task<string> GetTermsAndConditions()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.ProgressDialogDefaultLoading);

                var service = WebServiceFactory.Create(WebServiceType.GetTermsAndConditions);
                var response = await service.Execute<GetTermsAndConditionsResponse>();
                var termsAndConditions = response.GetDocumentUrl();

                DismissProgressDialog();

                if (termsAndConditions != null)
                {
                    return termsAndConditions;
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
            return null;
        }

        private async void displayList(List<QuestionType> questionTypes, List<FrequentlyAskedQuestion> questions)
        {
            await Task.Delay(100);
            DisplayedGroupedQuestions = GroupQuestions(questionTypes, questions);
        }

        private async void DownloadPdf(string url)
        {
            try
            {
                if (await Launcher.CanOpenAsync(url))
                    await Launcher.OpenAsync(url);
                else
                    await DisplayAlert(TextResources.AlertErrorTitle, TextResources.AlertDefaultError, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception ex)
            {
                await DisplayAlert(TextResources.AlertErrorTitle, TextResources.AlertDefaultError, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
                Debug.WriteLine("Exception DownloadPdf: " + ex.Message);
            }
        }

        private async void GoToOpaForm(string url)
        {
            SessionInfo.GetInstance().FromFAQPage = true;

            var navigationParams = new NavigationParameters {
                { "OpaUrl", url }
            };
            await Navigation.NavigateAsync("HTMLPage", navigationParams);
            Debug.WriteLine("Go to: HTMLPage");
        }

        public void Initialize(INavigationParameters parameters)
        {

            if (parameters.ContainsKey("QuestionTypes") && parameters.ContainsKey("Questions"))
            {
                QuestionTypes = parameters.GetValue<List<QuestionType>>("QuestionTypes");
                Questions = parameters.GetValue<List<FrequentlyAskedQuestion>>("Questions");

                displayList(QuestionTypes, Questions);
            }
        }
    }
}
