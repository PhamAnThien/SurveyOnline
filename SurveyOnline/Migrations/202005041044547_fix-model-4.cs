namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixmodel4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionSurveys", "QuestionTypeGroupID", c => c.Int());
            CreateIndex("dbo.QuestionSurveys", "QuestionTypeGroupID");
            AddForeignKey("dbo.QuestionSurveys", "QuestionTypeGroupID", "dbo.QuestionTypesGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionSurveys", "QuestionTypeGroupID", "dbo.QuestionTypesGroups");
            DropIndex("dbo.QuestionSurveys", new[] { "QuestionTypeGroupID" });
            DropColumn("dbo.QuestionSurveys", "QuestionTypeGroupID");
        }
    }
}
