using System.ComponentModel;

namespace ShoppingCartAPI.Enums
{
    public class StatusHandlersEnum
    {
        public enum StatusHandle
        {
            [Description("Create Success")]
            CreateSuccess = 0,
            [Description("Update Success")]
            UpdateSuccess = 1,
            [Description("Delete Success")]
            DeleteSuccess = 2,
        }
    }
}