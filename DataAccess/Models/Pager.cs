using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Pager
    {
       

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public int TotalPage { get; set; }
        public int StartPage { get; set; }
        public int EndPage { get; set; }


        public Pager()
        {
        }

        public Pager(int totalItems, int page, int pageSize = 10)
        {
           int totalPages = (int) Math.Ceiling((decimal)totalItems / (decimal)PageSize);
            int currentPage = page;

            int startPage = currentPage - 4;
            int endPage = currentPage + 4;

            if (startPage <= 0)
            {
                endPage = endPage - (startPage - 1);
                startPage = 1;
            }
            if(endPage > totalPages)
            {
                endPage = totalPages - 1;
                if(endPage > 10)
                {
                    startPage = endPage - 9;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPage = totalPages;
            StartPage = startPage;
            EndPage = endPage;

        }



    }
}
