using System.ComponentModel;

namespace Algo4
{
    public class CharColorItem : INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (value != id)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private char character;
        public char Character
        {
            get { return character; }
            set
            {
                if (value != character)
                {
                    character = value;
                    OnPropertyChanged(nameof(Character));
                }
            }
        }

        private Color textColor;
        public Color TextColor
        {
            get { return textColor; }
            set
            {
                if (value != textColor)
                {
                    textColor = value;
                    OnPropertyChanged(nameof(TextColor));
                }
            }
        }

        private Color borderColor;
        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                if (value != borderColor)
                {
                    borderColor = value;
                    OnPropertyChanged(nameof(BorderColor));
                }
            }
        }

        private Color backGroudColor;
        public Color BackGroundColor
        {
            get { return backGroudColor; }
            set
            {
                if (value != backGroudColor)
                {
                    backGroudColor = value;
                    OnPropertyChanged(nameof(BackGroundColor));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}