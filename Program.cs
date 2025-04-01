using System;
using System.Net.Sockets;
using System.Text;

class Client
{
    static void Main()
    {
        Console.WriteLine("Введите команду (date или time): ");
        string command = Console.ReadLine()?.Trim().ToLower();

        if (command != "date" && command != "time")
        {
            Console.WriteLine("Некорректная команда.");
            return;
        }

        string serverIp = "127.0.0.1";
        int port = 12345;

        try
        {
            using (TcpClient client = new TcpClient(serverIp, port))
            using (NetworkStream stream = client.GetStream())
            {
                byte[] requestBytes = Encoding.UTF8.GetBytes(command);
                stream.Write(requestBytes, 0, requestBytes.Length);

                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                Console.WriteLine($"Ответ от сервера: {response}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}
