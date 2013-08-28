namespace ApertureCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Settings : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        SettingsId = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.SettingsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Settings");
        }
    }
}
