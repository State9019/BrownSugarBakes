using System;
using BrownSugarBakes.TTR.DATA.EF;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using PagedList;
using BrownSugarBakes.UI.MVC.Repository;

namespace BrownSugarBakes.UI.MVC.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbBrownSugarBakesEntities context = new dbBrownSugarBakesEntities();

        public IPagedList<Tbl_Product> ListOfProducts { get; set; }

        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search", search??(object)DBNull.Value)
            };
            IPagedList<Tbl_Product> data = context.Database.SqlQuery<Tbl_Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1, pageSize);
            return new HomeIndexViewModel
            {
                ListOfProducts = data
            };
        }
    }

}