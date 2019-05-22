namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Checklists", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Checklists", "Url", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Checklists", "Url", c => c.String());
            AlterColumn("dbo.Checklists", "Name", c => c.String());
        }
    }
}
