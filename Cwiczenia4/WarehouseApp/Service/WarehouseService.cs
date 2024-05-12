using System.ComponentModel.DataAnnotations;
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
            new SqlConnection("");

        using var com = new SqlCommand("select count(*) from product", con);

        await con.OpenAsync();
        
        DbTransaction tran = await con.BeginTransactionAsync();
        com.Transaction = (SqlTransaction)tran;
        
        try
        {
            com.CommandText = "SELECT count(*) FROM warehouse WHERE IdProduct = @Id;";
            com.Parameters.AddWithValue("Id", payload.IdProduct);
            if ( !await VerifyCount(com, payload) ) 
                throw new ValidationException("Product with given is doesn't exist");

            com.Parameters.Clear();
            com.CommandText = "SELECT count(*) FROM product WHERE IdProduct = @Id;";
            com.Parameters.AddWithValue("Id", payload.IdWarehouse);
            if (!await VerifyCount(com, payload)) 
                throw new ValidationException("Warehouse with given is doesn't exist");
            
            com.Parameters.Clear();
            com.CommandText = "SELECT count(*) FROM order WHERE IdProduct = @IdProduct AND amount = @Amount;";
            com.Parameters.AddWithValue("IdProduct", payload.IdProduct);
            com.Parameters.AddWithValue("Amount", payload.Amount);
            if (!await VerifyCount(com, payload)) 
                throw new ValidationException("No matching order exists");
            
            com.Parameters.Clear();
            com.CommandText = "SELECT IdOrder FROM order WHERE IdProduct = @IdProduct;";
            com.Parameters.AddWithValue("IdProduct", payload.IdProduct);
            using var scalar = com.ExecuteScalarAsync();
            var idOrder = (Int32)scalar.Result!;
            
            com.Parameters.Clear();
            com.CommandText = "SELECT IdOrder FROM Product_Warehouse WHERE IdOrder = @IdOrder;";
            com.Parameters.AddWithValue("IdOrder", idOrder);
            if (!await VerifyCount(com, payload)) 
                throw new ValidationException("Order has already been realized");
            
            com.Parameters.Clear();
            com.CommandText = "UPDATE Order SET FulfiledAt = @now WHERE IdOrder = @IdOrder;";
            com.Parameters.AddWithValue("now", DateTime.Now);
            com.Parameters.AddWithValue("IdOrder", idOrder);
            com.ExecuteNonQuery();
            
            com.Parameters.Clear();
            com.CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct;";
            com.Parameters.AddWithValue("IdProduct", payload.IdProduct);
            using var priceTask = com.ExecuteScalarAsync();
            var price = (Int32)scalar.Result!;
            
            com.Parameters.Clear();
            com.CommandText = "INSERT INTO Product_Warehouse VALUES ()";
            com.Parameters.AddWithValue("Price", price *payload.Amount);
            com.Parameters.AddWithValue("now", DateTime.Now);
            com.ExecuteNonQuery();
            
            await tran.CommitAsync();

        }
        catch (SqlException e)
        {
            await tran.RollbackAsync();
        }
        catch(Exception e)
        {
            Console.WriteLine(e);
        }

        return -1;
    }
    
    private async Task<bool> VerifyCount(SqlCommand com, WarehousePayload payload)
    {
        var scalar = await com.ExecuteScalarAsync();
        var result = (Int32)scalar!;
        return result > 0;
    }
    
    
}
