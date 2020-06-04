namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newmodel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SurveyGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        SurveySubjectId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.SurveySubjects", t => t.SurveySubjectId, cascadeDelete: true)
                .Index(t => t.SurveySubjectId);
            
            CreateTable(
                "dbo.SurveySubjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SubjectName = c.String(),
                        Description = c.String(),
                        DescriptionFooter = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SurveyGroups", "SurveySubjectId", "dbo.SurveySubjects");
            DropIndex("dbo.SurveyGroups", new[] { "SurveySubjectId" });
            DropTable("dbo.SurveySubjects");
            DropTable("dbo.SurveyGroups");
        }
    }
}
