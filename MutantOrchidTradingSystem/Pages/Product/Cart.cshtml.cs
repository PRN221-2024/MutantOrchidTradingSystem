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
        public List<Item> cartItems;
        int totalCount = 0;
        public CartModel(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IAccountRepository accountRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _accountRepository = accountRepository;
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
            if (username == null)
            {
                return RedirectToPage("/Login");
            }
            else
            {
                var Order = new Order
                {
                    Created = DateTime.Now,
                    Name = "Order",
                    Status = true,
                    AccountId = _httpContextAccessor.HttpContext.Session.GetInt32("Id")

                };
                var order = _orderRepository.Add(Order);

                var cart = GetCartItems();
                if(cart != null)
                {
                    foreach (var item in cart)
                    {
                        var product = _productRepository.GetById(item.Product.Id);
                        if (product.Quantity < item.Quantity)
                        {
                            ModelState.AddModelError(string.Empty, $"Product {product.Name} not enough to.");
                        }
                        else 
                        {
                            
                            
                            product.Quantity -= item.Quantity;
                            _productRepository.UpdateProduct(product);

                            
                            var account = _accountRepository.GetById(_httpContextAccessor.HttpContext.Session.GetInt32("Id").Value);
                            account.Balance -= (item.Quantity * item.Product.Price);
                            _accountRepository.Update(account);
                            decimal total = (decimal)(item.Quantity * item.Product.Price);
                            var orderDetail = new OrderDetail
                            {
                                OrderId = order.Id,
                                ProductId = item.Product.Id,
                                Quantity = item.Quantity,
                                Price = total
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
                    existingItem.Quantity++;
                }
                else
                {
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
