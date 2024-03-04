using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;

namespace MutantOrchidTradingSysRazorPage.Pages.ProductDetail
{
    public class CartModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        public List<Item> cartItems;
        public CartModel(IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
        }
        public void OnGet()
        {
            cartItems = GetCartItems();
           
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
            return RedirectToPage("/Product/Cart");
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
