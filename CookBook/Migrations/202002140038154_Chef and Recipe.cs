namespace CookBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChefandRecipe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chefs",
                c => new
                    {
                        chefID = c.Int(nullable: false, identity: true),
                        chefName = c.String(),
                        experience = c.String(),
                        speciality = c.String(),
                    })
                .PrimaryKey(t => t.chefID);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeID = c.Int(nullable: false, identity: true),
                        RecipeName = c.String(),
                        Category = c.String(),
                        Cusine = c.String(),
                        Description = c.String(),
                        ChefID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeID)
                .ForeignKey("dbo.Chefs", t => t.ChefID, cascadeDelete: true)
                .Index(t => t.ChefID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recipes", "ChefID", "dbo.Chefs");
            DropIndex("dbo.Recipes", new[] { "ChefID" });
            DropTable("dbo.Recipes");
            DropTable("dbo.Chefs");
        }
    }
}
