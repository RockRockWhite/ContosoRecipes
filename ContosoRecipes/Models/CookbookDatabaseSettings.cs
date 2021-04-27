namespace ContosoRecipes.Models
{
    public class CookbookDatabaseSettings : ICookbookDatabaseSettings
    {
        public string CookbookCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICookbookDatabaseSettings
    {
        /* 接口类 */
        string CookbookCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}