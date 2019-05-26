namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ChecklistMains", "LogDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChecklistMains", "LogDate", c => c.DateTime(nullable: false));
        }
    }
}
