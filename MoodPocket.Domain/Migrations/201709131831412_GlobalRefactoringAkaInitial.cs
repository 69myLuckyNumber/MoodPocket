namespace MoodPocket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GlobalRefactoringAkaInitial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GalleryMemes", "Meme_Id", "dbo.Memes");
            DropIndex("dbo.GalleryMemes", new[] { "Meme_Id" });
            RenameColumn(table: "dbo.GalleryMemes", name: "Meme_Id", newName: "MemeID");
            DropPrimaryKey("dbo.GalleryMemes");
            AlterColumn("dbo.GalleryMemes", "MemeID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.GalleryMemes", new[] { "GalleryID", "MemeID" });
            CreateIndex("dbo.GalleryMemes", "MemeID");
            AddForeignKey("dbo.GalleryMemes", "MemeID", "dbo.Memes", "Id", cascadeDelete: true);
            DropColumn("dbo.GalleryMemes", "PictureID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GalleryMemes", "PictureID", c => c.Int(nullable: false));
            DropForeignKey("dbo.GalleryMemes", "MemeID", "dbo.Memes");
            DropIndex("dbo.GalleryMemes", new[] { "MemeID" });
            DropPrimaryKey("dbo.GalleryMemes");
            AlterColumn("dbo.GalleryMemes", "MemeID", c => c.Int());
            AddPrimaryKey("dbo.GalleryMemes", new[] { "GalleryID", "PictureID" });
            RenameColumn(table: "dbo.GalleryMemes", name: "MemeID", newName: "Meme_Id");
            CreateIndex("dbo.GalleryMemes", "Meme_Id");
            AddForeignKey("dbo.GalleryMemes", "Meme_Id", "dbo.Memes", "Id");
        }
    }
}
