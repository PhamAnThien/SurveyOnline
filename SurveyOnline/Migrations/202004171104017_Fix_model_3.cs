namespace SurveyOnline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fix_model_3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QuestionSurveys", "Sorter", c => c.Int(nullable: false));
            AddColumn("dbo.QuestionSurveys", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QuestionSurveys", "Description");
            DropColumn("dbo.QuestionSurveys", "Sorter");
        }
    }
}
