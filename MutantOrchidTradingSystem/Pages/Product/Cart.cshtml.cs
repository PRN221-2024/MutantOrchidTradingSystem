using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace MutantOrchidTradingSysRazorPage.Pages.ProductDetail
{
    public class CartModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IDeductionRequestRepository _deductionRequestRepository;
        public List<Item> cartItems;
        int totalCount = 0;
        public decimal totalPrice;
        public CartModel(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IAccountRepository accountRepository, IDeductionRequestRepository deductionRequestRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _accountRepository = accountRepository;
            _deductionRequestRepository = deductionRequestRepository;
        }
        public void OnGet()
        {
            cartItems = GetCartItems();
            
            foreach (var item in cartItems)
            {
                totalCount += item.Quantity; 
            }
            _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", totalCount);
            
           
        }
        public IActionResult OnGetDelete(int id)
        {
            var cart = GetCartItems();
            var item = cart.FirstOrDefault(item => item.Product.Id == id);
            if (item != null)
            {
                cart.Remove(item);
                SaveCartItems(cart);
                
                

            }
            foreach (var p in cart)
            {
                totalCount += p.Quantity;
            }
            _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", totalCount);



            return RedirectToPage("/Product/Cart");
        }

        public IActionResult OnGetCheckOut()
        {
            string username = _httpContextAccessor.HttpContext.Session.GetString("username");
            cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                totalCount += item.Quantity;
            }
            _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", totalCount);
            if (username == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                

                var cart = GetCartItems();
                if(cart != null)
                {
                    var Order = new Order
                    {
                        Created = DateTime.Now,
                        Name = "Order",
                        Status = true,
                        AccountId = _httpContextAccessor.HttpContext.Session.GetInt32("Id")

                    };
                    var order = _orderRepository.Add(Order);
                    foreach (var item in cart)
                    {
                        var product = _productRepository.GetById(item.Product.Id);
                        if (product.Quantity < item.Quantity)
                        {
                            ModelState.AddModelError(string.Empty, $"{product.Name} không đủ.");
                            return Page();
                        }
                        else 
                        {
                            totalPrice = (decimal)(item.Quantity * item.Product.Price);
                            var account = _accountRepository.GetById(_httpContextAccessor.HttpContext.Session.GetInt32("Id").Value);
                            if (totalPrice > account.Balance)
                            {
                                ModelState.AddModelError(string.Empty, $"Số dư không đủ");
                                return Page();
                            }

                            product.Quantity -= item.Quantity;
                            _productRepository.UpdateProduct(product);

                            
                           
                            account.Balance -= totalPrice;
                            _accountRepository.Update(account);
                            
                           
                            var deductionRequest = new DeductionRequest
                            {
                                AccountId = account.Id,
                                Amount = totalPrice,
                                Date = DateTime.Now,
                                Status = "Success"
                            };
                            _deductionRequestRepository.Create(deductionRequest);
                            var orderDetail = new OrderDetail
                            {
                                OrderId = order.Id,
                                ProductId = item.Product.Id,
                                Quantity = item.Quantity,
                                Price = totalPrice
                            };
                            _orderDetailRepository.AddOrderDetail(orderDetail);
                        }
                        
                    }
                    _httpContextAccessor.HttpContext.Session.Remove("Cart");
                    _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", 0);
                }
                return RedirectToPage("/Index");
            }
        }

        public IActionResult OnGetBuy(int id)
        {
            cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                totalCount += item.Quantity;
            }
            _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", totalCount);
            var product = _productRepository.GetById(id);
            var cart = GetCartItems();
            if (cart == null)
            {
                cart = new List<Item>();
                cart.Add(new Item { Product = product, Quantity = 1 });
                SaveCartItems(cart);
            }
           
               var existingItem = cart.FirstOrDefault(item => item.Product.Id == id);
                if (existingItem != null)
                {
                    if (product.Quantity < existingItem.Quantity + 1)
                    {
                    ModelState.AddModelError(string.Empty, $"Product {product.Name} not enough in stock.");
                    return RedirectToPage("/Product/Cart");
                    }
                    existingItem.Quantity++;
                }
                else
                {
                    if (product.Quantity < 1)
                    {
                    ModelState.AddModelError(string.Empty, $"Product {product.Name} not enough in stock.");
                    return RedirectToPage("/Product/Cart");
                    }
                    cart.Add(new Item { Product = product, Quantity = 1 });
                }
                
                SaveCartItems(cart);
            foreach (var item in cart)
            {
                totalCount += item.Quantity;
            }
            _httpContextAccessor.HttpContext.Session.SetInt32("CartCount", totalCount);


            return RedirectToPage("/Product/Cart");
        }

        private List<Item> GetCartItems()
        {
            var cartJson = _httpContextAccessor.HttpContext.Session.GetString("Cart");
            return string.IsNullOrEmpty(cartJson) ? new List<Item>() : JsonConvert.DeserializeObject<List<Item>>(cartJson);
        }

        private void SaveCartItems(List<Item> cartItems)
        {
            _httpContextAccessor.HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));
        }
    }
}
