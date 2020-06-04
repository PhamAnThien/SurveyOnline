namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AnswerTemplates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestTypeID = c.Int(nullable: false),
                        AnswerUnitID = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AnswerUnits", t => t.AnswerUnitID, cascadeDelete: true)
                .ForeignKey("dbo.QuestionTypes", t => t.QuestTypeID, cascadeDelete: true)
                .Index(t => t.QuestTypeID)
                .Index(t => t.AnswerUnitID);
            
            CreateTable(
                "dbo.AnswerUnits",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuestionTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionTypeName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.JobTitles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.QuestionSurveys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        QuestionTypeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SurveyProfiles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Birthday = c.DateTime(nullable: false),
                        Gender = c.String(),
                        Address = c.String(),
                        JobID = c.Int(nullable: false),
                        WorkplaceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.JobTitles", t => t.JobID, cascadeDelete: true)
                .ForeignKey("dbo.Workplaces", t => t.WorkplaceID, cascadeDelete: true)
                .Index(t => t.JobID)
                .Index(t => t.WorkplaceID);
            
            CreateTable(
                "dbo.Workplaces",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        WorkplaceTypeID = c.Int(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        TaxCode = c.String(),
                        ManagerName = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.WorkplaceTypes", t => t.WorkplaceTypeID, cascadeDelete: true)
                .Index(t => t.WorkplaceTypeID);
            
            CreateTable(
                "dbo.WorkplaceTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyProfiles", "WorkplaceID", "dbo.Workplaces");
            DropForeignKey("dbo.Workplaces", "WorkplaceTypeID", "dbo.WorkplaceTypes");
            DropForeignKey("dbo.SurveyProfiles", "JobID", "dbo.JobTitles");
            DropForeignKey("dbo.AnswerTemplates", "QuestTypeID", "dbo.QuestionTypes");
            DropForeignKey("dbo.AnswerTemplates", "AnswerUnitID", "dbo.AnswerUnits");
            DropIndex("dbo.Workplaces", new[] { "WorkplaceTypeID" });
            DropIndex("dbo.SurveyProfiles", new[] { "WorkplaceID" });
            DropIndex("dbo.SurveyProfiles", new[] { "JobID" });
            DropIndex("dbo.AnswerTemplates", new[] { "AnswerUnitID" });
            DropIndex("dbo.AnswerTemplates", new[] { "QuestTypeID" });
            DropTable("dbo.WorkplaceTypes");
            DropTable("dbo.Workplaces");
            DropTable("dbo.SurveyProfiles");
            DropTable("dbo.QuestionSurveys");
            DropTable("dbo.JobTitles");
            DropTable("dbo.QuestionTypes");
            DropTable("dbo.AnswerUnits");
            DropTable("dbo.AnswerTemplates");
        }
    }
}
