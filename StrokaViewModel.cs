using System;
using System.Collections.ObjectModel;

namespace Algo4
{
    public class StrokaViewModel
    {
        public ObservableCollection<CharColorItem> CharColorItems { get; } = new ObservableCollection<CharColorItem>();

        Color[] defColors = { Color.FromArgb("#3B82F8"), Colors.Transparent, Colors.Transparent };

        Color window = Colors.Yellow;
        public int windowStart;

        public int currentComparison = 0;

        public StrokaViewModel(string initialString)
        {
            UpdateCollection(initialString);
        }

        public void UpdateCollection(string inputString)
        {
            CharColorItems.Clear();
            int id = 0;
            foreach (char character in inputString)
            {
                CharColorItems.Add(new CharColorItem { Id = id++, Character = character, TextColor = defColors[0], BorderColor = defColors[1], BackGroundColor = defColors[2] });
            }
        }

        public void updateComparison(int newID)
        {
            this.currentComparison = newID;
            CharColorItems[currentComparison].TextColor = Colors.Yellow;
        }

        public void Highlight(int id, int length)
        {
            for (int i = id; i < id + length; i++)
            {
                CharColorItems[i].TextColor = Colors.Green;
                CharColorItems[i].BorderColor = Colors.Green;
            }
        }

        public void setWindoW(int length)
        {
            for (int i = windowStart; i < length; i++)
            {
                CharColorItems[i].BorderColor = Colors.Yellow;
            }
        }

        public void shiftWindow(int windowStart, int dlinaSlova)
        {
            for (int i = windowStart - 1; i >= this.windowStart; i--)
            {
                CharColorItems[i].BorderColor = defColors[1];
            }
            for (int i = windowStart; i < windowStart + dlinaSlova; i++)
            {
                CharColorItems[i].BorderColor = Colors.Yellow;
            }
        }

        public void ResetColors()
        {
            for (int i = 0; i < CharColorItems.Count; i++)
            {
                CharColorItems[i].TextColor = defColors[0];
                CharColorItems[i].BorderColor = defColors[1];
                CharColorItems[i].BackGroundColor = defColors[2];
            }
        }
    }
}

