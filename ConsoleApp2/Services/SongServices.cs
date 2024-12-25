using ATL;
using ConsoleApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace ConsoleApp2.Services
{
    internal class SongServices
    {
        private int id { get; set; }
        private bool isOnPause { get; set; }    
        private int CurrentTimer {  get; set; }


        WindowsMediaPlayer myplayer { get; set; } = new WindowsMediaPlayer();
        public List<Song> songsList { get; set; } = new List<Song>();

        public SongServices(string path)
        {
            Console.WriteLine("In Progres");
            AddLibrari(path);
            Console.WriteLine(" \n Music list");
        }

        public void ChoseMusic(int musicId)
        {

            try
            {

                var musicByMyId = songsList.FirstOrDefault(d => d.Id == musicId);
                myplayer.URL = musicByMyId.Directory;
            }
            catch (Exception ms)
            {
                Console.WriteLine(ms.Message);
                throw;
            }

            id = musicId;

        }

        public void VolumeChange()
        {
            Console.WriteLine();
            Console.WriteLine("Enter Volume 0 - 100");
            int volume = int.Parse(Console.ReadLine());

            myplayer.settings.volume = volume;

        }

        

        public void MusicDetiles()
        {
            var musicByMyId = songsList.FirstOrDefault(d => d.Id == id);
            musicByMyId.Volume = myplayer.settings.volume;

            int totalTime = musicByMyId.Duration;
            int barlength = 50;
            int progress = (int)((float)CurrentTimer / totalTime * barlength);

            Console.Write("[");
            for (int i = 0; i <= barlength; i++)
            {
                if (i < progress)
                    Console.Write("=");
                else
                    Console.Write(" ");
            }
            Console.Write($"] {myplayer.controls.currentPosition:F2}/{musicByMyId.Duration}s  ID: {musicByMyId.Id}  NAME: {musicByMyId.Name} \n Volume: {musicByMyId.Volume} ");



            int currentTimeOnPlayer = (int)myplayer.controls.currentPosition;
            int totalTimeOnPlayer = (int)myplayer.currentMedia.duration;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(myplayer.controls.currentPosition);
            Console.WriteLine(myplayer.currentMedia.duration);
            Console.WriteLine();
            Console.WriteLine();
            if (myplayer.controls.currentPosition >= myplayer.currentMedia.duration - 2 && currentTimeOnPlayer > 0)
            {
                ChoseMusic(musicByMyId.Id+1);
            }


        }

        public void PlayMusic()
        {
            isOnPause = false;
            myplayer.controls.play();

        }

        public void FastForward() 
        {
            Console.WriteLine("Fast Forwad");
            myplayer.controls.fastForward();

        }

        public void FastReverse() 
        {

            Console.WriteLine("5 Sec Reverse");

            if (myplayer.controls.currentPosition > 5) // Avoid going negative
            {
                myplayer.controls.currentPosition -= 5; // Adjust the step as needed
            }
            else
            {
                myplayer.controls.currentPosition = 0; // Reset to start if near the beginning
            }

            

        }


        public void PauseMusic()
        {
            isOnPause = true;
            myplayer.controls.pause();

        }

        private void AddLibrari(string path)
        {
            DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(path);
            FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles();
            int id = 1;
            foreach (FileInfo foundFile in filesInDir)
            {
                var track = new Track(foundFile.FullName);
                songsList.Add(new Song
                {
                    Id = id,
                    Name = track.Title,
                    Album = track.Album,
                    Artist = track.Artist,
                    Duration = track.Duration,
                    Directory = foundFile.FullName
                });
                id++;
            }

        }






    }
}
