using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
class MyBinaryFormatter
{
    static void Main(string[] args)
    {
        List<string> Customers = new List<string>();
        Customers.Add("Kailsh");
        Customers.Add("Ramsharan");
        Customers.Add("Panchanan");
        Customers.Add("Roupya Manjari");
        FileStream FS = new FileStream("Customer.txt", FileMode.Create);
        Serialize(Customers, FS);
        FS.Flush();
        FS.Close();
        FS = new FileStream("Customer.txt", FileMode.Open);
        List<string> Customers2 = Deserialize(FS);
        FS.Close();
        if (Customers2 != null)
        {
            foreach (string Customer in Customers2)
            {
                Console.WriteLine(Customer);
            }
        }
        Console.ReadLine();
    }
    private static void Serialize(List<string> customers, FileStream fs)
    {
        BinaryFormatter BF = new BinaryFormatter();
        try
        {
            BF.Serialize(fs, customers);
            Console.WriteLine("Successfully Serialized");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unable to Serialize from binary format");
            Console.WriteLine(ex.Message);
        }
    }
    private static List<string> Deserialize(FileStream fs)
    {
        BinaryFormatter BF = new BinaryFormatter();
        List<string> LS = null;
        try
        {
            LS = (List<string>)BF.Deserialize(fs);
            Console.WriteLine("Successfully Deserialized");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unable to Deserialize from binary format");
            Console.WriteLine(ex.Message);
        }
        return LS;
    }
    
}