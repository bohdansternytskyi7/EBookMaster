using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EBookMasterClassLibrary.Models;
using EBookMasterGUI.DTOs;
using Newtonsoft.Json;

public class ApiService
{
	private readonly HttpClient _httpClient;
	public string AccessToken { get; private set; }
	public string RefreshToken { get; private set; }

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
			var result = await response.Content.ReadAsStringAsync();
			var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(result);
			AccessToken = loginResponse.AccessToken;
			RefreshToken = loginResponse.RefreshToken;
			return result;
		}
		return null;
	}

	public async Task<List<BookDTO>> GetBooksAsync()
	{
		if (string.IsNullOrEmpty(AccessToken))
		{
			throw new InvalidOperationException("Access token is not available. Please login first.");
		}

		_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);

		var response = await _httpClient.GetAsync("api/bookborrowing/books");
		if (response.IsSuccessStatusCode)
		{
			var result = await response.Content.ReadAsStringAsync();
			var books = JsonConvert.DeserializeObject<List<Book>>(result);
			return books.Select(x => new BookDTO
			{
				Title = x.Title,
				Authors = string.Join(", ", x.Authors.Select(y => y.Name)),
				PublishingHouse = x.PublishingHouse.Name,
				PublicationYear = x.PublicationYear.Year,
				Series = x.Series?.Name ?? "",
				Categories = string.Join(", ", x.Categories.Select(y => y.Name)),
			}).ToList();
		}
		return null;
	}
}
