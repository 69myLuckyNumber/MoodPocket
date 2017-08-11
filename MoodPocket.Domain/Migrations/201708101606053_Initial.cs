namespace MoodPocket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGalleries",
                c => new
                    {
                        GalleryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GalleryID)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.GalleryPictures",
                c => new
                    {
                        GalleryID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GalleryID, t.PictureID })
                .ForeignKey("dbo.UserGalleries", t => t.GalleryID, cascadeDelete: true)
                .ForeignKey("dbo.UserPictures", t => t.PictureID, cascadeDelete: true)
                .Index(t => t.GalleryID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.UserPictures",
                c => new
                    {
                        PictureID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.PictureID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        ConfirmPassword = c.String(),
                        Salt = c.String(),
                        IsVerified = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGalleries", "UserID", "dbo.Users");
            DropForeignKey("dbo.GalleryPictures", "PictureID", "dbo.UserPictures");
            DropForeignKey("dbo.GalleryPictures", "GalleryID", "dbo.UserGalleries");
            DropIndex("dbo.GalleryPictures", new[] { "PictureID" });
            DropIndex("dbo.GalleryPictures", new[] { "GalleryID" });
            DropIndex("dbo.UserGalleries", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.UserPictures");
            DropTable("dbo.GalleryPictures");
            DropTable("dbo.UserGalleries");
        }
    }
}
