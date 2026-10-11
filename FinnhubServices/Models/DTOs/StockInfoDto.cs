using System.Text.Json.Serialization;
using FinnhubServices.Models.BusinessModels;

namespace FinnhubServices.Models.DTOs;

// https://finnhub.io/docs/api/stock-symbols
internal record StockInfoDto(
    [property: JsonPropertyName("symbol")] string Symbol,
    [property: JsonPropertyName("displaySymbol")] string DisplaySymbol,
    [property: JsonPropertyName("description")] string SymbolDescription,
    [property: JsonPropertyName("type")] string SecurityType,
    [property: JsonPropertyName("currency")] string Currency,
    [property: JsonPropertyName("mic")] string MarketIdentifierCode,
    [property: JsonPropertyName("figi")] string Figi
)
{
    public static explicit operator StockInfo(StockInfoDto dto)
    {
        return new StockInfo()
        {
            Symbol = dto.Symbol,
            DisplaySymbol = dto.DisplaySymbol,
            SymbolDescription = dto.SymbolDescription,
            SecurityType = dto.SecurityType,
            Currency = dto.Currency
        };
    }
}
