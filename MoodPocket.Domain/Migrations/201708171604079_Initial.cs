namespace MoodPocket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Galleries",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.GalleryPictures",
                c => new
                    {
                        GalleryID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GalleryID, t.PictureID })
                .ForeignKey("dbo.Galleries", t => t.GalleryID, cascadeDelete: true)
                .ForeignKey("dbo.Pictures", t => t.PictureID, cascadeDelete: true)
                .Index(t => t.GalleryID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.Pictures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Galleries", "Id", "dbo.Users");
            DropForeignKey("dbo.GalleryPictures", "PictureID", "dbo.Pictures");
            DropForeignKey("dbo.GalleryPictures", "GalleryID", "dbo.Galleries");
            DropIndex("dbo.GalleryPictures", new[] { "PictureID" });
            DropIndex("dbo.GalleryPictures", new[] { "GalleryID" });
            DropIndex("dbo.Galleries", new[] { "Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Pictures");
            DropTable("dbo.GalleryPictures");
            DropTable("dbo.Galleries");
        }
    }
}
