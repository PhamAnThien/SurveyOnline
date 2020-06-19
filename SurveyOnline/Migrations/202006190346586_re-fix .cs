namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class refix : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerCollections",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AnswerID = c.String(),
                        UserID = c.String(),
                        SurveySubjectId = c.Int(nullable: false),
                        SurveyGroupID = c.Int(nullable: false),
                        SurveyQuestionID = c.Int(nullable: false),
                        SurveyQuestionTypeID = c.Int(nullable: false),
                        SurveyAnswer = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AnswerCollections");
        }
    }
}
