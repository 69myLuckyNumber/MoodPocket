namespace MoodPocket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGalleryPictureEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UGalleries",
                c => new
                    {
                        GalleryID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.GalleryID)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.GalleryPictures",
                c => new
                    {
                        GalleryID = c.Int(nullable: false),
                        PictureID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GalleryID, t.PictureID })
                .ForeignKey("dbo.UGalleries", t => t.GalleryID, cascadeDelete: true)
                .ForeignKey("dbo.UPictures", t => t.PictureID, cascadeDelete: true)
                .Index(t => t.GalleryID)
                .Index(t => t.PictureID);
            
            CreateTable(
                "dbo.UPictures",
                c => new
                    {
                        PictureID = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                    })
                .PrimaryKey(t => t.PictureID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UGalleries", "User_Id", "dbo.Users");
            DropForeignKey("dbo.GalleryPictures", "PictureID", "dbo.UPictures");
            DropForeignKey("dbo.GalleryPictures", "GalleryID", "dbo.UGalleries");
            DropIndex("dbo.GalleryPictures", new[] { "PictureID" });
            DropIndex("dbo.GalleryPictures", new[] { "GalleryID" });
            DropIndex("dbo.UGalleries", new[] { "User_Id" });
            DropTable("dbo.UPictures");
            DropTable("dbo.GalleryPictures");
            DropTable("dbo.UGalleries");
        }
    }
}
