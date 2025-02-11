using ChatRoomWithBot.Domain.OperationResults;

namespace ChatRoomWithBot.Domain.Interfaces;

public interface IHttpRequests
{
	Task<OperationResult<T>> GetAsync<T>(string baseUrl, string resource, Dictionary<string, string>? headers = null, string jwtToken = null);

	Task<OperationResult<T>> PostAsync<T>(string baseUrl, string resource, object body,
		Dictionary<string, string>? headers = null, string jwtToken = null);


	 
	Task<OperationResult<T>> PutAsync<T>(string baseUrl, string resource, object requestBody,
		Dictionary<string, string>? headers = null, string jwtToken = null);


	Task<OperationResult<T>> DeleteAsync<T>(string baseUrl, string resource,
		Dictionary<string, string>? headers = null, string jwtToken = null);
}