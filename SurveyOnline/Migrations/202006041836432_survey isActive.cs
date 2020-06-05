namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class surveyisActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SurveySubjects", "isActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SurveySubjects", "isActive");
        }
    }
}
