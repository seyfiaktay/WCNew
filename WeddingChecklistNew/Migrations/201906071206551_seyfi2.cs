namespace WeddingChecklistNew.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seyfi2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Checklists", "Price", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Checklists", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
