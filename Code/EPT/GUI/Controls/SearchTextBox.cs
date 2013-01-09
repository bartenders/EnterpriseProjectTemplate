using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace EPT.GUI.Controls
{
    public enum SearchMode
    {
        Instant,
        Delayed,
    }

    /// <summary>
    /// http://davidowens.wordpress.com/2009/02/18/wpf-search-text-box/
    /// </summary>
    public class SearchTextBox : TextBox
    {
        public static DependencyProperty LabelTextProperty =
            DependencyProperty.Register(
                "LabelText",
                typeof (string),
                typeof (SearchTextBox));

        public static DependencyProperty LabelTextColorProperty =
            DependencyProperty.Register(
                "LabelTextColor",
                typeof (Brush),
                typeof (SearchTextBox));

        public static DependencyProperty SearchModeProperty =
            DependencyProperty.Register(
                "SearchMode",
                typeof (SearchMode),
                typeof (SearchTextBox),
                new PropertyMetadata(SearchMode.Instant));

        private static readonly DependencyPropertyKey HasTextPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "HasText",
                typeof (bool),
                typeof (SearchTextBox),
                new PropertyMetadata());

        public static DependencyProperty HasTextProperty = HasTextPropertyKey.DependencyProperty;

        private static readonly DependencyPropertyKey IsMouseLeftButtonDownPropertyKey =
            DependencyProperty.RegisterReadOnly(
                "IsMouseLeftButtonDown",
                typeof (bool),
                typeof (SearchTextBox),
                new PropertyMetadata());

        public static DependencyProperty IsMouseLeftButtonDownProperty =
            IsMouseLeftButtonDownPropertyKey.DependencyProperty;

        public static DependencyProperty SearchEventTimeDelayProperty =
            DependencyProperty.Register(
                "SearchEventTimeDelay",
                typeof (Duration),
                typeof (SearchTextBox),
                new FrameworkPropertyMetadata(
                    new Duration(new TimeSpan(0, 0, 0, 0, 500)),
                    OnSearchEventTimeDelayChanged));

        public static readonly RoutedEvent SearchEvent =
            EventManager.RegisterRoutedEvent(
                "Search",
                RoutingStrategy.Bubble,
                typeof (RoutedEventHandler),
                typeof (SearchTextBox));

        private readonly DispatcherTimer _searchEventDelayTimer;

        static SearchTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(
                typeof (SearchTextBox),
                new FrameworkPropertyMetadata(typeof (SearchTextBox)));
        }

        public SearchTextBox()
        {
            _searchEventDelayTimer = new DispatcherTimer {Interval = SearchEventTimeDelay.TimeSpan};
            _searchEventDelayTimer.Tick += OnSeachEventDelayTimerTick;
        }

        [Category("SearchOptions")]
        public string LabelText
        {
            get { return (string) GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        [Category("SearchOptions")]
        public Brush LabelTextColor
        {
            get { return (Brush) GetValue(LabelTextColorProperty); }
            set { SetValue(LabelTextColorProperty, value); }
        }

        [Category("SearchOptions")]
        public SearchMode SearchMode
        {
            get { return (SearchMode) GetValue(SearchModeProperty); }
            set { SetValue(SearchModeProperty, value); }
        }

        [Category("SearchOptions")]
        public bool HasText
        {
            get { return (bool) GetValue(HasTextProperty); }
            private set { SetValue(HasTextPropertyKey, value); }
        }

        [Category("SearchOptions")]
        public Duration SearchEventTimeDelay
        {
            get { return (Duration) GetValue(SearchEventTimeDelayProperty); }
            set { SetValue(SearchEventTimeDelayProperty, value); }
        }

        public bool IsMouseLeftButtonDown
        {
            get { return (bool) GetValue(IsMouseLeftButtonDownProperty); }
            private set { SetValue(IsMouseLeftButtonDownPropertyKey, value); }
        }

        private void OnSeachEventDelayTimerTick(object o, EventArgs e)
        {
            _searchEventDelayTimer.Stop();
            RaiseSearchEvent();
        }

        private static void OnSearchEventTimeDelayChanged(
            DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var stb = o as SearchTextBox;
            if (stb == null) return;
            stb._searchEventDelayTimer.Interval = ((Duration) e.NewValue).TimeSpan;
            stb._searchEventDelayTimer.Stop();
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);

            HasText = Text.Length != 0;

            if (SearchMode != SearchMode.Instant) return;
            _searchEventDelayTimer.Stop();
            _searchEventDelayTimer.Start();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var iconBorder = GetTemplateChild("PART_SearchIconBorder") as Border;
            if (iconBorder == null) return;
            iconBorder.MouseLeftButtonDown += IconBorder_MouseLeftButtonDown;
            iconBorder.MouseLeftButtonUp += IconBorder_MouseLeftButtonUp;
            iconBorder.MouseLeave += IconBorder_MouseLeave;
        }

        private void IconBorder_MouseLeftButtonDown(object obj, MouseButtonEventArgs e)
        {
            IsMouseLeftButtonDown = true;
        }

        private void IconBorder_MouseLeftButtonUp(object obj, MouseButtonEventArgs e)
        {
            if (!IsMouseLeftButtonDown) return;

            if (HasText && SearchMode == SearchMode.Instant)
            {
                Text = "";
            }

            if (HasText && SearchMode == SearchMode.Delayed)
            {
                RaiseSearchEvent();
            }

            IsMouseLeftButtonDown = false;
        }

        private void IconBorder_MouseLeave(object obj, MouseEventArgs e)
        {
            IsMouseLeftButtonDown = false;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.Escape && SearchMode == SearchMode.Instant)
            {
                Text = "";
            }
            else if ((e.Key == Key.Return || e.Key == Key.Enter) &&
                     SearchMode == SearchMode.Delayed)
            {
                RaiseSearchEvent();
            }
            else
            {
                base.OnKeyDown(e);
            }
        }

        private void RaiseSearchEvent()
        {
            var args = new RoutedEventArgs(SearchEvent);
            RaiseEvent(args);
        }

        [Category("SearchHandler")]
        public event RoutedEventHandler Search
        {
            add { AddHandler(SearchEvent, value); }
            remove { RemoveHandler(SearchEvent, value); }
        }
    }
}