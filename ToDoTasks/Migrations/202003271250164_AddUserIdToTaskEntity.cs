namespace ToDoTasks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToTaskEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ToDoTasks", "UserId");
            AddForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ToDoTasks", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.ToDoTasks", new[] { "UserId" });
            DropColumn("dbo.ToDoTasks", "UserId");
        }
    }
}
