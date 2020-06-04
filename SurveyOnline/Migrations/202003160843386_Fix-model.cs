namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fixmodel : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.QuestionSurveys", "QuestionTypeID");
            AddForeignKey("dbo.QuestionSurveys", "QuestionTypeID", "dbo.QuestionTypes", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionSurveys", "QuestionTypeID", "dbo.QuestionTypes");
            DropIndex("dbo.QuestionSurveys", new[] { "QuestionTypeID" });
        }
    }
}
