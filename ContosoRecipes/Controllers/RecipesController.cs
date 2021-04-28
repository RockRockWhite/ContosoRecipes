using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContosoRecipes.Models;
using ContosoRecipes.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
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

        // TODO JsonPatchDocument
        [HttpPut("{id}")]
        public ActionResult UpdateRecipe(string id, Recipe newRecipe)
        {
            var recipe = _recipeService.Get(id);

            if (recipe == null)
            {
                return NotFound();
            }

            _recipeService.Update(id, newRecipe);

            return NoContent();
        }


        // TODO JsonPatchDocument
        [HttpPatch("{id}")]
        public ActionResult UpdateRecipe(string id, JsonPatchDocument<Recipe> recipesUpdates)
        {
            // TODO await
            var recipe = _recipeService.Get(id);

            if (recipe == null)
            {
                return NotFound();
            }

            // TODO ApplyTo
            recipesUpdates.ApplyTo(recipe);
            _recipeService.Update(id, recipe);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public ActionResult DeleteRecipes(string id)
        {
            var recipe = _recipeService.Get(id);

            if (recipe == null)
            {
                return NotFound();
            }

            _recipeService.Remove(recipe.Id);

            return NoContent();
        }
    }
}