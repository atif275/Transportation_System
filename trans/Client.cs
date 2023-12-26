using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Client
{
    public Client()
    {
        StartClient();
        
    }

    public static void StartClient()
    {
        byte[] bytes = new byte[1024];

        try
        {

            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);


            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);


            try
            {

                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());
                string message = null;
                byte[] msg = null;
                int bytesSent = 0;
                int bytesRec = 0;
                while (true)
                {
                    Console.Write("Enter Message to send to server : ");
                    message = Console.ReadLine();

                    msg = Encoding.ASCII.GetBytes(message);
                    bytesSent = sender.Send(msg);
                    if (message == "q")
                    {
                        break;
                    }
                    bytesRec = sender.Receive(bytes);
                    Console.WriteLine("From server = {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    message = null;
                    msg = null;


                }

                // Encode the data string into a byte array.
                //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                //// Send the data through the socket.
                //int bytesSent = sender.Send(msg);

                //// Receive the response from the remote device.
                //int bytesRec = sender.Receive(bytes);
                //Console.WriteLine("Echoed test = {0}",
                //    Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Release the socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}


