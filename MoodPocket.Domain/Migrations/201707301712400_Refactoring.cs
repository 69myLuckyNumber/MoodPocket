namespace MoodPocket.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Refactoring : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.UGalleries", newName: "UserGalleries");
            RenameTable(name: "dbo.UPictures", newName: "UserPictures");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.UserPictures", newName: "UPictures");
            RenameTable(name: "dbo.UserGalleries", newName: "UGalleries");
        }
    }
}
