namespace ToDoTasks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDueDateToTodoTaskEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoTasks", "DueDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoTasks", "DueDate");
        }
    }
}
