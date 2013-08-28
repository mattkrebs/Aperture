namespace ApertureCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMediumPhotoColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "MediumPhotoUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "MediumPhotoUrl");
        }
    }
}
