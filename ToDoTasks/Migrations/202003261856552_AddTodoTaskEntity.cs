namespace ToDoTasks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTodoTaskEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoTasks",
                c => new
                    {
                        ToDoTaskId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        IsDone = c.Boolean(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ToDoTaskId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToDoTasks");
        }
    }
}
