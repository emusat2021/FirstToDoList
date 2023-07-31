using Npgsql;
using FirstToDoListBlazor.Model;
using FirstToDoListBlazor.Infrastructure;
namespace FirstToDoListBlazor.Services;



public class ToDoServicePGAdmin
{
    private Database database;

    public ToDoServicePGAdmin(Database database)
    {
        this.database = database;
    }

    public async Task Save(ToDoListModel toDoListModel)
    {
        await database.Save(Tables.FirstToDoListMemory, toDoListModel, n => n.Id);
    }

    public async Task<IEnumerable<ToDoListModel>> FindAll()
    {
        return await database.Get<ToDoListModel>($"SELECT data FROM {Tables.FirstToDoListMemory}");
    }

    public async Task<ToDoListModel?> FindById(string id)
    {
        var idParameter = new NpgsqlParameter("Id", id);
        var models = await database.Get<ToDoListModel>
        ($"SELECT data FROM {Tables.FirstToDoListMemory} WHERE id = @Id", new []{ idParameter });
        return models.FirstOrDefault();
    }
}

