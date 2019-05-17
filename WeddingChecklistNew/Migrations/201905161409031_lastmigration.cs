namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lastmigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChecklistMains", "DueDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.ChecklistMains", "Name", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ChecklistMains", "Name", c => c.String());
            DropColumn("dbo.ChecklistMains", "DueDate");
        }
    }
}
