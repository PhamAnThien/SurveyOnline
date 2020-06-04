namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmodelclass2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.QuestionTypesGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.QuestionTypes", "QuestionTypeGroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionTypes", "QuestionTypeGroupID");
            AddForeignKey("dbo.QuestionTypes", "QuestionTypeGroupID", "dbo.QuestionTypesGroups", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionTypes", "QuestionTypeGroupID", "dbo.QuestionTypesGroups");
            DropIndex("dbo.QuestionTypes", new[] { "QuestionTypeGroupID" });
            DropColumn("dbo.QuestionTypes", "QuestionTypeGroupID");
            DropTable("dbo.QuestionTypesGroups");
        }
    }
}
