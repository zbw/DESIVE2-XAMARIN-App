using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Desive2.CustomViews
{
    // Enables compilation of XAML to improve performance.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Accordion : ContentView
    {
        // Bindable property for the indicator view, typically used to display a visual cue (e.g., arrow).
        public static readonly BindableProperty IndicatorViewProperty = BindableProperty.Create(
            nameof(IndicatorView), typeof(View), typeof(Accordion), default(View));

        public View IndicatorView
        {
            get => (View)GetValue(IndicatorViewProperty);
            set => SetValue(IndicatorViewProperty, value);
        }

        // Bindable property for the content view, which holds the collapsible content of the accordion.
        public static readonly BindableProperty ContentViewProperty = BindableProperty.Create(
            nameof(AccordionContentView), typeof(View), typeof(Accordion), default(View));

        public View AccordionContentView
        {
            get => (View)GetValue(ContentViewProperty);
            set => SetValue(ContentViewProperty, value);
        }

        // Bindable property for the title text of the accordion.
        public static readonly BindableProperty TitleBindableProperty = BindableProperty.Create(
            nameof(Title), typeof(string), typeof(Accordion), default(string));

        public string Title
        {
            get => (string)GetValue(TitleBindableProperty);
            set => SetValue(TitleBindableProperty, value);
        }

        // Bindable property indicating whether the accordion is open or closed. Triggers a state change when toggled.
        public static readonly BindableProperty IsOpenBindablePropertyProperty = BindableProperty.Create(
            nameof(IsOpen), typeof(bool), typeof(Accordion), false, propertyChanged: IsOpenChanged);

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenBindablePropertyProperty); }
            set { SetValue(IsOpenBindablePropertyProperty, value); }
        }

        // Bindable property for the background color of the accordion header.
        public static readonly BindableProperty HeaderBackgroundColorProperty = BindableProperty.Create(
            nameof(HeaderBackgroundColor), typeof(Color), typeof(Accordion), Color.Transparent);

        public Color HeaderBackgroundColor
        {
            get { return (Color)GetValue(HeaderBackgroundColorProperty); }
            set { SetValue(HeaderBackgroundColorProperty, value); }
        }

        // Callback method triggered when the IsOpen property changes. Handles opening and closing animations.
        private static void IsOpenChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is Accordion control && newValue is bool isOpen)
            {
                if (!isOpen)
                {
                    VisualStateManager.GoToState(control, "Open");
                    control.Close();
                }
                else
                {
                    VisualStateManager.GoToState(control, "Closed");
                    control.Open();
                }
            }
        }

        // Animation duration for open and close transitions, defined in milliseconds.
        public uint AnimationDuration { get; set; }

        // Constructor initializing the accordion with default settings.
        public Accordion()
        {
            InitializeComponent();
            Close(); // Initially closes the accordion.
            AnimationDuration = 250; // Sets default animation duration.
            IsOpen = false; // Sets initial state as closed.
        }

        // Asynchronous method to close the accordion with animations for translation, rotation, and fading.
        async void Close()
        {
            await Task.WhenAll(
                _accContent.TranslateTo(0, -10, AnimationDuration),
                _indicatorContainer.RotateTo(-180, AnimationDuration),
                _accContent.FadeTo(0, 50)
            );
            _accContent.IsVisible = false; // Hides the content after animation completes.
        }

        // Asynchronous method to open the accordion with animations for translation, rotation, and fading.
        async void Open()
        {
            _accContent.IsVisible = true; // Makes the content visible before animation starts.
            await Task.WhenAll(
                _accContent.TranslateTo(0, 10, AnimationDuration),
                _indicatorContainer.RotateTo(0, AnimationDuration),
                _accContent.FadeTo(30, 50, Easing.SinIn)
            );
        }

        // Event handler for tapping on the accordion header. Toggles the IsOpen property.
        private void TitleTapped(object sender, EventArgs e)
        {
            IsOpen = !IsOpen;
        }
    }
}
