using Microsoft.AspNetCore.Mvc;

namespace RecipiecesWeb.Controllers
{
    [Route("[controller]")]
    [GenericControllerNameConvention] // Sets the controller name based on typeof(T).Name
    public class GenericController<T> : Controller
    {
        public virtual IActionResult Index()
        {
            return Content($"Hello from a generic {typeof(T).Name} controller.");
        }
    }
}