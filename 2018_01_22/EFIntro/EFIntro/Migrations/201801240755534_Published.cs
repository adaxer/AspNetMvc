namespace EFIntro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Published : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "Published", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Books", "Published");
        }
    }
}
