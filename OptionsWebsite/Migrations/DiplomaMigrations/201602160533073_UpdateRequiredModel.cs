namespace OptionsWebsite.Migrations.DiplomaMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRequiredModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Choices", "StudentID", c => c.String(nullable: false, maxLength: 9));
            AlterColumn("dbo.Choices", "StudentFirstName", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Choices", "StudentLastName", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Choices", "StudentLastName", c => c.String(maxLength: 40));
            AlterColumn("dbo.Choices", "StudentFirstName", c => c.String(maxLength: 40));
            AlterColumn("dbo.Choices", "StudentID", c => c.String(maxLength: 9));
        }
    }
}
