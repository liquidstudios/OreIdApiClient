using System;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json;
using OreIdApiClient.Models;

namespace OreIdApiClient
{
    public class OreIdClient
    {
        private readonly HttpClient httpClient;

        private string baseUrl = "https://service.oreid.io";

        public OreIdClient() : this(new HttpClient())
        {

        }

        public OreIdClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <summary>
        /// Gets the app access token which is needed for authenticating users and signing transactions
        /// </summary>
        /// <param name="apiKey">The api-key from the ORE ID app you created</param>
        /// <param name="serviceKey">The service key for the ORE ID Enterprise app you created (only available with ORE ID Enterprise)</param>
        /// <param name="body">A request object used to set user's new password or reset it</param>
        /// <param name="cancellationToken"></param>
        /// <returns>The app access token</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApiException"></exception>
        public async Task<AppTokenResponse> GetAppTokenAsync(string apiKey, string serviceKey, AppTokenRequest body, CancellationToken cancellationToken = default)
        {

            if (body == null)
                body = new AppTokenRequest();

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
               .Append("/api/app-token");

            var url = urlBuilder.ToString();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            httpRequest.Headers.TryAddWithoutValidation("service-key", ConvertToString(serviceKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<AppTokenResponse>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Get the list of blockchain names and urls.
        /// </summary>
        /// <param name="type">Type of configuration data (type=chains will return infomration for all the chains)</param>
        /// <param name="apiKey">The api-key from the ORE ID app you created</param>
        /// <param name="cancellationToken"></param>
        /// <returns>List of blockchain names and urls</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApiException"></exception>
        public async Task<ChainsConfiguration> GetSettingsChainsConfigAsync(string type, string apiKey, CancellationToken cancellationToken = default)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
                .Append("/api/services/config?")
                .Append(Uri.EscapeDataString("type") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(type, CultureInfo.InvariantCulture)))
                .Append("&");

            urlBuilder.Length--;

            var url = urlBuilder.ToString();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ChainsConfiguration>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Gets user's information record
        /// </summary>
        /// <param name="account">User's unique ORE ID account</param>
        /// <param name="apiKey">The api-key from the ORE ID app you created</param>
        /// <param name="cancellationToken"></param>
        /// <returns>User's information record</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApiException"></exception>
        public async Task<UserInfo> GetUserAsync(string account, string apiKey, CancellationToken cancellationToken = default)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
                .Append("/api/account/user?")
                .Append(Uri.EscapeDataString("account") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(account, CultureInfo.InvariantCulture))).Append("&");
            urlBuilder.Length--;

            var url = urlBuilder.ToString();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserInfo>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Adds a public key to a user's existing ORE ID account
        /// </summary>
        /// <param name="account">User's unique ORE ID account</param>
        /// <param name="permission">A 12 character name with same naming rule as an EOS account name</param>
        /// <param name="parentPermission">The parent permission (defaults to 'active')</param>
        /// <param name="walletType">The wallet provider where the key is stored</param>
        /// <param name="chainAccount">A 12 character chain account name</param>
        /// <param name="chainNetwork">A network nickname as listed in the configuration response</param>
        /// <param name="publicKey">The public key to be added to the account</param>
        /// <param name="apiKey"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApiException"></exception>
        public async Task AddPermissionAsync(string account, string permission, string parentPermission, string walletType, string chainAccount, string chainNetwork, string publicKey, string apiKey, CancellationToken cancellationToken = default)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            if (permission == null)
                throw new ArgumentNullException(nameof(permission));
            if (parentPermission == null)
                throw new ArgumentNullException(nameof(parentPermission));
            if (walletType == null)
                throw new ArgumentNullException(nameof(walletType));
            if (chainAccount == null)
                throw new ArgumentNullException(nameof(chainAccount));
            if (chainNetwork == null)
                throw new ArgumentNullException(nameof(chainNetwork));
            if (publicKey == null)
                throw new ArgumentNullException(nameof(publicKey));


            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
                .Append("/api/account/add-permission?")
                .Append(Uri.EscapeDataString("account") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(account, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("permission") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(permission, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("parent-permission") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(parentPermission, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("wallet-type") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(walletType, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("chain-account") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(chainAccount, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("chain-network") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(chainNetwork, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("public-key") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(publicKey, CultureInfo.InvariantCulture)))
                .Append("&");
            urlBuilder.Length--;

            var url = urlBuilder.ToString();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));

            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);


            if (response.IsSuccessStatusCode)
            {
                // Example json response
                // {"processId":"GLOBAL_CONTEXT_PROCESS_ID_MISSING","message":"added key:EOS8RTHA4WXfdfdduGQp9R42TexVdXyEtijHTjRPpcFMP2pZzYEiaBhnD for chainAccount:ore1siwzstth on chain:eos_kylin added to OREID account:ore1siwzstth as permission:testpermission1"}
                // We assume the key is added successfully. If not an excepton will be thrown.
                return;
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Opens the browser to sign a transaction
        /// </summary>
        /// <param name="account">The user's ORE ID account name. This is often the same as the chain account but it does not have to be, as the user can have more than one chain account or they may be on a different chain</param>
        /// <param name="allowChainAccountSelection">Allow user to choose an alternate chain account (and permission) during the signing process. This would allow the user to choose an account in an external wallet (e.g. Scatter) to sign with. The selected account would replace the chain account and permission used to sign the transaction.</param>
        /// <param name="appAccessToken">The app access token from get access token endpoint</param>
        /// <param name="broadcast">If true, the transaction will be submitted after being signed</param>
        /// <param name="callbackUrl">The browser will redirect to this URL once the users sign in flow is completed. It must match one of the callback urls in the app settings. It must be URL encoded</param>
        /// <param name="chainAccount">The blockchain account/address of the user signing the transaction.</param>
        /// <param name="chainNetwork">The network nickname (e.g. eos_main, eos_kylin) as list int eh 'network' in the chain configuration response</param>
        /// <param name="returnSignedTransaction">If false, doesn't return signed transaction object in result</param>
        /// <param name="state"></param>
        /// <param name="transaction">base64 encoded JSON of the transaction</param>
        /// <param name="apiKey">The api-key from the ORE ID app you created</param>
        /// <param name="openBrowser">An action that opens the browser depending on the type of application you make. It should take the url string as input</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public void SignTransactionAsync(string account, bool allowChainAccountSelection, string appAccessToken, bool broadcast, string callbackUrl, string chainAccount, string chainNetwork, bool returnSignedTransaction, string state, string transaction, string apiKey, Action<string> openBrowser, CancellationToken cancellationToken = default)
        {
            if (account == null)
                throw new ArgumentNullException(nameof(account));
            if (appAccessToken == null)
                throw new ArgumentNullException(nameof(appAccessToken));
            if (callbackUrl == null)
                throw new ArgumentNullException(nameof(callbackUrl));
            if (chainAccount == null)
                throw new ArgumentNullException(nameof(chainAccount));
            if (chainNetwork == null)
                throw new ArgumentNullException(nameof(chainNetwork));
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (transaction == null)
                throw new ArgumentNullException(nameof(transaction));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
                .Append("/sign?")
                .Append(Uri.EscapeDataString("account") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(account, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("allow_chain_account_selection") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(allowChainAccountSelection, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("app_access_token") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(appAccessToken, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("broadcast") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(broadcast, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("callback_url") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(callbackUrl, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("chain_account") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(chainAccount, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("chain_network") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(chainNetwork, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("return_signed_transaction") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(returnSignedTransaction, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("state") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(state, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("transaction") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(transaction, CultureInfo.InvariantCulture)))
                .Append("&");
            urlBuilder.Length--;

            var url = urlBuilder.ToString();
            openBrowser(url);
        }

        /// <summary>
        /// Opens the browser to authenticate a user
        /// </summary>
        /// <param name="appAccessToken">The app access token</param>
        /// <param name="provider">Either 'facebook', 'linkedin' or others supported by ORE ID. If not provided, the user will be prompted with a UI to choose one of the supported providers</param>
        /// <param name="callbackUrl">The browser will redirect to this URL once the users sign in flow is completed. It must match one of the callback urls in the app settings. It must be URL encoded</param>
        /// <param name="backgroundColor">A background color in hex (eg: 0022FF). This provides the ability to customize the background color of the web pages in the login flow</param>
        /// <param name="state">Any URL safe string (no special characters). It will be passed back as parameter when the callback is called. You can use it to restore your websites state after the callback has been called</param>
        /// <param name="openBrowser">An action that opens the browser depending on the type of application you make. It should take the url string as input</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AuthenticateUser(string appAccessToken, string provider, string callbackUrl, string backgroundColor, string state, Action<string> openBrowser)
        {
            if (appAccessToken == null)
                throw new ArgumentNullException(nameof(appAccessToken));
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            if (callbackUrl == null)
                throw new ArgumentNullException(nameof(callbackUrl));
            if (backgroundColor == null)
                throw new ArgumentNullException(nameof(backgroundColor));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
                .Append("/auth?")
                .Append(Uri.EscapeDataString("app_access_token") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(appAccessToken, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("provider") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(provider, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("callback_url") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(callbackUrl, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("background_color") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(backgroundColor, CultureInfo.InvariantCulture)))
                .Append("&");
            if (state == null)
                urlBuilder.Append(Uri.EscapeDataString("state") + "=")
                    .Append(Uri.EscapeDataString(ConvertToString(state, CultureInfo.InvariantCulture)))
                    .Append("&");

            urlBuilder.Length--;
            var url = urlBuilder.ToString();

            // We are using an action because the browser is opened differently on different environments
            openBrowser(url);
        }

        /// <summary>
        /// Opens the browser to clear all login tokens in the user's browser (on the service.oreid.io path)
        /// </summary>
        /// <param name="appId">The app id from the ORE ID app you created</param>
        /// <param name="providers">Either 'all' or a comma-separated list of one or more providers e.g. 'facebook,google' or others we support</param>
        /// <param name="callbackUrl">The browser will redirect to this URL once the users sign in flow is completed. It must match one of the callback urls in the app settings. It must be URL encoded</param>
        /// <param name="state">Any URL safe string (no special characters). It will be passed back as parameter when the callback is called. You can use it to restore your websites state after the callback has been called</param>
        /// <param name="openBrowser">An action that opens the browser depending on the type of application you make. It should take the url string as input</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void Logout(string appId, string providers, string callbackUrl, string state, Action<string> openBrowser)
        {
            if (appId == null)
                throw new ArgumentNullException(nameof(appId));
            if (providers == null)
                throw new ArgumentNullException(nameof(providers));
            if (callbackUrl == null)
                throw new ArgumentNullException(nameof(callbackUrl));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
               .Append("/logout?")
               .Append(Uri.EscapeDataString("app_id") + "=")
               .Append(Uri.EscapeDataString(ConvertToString(appId, CultureInfo.InvariantCulture)))
               .Append("&")
               .Append(Uri.EscapeDataString("providers") + "=")
               .Append(Uri.EscapeDataString(ConvertToString(providers, CultureInfo.InvariantCulture)))
               .Append("&")
               .Append(Uri.EscapeDataString("callback_url") + "=")
               .Append(Uri.EscapeDataString(ConvertToString(callbackUrl, CultureInfo.InvariantCulture)))
               .Append("&");
            if (state == null)
                urlBuilder.Append(Uri.EscapeDataString("state") + "=")
                    .Append(Uri.EscapeDataString(ConvertToString(state, CultureInfo.InvariantCulture)))
                    .Append("&");
            urlBuilder.Length--;
            var url = urlBuilder.ToString();

            // We are using an action because the browser is opened differently on different environments
            openBrowser(url);
        }

        /// <summary>
        /// Verifies a code entered by the user, that was sent to his phone or email
        /// </summary>
        /// <param name="emailOrPhone">User's phone or email e.g. someone@example.com</param>
        /// <param name="provider">Either 'phone' or 'email'</param>
        /// <param name="code">Code sent by sms or email</param>
        /// <param name="apiKey">The api-key for your application on ORE ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An object indicating whether the code has been verified or not</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApiException"></exception>
        public async Task<LoginCodeVerifyResponse> PasswordlessLoginVerifyCodeAsync(string emailOrPhone, string provider, int code, string apiKey, CancellationToken cancellationToken = default)
        {
            if (emailOrPhone == null)
                throw new ArgumentNullException(nameof(emailOrPhone));
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
                .Append("/api/account/login-passwordless-verify-code?")
                .Append(Uri.EscapeDataString("email") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(emailOrPhone, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("provider") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(provider, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("code") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(code, CultureInfo.InvariantCulture)))
                .Append("&");
            urlBuilder.Length--;

            var url = urlBuilder.ToString();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();
            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LoginCodeVerifyResponse>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Sends an authentication code to the user via sms or email
        /// </summary>
        /// <param name="emailOrPhone">User's phone or email e.g. someone@example.com</param>
        /// <param name="provider">Either 'phone' or 'email'</param>
        /// <param name="apiKey">The api-key for your application on ORE ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An object indicating whether the code was sent or not</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ApiException"></exception>
        public async Task<PasswordlessLoginSendCodeResponse> PasswordlessLoginSendCodeAsync(string emailOrPhone, string provider, string apiKey, CancellationToken cancellationToken = default)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            if (emailOrPhone == null)
                throw new ArgumentNullException(nameof(emailOrPhone));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
                .Append("/api/account/login-passwordless-send-code?")
                .Append(Uri.EscapeDataString("provider") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(provider, CultureInfo.InvariantCulture)))
                .Append("&")
                .Append(Uri.EscapeDataString("email") + "=")
                .Append(Uri.EscapeDataString(ConvertToString(emailOrPhone, CultureInfo.InvariantCulture)))
                .Append("&");
            urlBuilder.Length--;

            var url = urlBuilder.ToString();
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url);

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);


            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<PasswordlessLoginSendCodeResponse>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Opens the browser to authenticate a user
        /// </summary>
        /// <param name="provider">Either 'phone' or 'email'</param>
        /// <param name="callbackUrl">The browser will redirect to this URL once the users sign in flow is completed. It must match one of the callback urls in the app settings. It must be URL encoded</param>
        /// <param name="backgroundColor">A background color in hex (eg: 0022FF). This provides the ability to customize the background color of the web pages in the login flow</param>
        /// <param name="state">Any URL safe string (no special characters). It will be passed back as parameter when the callback is called. You can use it to restore your websites state after the callback has been called</param>
        /// <param name="emailOrPhone">User's phone or email e.g. someone@example.com</param>
        /// <param name="code">Code sent by sms or email</param>
        /// <param name="appAccessToken">The app access token</param>
        /// <param name="openBrowser">An action that opens the browser depending on the type of application you make. It should take the url string as input</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public void PasswordlessLoginAuthenticateUser(string provider, string callbackUrl, string backgroundColor, string state, string emailOrPhone, int code, string appAccessToken, Action<string> openBrowser)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));
            if (callbackUrl == null)
                throw new ArgumentNullException(nameof(callbackUrl));
            if (backgroundColor == null)
                throw new ArgumentNullException(nameof(backgroundColor));
            if (state == null)
                throw new ArgumentNullException(nameof(state));
            if (emailOrPhone == null)
                throw new ArgumentNullException(nameof(emailOrPhone));
            if (appAccessToken == null)
                throw new ArgumentNullException(nameof(appAccessToken));

            // Todo: Change this to allow different scenarious for phone or email missing.
            // Add also validation for the same above above.
            // This should apply to all the passwordless logins.
            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "")
              .Append("/auth?")
              .Append(Uri.EscapeDataString("app_access_token") + "=")
              .Append(Uri.EscapeDataString(ConvertToString(appAccessToken, CultureInfo.InvariantCulture)))
              .Append("&")
              .Append(Uri.EscapeDataString("provider") + "=")
              .Append(Uri.EscapeDataString(ConvertToString(provider, CultureInfo.InvariantCulture)))
              .Append("&")
              .Append(Uri.EscapeDataString("callback_url") + "=")
              .Append(Uri.EscapeDataString(ConvertToString(callbackUrl, CultureInfo.InvariantCulture)))
              .Append("&")
              .Append(Uri.EscapeDataString("background_color") + "=")
              .Append(Uri.EscapeDataString(ConvertToString(backgroundColor, CultureInfo.InvariantCulture)))
              .Append("&").Append(Uri.EscapeDataString("state") + "=")
              .Append(Uri.EscapeDataString(ConvertToString(state, CultureInfo.InvariantCulture)))
              .Append("&").Append(Uri.EscapeDataString("email") + "=")
              .Append(Uri.EscapeDataString(ConvertToString(emailOrPhone, CultureInfo.InvariantCulture)))
              .Append("&").Append(Uri.EscapeDataString("code") + "=")
              .Append(Uri.EscapeDataString(ConvertToString(code, CultureInfo.InvariantCulture)))
              .Append("&");
            urlBuilder.Length--;

            var url = urlBuilder.ToString();
            // Todo: Find a way to inform the user on what they should expect from the HttpListener
            // What they should expect from the callback url: account=ore1sidysmep&state=state&process_id=77093c79ec5c
            // Incase the wrong code is passed: state=state&error_code=cant_verify_login_code&process_id=bd2d7c111af6
            openBrowser(url);
        }

        // Continue here when I wake up
        public async Task CustodialSignTransactionAsync(string apiKey, string serviceKey, CustodialSignTransactionRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/api/transaction/sign");

            var url = urlBuilder.ToString();
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            if (serviceKey == null)
                throw new ArgumentNullException(nameof(serviceKey));
            httpRequest.Headers.TryAddWithoutValidation("service-key", ConvertToString(serviceKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();
            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: Fill here what to do when it is a success status code
                // The response is some kind of object.
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task CustodialChangePasswordAsync(string apiKey, string serviceKey, CustodialChangePasswordRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/api/custodial/change-password");

            var url = urlBuilder.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));
            if (serviceKey == null)
                throw new ArgumentNullException(nameof(serviceKey));
            httpRequest.Headers.TryAddWithoutValidation("service-key", ConvertToString(serviceKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: What to do if response is a success status code
                // The response is an object. I should fix this later
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task CustodialMigrateAccountAsync(string apiKey, string serviceKey, CustodialMigrateAccountRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/api/custodial/migrate-account");

            var url = urlBuilder.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            if (serviceKey == null)
                throw new ArgumentNullException(nameof(serviceKey));
            httpRequest.Headers.TryAddWithoutValidation("service-key", ConvertToString(serviceKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: Fill what to do when it is a success status code. Probably check for a return value
                // The response should be an object
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<CanAutoSign> CanautoSignAsync(string apiKey, string serviceKey, CanautoSignRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/api/transaction/can-auto-sign");

            var url = urlBuilder.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            if (serviceKey == null)
                throw new ArgumentNullException(nameof(serviceKey));
            httpRequest.Headers.TryAddWithoutValidation("service-key", ConvertToString(serviceKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: CanAutoSign - go through this to confirm if it is correct.
                return JsonConvert.DeserializeObject<CanAutoSign>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task CustodialCreateAccountAsync(string apiKey, string serviceKey, CustodialCreateAccountRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));
            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/api/custodial/new-user");

            var url = urlBuilder.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            if (serviceKey == null)
                //throw new ArgumentNullException(nameof(serviceKey));
                httpRequest.Headers.TryAddWithoutValidation("service-key", ConvertToString(serviceKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: CustodialCreateAccount - What to do when we get a success status code
                // There should be a return object according to the documentation 
                Console.WriteLine("Response Content: " + await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task CustodialCreateChainAccountForUserAsync(string apiKey, string serviceKey, CreateChainAccountForUserRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/api/custodial/new-chain-account");

            var url = urlBuilder.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            if (serviceKey == null)
                //throw new ArgumentNullException(nameof(serviceKey));
                httpRequest.Headers.TryAddWithoutValidation("service-key", ConvertToString(serviceKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: CustodialCreateChainAccountForUser - What to do when we get a success status code
                // There should be a return object according to the documentation 
                Console.WriteLine("Response Content: " + await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task GetChainAccountKylinAsync(GetChainAccountRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var url = "https://api.kylin.alohaeos.com:443/v1/chain/get_account";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };
            var httpClient = new HttpClient();

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: GetChainAccountKylinAsync - Find out what the response if and generate the necessary object if required.
                // Should we also rename this function to ChainGetAccountKylin() ...?
                Console.WriteLine("task completed.");
                Console.WriteLine("Response content: " + await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task ChainHistoryGetTransactionKylinAsync(ChainHistoryGetTransactionKylinRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var url = "https://api.kylin.alohaeos.com:443/v1/history/get_transaction";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };
            var httpClient = new HttpClient();

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: ChainHistoryGetTransactionKylinAsync - Fill out what to do if success status code is received
                // Check if we get an object as a response
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task ChainHistoryGetTransactionEosMainAsync(ChainHistoryGetTransactionEosMainRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var url = "https://api.kylin.alohaeos.com:443/v1/chain/get_account";
            var httpRequest = new HttpRequestMessage(HttpMethod.Get, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };
            var httpClient = new HttpClient();

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: ChainHistoryGetTransactionEosMainAsync - Fill out what to do if we get a success status code
                // Is there a response object?
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<TokenAirdrop> TokenTransferAsync(string apiKey, string serviceKey, TokenTransferRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/api/token/airdrop");

            var url = urlBuilder.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            if (serviceKey == null)
                throw new ArgumentNullException(nameof(serviceKey));
            httpRequest.Headers.TryAddWithoutValidation("service-key", ConvertToString(serviceKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();
            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: TokenTranferAsync: Check this method to double check everything is okay.
                return JsonConvert.DeserializeObject<TokenAirdrop>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task TransactionComposeActionAsync(string apiKey, TransactionComposeActionRequest body, CancellationToken cancellationToken = default)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            var urlBuilder = new StringBuilder(baseUrl != null ? baseUrl.TrimEnd('/') : "").Append("/api/transaction/compose-action");

            var url = urlBuilder.ToString();

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json")
            };

            if (apiKey == null)
                throw new ArgumentNullException(nameof(apiKey));
            httpRequest.Headers.TryAddWithoutValidation("api-key", ConvertToString(apiKey, CultureInfo.InvariantCulture));

            cancellationToken.ThrowIfCancellationRequested();

            var response = await httpClient.SendAsync(httpRequest, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                // Todo: TransactionComposeAction: Find out if there should be a return object. 
                // I think there should be a return object.
            }
            else
            {
                throw new ApiException(await response.Content.ReadAsStringAsync());
            }
        }

        private string ConvertToString(object value, CultureInfo cultureInfo)
        {
            if (value == null)
            {
                return "";
            }

            if (value is Enum)
            {
                var name = Enum.GetName(value.GetType(), value);
                if (name != null)
                {
                    var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
                    if (field != null)
                    {
                        if (System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attribute)
                        {
                            return attribute.Value ?? name;
                        }
                    }

                    var converted = Convert.ToString(Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()), cultureInfo));
                    return converted;
                }
            }
            else if (value is bool b)
            {
                return Convert.ToString(b, cultureInfo).ToLowerInvariant();
            }
            else if (value is byte[] bytes)
            {
                return Convert.ToBase64String(bytes);
            }
            else if (value.GetType().IsArray)
            {
                var array = ((Array)value).OfType<object>();
                return string.Join(",", array.Select(o => ConvertToString(o, cultureInfo)));
            }

            var result = Convert.ToString(value, cultureInfo);
            return result ?? "";
        }
    }
}