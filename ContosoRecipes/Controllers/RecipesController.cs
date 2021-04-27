using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoRecipes.Models;
using ContosoRecipes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContosoRecipes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        // TODO readonly
        private readonly RecipesService _recipeService;

        public RecipesController(RecipesService recipesService) => _recipeService = recipesService;

        [HttpGet]
        public ActionResult GetRecipes([FromQuery] int index)
        {
            var recipes = _recipeService.Get();

            if (recipes.Any())
            {
                return Ok(recipes.Take(index));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult CreateNewRecipe([FromBody] Recipe newRecipe)
        {
            _recipeService.Create(newRecipe);
            bool badThingsHappend = false;
            if (badThingsHappend)
            {
                return BadRequest();
            }
            else
            {
                return Created("", newRecipe);
            }
        }

        [HttpDelete]
        public ActionResult DeleteRecipes()
        {
            bool badThingsHappend = false;
            if (badThingsHappend)
            {
                return BadRequest();
            }
            else
            {
                return NoContent();
            }
        }
    }
}