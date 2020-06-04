namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_model_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionSurveys", "SurveyGroupID", c => c.Int(nullable: false));
            CreateIndex("dbo.QuestionSurveys", "SurveyGroupID");
            AddForeignKey("dbo.QuestionSurveys", "SurveyGroupID", "dbo.SurveyGroups", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionSurveys", "SurveyGroupID", "dbo.SurveyGroups");
            DropIndex("dbo.QuestionSurveys", new[] { "SurveyGroupID" });
            DropColumn("dbo.QuestionSurveys", "SurveyGroupID");
        }
    }
}
