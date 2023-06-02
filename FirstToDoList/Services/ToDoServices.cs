using FirstToDoListBlazor.Model;
namespace FirstToDoList.Services;

    public class ToDoServices
    {
        private List<string> ToDoListInDataBase {get; set; } = new ();
        
        //save user's data to db
        public async Task Save(ToDoListModel toDoListModel)
        {
            
            ToDoListInDataBase.Clear();
            ToDoListInDataBase.AddRange(toDoListModel.ToDoList);
        }
        //db to user
        public async Task GetListFromDB(ToDoListModel model)
        {
            
            //model.ToDoList := ToDoListToMemory;
            //model.ToDoList <= ToDoListToMemory;
            //"=" and "=="


            model.ToDoList.Clear();
            model.ToDoList.AddRange(ToDoListInDataBase);
        }
        
        
    }
