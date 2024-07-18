using ToDoApp.Data.Context;

var context = new ToDoContext();

//var user = new User
//{
//    Name = "John Doe",
//    ToDoItems = new List<ToDoItem>
//    {
//        new ToDoItem
//        {
//            Description = "Buy milk",
//            DueDate = DateTime.Now.AddDays(1),
//            IsDone = false
//        },
//        new ToDoItem
//        {
//            Description = "Buy bread",
//            DueDate = DateTime.Now.AddDays(1),
//            IsDone = false
//        }
//    }
//};

//context.Users.Add(user);

var item = await context.ToDoItems.FindAsync(2);
item.IsDone = true;
;
await context.SaveChangesAsync();
;


