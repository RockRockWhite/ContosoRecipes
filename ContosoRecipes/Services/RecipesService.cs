using System.Collections.Generic;
using ContosoRecipes.Models;
using MongoDB.Driver;

namespace ContosoRecipes.Services
{
    public class RecipesService
    {
        private readonly IMongoCollection<Recipe> recipes;

        public RecipesService(CookbookDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            recipes = database.GetCollection<Recipe>(settings.CookbookCollectionName);
        }

        // TODO ture
        public List<Recipe> Get() => recipes.Find(recipe => true).ToList();

        // TODO FirstOrDefault
        public Recipe Get(string id) => recipes.Find(recipe => recipe.Id == id).FirstOrDefault();

        public Recipe Create(Recipe recipe)
        {
            recipes.InsertOne(recipe);
            return recipe;
        }

        public void Update(string id, Recipe recipe)
        {
            recipes.ReplaceOne(book => book.Id == id, recipe);
        }

        // TODO bookIn?
        public void Remove(Recipe recipeIn) => recipes.DeleteOne(recipe => recipe.Id == recipeIn.Id);
        public void Remove(string id) => recipes.DeleteOne(recipe => recipe.Id == id);
    }
}