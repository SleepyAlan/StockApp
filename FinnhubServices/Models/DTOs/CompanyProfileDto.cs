using System.Text.Json.Serialization;
using FinnhubServices.Models.BusinessModels;

namespace FinnhubServices.Models.DTOs;

// https://finnhub.io/docs/api/company-profile2
internal record CompanyProfileDto(
    [property: JsonPropertyName("ticker")] string Ticker,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("currency")] string Currency,
    [property: JsonPropertyName("exchange")] string Exchange,
    [property: JsonPropertyName("ipo")] string? Ipo,
    [property: JsonPropertyName("marketCapitalization")] decimal MarketCapitalization,
    [property: JsonPropertyName("shareOutstanding")] decimal ShareOutstanding,
    [property: JsonPropertyName("phone")] string? Phone,
    [property: JsonPropertyName("weburl")] string? Weburl,
    [property: JsonPropertyName("logo")] string? Logo,
    [property: JsonPropertyName("finnhubIndustry")] string? FinnhubIndustry)
{

    public static explicit operator CompanyProfile(CompanyProfileDto dto)
    {
        return new CompanyProfile()
        {
            TickerSymbol = dto.Ticker,
            Name = dto.Name,
            Country = dto.Country,
            Currency = dto.Currency,
            Exchange = dto.Exchange,
            MarketCapitalization = dto.MarketCapitalization,
            SharesOutstanding = dto.ShareOutstanding,
            PhoneNumber = dto.Phone,
            WebUrl = dto.Weburl,
            LogoUrl = dto.Logo,
            Industry = dto.FinnhubIndustry,
            IpoDate = DateTime.TryParse(dto.Ipo, out var parsedDate)
                    ? parsedDate : null
        };
    }
}