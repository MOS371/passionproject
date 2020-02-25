namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ingredients : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientID = c.Int(nullable: false, identity: true),
                        IngredientName = c.String(),
                        IngredientCategory = c.String(),
                        IngredientNotes = c.String(),
                    })
                .PrimaryKey(t => t.IngredientID);
            
            CreateTable(
                "dbo.IngredientRecipes",
                c => new
                    {
                        Ingredient_IngredientID = c.Int(nullable: false),
                        Recipe_RecipeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ingredient_IngredientID, t.Recipe_RecipeID })
                .ForeignKey("dbo.Ingredients", t => t.Ingredient_IngredientID, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.Recipe_RecipeID, cascadeDelete: true)
                .Index(t => t.Ingredient_IngredientID)
                .Index(t => t.Recipe_RecipeID);
            
            AlterColumn("dbo.Chefs", "experience", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientRecipes", "Recipe_RecipeID", "dbo.Recipes");
            DropForeignKey("dbo.IngredientRecipes", "Ingredient_IngredientID", "dbo.Ingredients");
            DropIndex("dbo.IngredientRecipes", new[] { "Recipe_RecipeID" });
            DropIndex("dbo.IngredientRecipes", new[] { "Ingredient_IngredientID" });
            AlterColumn("dbo.Chefs", "experience", c => c.String());
            DropTable("dbo.IngredientRecipes");
            DropTable("dbo.Ingredients");
        }
    }
}
