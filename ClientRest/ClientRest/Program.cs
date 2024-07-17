using System;
using System.Threading.Tasks;
using ClientRest;

class Program
{
    static async Task Main(string[] args)
    {
        var apiClient = new ApiClient("http://localhost:8080");

        try
        {
            var newJocDTO = new JocCreateDTO() { configuratie = "Z,4,T,5,O,6,P,8",id = 3 };
            var savedJoc = await apiClient.SaveJocAsync(newJocDTO);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}