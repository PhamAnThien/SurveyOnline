namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewmodel2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WorkplaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Workplaces", t => t.WorkplaceID, cascadeDelete: true)
                .Index(t => t.WorkplaceID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "WorkplaceID", "dbo.Workplaces");
            DropIndex("dbo.Courses", new[] { "WorkplaceID" });
            DropTable("dbo.Courses");
        }
    }
}
