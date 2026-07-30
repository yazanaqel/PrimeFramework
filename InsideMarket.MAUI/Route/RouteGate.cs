using System;
using System.Collections.Generic;
using System.Text;

namespace InsideMarket.MAUI.Route;

public class RouteGate
{
    public static class UsersRouteGate
    {
        private const string ControllerName = "Users/";
        public const string Login = $"{ControllerName}Login";
        public const string Register = $"{ControllerName}Register";
        public const string Logout = $"{ControllerName}Logout";
        public const string GetUserProfile = "api/Home/GetUserProfile";

    }
    public static class CategoryRouteGate
    {
        private const string ControllerName = "api/Categories/";
        public const string GetAllCategories = $"{ControllerName}GetAllCategories";

    }
    public static class StoreRouteGate
    {
        private const string ControllerName = "api/Stores/";
        public const string GetOwnerStore = $"{ControllerName}GetOwnerStore";
        public const string GetStoreById = $"{ControllerName}GetStoreById";
        public const string GetAllStores = $"{ControllerName}GetAllStores";
        public const string CreateStore = $"{ControllerName}CreateStore";

    }
    public static class OrderRouteGate
    {
        private const string ControllerName = "api/Orders/";
        public const string CreateOrder = $"{ControllerName}CreateOrder";
        public const string GetUserOrders = $"{ControllerName}GetUserOrders";
        public const string GetStoreOrders = $"{ControllerName}GetStoreOrders";
        public const string GetOrderById = $"{ControllerName}GetOrderById";
        public const string ChangeOrderItemStatus = $"{ControllerName}ChangeOrderItemStatus";


    }
    public static class ProductRouteGate
    {
        private const string ControllerName = "api/Products/";
        public const string GetProductById = $"{ControllerName}GetProductById";
        public const string GetStoreProducts = $"{ControllerName}GetStoreProducts";
        public const string GetStoreProductsById = $"{ControllerName}GetStoreProductsById";
        public const string GetCategorizedProducts = $"{ControllerName}GetCategorizedProducts";
        public const string CreateProduct = $"{ControllerName}CreateProduct";

    }
}
