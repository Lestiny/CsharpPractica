using System;

namespace MediaPlayerApp
{
    interface IVideoPlayer
    {
        void Play(string fileName);
    }

    interface IAudioPlayer
    {
        void Play(string fileName);
    }

    class MediaDevice : IVideoPlayer, IAudioPlayer
    {
        void IVideoPlayer.Play(string fileName)
        {
            Console.WriteLine("Воспроизведение видео файла: " + fileName);
        }

        void IAudioPlayer.Play(string fileName)
        {
            Console.WriteLine("Воспроизведение аудио файла: " + fileName);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            MediaDevice device = new MediaDevice();

            IVideoPlayer videoPlayer = device;
            IAudioPlayer audioPlayer = device;

            videoPlayer.Play("mmmmm.mp4");
            audioPlayer.Play("nnnnn.mp3");

            Console.ReadKey();
        }
    }
}