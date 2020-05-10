namespace newdip.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetypeedge : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Edges", "Weight", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Edges", "Weight", c => c.Int(nullable: false));
        }
    }
}
