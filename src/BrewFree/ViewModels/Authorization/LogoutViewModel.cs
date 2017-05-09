using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BrewFree.ViewModels.Authorization
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}
