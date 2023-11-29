using SimpleTCP;

internal class Program
{
    static void Main(string[] args)
    {
        var filename = "C:\\Temp\\test.txt";
        var port = 0;
        var server = new SimpleTcpServer();

        // Parse command line arguments for port
        for (int i = 0; i < args.Length; i++)
        {
            if (args[i] == "--port" && i + 1 < args.Length)
            {
                if (int.TryParse(args[i + 1], out int parsedPort))
                {
                    port = parsedPort;
                }
                else
                {
                    Console.WriteLine("Invalid port number.");
                    return;
                }
                break;
            }
        }

        if (port == 0)
        {
            Console.WriteLine("Port not specified.");
            return;
        }

        server.Delimiter = 0x13;
        server.DataReceived += (sender, msg) =>
        {
            var message = msg.MessageString;
            Console.WriteLine("RX: " + message);
            File.AppendAllText(filename, message);
        };

        server.Start(port);


        Console.WriteLine("Press [Enter] key to exit.");
        Console.ReadLine();
        server.Stop();
    }
}