namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChecklistImages", "Type", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChecklistImages", "Type");
        }
    }
}
