namespace todotoo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTasksTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tasks", "user_UserID", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "user_UserID" });
            RenameColumn(table: "dbo.Tasks", name: "user_UserID", newName: "UserID");
            AlterColumn("dbo.Tasks", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.Tasks", "UserID");
            AddForeignKey("dbo.Tasks", "UserID", "dbo.Users", "UserID", cascadeDelete: true);
            DropColumn("dbo.Tasks", "UseID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "UseID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Tasks", "UserID", "dbo.Users");
            DropIndex("dbo.Tasks", new[] { "UserID" });
            AlterColumn("dbo.Tasks", "UserID", c => c.Int());
            RenameColumn(table: "dbo.Tasks", name: "UserID", newName: "user_UserID");
            CreateIndex("dbo.Tasks", "user_UserID");
            AddForeignKey("dbo.Tasks", "user_UserID", "dbo.Users", "UserID");
        }
    }
}
