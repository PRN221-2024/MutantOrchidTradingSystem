using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public partial class Product
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Tên sản phẩm không được rỗng!")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Mô tả không được rỗng!")]
    [StringLength(500, MinimumLength = 30, ErrorMessage = "Mô tả phải có ít nhất 30 kí tự")]
    public string? Description { get; set; }
    public bool? Status { get; set; }
    [Required(ErrorMessage = "Giá không được rỗng!")]
    [Range(1, 10000000, ErrorMessage = "Giá phải nằm trong khoảng từ 1 đến 10,000,000")]
    public decimal? Price { get; set; }
    [Required(ErrorMessage = "Số lượng không được rỗng!")]
    [Range(1, 50, ErrorMessage = "Số lượng phải nằm trong khoảng từ 1 đến 50")]
    public int? Quantity { get; set; }
    [Required(ErrorMessage = "Đường dẫn không được rỗng!")]
    public string? Path { get; set; }

    public string? FileName { get; set; }

    public virtual ICollection<Auction> Auctions { get; set; } = new List<Auction>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public ValidationResult ProductNameNotIdentical(List<string> existingProductName)
    {
        if (Name != null && existingProductName.Contains(Name))
        {
            return new ValidationResult("Product Name đã tồn tại! Hãy nhập lại Product Name!");
        }
        return ValidationResult.Success;
    }
}
