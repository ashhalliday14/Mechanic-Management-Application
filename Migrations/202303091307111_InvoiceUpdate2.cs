namespace AdvancedProgramming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InvoiceUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Invoices", "CustomerName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Invoices", "CustomerName");
        }
    }
}
