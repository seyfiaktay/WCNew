namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChecklistMains", "Private", c => c.Boolean(nullable: false));
            AddColumn("dbo.ChecklistMains", "Definition", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChecklistMains", "Definition");
            DropColumn("dbo.ChecklistMains", "Private");
        }
    }
}
