namespace InventoryManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class includeitemname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Inventories", "ItemName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Inventories", "ItemName");
        }
    }
}
