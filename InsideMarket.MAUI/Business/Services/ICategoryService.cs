using InsideMarket.MAUI.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsideMarket.MAUI.Business.Services;

public interface ICategoryService
{
    Task<List<Category>> GetAllCategories();
}
