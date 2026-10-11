using FinnhubServices.Interfaces;
using FinnhubServices.Models.DTOs;
using Microsoft.Extensions.Options;
using FinnhubServices.Options;
using System.Text.Json;
using FinnhubServices.Models.BusinessModels;

namespace FinnhubServices.Implementations;

public class FinnhubService(
    HttpClient httpClient,
    IOptions<ApiOptions> options) : IFinnhubService
{
    private readonly string _quoteUrl = "/api/v1/quote";
    private readonly string _companyProfileUrl = "/api/v1/stock/profile2";
    private readonly string _apiKey = options.Value.ApiKey;

    private static readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public async Task<CurrentStockQuote> GetStockPriceQuote(string symbol)
    {
        var url = $"{_quoteUrl}?symbol={symbol}&token={_apiKey}";
        var dto = await GetRequest<StockQuoteDto>(url);
        return (CurrentStockQuote)dto;
    }

    public async Task<CompanyProfile> GetCompanyProfile(string symbol)
    {
        var url = $"{_companyProfileUrl}?symbol={symbol}&token={_apiKey}";

        var dto = await GetRequest<CompanyProfileDto>(url);
        return (CompanyProfile)dto;
    }

    private async Task<T> GetRequest<T>(string relativeUrl)
    {

        using HttpResponseMessage response = await httpClient.GetAsync(
            relativeUrl, HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        using var stream = await response.Content.ReadAsStreamAsync();

        var dto = await JsonSerializer.DeserializeAsync<T>(stream, _jsonOptions);

        return dto ??
            throw new InvalidOperationException("No response from finnhub server.");
    }

}

