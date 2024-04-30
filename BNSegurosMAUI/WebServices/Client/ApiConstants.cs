using System;
namespace BNSegurosMAUI.WebServices.Client
{
    public static class ApiConstants
    {
        public const int DefaultTimeout = 20;
        // PROD
        //public const string BaseUrl = "https://app.BNSegurosMAUI.com/api/v1/";
        //public const string BaseOpaUrl = "https://app.BNSegurosMAUI.com/html/BNCorredora.html?TipoFormulario=";
        // QA
        public const string BaseUrl = "http://bnsegurosqa.mobtion.net/api/v1/";
        public const string BaseOpaUrl = "http://bnsegurosqa.mobtion.net/html/BNCorredoraQA.html?TipoFormulario=";

        public const string Contacts = "contacts.json";
        public const string Agents = "brokers.json";
        public const string Assistance = "assistances.json";
        public const string Tips = "tips.json";
        public const string Advertisements = "advertisements.json";
        public const string Insurances = "insurances.json";
        public const string QuestionTypes = "question_types.json";
        public const string FrequentlyAskedQuestions = "questions.json";
        public const string TermsAndConditions = "terms_and_conditions.json";
        public const string Currencies = "currencies.json";
        public const string MedicalExpensesCategories = "medical_expenses_categories.json";
        public const string MedicalExpensesClasses = "medical_expenses_classes.json";
        public const string HomeAccidentQuotes = "home_accidents.json";
        public const string UnemploymentQuotes = "unemployment_insurances.json";
        public const string MedicalExpensesQuotes = "medical_expenses.json";
        public const string OtherInsuranceQuotes = "other_insurances.json";
        public const string SendQuote = "send_quotations.json";
        public const string Location = "bn_location.json";
        public const string OtherInsurances = "more_insurances.json";
        public const string BicycleInsurances = "bicycle_insurances.json";
        public const string Sinisters = "sinisters.json";
        public const string SelfIssuedInsurances = "self_issued_insurances.json";

        public const string GetHomeAccidentQuotesQuery = "?insurance_id={0}&date={1}&name={2}&lastname={3}&email={4}&terms_and_conditions={5}&identification={6}&identification_type={7}&gender={8}&telephone={9}";
        public const string GetUnemploymentQuotesQuery = "?insurance_id={0}&date={1}&name={2}&lastname={3}&email={4}&terms_and_conditions={5}&identification={6}&identification_type={7}&gender={8}&telephone={9}&currency={10}&loan_amount={11}";
        public const string GetMedicalExpensesQuotesQuery = "?insurance_id={0}&date={1}&name={2}&lastname={3}&email={4}&terms_and_conditions={5}&identification={6}&identification_type={7}&gender={8}&telephone={9}&id_category={10}&id_class={11}";
        public const string GetOtherInsuranceQuotesQuery = "?insurance_id={0}&date={1}&name={2}&lastname={3}&email={4}&terms_and_conditions={5}&identification={6}&identification_type={7}&gender={8}&telephone={9}&other_insurance_type={10}";
        public const string GetBicycleInsuranceQuotesQuery = "?insurance_id={0}&date={1}&name={2}&lastname={3}&email={4}&terms_and_conditions={5}&identification={6}&identification_type={7}&gender={8}&telephone={9}&currency={10}&byke_value={11}&bicycle_age={12}&accidents_and_assistance={13}";
    }
}