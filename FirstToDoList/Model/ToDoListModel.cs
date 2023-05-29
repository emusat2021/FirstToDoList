namespace FirstToDoListBlazor.Model;

public class ToDoListModel
{
    public string? Id { get; set; }
    public HashSet<string> ToDoList { get; set; } = new ();
}
