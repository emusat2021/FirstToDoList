using FirstToDoListBlazor.Model;
namespace FirstToDoList.Services;

    public class ToDoServices
    {
        private List<string> ToDoListToMemory {get; set; } = new ();
        public async Task Load(ToDoListModel model)
        {
            model.ToDoList = ToDoListToMemory;
        }
        public async Task Save(ToDoListModel toDoListModel)
        {
            // ToDoListToMemory.AddRange(toDoListModel.ToDoList);
            // toDoListModel.ToDoList.CopyTo(ToDoListToMemory);
            // ToDoListToMemory = string.Join(",", toDoListModel.ToDoList);
            ToDoListToMemory.Equals(toDoListModel.ToDoList);

        }
        
        
    }
