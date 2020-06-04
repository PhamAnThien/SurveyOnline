namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmodelclass4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionSurveys", "SurveySubjectID", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionSurveys", "isConditionQuestion", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuestionSurveys", "ConditionQuestionID", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionSurveys", "ConditionValue", c => c.String());
            AddColumn("dbo.QuestionSurveys", "ListSource", c => c.String());
            CreateIndex("dbo.QuestionSurveys", "SurveySubjectID");
            AddForeignKey("dbo.QuestionSurveys", "SurveySubjectID", "dbo.SurveySubjects", "ID", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionSurveys", "SurveySubjectID", "dbo.SurveySubjects");
            DropIndex("dbo.QuestionSurveys", new[] { "SurveySubjectID" });
            DropColumn("dbo.QuestionSurveys", "ListSource");
            DropColumn("dbo.QuestionSurveys", "ConditionValue");
            DropColumn("dbo.QuestionSurveys", "ConditionQuestionID");
            DropColumn("dbo.QuestionSurveys", "isConditionQuestion");
            DropColumn("dbo.QuestionSurveys", "SurveySubjectID");
        }
    }
}
