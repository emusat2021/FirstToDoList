using Npgsql;
using NpgsqlTypes;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace FirstToDoListBlazor.Infrastructure;
    public class Database
    {
        private readonly string connectionString;
        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public async Task<int> Save<T>(string tableName, T item, Func<T, string> getId, bool ignoreDuplicateConflictOnId = false) where T : class
        {
            return await Save(new (string tableName, object[] items, Func<object, string> getId, bool ignoreDuplicateConflictOnId)[]
            {
                (tableName, new []{item}, (o) => getId((T)o), ignoreDuplicateConflictOnId)
            });
        }        
        public async Task<int> Save((string tableName, object[] items, Func<object, string> getId, bool ignoreDuplicateConflictOnId)[] tablesToSave)
        {
            return await Execute((cmd) =>
            {
                var i = 0;

                foreach (var table in tablesToSave)
                {
                    foreach (var item in table.items)
                    {
                        cmd.CommandText += $@"
                            INSERT INTO {table.tableName.ToLower()} (id, data) 
                            VALUES (@Id{i}, @Json{i}){ (table.ignoreDuplicateConflictOnId ? " ON CONFLICT (id) DO NOTHING" : " ON CONFLICT (id) DO UPDATE SET data = EXCLUDED.data")};";

                        cmd.Parameters.AddWithValue($"Id{i}", table.getId(item));
                        cmd.Parameters.AddWithValue($"Json{i}", NpgsqlDbType.Jsonb, JsonSerializer.Serialize(item));

                        i++;
                    }
                }
            });
        }

    public async Task EnsureJsonStandardTable(string tableName)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = $@"CREATE TABLE IF NOT EXISTS public.{tableName.ToLower()} (
                                        id character varying COLLATE pg_catalog.""default"" PRIMARY KEY NOT NULL,
                                        data jsonb) 
                                        WITH (OIDS = FALSE) 
                                        TABLESPACE pg_default; 
                                        ";
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<IEnumerable<T>> Get<T>(string query, NpgsqlParameter[] parameters=null)
    {
        using (var conn = new NpgsqlConnection(connectionString))
        {
            conn.Open();

            using (var cmd = new NpgsqlCommand())
            {
                cmd.Connection = conn;
                cmd.CommandTimeout = 0;
                cmd.CommandText = query;

                if (parameters != null && parameters.Any())
                {
                    cmd.Parameters.Clear();

                    foreach (var parameter in parameters)
                    {
                        parameter.Collection = null;
                        cmd.Parameters.Add(parameter);
                    }
                }

                var reader = await cmd.ExecuteReaderAsync();

                var items = new List<T>();

                while (reader.Read())
                {
                    items.Add(JsonSerializer.Deserialize<T>(reader["data"].ToString()));
                }

                return items;
            }
        }
    }
        public async Task<int> Execute(Action<NpgsqlCommand> action)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new NpgsqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandTimeout = 0;

                    action(cmd);

                    return await cmd.ExecuteNonQueryAsync();
                }
            }
        }
        public async Task BulkInsert<T>(T[] items, string tableName, Func<T, string> getId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                await conn.OpenAsync();
                using (var writer = conn.BeginBinaryImport($"COPY {tableName} (id, data) FROM STDIN (FORMAT BINARY)"))
                {
                    foreach (var item in items)
                    {
                        writer.StartRow();
                        writer.Write(getId(item));
                        writer.Write(JsonSerializer.Serialize(item), NpgsqlDbType.Jsonb);
                    }

                    writer.Complete();
                }
            }
        }        
    }