using System;
using System.Net.Http;
using BNSegurosMAUI.WebServices.Client;

namespace BNSegurosMAUI.WebServices
{
    public enum WebServiceType
    {
        GetContacts,
        GetAgents,
        GetAssistance,
        GetTips,
        GetAdvertisements,
        GetInsurances,
        GetQuestionTypes,
        GetFrequentlyAskedQuestions,
        GetTermsAndConditions,
        GetCurrencies,
        GetMedicalExpensesCategories,
        GetMedicalExpensesClasses,
        GetOtherInsurances,
        GetHomeAccidentQuotes,
        GetUnemploymentQuotes,
        GetMedicalExpensesQuotes,
        GetOtherInsuranceQuotes,
        GetBicycleInsuranceQuotes,
        SendQuote,
        GetLocation,
        GetSinisters,
        GetOnlineInsurances
    }

    public static class WebServiceFactory
    {
        public static WebService Create(WebServiceType type, string[] urlParams = null, IRequestBody requestBody = null)
        {
            WebService service = null;

            switch (type)
            {
                case WebServiceType.GetContacts:
                    service = new WebService(ApiConstants.Contacts, HttpMethod.Get);
                    break;
                case WebServiceType.GetAgents:
                    service = new WebService(ApiConstants.Agents, HttpMethod.Get);
                    break;
                case WebServiceType.GetAssistance:
                    service = new WebService(ApiConstants.Assistance, HttpMethod.Get);
                    break;
                case WebServiceType.GetTips:
                    service = new WebService(ApiConstants.Tips, HttpMethod.Get);
                    break;
                case WebServiceType.GetAdvertisements:
                    service = new WebService(ApiConstants.Advertisements, HttpMethod.Get);
                    break;
                case WebServiceType.GetInsurances:
                    service = new WebService(ApiConstants.Insurances, HttpMethod.Get);
                    break;
                case WebServiceType.GetQuestionTypes:
                    service = new WebService(ApiConstants.QuestionTypes, HttpMethod.Get);
                    break;
                case WebServiceType.GetFrequentlyAskedQuestions:
                    service = new WebService(ApiConstants.FrequentlyAskedQuestions, HttpMethod.Get);
                    break;
                case WebServiceType.GetTermsAndConditions:
                    service = new WebService(ApiConstants.TermsAndConditions, HttpMethod.Get);
                    break;
                case WebServiceType.GetCurrencies:
                    service = new WebService(ApiConstants.Currencies, HttpMethod.Get);
                    break;
                case WebServiceType.GetMedicalExpensesCategories:
                    service = new WebService(ApiConstants.MedicalExpensesCategories, HttpMethod.Get);
                    break;
                case WebServiceType.GetMedicalExpensesClasses:
                    service = new WebService(ApiConstants.MedicalExpensesClasses, HttpMethod.Get);
                    break;
                case WebServiceType.GetOtherInsurances:
                    service = new WebService(ApiConstants.OtherInsurances, HttpMethod.Get);
                    break;
                case WebServiceType.GetHomeAccidentQuotes:
                    service = new WebService(CreateQueryString(ApiConstants.HomeAccidentQuotes, ApiConstants.GetHomeAccidentQuotesQuery, urlParams), HttpMethod.Get);
                    break;
                case WebServiceType.GetUnemploymentQuotes:
                    service = new WebService(CreateQueryString(ApiConstants.UnemploymentQuotes, ApiConstants.GetUnemploymentQuotesQuery, urlParams), HttpMethod.Get);
                    break;
                case WebServiceType.GetMedicalExpensesQuotes:
                    service = new WebService(CreateQueryString(ApiConstants.MedicalExpensesQuotes, ApiConstants.GetMedicalExpensesQuotesQuery, urlParams), HttpMethod.Get);
                    break;
                case WebServiceType.GetOtherInsuranceQuotes:
                    service = new WebService(CreateQueryString(ApiConstants.OtherInsuranceQuotes, ApiConstants.GetOtherInsuranceQuotesQuery, urlParams), HttpMethod.Get);
                    break;
                case WebServiceType.GetBicycleInsuranceQuotes:
                    service = new WebService(CreateQueryString(ApiConstants.BicycleInsurances, ApiConstants.GetBicycleInsuranceQuotesQuery, urlParams), HttpMethod.Get);
                    break;
                case WebServiceType.SendQuote:
                    service = new WebService(ApiConstants.SendQuote, HttpMethod.Post, requestBody);
                    break;
                case WebServiceType.GetLocation:
                    service = new WebService(ApiConstants.Location, HttpMethod.Get);
                    break;
                case WebServiceType.GetSinisters:
                    service = new WebService(ApiConstants.Sinisters, HttpMethod.Get);
                    break;
                case WebServiceType.GetOnlineInsurances:
                    service = new WebService(ApiConstants.SelfIssuedInsurances, HttpMethod.Get);
                    break;
            }
            return service;
        }

        private static string CreateQueryString(string serviceName, string serviceQuery, string[] urlParams)
        {
            return string.Format("{0}{1}", serviceName, string.Format(serviceQuery, urlParams));
        }
    }
}
