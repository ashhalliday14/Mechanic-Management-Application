namespace AdvancedProgramming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskUpdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Tasks", "JobID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tasks", "JobID", c => c.Int(nullable: false));
        }
    }
}
