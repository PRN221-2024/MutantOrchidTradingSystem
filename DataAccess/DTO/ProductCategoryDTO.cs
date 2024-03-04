using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class ProductCategoryDTO
    {
        public Category Category { get; set; }
        public int ProductCount { get; set; }


    }
}
