namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmodelclass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubQuestions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionID = c.Int(nullable: false),
                        Question = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.QuestionSurveys", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubQuestions", "QuestionID", "dbo.QuestionSurveys");
            DropIndex("dbo.SubQuestions", new[] { "QuestionID" });
            DropTable("dbo.SubQuestions");
        }
    }
}
