using System.Data.Common;
using Microsoft.Data.SqlClient;
using WarehouseApp.Model;

namespace WarehouseApp.Service;


public class WarehouseService : IWarehouseService
{
    private WarehouseService() {}
    private static WarehouseService? _instance;
    
    public static WarehouseService GetInstance()
    {
        if (_instance == null)
            _instance = new WarehouseService();
        return _instance;
    }

    public async Task<int> ChangeWarehouse(WarehousePayload payload)
    {
        using var con =
            new SqlConnection(
                "Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True;TrustServerCertificate=True;User=s24106;Password=********");

        using var com = new SqlCommand("select * from warehouse", con);

        await con.OpenAsync();
        //
        // DbTransaction tran = await con.BeginTransactionAsync();
        // com.Transaction = (SqlTransaction)tran;
        // try
        // {
        //     var list = new List<Warehouse>();
        //     await using (var dr = await com.ExecuteReaderAsync())
        //     {
        //         while (await dr.ReadAsync())
        //         {
        //             list.Add(new Warehouse
        //             {
        //                 IdWarehouse = (int)dr["IdWarehouse"],
        //                 Name = dr["Name"].ToString(),
        //                 Address = dr["Address"].ToString()
        //             });
        //         }
        //     }
        //
        //     return list.Count;
        // }
        // catch(Exception e)
        // {
        //     Console.WriteLine(e);
        // }

        return -1;
    }
}
