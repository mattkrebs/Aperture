namespace ApertureCMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogEntries",
                c => new
                    {
                        EntryId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        Tags = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Enabled = c.Boolean(nullable: false),
                        Gallery_GalleryId = c.Int(),
                    })
                .PrimaryKey(t => t.EntryId)
                .ForeignKey("dbo.Galleries", t => t.Gallery_GalleryId)
                .Index(t => t.Gallery_GalleryId);
            
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        GalleryId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Tags = c.String(),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GalleryId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        PhotoUrl = c.String(),
                        ThumbnailUrl = c.String(),
                        Tags = c.String(),
                        Enabled = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId);
            
            CreateTable(
                "dbo.PhotoGalleries",
                c => new
                    {
                        Photo_PhotoId = c.Int(nullable: false),
                        Gallery_GalleryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Photo_PhotoId, t.Gallery_GalleryId })
                .ForeignKey("dbo.Photos", t => t.Photo_PhotoId, cascadeDelete: true)
                .ForeignKey("dbo.Galleries", t => t.Gallery_GalleryId, cascadeDelete: true)
                .Index(t => t.Photo_PhotoId)
                .Index(t => t.Gallery_GalleryId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PhotoGalleries", new[] { "Gallery_GalleryId" });
            DropIndex("dbo.PhotoGalleries", new[] { "Photo_PhotoId" });
            DropIndex("dbo.BlogEntries", new[] { "Gallery_GalleryId" });
            DropForeignKey("dbo.PhotoGalleries", "Gallery_GalleryId", "dbo.Galleries");
            DropForeignKey("dbo.PhotoGalleries", "Photo_PhotoId", "dbo.Photos");
            DropForeignKey("dbo.BlogEntries", "Gallery_GalleryId", "dbo.Galleries");
            DropTable("dbo.PhotoGalleries");
            DropTable("dbo.Photos");
            DropTable("dbo.Galleries");
            DropTable("dbo.BlogEntries");
        }
    }
}
