using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

public class ApiService
{
	private readonly HttpClient _httpClient;

	public ApiService()
	{
		_httpClient = new HttpClient();
		_httpClient.BaseAddress = new Uri("https://localhost:44336");
		_httpClient.DefaultRequestHeaders.Accept.Clear();
		_httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
	}

	public async Task<string> LoginAsync(string email, string password)
	{
		var response = await _httpClient.PostAsync($"api/accounts/login?email={email}&password={password}", null);
		if (response.IsSuccessStatusCode)
		{
			return await response.Content.ReadAsStringAsync();
		}
		return null;
	}
}
