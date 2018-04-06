namespace TP_2_Restaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewBag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RestaurantViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Address = c.String(nullable: false, maxLength: 150),
                        ZipCode = c.String(nullable: false, maxLength: 8),
                        Phone = c.String(nullable: false, maxLength: 14),
                        Website = c.String(nullable: false, maxLength: 255),
                        Cuisine_Id = c.Int(nullable: false),
                        PriceRange_Id = c.Int(nullable: false),
                        BYOW = c.Boolean(nullable: false),
                        Rating = c.Double(nullable: false),
                        Logo_Id = c.String(),
                        Cuisine = c.String(),
                        PriceRange = c.String(),
                        NbRatings = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cuisines", "RestaurantView_Id", c => c.Int());
            AddColumn("dbo.PriceRanges", "RestaurantView_Id", c => c.Int());
            CreateIndex("dbo.Cuisines", "RestaurantView_Id");
            CreateIndex("dbo.PriceRanges", "RestaurantView_Id");
            AddForeignKey("dbo.Cuisines", "RestaurantView_Id", "dbo.RestaurantViews", "Id");
            AddForeignKey("dbo.PriceRanges", "RestaurantView_Id", "dbo.RestaurantViews", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PriceRanges", "RestaurantView_Id", "dbo.RestaurantViews");
            DropForeignKey("dbo.Cuisines", "RestaurantView_Id", "dbo.RestaurantViews");
            DropIndex("dbo.PriceRanges", new[] { "RestaurantView_Id" });
            DropIndex("dbo.Cuisines", new[] { "RestaurantView_Id" });
            DropColumn("dbo.PriceRanges", "RestaurantView_Id");
            DropColumn("dbo.Cuisines", "RestaurantView_Id");
            DropTable("dbo.RestaurantViews");
        }
    }
}
