using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace NP_Project_Server
{
    public class server
    {
        TcpListener listener = null;
        public void game(TcpListener listener)
        {
            this.listener = listener;
            Console.WriteLine("Waiting for incoming client connections...");
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Accepted new client connection...");
            StreamReader reader = new StreamReader(client.GetStream());
            StreamWriter writer = new StreamWriter(client.GetStream());

            int serverPoints = 0;

            while (serverPoints < 2)
            {

                Console.WriteLine("place bomb  in given block");
                Console.WriteLine(" *************************");
                Console.WriteLine("     1    |   2   |    3");
                Console.WriteLine("     4    |   5   |    6");
                Console.WriteLine("     7    |   8   |    9");

                Console.WriteLine("Enter Block Number");
                string placeBomb = Console.ReadLine();
                if (placeBomb == "1" || placeBomb == "2" || placeBomb == "3" || placeBomb == "4" || placeBomb == "5" || placeBomb == "6" || placeBomb == "7" || placeBomb == "8" || placeBomb == "9")
                {


                    Console.WriteLine("BOMB HAS BEEN PLACED AT BLOCK : " + placeBomb);
                    writer.WriteLine(placeBomb);
                    writer.Flush();

                    string bomb = reader.ReadLine();
                    if (bomb != "none")
                    {
                        for (int i = 1; i < 4; i++)
                        {
                            Console.WriteLine("CLIENT PLACED A BOMB , GUESS THE BLOCK  --- TOTAL ATTEMPS 3");
                            Console.WriteLine("ATTEMP NUMBER --> " + i);

                            Console.WriteLine("     1    |   2   |    3");
                            Console.WriteLine("     4    |   5   |    6");
                            Console.WriteLine("     7    |   8   |    9");
                            Console.WriteLine("GUESS...");
                            string guessBomb = Console.ReadLine();
                            if (guessBomb == bomb)
                            {
                                Console.WriteLine("-----------WELL DONE!! BOMB DIFFUSED SUCCESFULLY-----------");
                                serverPoints++;

                                break;
                            }
                            else
                            {
                                Console.WriteLine("Wrong ");
                            }
                        }
                    }
                    Console.WriteLine("server Points: " + serverPoints);
                    if (serverPoints == 2)
                    {
                        Console.WriteLine("----------------server win------------");
                        Console.WriteLine("Total Server Points: " + serverPoints);
                        break;
                    }

                }
                else { Console.WriteLine("Invalid Input"); }
            }




            reader.Close();
            writer.Close();
            client.Close();
            Console.ReadLine();
        }

    }

  
    class Program
    {
        static void Main(string[] args)
        {

            //start
            Console.WriteLine("This is server");

            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8080);
                listener.Start();

                while (true)
                {
                    server ob = new server();
                    ob.game(listener);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (listener != null)
                {
                    listener.Stop();
                }
            }

            //end
        }
    }
}
