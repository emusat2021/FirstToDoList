using System;
using System.ComponentModel.DataAnnotations;
namespace FirstToDoListBlazor.Model;

public class ToDoListModel
{
    public string? Id { get; set; }

    public List<ToDoListEntry> ToDoList { get; set; } = new ();

}
public class ToDoListEntry
{
    public string TaskName { get; set; }
    public bool DoneTask { get; set; }

    public override string ToString()
    {
        return TaskName;
    }
}

