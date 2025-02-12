using System.Net;
using ChatRoomWithBot.Domain.Extensions;
using ChatRoomWithBot.Domain.Interfaces;
using ChatRoomWithBot.Domain.OperationResults;
using RestSharp;

namespace ChatRoomWithBot.Infra.HttpRequest.Infra.HttpRequest
{
    internal class HttpRequests : IHttpRequests
    {
        private readonly RestClient _client = new();

        public async Task<OperationResult<T>> PostAsync<T>(string baseUrl, string resource, object body, Dictionary<string, string> headers = null, string jwtToken = null)
        {
            try
            {
                baseUrl = baseUrl.RemoveTrailingSlash();
                var request = new RestRequest($"{baseUrl}/{resource}", Method.Post)
                    .AddJsonBody(body);

                AddHeaders(request, headers, jwtToken);

                var response = await _client.ExecuteAsync<T>(request);

                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.CreateFailureResult(ex.Message, default);
            }
        }

        public async Task<OperationResult<T>> GetAsync<T>(string baseUrl, string resource, Dictionary<string, string> headers = null, string jwtToken = null)
        {
            try
            {
                baseUrl = baseUrl.RemoveTrailingSlash();
                var request = new RestRequest($"{baseUrl}/{resource}", Method.Get);

                AddHeaders(request, headers, jwtToken);

                var response = await _client.ExecuteAsync<T>(request);

                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.CreateFailureResult(ex.Message, default);
            }
        }

        public async Task<OperationResult<T>> PutAsync<T>(string baseUrl, string resource, object requestBody, Dictionary<string, string> headers = null, string jwtToken = null)
        {
            try
            {
                baseUrl = baseUrl.RemoveTrailingSlash();
                var request = new RestRequest($"{baseUrl}/{resource}", Method.Put)
                    .AddJsonBody(requestBody);

                AddHeaders(request, headers, jwtToken);

                var response = await _client.ExecuteAsync<T>(request);

                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.CreateFailureResult(ex.Message, default);
            }
        }

        public async Task<OperationResult<T>> DeleteAsync<T>(string baseUrl, string resource, Dictionary<string, string> headers = null, string jwtToken = null)
        {
            try
            {
                baseUrl = baseUrl.RemoveTrailingSlash();
                var request = new RestRequest($"{baseUrl}/{resource}", Method.Delete);

                AddHeaders(request, headers, jwtToken);

                var response = await _client.ExecuteAsync<T>(request);

                return HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return OperationResult<T>.CreateFailureResult(ex.Message, default);
            }
        }

        private void AddHeaders(RestRequest request, Dictionary<string, string> headers = null, string jwtToken = null)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (!string.IsNullOrEmpty(jwtToken))
            {
                request.AddHeader("Authorization", $"Bearer {jwtToken}");
            }
        }

        private OperationResult<T> HandleResponse<T>(RestResponse<T> response)
        {
            if (!response.IsSuccessful)
            {
                return OperationResult<T>.CreateFailureResult($"Error: {response.StatusCode}", default);
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return OperationResult<T>.CreateSuccessResult(default);
            }


            if (typeof(T) == typeof(string) && response.Data == null)
            {
                return OperationResult<T>.CreateSuccessResult((T)(object)response.Content);
            }

            return OperationResult<T>.CreateSuccessResult(response.Data);
        }
    }
}
