namespace AdvancedProgramming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "CustomerName", c => c.String());
            DropColumn("dbo.Jobs", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Jobs", "CustomerName");
        }
    }
}
