using FirstToDoListBlazor.Model;
namespace FirstToDoList.Services;

    public class ToDoServices
    {
        public List<string> ToDoListToMemory {get; set; } = new ();

        public async Task Save(ToDoListModel toDoListModel)
        {
            ToDoListToMemory.AddRange(toDoListModel.ToDoList);
        }
        
        
    }
