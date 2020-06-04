namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewmodel1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "WorkplaceID", c => c.Int(nullable: false));
            AddColumn("dbo.Departments", "WorkplaceID", c => c.Int(nullable: false));
            CreateIndex("dbo.Branches", "WorkplaceID");
            CreateIndex("dbo.Departments", "WorkplaceID");
            AddForeignKey("dbo.Branches", "WorkplaceID", "dbo.Workplaces", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Departments", "WorkplaceID", "dbo.Workplaces", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Departments", "WorkplaceID", "dbo.Workplaces");
            DropForeignKey("dbo.Branches", "WorkplaceID", "dbo.Workplaces");
            DropIndex("dbo.Departments", new[] { "WorkplaceID" });
            DropIndex("dbo.Branches", new[] { "WorkplaceID" });
            DropColumn("dbo.Departments", "WorkplaceID");
            DropColumn("dbo.Branches", "WorkplaceID");
        }
    }
}
