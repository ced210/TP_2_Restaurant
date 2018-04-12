namespace TP_2_Restaurant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CedEtDom : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RatingViews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Restaurant_Id = c.Int(nullable: false),
                        Rating_Date = c.DateTime(nullable: false),
                        Rater_Name = c.String(nullable: false, maxLength: 50),
                        Rating_Value = c.Double(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RatingViews");
        }
    }
}
