using Npgsql;
using FirstToDoListBlazor.Model;
using FirstToDoListBlazor.Infrastructure;
namespace FirstToDoListBlazor.Services;

    public class ToDoServicesMemory
    {
        private List<ToDoListEntry> ToDoListInDataBase {get; set; } = new ();
        
        //save user's data to db
        public async Task Save(ToDoListModel toDoListModel)
        {
            Console.WriteLine(String.Join(", ", toDoListModel.ToDoList));
            ToDoListInDataBase.Clear();
            ToDoListInDataBase.AddRange(toDoListModel.ToDoList);
        }
        //db to user
        public async Task GetListFromMemoryDB(ToDoListModel model)
        {
            
            //model.ToDoList := ToDoListToMemory;
            //model.ToDoList <= ToDoListToMemory;
            //"=" and "=="


            model.ToDoList.Clear();
            model.ToDoList.AddRange(ToDoListInDataBase);
        }
        
        
    }



