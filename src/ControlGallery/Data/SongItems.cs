using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ControlGallery.Data
{

    public static class SongItems
    {

        private const string Classic = "Classic";
        private const string Pop = "Pop";
        private const string Metal = "Metal";

        public static ObservableCollection<Song> Songs = new ObservableCollection<Song>()
            {
                // Classic
                new Song() { Title = "Goldberg Variations", Genre = Classic, Duration = 3.05, Likes = 12394, LikedByMe = false },
                new Song() { Title = "Pavane, Op.50", Genre = Classic, Duration = 5.37, Likes = 3829, LikedByMe = false },
                new Song() { Title = "I Giorni", Genre = Classic, Duration = 5.51, Likes = 12394, LikedByMe = true },
                new Song() { Title = "River Flows in You", Genre = Classic, Duration = 3.38, Likes = 14299, LikedByMe = true },
                new Song() { Title = "Fly", Genre = Classic, Duration = 3.50, Likes = 94, LikedByMe = false },
                new Song() { Title = "Bring Him Home", Genre = Classic, Duration = 4.15, Likes = 235389, LikedByMe = true },
                    
                // Pop
                new Song() { Title = "The Middle", Genre = Pop, Duration = 3.05, Likes = 2398499, LikedByMe = true },
                new Song() { Title = "IDGAF", Genre = Pop, Duration = 3.38, Likes = 284818, LikedByMe = false },
                new Song() { Title = "God is a woman", Genre = Pop, Duration = 3.18, Likes = 84882, LikedByMe = false },
                new Song() { Title = "Back To You", Genre = Pop, Duration = 3.30, Likes = 34595, LikedByMe = true },
                new Song() { Title = "Him & I", Genre = Pop, Duration = 4.29, Likes = 768298, LikedByMe = true },
                new Song() { Title = "Paris", Genre = Pop, Duration = 3.42, Likes = 19292, LikedByMe = false },

                // Metal
                new Song() { Title = "Hail to the King", Genre = Metal, Duration = 5.06, Likes = 2148, LikedByMe = true },
                new Song() { Title = "Duality", Genre = Metal, Duration = 4.13, Likes = 242448, LikedByMe = false },
                new Song() { Title = "Wash It All Away", Genre = Metal, Duration = 3.45, Likes = 6748, LikedByMe = false },
            }
            .Shuffle<Song, ObservableCollection<Song>>();


        // Taken from https://stackoverflow.com/questions/273313/randomize-a-listt/1262619#1262619
        private static TCollection Shuffle<T, TCollection>(this TCollection list)
            where TCollection : IList<T>
        {
            var rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

    }

    public class Song : BindableBase
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public double Duration { get; set; } // Yes, this shouldn't be a double, but these data sources are only for demonstration, so w/e
        public int Likes { get; set; }
        public bool LikedByMe { get; set; }
    }

}
