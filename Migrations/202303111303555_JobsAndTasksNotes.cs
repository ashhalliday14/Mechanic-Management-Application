namespace AdvancedProgramming.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class JobsAndTasksNotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "Notes", c => c.String());
            AddColumn("dbo.Tasks", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "Notes");
            DropColumn("dbo.Jobs", "Notes");
        }
    }
}
