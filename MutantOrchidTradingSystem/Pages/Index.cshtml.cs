using DataAccess.DTO;
using DataAccess.Models;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MutantOrchidTradingSysRazorPage.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ProductRepository _productRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly ProductCategoryRepository _productCategoryRepository;
        public IndexModel(ProductRepository productRepository, CategoryRepository categoryRepository, ProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _productCategoryRepository = productCategoryRepository;
        }
        public List<Product> listProducts { get; set; }
        public List<Category> categories { get; set; }
        public List<ProductCategoryDTO> productCategoryDTOs { get; set; }

        [BindProperty]
        public string SearchKeyword { get; set; }
        public void OnGet()
        {
            categories = _categoryRepository.GetAll();
            List<ProductCategoryDTO> listProductCategoryDTOs = new List<ProductCategoryDTO>();
            //display product count in each category
            foreach (var item in categories)
            {
               
                var productIdsInCategory = _productCategoryRepository.GetProductIdsByCategoryId(item.Id);
                ProductCategoryDTO productCategoryDTO = new ProductCategoryDTO();
                
                int productCount = 0;
                foreach (var productIds in productIdsInCategory)
                {
                    Product product = _productRepository.GetById(productIds);
                    if (product != null)
                    {
                        productCount++;
                        
                    }
                }
                //add product count to productCategoryDTO
              productCategoryDTO.Category = item;
              productCategoryDTO.ProductCount = productCount;
                
                listProductCategoryDTOs.Add(productCategoryDTO);

            }
            productCategoryDTOs = listProductCategoryDTOs;
            listProducts = _productRepository.GetAll();
        }
        public void OnGetByCategory(int categoryId)
        {
            
            var productIdsInCategory = _productCategoryRepository.GetProductIdsByCategoryId(categoryId);
            List<Product> productsInCategorys = new List<Product>();
            foreach (var item in productIdsInCategory)
            {
                Product product = _productRepository.GetById(item);
                if(product != null)
                {
                    productsInCategorys.Add(product);
                }
            }

            listProducts = productsInCategorys;


            categories = _categoryRepository.GetAll();
            List<ProductCategoryDTO> listProductCategoryDTOs = new List<ProductCategoryDTO>();
            foreach (var item in categories)
            {
                int productCount = 0;
                var productIdsInCategorys = _productCategoryRepository.GetProductIdsByCategoryId(item.Id);
                ProductCategoryDTO productCategoryDTO = new ProductCategoryDTO();

                foreach (var productIds in productIdsInCategorys)
                {
                    Product product = _productRepository.GetById(productIds);
                    if (product != null)
                    {
                        productCount++;
                        
                    }
                }

                productCategoryDTO.Category = item;
                productCategoryDTO.ProductCount = productCount;

                listProductCategoryDTOs.Add(productCategoryDTO);
            }
            productCategoryDTOs = listProductCategoryDTOs;

        }

        public IActionResult OnPost()
        {
            categories = _categoryRepository.GetAll();
            List<ProductCategoryDTO> listProductCategoryDTOs = new List<ProductCategoryDTO>();
            //display product count in each category
            foreach (var item in categories)
            {

                var productIdsInCategory = _productCategoryRepository.GetProductIdsByCategoryId(item.Id);
                ProductCategoryDTO productCategoryDTO = new ProductCategoryDTO();

                int productCount = 0;
                foreach (var productIds in productIdsInCategory)
                {
                    Product product = _productRepository.GetById(productIds);
                    if (product != null)
                    {
                        productCount++;

                    }
                }
                //add product count to productCategoryDTO
                productCategoryDTO.Category = item;
                productCategoryDTO.ProductCount = productCount;

                listProductCategoryDTOs.Add(productCategoryDTO);

            }
            productCategoryDTOs = listProductCategoryDTOs;
            listProducts = _productRepository.searchProduct(SearchKeyword);
            //return RedirectToPage("/Index", new { keyword = SearchKeyword });
            return Page();
        }
       
    }
}
