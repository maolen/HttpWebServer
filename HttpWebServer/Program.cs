using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace HttpWebServer
{
    class Program
    {
        // https://metanit.com/sharp/net/7.1.php
        static void Main(string[] args)
        {
            using (var listener = new HttpListener())
            {
                listener.Prefixes.Add("http://localhost/");
                listener.Start();
                // Получить информацию о входящем соединении
                var context = listener.GetContext();

                // Получить сам запрос, можно его проанализировать
                var request = context.Request;

                var searchingFileUri = request.RawUrl.Remove('/');

                var response = context.Response;

                // Не только текст, фото, видео, т.е. массив байтов
                var responseText = File.ReadAllText(searchingFileUri);
                var bytes = System.Text.Encoding.UTF8.GetBytes(responseText);
                response.OutputStream.Write(bytes, 0, bytes.Length);
                response.Close();
            }
        }
    }
}
