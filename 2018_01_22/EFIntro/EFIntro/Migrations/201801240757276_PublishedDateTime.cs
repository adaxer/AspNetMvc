namespace EFIntro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PublishedDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Published", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Published", c => c.String());
        }
    }
}
