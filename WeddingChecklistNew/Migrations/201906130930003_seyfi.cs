namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Checklists", "Done", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Checklists", "Done");
        }
    }
}
