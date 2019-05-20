namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Checklists", "ChecklistMainId", "dbo.ChecklistMains");
            DropIndex("dbo.Checklists", new[] { "ChecklistMainId" });
            AlterColumn("dbo.Checklists", "ChecklistMainId", c => c.Int());
            CreateIndex("dbo.Checklists", "ChecklistMainId");
            AddForeignKey("dbo.Checklists", "ChecklistMainId", "dbo.ChecklistMains", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Checklists", "ChecklistMainId", "dbo.ChecklistMains");
            DropIndex("dbo.Checklists", new[] { "ChecklistMainId" });
            AlterColumn("dbo.Checklists", "ChecklistMainId", c => c.Int(nullable: false));
            CreateIndex("dbo.Checklists", "ChecklistMainId");
            AddForeignKey("dbo.Checklists", "ChecklistMainId", "dbo.ChecklistMains", "Id", cascadeDelete: true);
        }
    }
}
