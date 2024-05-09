using System.Data.Common;
using Microsoft.Data.SqlClient;
using WarehouseApp.Model;

namespace WarehouseApp.Service;


public class WarehouseService : IWarehouseService
{
    private WarehouseService() {}
    private static WarehouseService? instance;
    
    public static WarehouseService GetInstance()
    {
        if (instance == null)
            instance = new WarehouseService();
        return instance;
    }

    public async Task<int> ChangeWarehouse(WarehousePayload payload)
    {
        using var con = new SqlConnection("Data Source=SBD2019;Initial Catalog=pegago;Integrated Security=True");
        using var com = new SqlCommand("select * from warehouse", con);
        await con.OpenAsync();
        DbTransaction tran = await con.BeginTransactionAsync();
        com.Transaction = (SqlTransaction) tran;
        try
        {
            var list = new List<Warehouse>();
            using (var dr = await com.ExecuteReaderAsync())
            {
                while (await dr.ReadAsync())
                {
                    list.Add(
                        new Warehouse(
                            (int) dr["IdWarehouse"], 
                            dr["Name"].ToString()!, 
                            dr["Address"].ToString()!)
                        );
                }
            }
            await tran.CommitAsync();
            Console.WriteLine(list[0].ToString());
            return list.Count;
        }
        catch (SqlException exc)
        {
            await tran.RollbackAsync();
        }
        catch (Exception exc)
        {
            await tran.RollbackAsync();
        }

        return -1;
    }
}
