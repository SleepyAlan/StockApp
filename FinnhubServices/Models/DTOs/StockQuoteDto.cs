using System.Text.Json.Serialization;
using FinnhubServices.Models.BusinessModels;

namespace FinnhubServices.Models.DTOs;

// https://finnhub.io/docs/api/quote
internal record StockQuoteDto(
    [property: JsonPropertyName("c")] decimal CurrentPrice,
    [property: JsonPropertyName("d")] decimal Change,
    [property: JsonPropertyName("dp")] decimal PercentChange,
    [property: JsonPropertyName("h")] decimal HighPrice,
    [property: JsonPropertyName("l")] decimal LowPrice,
    [property: JsonPropertyName("o")] decimal OpenPrice,
    [property: JsonPropertyName("pc")] decimal PreviousClose,
    [property: JsonPropertyName("t")] long Timestamp
)
{
    public static explicit operator CurrentStockQuote(StockQuoteDto dto)
    {
        return new CurrentStockQuote()
        {
            CurrentPriceToday = dto.CurrentPrice,
            Change = dto.Change,
            PercentChange = dto.Change,
            HighPriceOfTheDay = dto.HighPrice,
            LowPriceOfTheDay = dto.LowPrice,
            OpenPrice = dto.OpenPrice,
            PreviousClose = dto.PreviousClose
        };
    }
}