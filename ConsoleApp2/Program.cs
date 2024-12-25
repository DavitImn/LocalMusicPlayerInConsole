using System.Diagnostics;
using System.IO;
using System.Media;
using System.Text;
using ATL;
using NAudio.Wave;
using WMPLib;
using ConsoleApp2.Models;
using ConsoleApp2.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace ConsoleApp2;


internal class Program
{


    static void Main(string[] args)
    {

        #region encoding
        Encoding encoding = Encoding.UTF8;
        Console.OutputEncoding = encoding;
        #endregion


        string path = "C:\\Users\\user\\Desktop\\muss";
        SongServices songServices = new SongServices(path);


        bool haveCount = false;

        while (true)
        {
            Console.WriteLine("Enter For search");
            string Searcher = Console.ReadLine();

            var bylength = songServices.songsList.Where(m => m.Name.ToLower().Contains(Searcher)).ToList();

            foreach (var item in bylength)
            {
                Console.WriteLine($"{item.Duration} |  MussId: {item.Id} Name: {item.Name} ");
            }
            if (bylength.Count > 0)
            {
                break;
            }

            Console.WriteLine("No item by search try again");

        }






        Console.WriteLine("Chose Song To Play By Id :");
        int thisisid = int.Parse(Console.ReadLine());


        


        Console.WriteLine("Player Options: \n SPACE - PLAY \n P - PAUSE \n N - NEXT MUSIC \n B - PREVIOUS MUSIC \n O - NEW MUSIC FROM LIST \n F -FAST FORWARD \n R - REVERSE \n V - VOLUME");

        var option = Console.ReadKey(true).Key;
        bool isOnPause = true;



        Console.Clear();
        songServices.ChoseMusic(thisisid);
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Player Options: \n SPACE - PLAY \n P - PAUSE \n N - NEXT MUSIC \n B - PREVIOUS MUSIC \n O - NEW MUSIC FROM LIST \n F -FAST FORWARD \n R - REVERSE \n V - VOLUME");




            Console.WriteLine();
           

            songServices.MusicDetiles();

          

            
            if (Console.KeyAvailable)
            {

                option = Console.ReadKey(true).Key;


                switch (option)
                {
                    case ConsoleKey.Spacebar:
                        songServices.PlayMusic();
                        isOnPause = false;
                        break;

                    case ConsoleKey.P:
                        songServices.PauseMusic();
                        isOnPause = true;
                        break;

                    case ConsoleKey.O:
                        Console.Clear();
                        foreach (var item in songServices.songsList)
                        {
                            Console.WriteLine($"MussId: {item.Id} Name: {item.Name}  , Directory: ");
                        }
                        Console.WriteLine("Chose Song To Play By Id :");
                        thisisid = int.Parse(Console.ReadLine());
                        songServices.ChoseMusic(thisisid);
                        break;

                    case ConsoleKey.N:
                        thisisid += 1;
                        songServices.ChoseMusic(thisisid);
                        break;

                    case ConsoleKey.B:
                        thisisid -= 1;
                        songServices.ChoseMusic(thisisid);
                        break;

                    case ConsoleKey.V:
                        isOnPause = true;
                        songServices.VolumeChange();
                        isOnPause = false;
                        break;

                        case ConsoleKey.F:

                        songServices.FastForward();
                        break;

                    case ConsoleKey.R:

                        songServices.FastReverse();
                        break;

                    default:
                        break;
                }
            }

           


            Thread.Sleep(1000);
        }












    }
}
