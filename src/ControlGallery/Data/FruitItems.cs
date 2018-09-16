using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ControlGallery.Data
{

    // A very simple, static data source.

    public class Fruit
    {
        public string Name { get; set; }
        public Color Color { get; set; }

        public Fruit(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public override string ToString() => Name;
    }

    public static class FruitItems
    {

        public static ObservableCollection<Fruit> Fruits { get; }

        static FruitItems()
        {
            Fruits = new ObservableCollection<Fruit>()
            {
                new Fruit("Strawberry", Colors.DarkRed),
                new Fruit("Blueberry", Colors.DarkBlue),
                new Fruit("Pineapple", Colors.Yellow),
                new Fruit("Orange", Colors.Orange)
            };
        }

    }

}
