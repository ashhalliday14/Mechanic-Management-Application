namespace AdvancedProgramming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "JobID", c => c.String());
            DropColumn("dbo.Invoices", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Invoices", "CustomerId", c => c.Int(nullable: false));
            DropColumn("dbo.Invoices", "JobID");
        }
    }
}
