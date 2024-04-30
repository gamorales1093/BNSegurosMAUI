using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BNSegurosMAUI.Helpers;
using BNSegurosMAUI.Models;
using BNSegurosMAUI.Resources.Text;
using BNSegurosMAUI.Utils;
using BNSegurosMAUI.WebServices;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui;
using CommunityToolkit.Maui.Core;

namespace BNSegurosMAUI.ViewModels
{
    public class RequestInsurancePageViewModel : BasePageViewModel, IInitialize
    {
        #region Constructor
        public RequestInsurancePageViewModel(INavigationService navigationService, IPageDialogService pageDialog, IDialogService dialogService, IPopupService popupServices)
            : base(navigationService, pageDialog, dialogService, popupServices)
        {
            Accept = false;
            Date = DateTime.Today;

            EntryUnfocusedCommand = new DelegateCommand<string>((args) => EntryUnfocusedCommandExecute(args));
            DatePickerUnfocusedCommand = new DelegateCommand<string>((args) => DatePickerUnfocusedCommandExecute());
            CheckTermAndConditionsCommand = new DelegateCommand(() => CheckTermAndConditionsCommandExecute());
            TermAndConditionsCommand = new DelegateCommand(async () => await TermAndConditionsCommandExecute());
            CheckAccidentAndAssistCommand = new DelegateCommand(() => CheckAccidentAndAssistCommandExecute());
            AccidentAndAssistCommand = new DelegateCommand(async () => await AccidentAndAssistCommandExecute());
            CloseCommand = new DelegateCommand(ClosePage);

            ShowInsuranceQuoteHomeCommand = new DelegateCommand(async () => await ShowInsuranceQuoteHome(), CanExecuteInsuranceQuote);
            ShowInsuranceQuoteJoblessCommand = new DelegateCommand(async () => await ShowInsuranceQuoteJobless(), CanExecuteInsuranceQuote);
            ShowInsuranceQuoteMedicalCommand = new DelegateCommand(async () => await ShowInsuranceQuoteMedical(), CanExecuteInsuranceQuote);
            ShowInsuranceQuoteOthersCommand = new DelegateCommand(async () => await ShowInsuranceQuoteOthers(), CanExecuteInsuranceQuote);
            ShowInsuranceBicycleCommand = new DelegateCommand(async () => await ShowInsuranceQuoteBicycle(), CanExecuteInsuranceQuote);

            ShowInsuranceQuoteHomeCommand.ObservesProperty(() => Name);
            ShowInsuranceQuoteJoblessCommand.ObservesProperty(() => Name);
            ShowInsuranceQuoteMedicalCommand.ObservesProperty(() => Name);
            ShowInsuranceQuoteOthersCommand.ObservesProperty(() => Name);
            ShowInsuranceBicycleCommand.ObservesProperty(() => Name);

            ShowInsuranceQuoteHomeCommand.ObservesProperty(() => LastName);
            ShowInsuranceQuoteJoblessCommand.ObservesProperty(() => LastName);
            ShowInsuranceQuoteMedicalCommand.ObservesProperty(() => LastName);
            ShowInsuranceQuoteOthersCommand.ObservesProperty(() => LastName);
            ShowInsuranceBicycleCommand.ObservesProperty(() => LastName);

            ShowInsuranceQuoteHomeCommand.ObservesProperty(() => IdNumber);
            ShowInsuranceQuoteJoblessCommand.ObservesProperty(() => IdNumber);
            ShowInsuranceQuoteMedicalCommand.ObservesProperty(() => IdNumber);
            ShowInsuranceQuoteOthersCommand.ObservesProperty(() => IdNumber);
            ShowInsuranceBicycleCommand.ObservesProperty(() => IdNumber);

            ShowInsuranceQuoteHomeCommand.ObservesProperty(() => Date);
            ShowInsuranceQuoteJoblessCommand.ObservesProperty(() => Date);
            ShowInsuranceQuoteMedicalCommand.ObservesProperty(() => Date);
            ShowInsuranceQuoteOthersCommand.ObservesProperty(() => Date);
            ShowInsuranceBicycleCommand.ObservesProperty(() => Date);

            ShowInsuranceQuoteHomeCommand.ObservesProperty(() => Telephone);
            ShowInsuranceQuoteJoblessCommand.ObservesProperty(() => Telephone);
            ShowInsuranceQuoteMedicalCommand.ObservesProperty(() => Telephone);
            ShowInsuranceQuoteOthersCommand.ObservesProperty(() => Telephone);
            ShowInsuranceBicycleCommand.ObservesProperty(() => Telephone);

            ShowInsuranceQuoteHomeCommand.ObservesProperty(() => Email);
            ShowInsuranceQuoteJoblessCommand.ObservesProperty(() => Email);
            ShowInsuranceQuoteMedicalCommand.ObservesProperty(() => Email);
            ShowInsuranceQuoteOthersCommand.ObservesProperty(() => Email);
            ShowInsuranceBicycleCommand.ObservesProperty(() => Email);

            ShowInsuranceQuoteJoblessCommand.ObservesProperty(() => CreditQuote);

            ShowInsuranceQuoteHomeCommand.ObservesProperty(() => Accept);
            ShowInsuranceQuoteJoblessCommand.ObservesProperty(() => Accept);
            ShowInsuranceQuoteMedicalCommand.ObservesProperty(() => Accept);
            ShowInsuranceQuoteOthersCommand.ObservesProperty(() => Accept);
            ShowInsuranceBicycleCommand.ObservesProperty(() => Accept);

            ShowInsuranceBicycleCommand.ObservesProperty(() => BicycleValue);
            ShowInsuranceBicycleCommand.ObservesProperty(() => BicycleYear);
            ShowInsuranceBicycleCommand.ObservesProperty(() => AccidentAndAssist);

            Debug.WriteLine("Constructor: RequestInsurancePageViewModel");
        }
        #endregion

        #region Variables
        private Insurance _model;
        private string _termsAndConditions;

        /// <summary>
        /// PERSONAL DATA
        /// </summary>
        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }
        private bool _errorName;
        public bool ErrorName { get => _errorName; set => SetProperty(ref _errorName, value); }
        private string _errorNameResult;
        public string ErrorNameResult { get => _errorNameResult; set => SetProperty(ref _errorNameResult, value); }

        private string _lastName;
        public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }
        private bool _errorLastName;
        public bool ErrorLastName { get => _errorLastName; set => SetProperty(ref _errorLastName, value); }
        private string _errorLastNameResult;
        public string ErrorLastNameResult { get => _errorLastNameResult; set => SetProperty(ref _errorLastNameResult, value); }

        private string _typeId;
        public string TypeId { get => _typeId; set => SetProperty(ref _typeId, value); }
        private bool _errorTypeId;
        public bool ErrorTypeId { get => _errorTypeId; set => SetProperty(ref _errorTypeId, value); }
        private ObservableCollection<ItemDropDown> _typeIds;
        public ObservableCollection<ItemDropDown> TypeIds { get => _typeIds; set => SetProperty(ref _typeIds, value); }
        private string _errorIdResult;
        public string ErrorIdResult { get => _errorIdResult; set => SetProperty(ref _errorIdResult, value); }

        private string _idNumber;
        public string IdNumber { get => _idNumber; set => SetProperty(ref _idNumber, value); }
        private bool _errorId;
        public bool ErrorId { get => _errorId; set => SetProperty(ref _errorId, value); }
        private string _errorIdNumberResult;
        public string ErrorIdNumberResult { get => _errorIdNumberResult; set => SetProperty(ref _errorIdNumberResult, value); }

        private string _gender;
        public string Gender { get => _gender; set => SetProperty(ref _gender, value); }
        private bool _errorGender;
        public bool ErrorGender { get => _errorGender; set => SetProperty(ref _errorGender, value); }
        private ObservableCollection<ItemDropDown> _genders;
        public ObservableCollection<ItemDropDown> Genders { get => _genders; set => SetProperty(ref _genders, value); }

        private DateTime _date;
        public DateTime Date { get => _date; set => SetProperty(ref _date, value); }
        public DateTime MinDate;
        public DateTime MaxDate;
        private bool _errorDate;
        public bool ErrorDate { get => _errorDate; set => SetProperty(ref _errorDate, value); }

        private string _telephone;
        public string Telephone { get => _telephone; set => SetProperty(ref _telephone, value); }
        private bool _errorTelephone;
        public bool ErrorTelephone { get => _errorTelephone; set => SetProperty(ref _errorTelephone, value); }
        private string _errorTelephoneResult;
        public string ErrorTelephoneResult { get => _errorTelephoneResult; set => SetProperty(ref _errorTelephoneResult, value); }

        private string _email;
        public string Email { get => _email; set => SetProperty(ref _email, value); }
        private bool _errorEmail;
        public bool ErrorEmail { get => _errorEmail; set => SetProperty(ref _errorEmail, value); }
        private string _errorEmailResult;
        public string ErrorEmailResult { get => _errorEmailResult; set => SetProperty(ref _errorEmailResult, value); }

        /// <summary>
        /// SPECIFIC DATA
        /// </summary>
        private bool _insuranceHomeIsVisible;
        public bool InsuranceHomeIsVisible { get => _insuranceHomeIsVisible; set => SetProperty(ref _insuranceHomeIsVisible, value); }

        private bool _insuranceMedicalIsVisible;
        public bool InsuranceMedicalIsVisible { get => _insuranceMedicalIsVisible; set => SetProperty(ref _insuranceMedicalIsVisible, value); }

        private bool _insuranceJoblessIsVisible;
        public bool InsuranceJoblessIsVisible { get => _insuranceJoblessIsVisible; set => SetProperty(ref _insuranceJoblessIsVisible, value); }

        private bool _insuranceOthersIsVisible;
        public bool InsuranceOthersIsVisible { get => _insuranceOthersIsVisible; set => SetProperty(ref _insuranceOthersIsVisible, value); }

        private bool _insuranceBicycleIsVisible;
        public bool InsuranceBicycleIsVisible { get => _insuranceBicycleIsVisible; set => SetProperty(ref _insuranceBicycleIsVisible, value); }

        //Mapfre Info
        private string _typeInsurancePlan;
        public string TypeInsurancePlan { get => _typeInsurancePlan; set => SetProperty(ref _typeInsurancePlan, value); }
        private bool _errorTypeInsurancePlan;
        public bool ErrorTypeInsurancePlan { get => _errorTypeInsurancePlan; set => SetProperty(ref _errorTypeInsurancePlan, value); }
        private ObservableCollection<ItemDropDown> _typeInsurancePlans;
        public ObservableCollection<ItemDropDown> TypeInsurancePlans { get => _typeInsurancePlans; set => SetProperty(ref _typeInsurancePlans, value); }

        private string _howMany;
        public string HowMany { get => _howMany; set => SetProperty(ref _howMany, value); }
        private bool _errorHowMany;
        public bool ErrorHowMany { get => _errorHowMany; set => SetProperty(ref _errorHowMany, value); }
        private ObservableCollection<ItemDropDown> _howManyPersons;
        public ObservableCollection<ItemDropDown> HowManyPersons { get => _howManyPersons; set => SetProperty(ref _howManyPersons, value); }

        //Sagicor Info
        private string _typeCurrency;
        public string TypeCurrency { get => _typeCurrency; set => SetProperty(ref _typeCurrency, value); }
        private bool _errorTypeCurrency;
        public bool ErrorTypeCurrency { get => _errorTypeCurrency; set => SetProperty(ref _errorTypeCurrency, value); }
        private ObservableCollection<ItemDropDown> _currency;
        public ObservableCollection<ItemDropDown> Currency { get => _currency; set => SetProperty(ref _currency, value); }

        private string _creditQuote;
        public string CreditQuote { get => _creditQuote; set => SetProperty(ref _creditQuote, value); }
        private bool _errorCreditQuote;
        public bool ErrorCreditQuote { get => _errorCreditQuote; set => SetProperty(ref _errorCreditQuote, value); }
        private string _errorCreditQuoteResult;
        public string ErrorCreditQuoteResult { get => _errorCreditQuoteResult; set => SetProperty(ref _errorCreditQuoteResult, value); }

        //Others Insurances Info
        private string _typeInsurance;
        public string TypeInsurance { get => _typeInsurance; set => SetProperty(ref _typeInsurance, value); }
        private bool _errorTypeInsurance;
        public bool ErrorTypeInsurance { get => _errorTypeInsurance; set => SetProperty(ref _errorTypeInsurance, value); }
        private ObservableCollection<ItemDropDown> _typeInsurances;
        public ObservableCollection<ItemDropDown> TypeInsurances { get => _typeInsurances; set => SetProperty(ref _typeInsurances, value); }

        private string _otherInsuranceEntry;
        public string OtherInsuranceEntry { get => _otherInsuranceEntry; set => SetProperty(ref _otherInsuranceEntry, value); }
        private bool _errorOtherInsuranceEntry;
        public bool ErrorOtherInsuranceEntry { get => _errorOtherInsuranceEntry; set => SetProperty(ref _errorOtherInsuranceEntry, value); }
        private string _errorOtherInsuranceEntryResult;
        public string ErrorOtherInsuranceEntryResult { get => _errorOtherInsuranceEntryResult; set => SetProperty(ref _errorOtherInsuranceEntryResult, value); }

        private bool _otherInsuranceEntryIsVisible;
        public bool OtherInsuranceEntryIsVisible { get => _otherInsuranceEntryIsVisible; set => SetProperty(ref _otherInsuranceEntryIsVisible, value); }

        //Bicycle Insurance Info
        private string _bicycleTypeCurrency;
        public string BicycleTypeCurrency { get => _bicycleTypeCurrency; set => SetProperty(ref _bicycleTypeCurrency, value); }
        private bool _errorBicycleTypeCurrency;
        public bool ErrorBicycleTypeCurrency { get => _errorBicycleTypeCurrency; set => SetProperty(ref _errorBicycleTypeCurrency, value); }
        private ObservableCollection<ItemDropDown> _bicycleCurrency;
        public ObservableCollection<ItemDropDown> BicycleCurrency { get => _bicycleCurrency; set => SetProperty(ref _bicycleCurrency, value); }

        private string _bicycleValue;
        public string BicycleValue { get => _bicycleValue; set => SetProperty(ref _bicycleValue, value); }
        private bool _errorBicycleValue;
        public bool ErrorBicycleValue { get => _errorBicycleValue; set => SetProperty(ref _errorBicycleValue, value); }
        private string _errorBicycleValueResult;
        public string ErrorBicycleValueResult { get => _errorBicycleValueResult; set => SetProperty(ref _errorBicycleValueResult, value); }

        private string _bicycleYear;
        public string BicycleYear { get => _bicycleYear; set => SetProperty(ref _bicycleYear, value); }
        private bool _errorBicycleYear;
        public bool ErrorBicycleYear { get => _errorBicycleYear; set => SetProperty(ref _errorBicycleYear, value); }
        private string _errorBicycleYearResult;
        public string ErrorBicycleYearResult { get => _errorBicycleYearResult; set => SetProperty(ref _errorBicycleYearResult, value); }

        /// <summary>
        /// Terms and conditions
        /// </summary>
        private bool _accept;
        public bool Accept { get { return _accept; } set { _accept = value; RaisePropertyChanged(); } }
        //public bool Accept { get { return _accept; } set { _accept = value; } }

        /// <summary>
        /// Accident and Assist
        /// </summary>
        private bool _accidentAndAssist;
        public bool AccidentAndAssist { get { return _accidentAndAssist; } set { _accidentAndAssist = value; RaisePropertyChanged(); } }
        //public bool AccidentAndAssist { get { return _accidentAndAssist; } set { _accidentAndAssist = value; } }

        private int ValAccidentAndAssist = 0;

        public DelegateCommand GetTpeIdCommand { get; }
        public DelegateCommand<string> EntryUnfocusedCommand { get; }
        public DelegateCommand<string> DatePickerUnfocusedCommand { get; }
        public DelegateCommand CheckTermAndConditionsCommand { get; }
        public DelegateCommand TermAndConditionsCommand { get; }
        public DelegateCommand CheckAccidentAndAssistCommand { get; }
        public DelegateCommand AccidentAndAssistCommand { get; }

        public DelegateCommand ShowInsuranceQuoteHomeCommand { get; }
        public DelegateCommand ShowInsuranceQuoteJoblessCommand { get; }
        public DelegateCommand ShowInsuranceQuoteMedicalCommand { get; }
        public DelegateCommand ShowInsuranceQuoteOthersCommand { get; }
        public DelegateCommand ShowInsuranceBicycleCommand { get; }
        public DelegateCommand CloseCommand { get; private set; }
        #endregion

        #region Methods
        private void SetUpInsuranceModel()
        {
            if (_model.Type == InsuranceType.Home)
            {
                InsuranceHomeIsVisible = true;
                InsuranceJoblessIsVisible = false;
                InsuranceMedicalIsVisible = false;
                InsuranceOthersIsVisible = false;
                InsuranceBicycleIsVisible = false;
            }
            else if (_model.Type == InsuranceType.Unemployment)
            {
                InsuranceHomeIsVisible = false;
                InsuranceJoblessIsVisible = true;
                InsuranceMedicalIsVisible = false;
                InsuranceOthersIsVisible = false;
                InsuranceBicycleIsVisible = false;
            }
            else if (_model.Type == InsuranceType.MedicalExpense)
            {
                InsuranceHomeIsVisible = false;
                InsuranceJoblessIsVisible = false;
                InsuranceMedicalIsVisible = true;
                InsuranceOthersIsVisible = false;
                InsuranceBicycleIsVisible = false;
            }
            else if (_model.Type == InsuranceType.Other)
            {
                InsuranceHomeIsVisible = false;
                InsuranceJoblessIsVisible = false;
                InsuranceMedicalIsVisible = false;
                InsuranceOthersIsVisible = true;
                InsuranceBicycleIsVisible = false;
            }
            else if (_model.Type == InsuranceType.Bicycle)
            {
                InsuranceHomeIsVisible = false;
                InsuranceJoblessIsVisible = false;
                InsuranceMedicalIsVisible = false;
                InsuranceOthersIsVisible = false;
                InsuranceBicycleIsVisible = true;
            }
        }

        private void SetUpGeneralDropDownInfo()
        {
            // Personal info
            TypeId = TextResources.Request_ListPlaceholder;
            ObservableCollection<ItemDropDown> TypeIdList = new ObservableCollection<ItemDropDown>();
            TypeIdList.Add(new ItemDropDown { Id = "1", Name = "Nacional", Value = "N" });
            TypeIdList.Add(new ItemDropDown { Id = "2", Name = "Extranjero", Value = "E" });
            TypeIds = TypeIdList;

            Gender = TextResources.Request_ListPlaceholder;
            ObservableCollection<ItemDropDown> GendersList = new ObservableCollection<ItemDropDown>();
            GendersList.Add(new ItemDropDown { Id = "1", Name = "Masculino", Value = "M" });
            GendersList.Add(new ItemDropDown { Id = "2", Name = "Femenino", Value = "F" });
            Genders = GendersList;
        }

        private void SetUpUnemploymentDropDownInfo(List<Currency> currencies)
        {
            //Sagicor
            TypeCurrency = TextResources.Request_ListPlaceholder;
            ObservableCollection<ItemDropDown> CurrencyList = new ObservableCollection<ItemDropDown>();

            foreach (var currency in currencies)
            {
                CurrencyList.Add(new ItemDropDown
                {
                    Id = currency.Id.ToString(),
                    Name = currency.Name,
                    Value = currency.Code
                });
            }
            Currency = CurrencyList;
        }

        private void SetUpMedicalExpensesDropDownInfo(List<MedicalInsuranceCategory> categories, List<MedicalInsuranceClass> classes)
        {
            //Mapfre
            TypeInsurancePlan = TextResources.Request_ListPlaceholder;
            ObservableCollection<ItemDropDown> TypeInsurancePlansList = new ObservableCollection<ItemDropDown>();

            foreach (var insuranceClass in classes)
            {
                TypeInsurancePlansList.Add(new ItemDropDown
                {
                    Id = insuranceClass.Id.ToString(),
                    Name = insuranceClass.DisplayedText,
                    Value = insuranceClass.Id.ToString()
                });
            }
            TypeInsurancePlans = TypeInsurancePlansList;

            HowMany = TextResources.Request_ListPlaceholder;
            ObservableCollection<ItemDropDown> HowManyPersonsList = new ObservableCollection<ItemDropDown>();

            foreach (var category in categories)
            {
                HowManyPersonsList.Add(new ItemDropDown
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                    Value = category.Id.ToString()
                });
            }
            HowManyPersons = HowManyPersonsList;
        }

        private void SetUpOtherInsurancesDropDownInfo(List<OtherInsurance> otherInsurances)
        {
            //Other
            TypeInsurance = TextResources.Request_ListPlaceholder;
            ObservableCollection<ItemDropDown> OtherInsurancesList = new ObservableCollection<ItemDropDown>();

            foreach (var otherInsurance in otherInsurances)
            {
                OtherInsurancesList.Add(new ItemDropDown
                {
                    Id = otherInsurance.Id.ToString(),
                    Name = otherInsurance.Name
                });
            }
            TypeInsurances = OtherInsurancesList;
        }

        private void SetUpBicycleInsuranceDropDownInfo(List<Currency> currencies)
        {
            //Bicycle Insurance
            BicycleTypeCurrency = TextResources.Request_ListPlaceholder;
            ObservableCollection<ItemDropDown> CurrencyList = new ObservableCollection<ItemDropDown>();

            foreach (var currency in currencies)
            {
                CurrencyList.Add(new ItemDropDown
                {
                    Id = currency.Id.ToString(),
                    Name = currency.Name,
                    Value = currency.Code
                });
            }
            BicycleCurrency = CurrencyList;
        }

        private void EntryUnfocusedCommandExecute(string entryName)
        {
            switch (entryName)
            {
                case ValidationConstants.ValidationName:
                    if (string.IsNullOrWhiteSpace(Name))
                    {
                        ErrorName = true;
                        ErrorNameResult = TextResources.Request_RequiredDataMessage;
                    }
                    else if (!TextUtils.IsLetters(Name))
                    {
                        ErrorName = true;
                        ErrorNameResult = TextResources.Request_ValidFormatDataMessage;
                    }
                    else
                    {
                        ErrorName = false;
                    }
                    break;
                case ValidationConstants.ValidationLastName:
                    if (string.IsNullOrWhiteSpace(LastName))
                    {
                        ErrorLastName = true;
                        ErrorLastNameResult = TextResources.Request_RequiredDataMessage;
                    }
                    else if (!TextUtils.IsLetters(LastName))
                    {
                        ErrorLastName = true;
                        ErrorLastNameResult = TextResources.Request_ValidFormatDataMessage;
                    }
                    else
                    {
                        ErrorLastName = false;
                    }
                    break;
                case ValidationConstants.ValidationId:
                    DropDownTypeIdValidate();
                    if (string.IsNullOrWhiteSpace(IdNumber))
                    {
                        ErrorId = true;
                        ErrorIdNumberResult = TextResources.Request_RequiredDataMessage;
                    }
                    else if (!TextUtils.IsNumeric(IdNumber))
                    {
                        ErrorId = true;
                        ErrorIdNumberResult = TextResources.Request_ValidFormatDataMessage;
                    }
                    else if (!DropDownTypeIdValidate())
                    {
                        ErrorId = true;
                    }
                    else
                    {
                        var id = Int32.Parse(TypeIds.Where(x => x.Name == TypeId).FirstOrDefault().Id);
                        if (id == 1)
                        {
                            var idFormat = String.Format("{0:#-####-####}", Int32.Parse(IdNumber));
                            IdNumber = idFormat;
                        }
                        
                        ErrorId = false;
                    }
                    break;
                case ValidationConstants.ValidationTelephone:
                    if (string.IsNullOrWhiteSpace(Telephone))
                    {
                        ErrorTelephone = true;
                        ErrorTelephoneResult = TextResources.Request_RequiredDataMessage;
                    }
                    else
                    {
                        var isValidTelephone = TextUtils.IsNumeric(Telephone) && Telephone.Length == 8;
                        if (!isValidTelephone)
                        {
                            ErrorTelephone = true;
                            ErrorTelephoneResult = TextResources.Request_InvalidTelephoneDataMessage;
                        }
                        else
                        {
                            var telephoneFormat = String.Format("{0:####-####}", Int32.Parse(Telephone));
                            Telephone = telephoneFormat;
                            ErrorTelephone = false;
                        }
                    }
                    break;
                case ValidationConstants.ValidationEmail:
                    if (string.IsNullOrWhiteSpace(Email))
                    {
                        ErrorEmail = true;
                        ErrorEmailResult = TextResources.Request_RequiredDataMessage;
                    }
                    else if (!TextUtils.IsValidEmail(Email))
                    {
                        ErrorEmail = true;
                        ErrorEmailResult = TextResources.Request_InvaliEmailDataMessage;
                    }
                    else
                    {
                        ErrorEmail = false;
                    }
                    break;
                case ValidationConstants.ValidationCreditQuote:
                    if (string.IsNullOrWhiteSpace(CreditQuote))
                    {
                        ErrorCreditQuote = true;
                        ErrorCreditQuoteResult = TextResources.Request_RequiredDataMessage;
                    }
                    else
                    {
                        var isValidAmount = TextUtils.IsAmount(CreditQuote);

                        if (!isValidAmount)
                        {
                            ErrorCreditQuote = true;
                            ErrorCreditQuoteResult = TextResources.Request_ValidFormatDataMessage;
                        }
                        else
                        {
                            try
                            {
                                var formattedAmount = string.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", decimal.Parse(CreditQuote, CultureInfo.InvariantCulture));
                                CreditQuote = formattedAmount;
                                ErrorCreditQuote = false;
                            }
                            catch (Exception ex)
                            {
                                ErrorCreditQuote = true;
                                ErrorCreditQuoteResult = TextResources.Request_ValidFormatDataMessage;
                                Debug.WriteLine("Exception EntryUnfocusedCommandExecute/ValidationCreditQuote: " + ex.Message);
                            }
                        }
                    }
                    break;
                case ValidationConstants.ValidationOtherInsurance:
                    if (string.IsNullOrWhiteSpace(OtherInsuranceEntry))
                    {
                        ErrorOtherInsuranceEntry = true;
                        ErrorOtherInsuranceEntryResult = TextResources.Request_RequiredDataMessage;
                    }
                    else if (!TextUtils.IsLetters(OtherInsuranceEntry))
                    {
                        ErrorOtherInsuranceEntry = true;
                        ErrorOtherInsuranceEntryResult = TextResources.Request_ValidFormatDataMessage;
                    }
                    else
                    {
                        ErrorOtherInsuranceEntry = false;
                    }
                    break;
                case ValidationConstants.ValidationBicycleValue:
                    if (string.IsNullOrWhiteSpace(BicycleValue))
                    {
                        ErrorBicycleValue = true;
                        ErrorBicycleValueResult = TextResources.Request_RequiredDataMessage;
                    }
                    else
                    {
                        var isValidAmount = TextUtils.IsAmount(BicycleValue);

                        if (!isValidAmount)
                        {
                            ErrorBicycleValue = true;
                            ErrorBicycleValueResult = TextResources.Request_ValidFormatDataMessage;
                        }
                        else
                        {
                            try
                            {
                                var formattedAmount = string.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", decimal.Parse(BicycleValue, CultureInfo.InvariantCulture));
                                BicycleValue = formattedAmount;
                                ErrorBicycleValue = false;
                            }
                            catch (Exception ex)
                            {
                                ErrorBicycleValue = true;
                                ErrorBicycleValueResult = TextResources.Request_ValidFormatDataMessage;
                                Debug.WriteLine("Exception EntryUnfocusedCommandExecute/ValidationBicycleValue: " + ex.Message);
                            }
                        }
                    }
                    break;
                case ValidationConstants.ValidationBicycleYear:
                    if (string.IsNullOrWhiteSpace(BicycleYear))
                    {
                        ErrorBicycleYear = true;
                        ErrorBicycleYearResult = TextResources.Request_RequiredDataMessage;
                    }
                    else
                    {
                        var validYear = DateTime.Today.Year - 10;
                        var isValidYear = TextUtils.IsNumeric(BicycleYear) && BicycleYear.Length == 4 && Int32.Parse(BicycleYear) >= validYear && Int32.Parse(BicycleYear) <= DateTime.Today.Year;
                        if (!isValidYear)
                        {
                            ErrorBicycleYear = true;
                            ErrorBicycleYearResult = TextResources.Request_InvalidYearMessage;
                        }
                        else
                        {
                            ErrorBicycleYear = false;
                        }
                    }
                    break;
            }
        }

        private void DatePickerUnfocusedCommandExecute()
        {
            // check someone is too young or not
            var age = CalculateAge(Date.Date);
            var day = DateTime.Today.Day;
            var mounth = DateTime.Today.Month;
            var year = DateTime.Today.Year;
            MinDate = new DateTime(year - 120, mounth, day);
            MaxDate = new DateTime(year - 18, mounth, day);

            if (Date.Date == DateTime.Today || Date.Date < MinDate || Date.Date > MaxDate)
                ErrorDate = true;
            else
                ErrorDate = false;
        }

        private int CalculateAge(DateTime birthDay)
        {
            int years = DateTime.Now.Year - birthDay.Year;

            if ((birthDay.Month > DateTime.Now.Month) || (birthDay.Month == DateTime.Now.Month && birthDay.Day > DateTime.Now.Day))
                years--;

            return years;
        }

        public void DropDownUnfocused(string value)
        {
            if (InsuranceOthersIsVisible == true)
            {
                if (value == "Otro")
                {
                    OtherInsuranceEntryIsVisible = true;
                }
                else
                {
                    OtherInsuranceEntryIsVisible = false;
                    OtherInsuranceEntry = string.Empty;
                }
            }
        }

        private bool CanExecuteInsuranceQuote()
        {
            bool entryValues;
            string idNumberFormat = string.Empty;
            string telephoneFormat = string.Empty;

            if (IdNumber != null && Telephone != null)
            {
                idNumberFormat = IdNumber.Replace("-", "");
                telephoneFormat = Telephone.Replace("-", "");
            }

            entryValues = !string.IsNullOrWhiteSpace(Name)
                    && TextUtils.IsLetters(Name)
                    && !string.IsNullOrWhiteSpace(LastName)
                    && TextUtils.IsLetters(LastName)
                    && !string.IsNullOrWhiteSpace(idNumberFormat)
                    && idNumberFormat.ToCharArray().All(char.IsDigit)
                    && !string.IsNullOrWhiteSpace(telephoneFormat)
                    && TextUtils.IsNumeric(telephoneFormat)
                    && telephoneFormat.Length == 8
                    && !string.IsNullOrWhiteSpace(Email)
                    && TextUtils.IsValidEmail(Email);

            if (InsuranceJoblessIsVisible == true)
            {
                entryValues = entryValues
                    && !string.IsNullOrWhiteSpace(CreditQuote)
                    && TextUtils.IsAmount(CreditQuote);
            }

            if (OtherInsuranceEntryIsVisible == true)
            {
                entryValues = entryValues
                    && !string.IsNullOrWhiteSpace(OtherInsuranceEntry)
                    && TextUtils.IsLetters(OtherInsuranceEntry);
            }

            if (InsuranceBicycleIsVisible == true)
            {
                var validYear = DateTime.Today.Year - 10;
                entryValues = entryValues
                    && !string.IsNullOrWhiteSpace(BicycleValue)
                    && TextUtils.IsAmount(BicycleValue)
                    && !string.IsNullOrWhiteSpace(BicycleYear)
                    && TextUtils.IsNumeric(BicycleYear)
                    && BicycleYear.Length == 4
                    && Int32.Parse(BicycleYear) >= validYear && Int32.Parse(BicycleYear) <= DateTime.Today.Year;
            }
            //todo
            var age = CalculateAge(Date.Date);
            var day = DateTime.Today.Day;
            var mounth = DateTime.Today.Month;
            var year = DateTime.Today.Year;
            MinDate = new DateTime(year - 120, mounth, day);
            MaxDate = new DateTime(year - 18, mounth, day);

            if (Date.Date == DateTime.Today || Date.Date < MinDate || Date.Date > MaxDate)
                ErrorDate = true;
            else
                ErrorDate = false;


            bool dateValue = Date.Date != DateTime.Today && Date.Date > MinDate && Date.Date < MaxDate;
            bool value = entryValues && dateValue && Accept;

            return value;
        }

        private void ShowDataValidationsRequired()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                ErrorName = true;
                ErrorNameResult = TextResources.Request_RequiredDataMessage;
                Accept = false;
            }
            else if (string.IsNullOrWhiteSpace(LastName))
            {
                ErrorLastName = true;
                ErrorLastNameResult = TextResources.Request_RequiredDataMessage;
                Accept = false;
            }
            else if (string.IsNullOrWhiteSpace(IdNumber))
            {
                ErrorId = true;
                ErrorIdNumberResult = TextResources.Request_RequiredDataMessage;
                Accept = false;
            }
            else if (string.IsNullOrWhiteSpace(Telephone))
            {
                ErrorTelephone = true;
                ErrorTelephoneResult = TextResources.Request_RequiredDataMessage;
                Accept = false;
            }
            else if (string.IsNullOrWhiteSpace(Email))
            {
                ErrorEmail = true;
                ErrorEmailResult = TextResources.Request_RequiredDataMessage;
                Accept = false;
            }
            else if (InsuranceJoblessIsVisible == true)
            {
                if (string.IsNullOrWhiteSpace(CreditQuote))
                {
                    ErrorCreditQuote = true;
                    ErrorCreditQuoteResult = TextResources.Request_RequiredDataMessage;
                    Accept = false;
                }
            }
            else if (OtherInsuranceEntryIsVisible == true)
            {
                if (string.IsNullOrWhiteSpace(OtherInsuranceEntry))
                {
                    ErrorOtherInsuranceEntry = true;
                    ErrorOtherInsuranceEntryResult = TextResources.Request_RequiredDataMessage;
                    Accept = false;
                }
            }
            else if (InsuranceBicycleIsVisible == true)
            {
                if (string.IsNullOrWhiteSpace(BicycleValue))
                {
                    ErrorBicycleValue = true;
                    ErrorBicycleValueResult = TextResources.Request_RequiredDataMessage;
                    Accept = false;
                }
                else if (string.IsNullOrWhiteSpace(BicycleYear))
                {
                    ErrorBicycleYear = true;
                    ErrorBicycleYearResult = TextResources.Request_RequiredDataMessage;
                    Accept = false;
                }
            }
        }

        private void CheckTermAndConditionsCommandExecute()
        {
            CanExecuteInsuranceQuote();
            DropDownTypeIdValidate();
            DropDownGenderValidate();

            if (_model.Type == InsuranceType.Unemployment) // Seguro de desempleo
            {
                DropDownCurrencyValidate();
            }
            else if (_model.Type == InsuranceType.MedicalExpense) // Seguro gastos médicos
            {
                DropDownTypeInsurancePlanValidate();
                DropDownHowManyPersonsValidate();
            }
            else if (_model.Type == InsuranceType.Other) // Otros
            {
                DropDownTypeInsurancesValidate();
                if (OtherInsuranceEntryIsVisible == true)
                    OtherInsuranceValidate();
            }
            else if (_model.Type == InsuranceType.Bicycle) // Bicycle
            {
                DropDownBicycleCurrencyValidate();
            }

            ShowDataValidationsRequired();

            Debug.WriteLine("Check: CheckTermAndConditionsCommandExecute");
        }

        private void CheckAccidentAndAssistCommandExecute()
        {
            if (AccidentAndAssist == true)
                ValAccidentAndAssist = 1;
            else
                ValAccidentAndAssist = 0;

            Debug.WriteLine("Check: CheckAccidentAndAssistCommandExecute");
        }

        private async Task TermAndConditionsCommandExecute()
        {
            try
            {
                if (await Launcher.CanOpenAsync(_termsAndConditions))
                    await Launcher.OpenAsync(_termsAndConditions);
                else
                    await DisplayAlert(TextResources.InsuranceQuote_ErrorTitle, TextResources.InsuranceQuote_ErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception ex)
            {
                await DisplayAlert(TextResources.InsuranceQuote_ErrorTitle, TextResources.InsuranceQuote_ErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
                Debug.WriteLine("Exception TermAndConditionsCommandExecute: " + ex.Message);
            }
        }

        private async Task AccidentAndAssistCommandExecute()
        {
            try
            {
                //if (await Launcher.CanOpenAsync(_termsAndConditions))
                //    await Launcher.OpenAsync(_termsAndConditions);
                //else
                //    await DisplayAlert(TextResources.InsuranceQuote_ErrorTitle, TextResources.InsuranceQuote_ErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
            }
            catch (Exception ex)
            {
                //await DisplayAlert(TextResources.InsuranceQuote_ErrorTitle, TextResources.InsuranceQuote_ErrorMessage, TextResources.Alert_AcceptButtonText, AlertIconType.Error);
                Debug.WriteLine("Exception TermAndConditionsCommandExecute: " + ex.Message);
            }
        }

        private bool DropDownTypeIdValidate()
        {
            Debug.WriteLine("TypeId Changed");

            bool idValue = !string.IsNullOrWhiteSpace(TypeId) && !TypeId.Equals(TextResources.Request_ListPlaceholder);

            if (IdNumber == null)
                IdNumber = String.Empty;

            if (idValue == true)
            {
                ErrorTypeId = false;
                int id = Int32.Parse(TypeIds.Where(x => x.Name == TypeId).FirstOrDefault().Id);
                var idNumberFormat = IdNumber.Replace("-", "");

                if (idNumberFormat == String.Empty)
                {
                    ErrorId = true;
                    ErrorIdNumberResult = TextResources.Request_RequiredDataMessage;
                    idValue = false;
                }
                else if (id == 1 && idNumberFormat != null && idNumberFormat.Length != 9)
                {
                    ErrorId = true;
                    ErrorIdNumberResult = TextResources.Request_InvalidNacionalIdDataMessage;
                    idValue = false;
                }
                else if (id == 2 && IdNumber != null && IdNumber.Length != 12) // DIMEX
                {
                    ErrorId = true;
                    ErrorIdNumberResult = TextResources.Request_InvalidDimexIdDataMessage;
                    idValue = false;
                }
            }
            else
            {
                ErrorTypeId = true;
                ErrorIdResult = TextResources.Request_DropDownMessage;
            }

            return idValue;
        }

        private bool OtherInsuranceValidate()
        {
            Debug.WriteLine("OtherInsurance Changed");

            bool otherInsurance = !string.IsNullOrWhiteSpace(OtherInsuranceEntry);

            if (otherInsurance == false)
            {
                if (string.IsNullOrWhiteSpace(OtherInsuranceEntry))
                {
                    ErrorOtherInsuranceEntry = true;
                    ErrorOtherInsuranceEntryResult = TextResources.Request_RequiredDataMessage;
                    otherInsurance = false;
                }
                else if (!TextUtils.IsLetters(OtherInsuranceEntry))
                {
                    ErrorOtherInsuranceEntry = true;
                    ErrorOtherInsuranceEntryResult = TextResources.Request_ValidFormatDataMessage;
                    otherInsurance = false;
                }
                else
                {
                    ErrorOtherInsuranceEntry = false;
                    otherInsurance = true;
                }
            }
            else
            {
                otherInsurance = true;
            }

            return otherInsurance;
        }

        private bool DropDownGenderValidate()
        {
            Debug.WriteLine("Gender Changed");

            bool genderValue = !string.IsNullOrWhiteSpace(Gender) && !Gender.Equals(TextResources.Request_ListPlaceholder);

            if (!genderValue)
                ErrorGender = true;
            else
                ErrorGender = false;

            return genderValue;
        }

        private bool DropDownCurrencyValidate()
        {
            Debug.WriteLine("TypeCurrency Changed");

            bool currencyValue = !string.IsNullOrWhiteSpace(TypeCurrency) && !TypeCurrency.Equals(TextResources.Request_ListPlaceholder);

            if (!currencyValue)
                ErrorTypeCurrency = true;
            else
                ErrorTypeCurrency = false;

            return currencyValue;
        }

        private bool DropDownTypeInsurancePlanValidate()
        {
            Debug.WriteLine("TypeInsurancePlan Changed");

            bool typeInsurancePlanValue = !string.IsNullOrWhiteSpace(TypeInsurancePlan) && !TypeInsurancePlan.Equals(TextResources.Request_ListPlaceholder);

            if (!typeInsurancePlanValue)
                ErrorTypeInsurancePlan = true;
            else
                ErrorTypeInsurancePlan = false;

            return typeInsurancePlanValue;
        }

        private bool DropDownHowManyPersonsValidate()
        {
            Debug.WriteLine("HowManyPersons Changed");

            bool howManyPersonsValue = !string.IsNullOrWhiteSpace(HowMany) && !HowMany.Equals(TextResources.Request_ListPlaceholder);

            if (!howManyPersonsValue)
                ErrorHowMany = true;
            else
                ErrorHowMany = false;

            return howManyPersonsValue;
        }

        private bool DropDownTypeInsurancesValidate()
        {
            Debug.WriteLine("TypeInsurances Changed");

            bool typeInsurancesValue = !string.IsNullOrWhiteSpace(TypeInsurance) && !TypeInsurance.Equals(TextResources.Request_ListPlaceholder);

            if (!typeInsurancesValue)
                ErrorTypeInsurance = true;
            else
                ErrorTypeInsurance = false;

            return typeInsurancesValue;
        }

        private bool DropDownBicycleCurrencyValidate()
        {
            Debug.WriteLine("BicycleTypeCurrency Changed");

            bool currencyValue = !string.IsNullOrWhiteSpace(BicycleTypeCurrency) && !BicycleTypeCurrency.Equals(TextResources.Request_ListPlaceholder);

            if (!currencyValue)
                ErrorBicycleTypeCurrency = true;
            else
                ErrorBicycleTypeCurrency = false;

            return currencyValue;
        }

        private async Task ShowInsuranceQuoteHome()
        {
            bool idValue = DropDownTypeIdValidate();
            bool genderValue = DropDownGenderValidate();

            if (idValue == true && genderValue == true)
                await LoadHomeInsuranceQuotes();
            else
                Accept = false;
        }

        private async Task ShowInsuranceQuoteJobless()
        {
            bool idValue = DropDownTypeIdValidate();
            bool genderValue = DropDownGenderValidate();
            bool currencyValue = DropDownCurrencyValidate();

            if (idValue == true && genderValue == true && currencyValue == true)
                await LoadUnemploymentInsuranceQuotes();
            else
                Accept = false;
        }

        private async Task ShowInsuranceQuoteMedical()
        {
            bool idValue = DropDownTypeIdValidate();
            bool genderValue = DropDownGenderValidate();
            bool typeInsurancePlanValue = DropDownTypeInsurancePlanValidate();
            bool howManyPersonsValue = DropDownHowManyPersonsValidate();

            if (idValue == true && genderValue == true && typeInsurancePlanValue == true && howManyPersonsValue == true)
                await LoadMedicalInsuranceQuotes();
            else
                Accept = false;
        }

        private async Task ShowInsuranceQuoteOthers()
        {
            bool idValue = DropDownTypeIdValidate();
            bool genderValue = DropDownGenderValidate();
            bool typeInsurancesValue = DropDownTypeInsurancesValidate();
            bool otherInsuranceValue = true;
            if (OtherInsuranceEntryIsVisible == true)
                otherInsuranceValue = OtherInsuranceValidate();

            if (idValue == true && genderValue == true && typeInsurancesValue == true && otherInsuranceValue)
                await LoadOtherInsuranceQuotes();
            else
                Accept = false;
        }

        private async Task ShowInsuranceQuoteBicycle()
        {
            bool idValue = DropDownTypeIdValidate();
            bool genderValue = DropDownGenderValidate();
            bool currencyValue = DropDownBicycleCurrencyValidate();

            if (idValue == true && genderValue == true && currencyValue == true)
                await LoadBicycleInsuranceQuotes();
            else
                Accept = false;
            Debug.WriteLine("Load: ShowInsuranceBicycle");
        }
        #endregion

        #region WebServices
        private async Task<Models.Contact> GetContactInformation()
        {
            if (await IsConnected())
            {
                ShowProgressDialog(TextResources.InsuranceQuote_ProgressDialogSendingData);

                var service = WebServiceFactory.Create(WebServiceType.GetContacts);
                var response = await service.Execute<GetContactsResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    return response.Data[0];
                }
            }
            return null;
        }

        private async Task LoadHomeInsuranceQuotes()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.InsuranceQuote_ProgressDialogSendingData);

                var urlParams = GetHomeInsuranceQuotesParams();
                var service = WebServiceFactory.Create(WebServiceType.GetHomeAccidentQuotes, urlParams);
                var response = await service.Execute<GetInsuranceQuotesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    var contactInfo = await GetContactInformation();
                    var navigationParams = new NavigationParameters {
                        { "Quotes", response.Data },
                        { "Insurance", _model },
                        { "Contact", contactInfo }
                    };

                    await Navigation.NavigateAsync("CotizeInsurancePage", navigationParams);
                    Debug.WriteLine("Load: LoadHomeInsuranceQuotes");
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        private async Task LoadUnemploymentInsuranceQuotes()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.InsuranceQuote_ProgressDialogSendingData);

                var urlParams = GetUnemploymentInsuranceQuotesParams();
                var service = WebServiceFactory.Create(WebServiceType.GetUnemploymentQuotes, urlParams);
                var response = await service.Execute<GetInsuranceQuotesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    var contactInfo = await GetContactInformation();
                    var navigationParams = new NavigationParameters {
                        { "Quotes", response.Data },
                        { "Insurance", _model },
                        { "Contact", contactInfo }
                    };

                    await Navigation.NavigateAsync("CotizeInsurancePage", navigationParams);
                    Debug.WriteLine("Load: LoadUnemploymentInsuranceQuotes");
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        private async Task LoadMedicalInsuranceQuotes()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.InsuranceQuote_ProgressDialogSendingData);

                var urlParams = GetMedicalInsuranceQuotesParams();
                var service = WebServiceFactory.Create(WebServiceType.GetMedicalExpensesQuotes, urlParams);
                var response = await service.Execute<GetInsuranceQuotesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    var contactInfo = await GetContactInformation();
                    var navigationParams = new NavigationParameters {
                        { "Quotes", response.Data },
                        { "Insurance", _model },
                        { "Contact", contactInfo }
                    };

                    await Navigation.NavigateAsync("CotizeInsurancePage", navigationParams);
                    Debug.WriteLine("Load: LoadMedicalInsuranceQuotes");
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        private async Task LoadOtherInsuranceQuotes()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.InsuranceQuote_ProgressDialogSendingData);

                var urlParams = GetOtherInsuranceQuotesParams();
                var service = WebServiceFactory.Create(WebServiceType.GetOtherInsuranceQuotes, urlParams);
                var response = await service.Execute<GetInsuranceQuotesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful)
                {
                    await DisplayAlert(TextResources.InsuranceQuote_PopupTitle, response.Message, TextResources.AlertAcceptButton, AlertIconType.Mail);
                    await Navigation.NavigateAsync("/HomePage");
                    Debug.WriteLine("Return to: HomePage");
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        private async Task LoadBicycleInsuranceQuotes()
        {
            if (await IsConnected(true))
            {
                ShowProgressDialog(TextResources.InsuranceQuote_ProgressDialogSendingData);

                var urlParams = GetBicycleInsuranceQuotesParams();
                var service = WebServiceFactory.Create(WebServiceType.GetBicycleInsuranceQuotes, urlParams);
                var response = await service.Execute<GetInsuranceQuotesResponse>();

                DismissProgressDialog();

                if (response.IsSuccessful && response.Data?.Count > 0)
                {
                    var contactInfo = await GetContactInformation();
                    var navigationParams = new NavigationParameters {
                        { "Quotes", response.Data },
                        { "Insurance", _model },
                        { "Contact", contactInfo }
                    };

                    await Navigation.NavigateAsync("CotizeInsurancePage", navigationParams);
                    Debug.WriteLine("Load: LoadUnemploymentInsuranceQuotes");
                }
                else
                {
                    await DisplayAlert(TextResources.AlertErrorTitle, response.ErrorMessage, TextResources.AlertAcceptButton, AlertIconType.Error);
                }
            }
        }

        private string[] GetHomeInsuranceQuotesParams()
        {
            var urlParams = new string[10];
            SetPersonalInfoUrlParams(urlParams);
            return urlParams;
        }

        private string[] GetUnemploymentInsuranceQuotesParams()
        {
            var urlParams = new string[12];
            SetPersonalInfoUrlParams(urlParams);
            urlParams[10] = Currency.First(x => TypeCurrency.Equals(x.Name)).Value;
            urlParams[11] = Decimal.Parse(CreditQuote, CultureInfo.InvariantCulture).ToString();
            return urlParams;
        }

        private string[] GetMedicalInsuranceQuotesParams()
        {
            var urlParams = new string[12];
            SetPersonalInfoUrlParams(urlParams);
            urlParams[10] = HowManyPersons.First(x => HowMany.Equals(x.Name)).Value;
            urlParams[11] = TypeInsurancePlans.First(x => TypeInsurancePlan.Equals(x.Name)).Value;
            return urlParams;
        }

        private string[] GetOtherInsuranceQuotesParams()
        {
            var urlParams = new string[11];
            SetPersonalInfoUrlParams(urlParams);

            if (OtherInsuranceEntryIsVisible == true)
                urlParams[10] = OtherInsuranceEntry;
            else
                urlParams[10] = TypeInsurance.ToString();

            return urlParams;
        }

        private string[] GetBicycleInsuranceQuotesParams()
        {
            var urlParams = new string[14];
            SetPersonalInfoUrlParams(urlParams);
            urlParams[10] = BicycleCurrency.First(x => BicycleTypeCurrency.Equals(x.Name)).Value;
            urlParams[11] = Decimal.Parse(BicycleValue, CultureInfo.InvariantCulture).ToString();
            urlParams[12] = BicycleYear;
            urlParams[13] = ValAccidentAndAssist.ToString();
            return urlParams;
        }

        /// <summary>
        /// Inserts the personal data parameters in the string array.
        /// </summary>
        /// <param name="urlParams">MUST have a minimum size of 10.</param>
        private void SetPersonalInfoUrlParams(string[] urlParams)
        {
            urlParams[0] = _model.Id.ToString();
            urlParams[1] = Date.ToString("dd-MM-yyyy");
            urlParams[2] = Name;
            urlParams[3] = LastName;
            urlParams[4] = Email;
            urlParams[5] = "true";
            urlParams[6] = IdNumber;
            urlParams[7] = TypeIds.First(x => TypeId.Equals(x.Name)).Value;
            urlParams[8] = Genders.First(x => Gender.Equals(x.Name)).Value;
            urlParams[9] = Telephone;
        }

        private async void ClosePage()
        {
            await Navigation.GoBackAsync();
            Debug.WriteLine("Return to: ClosePage");
        }
        #endregion

        public void Initialize(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("Insurance") && parameters.ContainsKey("TermsAndConditions"))
            {
                _model = parameters.GetValue<Insurance>("Insurance");
                _termsAndConditions = parameters.GetValue<string>("TermsAndConditions");

                Title = _model.Name;

                SetUpInsuranceModel();
                SetUpGeneralDropDownInfo();

                if (_model.Type == InsuranceType.Unemployment && parameters.ContainsKey("Currencies"))
                {
                    var currencies = parameters.GetValue<List<Currency>>("Currencies");
                    SetUpUnemploymentDropDownInfo(currencies);
                }
                else if (_model.Type == InsuranceType.MedicalExpense && parameters.ContainsKey("MedicalCategories") && parameters.ContainsKey("MedicalClasses"))
                {
                    var categories = parameters.GetValue<List<MedicalInsuranceCategory>>("MedicalCategories");
                    var classes = parameters.GetValue<List<MedicalInsuranceClass>>("MedicalClasses");
                    SetUpMedicalExpensesDropDownInfo(categories, classes);
                }
                else if (_model.Type == InsuranceType.Other && parameters.ContainsKey("OtherInsurances"))
                {
                    var otherInsurances = parameters.GetValue<List<OtherInsurance>>("OtherInsurances");
                    SetUpOtherInsurancesDropDownInfo(otherInsurances);
                }
                else if (_model.Type == InsuranceType.Bicycle && parameters.ContainsKey("Currencies"))
                {
                    var currencies = parameters.GetValue<List<Currency>>("Currencies");
                    SetUpBicycleInsuranceDropDownInfo(currencies);
                }
            }
        }
    }
}
