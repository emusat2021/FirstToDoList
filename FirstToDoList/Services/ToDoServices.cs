using FirstToDoListBlazor.Model;
namespace FirstToDoList.Services;

    public class ToDoServices
    {
        private List<string> ToDoListInDataBase {get; set; } = new ();
        public async Task GetListFromDB(ToDoListModel model)
        {
            //db to user
            //model.ToDoList := ToDoListToMemory;
            //model.ToDoList <= ToDoListToMemory;
            //"=" and "=="


            model.ToDoList.Clear();
            model.ToDoList.AddRange(ToDoListInDataBase);
        }
        public async Task Save(ToDoListModel toDoListModel)
        {
            //save user's data to db
            ToDoListInDataBase.Clear();
            ToDoListInDataBase.AddRange(toDoListModel.ToDoList);
        }
        
        
    }
