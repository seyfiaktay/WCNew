namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checklists", "LogDate", c => c.DateTime());
            AddColumn("dbo.Checklists", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Checklists", "UserId");
            DropColumn("dbo.Checklists", "LogDate");
        }
    }
}
