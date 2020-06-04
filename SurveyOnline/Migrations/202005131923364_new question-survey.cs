namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newquestionsurvey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ListTables", "QuestionSurvey_ID", c => c.Int());
            CreateIndex("dbo.ListTables", "QuestionSurvey_ID");
            AddForeignKey("dbo.ListTables", "QuestionSurvey_ID", "dbo.QuestionSurveys", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ListTables", "QuestionSurvey_ID", "dbo.QuestionSurveys");
            DropIndex("dbo.ListTables", new[] { "QuestionSurvey_ID" });
            DropColumn("dbo.ListTables", "QuestionSurvey_ID");
        }
    }
}
