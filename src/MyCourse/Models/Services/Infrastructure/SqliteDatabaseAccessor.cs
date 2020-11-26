using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace MyCourse.Models.Services.Infrastructure
{
    public class SqliteDatabaseAccessor : IDatabaseAccessor
    {

        private readonly IConfiguration configuration;
public SqliteDatabaseAccessor(IConfiguration configuration)
{
    this.configuration = configuration;
}

        public async Task<DataSet> QueryAsync(FormattableString formattableQuery)
        {


//Creiamo dei SqliteParameter a partire dalla FormattableString
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for (var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            string query = formattableQuery.ToString();
            string connectionString = configuration.GetSection("ConnectionStrings").GetValue <string> ("Default");


            using (var conn = new SqliteConnection(connectionString))
            {
                await conn.OpenAsync();
                using ( var cmd = new SqliteCommand(query,conn))
                {
                    //Aggiungiamo i SqliteParameters al SqliteCommand
                    cmd.Parameters.AddRange(sqliteParameters);

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        var dataset = new DataSet();
                        dataset.EnforceConstraints=false;
                        
                        do
                        {
                        var dataTable = new DataTable();
                        dataset.Tables.Add(dataTable);
                        dataTable.Load(reader);
                        } while (!reader.IsClosed);


                        return dataset;
                    }
                }
            }

        }
    }
}