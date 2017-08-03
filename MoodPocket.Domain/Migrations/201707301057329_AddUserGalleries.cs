namespace MoodPocket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserGalleries : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UGalleries", "User_Id", "dbo.Users");
            DropIndex("dbo.UGalleries", new[] { "User_Id" });
            RenameColumn(table: "dbo.UGalleries", name: "User_Id", newName: "UserID");
            AlterColumn("dbo.UGalleries", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.UGalleries", "UserID");
            AddForeignKey("dbo.UGalleries", "UserID", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UGalleries", "UserID", "dbo.Users");
            DropIndex("dbo.UGalleries", new[] { "UserID" });
            AlterColumn("dbo.UGalleries", "UserID", c => c.Int());
            RenameColumn(table: "dbo.UGalleries", name: "UserID", newName: "User_Id");
            CreateIndex("dbo.UGalleries", "User_Id");
            AddForeignKey("dbo.UGalleries", "User_Id", "dbo.Users", "Id");
        }
    }
}
