using ClaseJson;
using System;
using System.IO;
using System.Net;
using System.Text.Json;

partial class Program
{
    static void Main(string[] args)
    {
        GetPrecios();
    }

    private static void GetPrecios()
    {
        var url = "https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader == null) return;
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string myJsonResponse = objReader.ReadToEnd();
                        CambioBTC moneda = JsonSerializer.Deserialize<CambioBTC>(myJsonResponse);


                        Console.WriteLine("Moneda: " + moneda.chartName + " Precio: " + moneda.bpi.USD);

                    }
                }
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
        }
    }
}
