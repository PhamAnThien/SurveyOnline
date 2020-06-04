namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmodelclass5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuestionSurveys", "SurveyGroupID", "dbo.SurveyGroups");
            DropForeignKey("dbo.QuestionSurveys", "SurveySubjectID", "dbo.SurveySubjects");
            DropIndex("dbo.QuestionSurveys", new[] { "SurveySubjectID" });
            DropIndex("dbo.QuestionSurveys", new[] { "SurveyGroupID" });
            AlterColumn("dbo.QuestionSurveys", "Sorter", c => c.Int());
            AlterColumn("dbo.QuestionSurveys", "SurveySubjectID", c => c.Int());
            AlterColumn("dbo.QuestionSurveys", "SurveyGroupID", c => c.Int());
            AlterColumn("dbo.QuestionSurveys", "ConditionQuestionID", c => c.Int());
            CreateIndex("dbo.QuestionSurveys", "SurveySubjectID");
            CreateIndex("dbo.QuestionSurveys", "SurveyGroupID");
            AddForeignKey("dbo.QuestionSurveys", "SurveyGroupID", "dbo.SurveyGroups", "ID");
            AddForeignKey("dbo.QuestionSurveys", "SurveySubjectID", "dbo.SurveySubjects", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuestionSurveys", "SurveySubjectID", "dbo.SurveySubjects");
            DropForeignKey("dbo.QuestionSurveys", "SurveyGroupID", "dbo.SurveyGroups");
            DropIndex("dbo.QuestionSurveys", new[] { "SurveyGroupID" });
            DropIndex("dbo.QuestionSurveys", new[] { "SurveySubjectID" });
            AlterColumn("dbo.QuestionSurveys", "ConditionQuestionID", c => c.Int(nullable: true));
            AlterColumn("dbo.QuestionSurveys", "SurveyGroupID", c => c.Int(nullable: true));
            AlterColumn("dbo.QuestionSurveys", "SurveySubjectID", c => c.Int(nullable: true));
            AlterColumn("dbo.QuestionSurveys", "Sorter", c => c.Int(nullable: true));
            CreateIndex("dbo.QuestionSurveys", "SurveyGroupID");
            CreateIndex("dbo.QuestionSurveys", "SurveySubjectID");
            AddForeignKey("dbo.QuestionSurveys", "SurveySubjectID", "dbo.SurveySubjects", "ID", cascadeDelete: true);
            AddForeignKey("dbo.QuestionSurveys", "SurveyGroupID", "dbo.SurveyGroups", "ID", cascadeDelete: true);
        }
    }
}
