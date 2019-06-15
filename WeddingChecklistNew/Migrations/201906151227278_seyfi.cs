namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        ChecklistMainId = c.Int(nullable: false),
                        Type = c.Byte(nullable: false),
                        LogDate = c.DateTime(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChecklistMains", t => t.ChecklistMainId, cascadeDelete: true)
                .Index(t => t.ChecklistMainId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ChecklistMainId", "dbo.ChecklistMains");
            DropIndex("dbo.Comments", new[] { "ChecklistMainId" });
            DropTable("dbo.Comments");
        }
    }
}
