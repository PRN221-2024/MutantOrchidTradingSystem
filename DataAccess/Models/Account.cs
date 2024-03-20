using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models;

public partial class Account
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Tài khoản không được rỗng!")]
    public string? Username { get; set; }
    [Required(ErrorMessage = "Mật khẩu không được rỗng!")]
    [StringLength(12, MinimumLength = 6, ErrorMessage = "Mật khẩu từ 6 - 12 kí tự, vui lòng nhập lại!")]
    public string? Password { get; set; }
    [Required(ErrorMessage = "Họ và tên không được rỗng!")]
    public string? FullName { get; set; }
    [EmailAddress(ErrorMessage = "Địa chỉ email phải có @")]
    [Required(ErrorMessage = "Email không được rỗng!")]
    public string? Email { get; set; }

    public bool? Status { get; set; }
    [Required(ErrorMessage = "Địa Chỉ không được rỗng!")]
    public string? Address { get; set; }
    [Required(ErrorMessage = "Số Điện Thoại không được rỗng!")]
    [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại phải 10 số!")]
    public string? Phone { get; set; }

    public decimal? Balance { get; set; }

    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();

    public virtual ICollection<DepositRequest> DepositRequests { get; set; } = new List<DepositRequest>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<RoleAccount> RoleAccounts { get; set; } = new List<RoleAccount>();

    public ValidationResult UsernameNotIdentical(List<string> existingUsernames)
    {
        if (Username != null && existingUsernames.Contains(Username))
        {
            return new ValidationResult("Username đã tồn tại! Hãy nhập lại username!");
        }
        return ValidationResult.Success;
    }
}
