namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnewmodel3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ListTables", t => t.ParentID)
                .Index(t => t.ParentID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListTables", "ParentID", "dbo.ListTables");
            DropIndex("dbo.ListTables", new[] { "ParentID" });
            DropTable("dbo.ListTables");
        }
    }
}
