using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RecipeUIClassLib.Infrastructure.Interfaces;
using refit = Refit;

namespace RecipeUIClassLib.Areas.Recipes.Models
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IRecipeWebApi _recipeService;

        public IEnumerable<RecipeViewModel> Recipes { get; set; }

        public IndexModel(IRecipeWebApi recipeService)
        {
            _recipeService = recipeService ?? throw new ArgumentNullException(nameof(recipeService));
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                 Recipes = await _recipeService.ListAsync();

                return Page();
            }
            catch (refit.ValidationApiException validationException)
            {
                // handle validation here by using validationException.Content,
                // which is type of ProblemDetails according to RFC 7807

                // If the response contains additional properties on the problem details,
                // they will be added to the validationException.Content.Extensions collection.
            }
            catch (refit.ApiException exception)
            {
                // other exception handling
            }
            // TODO: is this correct?
            return Page();
        }
    }
}