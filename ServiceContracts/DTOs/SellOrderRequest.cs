using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTOs;

public class SellOrderRequest
{
    [Required(ErrorMessage = "Stock symbol is required.")]
    public string? StockSymbol { get; set; }

    [Required(ErrorMessage = "Stock name is required.")]
    public string? StockName { get; set; }

    [Required(ErrorMessage = "Date and time of order is required.")]
    [Range(typeof(DateTime), "2000-01-01", "2200-12-31", ErrorMessage = "Date must be between Jan 01, 2000 and Dec 31, 2200")]
    public DateTime DateAndTimeOfOrder { get; set; }

    [Range(1, 100000, ErrorMessage = "Quantity must be between 1 and 100000.")]
    public uint Quantity { get; set; }

    [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000.")]
    public double Price { get; set; }
}
