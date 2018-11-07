using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ControlGallery.Data
{

    /// <summary>
    /// Provides string item sources with a varying number of items.
    /// </summary>
    public static class LoremItems
    {

        private const string Lorem = "Lorem ipsum dolor sit amet consetetur sadipscing elitr sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat sed diam voluptua";
        private static readonly string[] _loremWords = Lorem.Split(new char[] { ' ' });

        public static ObservableCollection<string> Lorem5 =>
            new ObservableCollection<string>(PickWords(5));

        public static ObservableCollection<string> Lorem10 =>
            new ObservableCollection<string>(PickWords(10));

        public static ObservableCollection<string> Lorem50 =>
            new ObservableCollection<string>(PickWords(50));

        public static ObservableCollection<string> Lorem100 =>
            new ObservableCollection<string>(PickWords(100));

        public static ObservableCollection<string> Lorem1000 =>
            new ObservableCollection<string>(PickWords(1000));

        private static IEnumerable<string> PickWords(int count)
        {
            if (count < 0)
                yield break;

            int loremWordsIndex = 0;
            for (int i = 0; i < count; i++)
            {
                if (loremWordsIndex == _loremWords.Length)
                    loremWordsIndex = 0;

                yield return _loremWords[loremWordsIndex++];
            }
        }

    }

}
