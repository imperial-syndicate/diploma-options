namespace OptionsWebsite.Migrations.DiplomaMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateValidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Options", "Title", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Options", "Title", c => c.String(maxLength: 50));
        }
    }
}
