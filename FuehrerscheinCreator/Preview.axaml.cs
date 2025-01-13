using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

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
            set => SetValue(LastNameProperty,  string.IsNullOrWhiteSpace(value) ? "[LastName]" : value);
        }

        public static readonly StyledProperty<string> BirthdayProperty =
            AvaloniaProperty.Register<Preview, string>(nameof(Birthday));

        public string Birthday
        {
            get => GetValue(BirthdayProperty);
            set => SetValue(BirthdayProperty,  string.IsNullOrWhiteSpace(value) ? "[BirthDay]" : value);
        }
        public string CreatedAt =>  DateTime.Now.ToString("dd.MM.yyyy");
        public string ExpiresAt => DateTime.Now.AddYears(4).ToString("dd.MM.yyyy");
    }
}