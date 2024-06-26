﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
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

	public async Task<bool> LoginAsync(string email, string password)
	{
		var response = await _httpClient.PostAsync($"api/accounts/login?email={email}&password={password}", null);
		if (response.IsSuccessStatusCode)
		{
			var result = await response.Content.ReadAsStringAsync();
			var loginResponse = JsonConvert.DeserializeObject<LoginResponseDTO>(result);
			AccessToken = loginResponse.AccessToken;
			RefreshToken = loginResponse.RefreshToken;
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
			return true;
		}
		return false;
	}

	public async Task<List<BookDTO>> GetBooksAsync()
	{
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
				BookBorrowings = x.BookBorrowings.Select(y => new BookBorrowingDTO()
				{
					BorrowingDate = y.BorrowingDate,
					ReturnDate = y.ReturnDate
				}).ToList()
			}).ToList();
		}
		return null;
	}

	public async Task<List<BookBorrowingDTO>> GetBookBorrowHistoryAsync(string title, string authors)
	{
		var response = await _httpClient.GetAsync($"api/bookborrowing/borrowhistory?title={title}&authors={authors}");
		if (response.IsSuccessStatusCode)
		{
			var result = await response.Content.ReadAsStringAsync();
			var books = JsonConvert.DeserializeObject<List<BookBorrowingDTO>>(result);
			return books.Select(x => new BookBorrowingDTO
			{
				BorrowingDate = x.BorrowingDate,
				ReturnDate = x.ReturnDate
			}).ToList();
		}
		return null;
	}

	public async void BorrowBookAsync(string title, string authors)
	{
		var response = await _httpClient.PostAsync($"api/bookborrowing/borrow?title={title}&authors={authors}", null);
		HandleBookResponse(response);

	}

	public async void ReturnBookAsync(string title, string authors)
	{
		var response = await _httpClient.PostAsync($"api/bookborrowing/return?title={title}&authors={authors}", null);
		HandleBookResponse(response, false);
	}

	private async void HandleBookResponse(HttpResponseMessage httpResponseMessage, bool borrow = true)
	{
		if (httpResponseMessage.IsSuccessStatusCode)
		{
			MessageBox.Show($"Book {(borrow ? "borrowed" : "returned")} successfully.", "Success", MessageBoxButtons.OK);
		}
		else if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.BadRequest)
		{
			var errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
			MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
		else
		{
			var errorMessage = await httpResponseMessage.Content.ReadAsStringAsync();
			MessageBox.Show($"Error: {httpResponseMessage.StatusCode}\n{errorMessage}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
