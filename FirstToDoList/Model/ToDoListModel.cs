namespace FirstToDoListBlazor.Model;

public class ToDoListModel
{
    public string? Id { get; set; }
    public List<string> ToDoList { get; set; } = new ();
}
