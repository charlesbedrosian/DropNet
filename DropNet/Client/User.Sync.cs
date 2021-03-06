﻿#if !WINDOWS_PHONE

using DropNet.Models;
using RestSharp;
using System.Net;
using DropNet.Helpers;
using DropNet.Authenticators;

namespace DropNet
{
    public partial class DropNetClient
    {

        public UserLogin Login(string email, string password)
        {
            _restClient.BaseUrl = _apiBaseUrl;
            _restClient.Authenticator = new OAuthAuthenticator(_restClient.BaseUrl, _apiKey, _appsecret);

            var request = _requestHelper.CreateLoginRequest(_apiKey, email, password);

            var response = _restClient.Execute<UserLogin>(request);

            _userLogin = response.Data;

            return _userLogin;
        }

        public RestResponse CreateAccount(string email, string firstName, string lastName, string password)
        {
            _restClient.BaseUrl = _apiBaseUrl;

            var request = _requestHelper.CreateNewAccountRequest(_apiKey, email, firstName, lastName, password);

            return _restClient.Execute(request);
        }

        public AccountInfo Account_Info()
        {
            //This has to be here as Dropbox change their base URL between calls
            _restClient.BaseUrl = _apiBaseUrl;
            _restClient.Authenticator = new OAuthAuthenticator(_restClient.BaseUrl, _apiKey, _appsecret, _userLogin.Token, _userLogin.Secret);

            var request = _requestHelper.CreateAccountInfoRequest();

            var response = _restClient.Execute<AccountInfo>(request);

            return response.Data;
        }

    }
}
#endif