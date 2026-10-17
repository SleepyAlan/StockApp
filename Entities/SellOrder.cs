using System.ComponentModel.DataAnnotations;

namespace Entities;

public class SellOrder
{
    public Guid SellOrderID { get; set; }

    [Required(ErrorMessage = "Stock symbol is required.")]
    public string? StockSymbol { get; set; }

    [Required(ErrorMessage = "Stock name is required.")]
    public string? StockName { get; set; }

    [Required(ErrorMessage = "Date and time of order is required.")]
    public DateTime DateAndTimeOfOrder { get; set; }

    [Range(1, 100000, ErrorMessage = "Quantity must be between 1 and 100000.")]
    public uint Quantity { get; set; }

    [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000.")]
    public double Price { get; set; }
}