using System;
using System.IO;
using System.Net;

namespace HttpWebServerProject
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add("http://localhost/");
                listener.Start();
                while (true)
                {
                    var context = listener.GetContext();
                    var request = context.Request;

                    var searchingFileUri = string.Empty;
                    if (request.RawUrl.IndexOf('?') != -1)
                        searchingFileUri = request.RawUrl.Substring(0, request.RawUrl.IndexOf('?'));
                    else
                        searchingFileUri = request.RawUrl;



                    var response = context.Response;
                    if (File.Exists(@"C:\Users\нэт\source\repos\HttpWebServerProject\HttpWebServerProject\bin\Debug\netcoreapp3.0" + searchingFileUri))
                    {
                        var responseText = File.ReadAllText(@"C:\Users\нэт\source\repos\HttpWebServerProject\HttpWebServerProject\bin\Debug\netcoreapp3.0" + searchingFileUri);
                        var bytes = System.Text.Encoding.UTF8.GetBytes(responseText);
                        response.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    else
                    {
                        var bytes = new byte[0];
                        response.OutputStream.Write(bytes, 0, bytes.Length);
                    }
                    response.Close();
                }
            }


        }
    }
}
