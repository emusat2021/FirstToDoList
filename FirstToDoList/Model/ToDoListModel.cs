namespace FirstToDoListBlazor.Model;

public class ToDoListModel
{
    public string? Id { get; set; }
    public List<string> ToDoList { get; set; } = new ();
}
//
//1  List<bool, string>
// List = med all
// List  = done, prio
//2
//entry is done aller inte
//obj sting, bool
// use bool flage if when entry.