using System;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;  // Import this namespace to work with Bitmap

namespace FuehrerscheinCreator
{
    public partial class Preview : UserControl
    {
        public Preview()
        {
            InitializeComponent();
            DataContext = this;
            FirstName = "[FirstName]";
            LastName = "[LastName]";
            Birthday = "[Birthday]";

            Selected = new Bitmap("Assets/profile.png");
        
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public static readonly StyledProperty<string> FirstNameProperty =
            AvaloniaProperty.Register<Preview, string>(nameof(FirstName));

        public string FirstName
        {
            get => GetValue(FirstNameProperty);
            set => SetValue(FirstNameProperty, string.IsNullOrWhiteSpace(value) ? "[FirstName]" : value);
        }

        public static readonly StyledProperty<string> LastNameProperty =
            AvaloniaProperty.Register<Preview, string>(nameof(LastName));

        public string LastName
        {
            get => GetValue(LastNameProperty);
            set => SetValue(LastNameProperty, string.IsNullOrWhiteSpace(value) ? "[LastName]" : value);
        }

        // Change Selected to hold a Bitmap (not just a string)
        public static readonly StyledProperty<Bitmap> SelectedProperty =
            AvaloniaProperty.Register<Preview, Bitmap>(nameof(Selected));

        // Update the Selected property to return a Bitmap
        public Bitmap Selected
        {
            get => GetValue(SelectedProperty);
            set => SetValue(SelectedProperty, value);
        }

        public static readonly StyledProperty<string> BirthdayProperty =
            AvaloniaProperty.Register<Preview, string>(nameof(Birthday));

        public string Birthday
        {
            get => GetValue(BirthdayProperty);
            set => SetValue(BirthdayProperty, string.IsNullOrWhiteSpace(value) ? "[Birthday]" : value);
        }

        public string CreatedAt => DateTime.Now.ToString("dd.MM.yyyy");
        public string ExpiresAt => DateTime.Now.AddYears(4).ToString("dd.MM.yyyy");
    }
}
