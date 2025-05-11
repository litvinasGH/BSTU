using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CustomControls
{
    public class NumericUpDown : Control
    {
        static NumericUpDown() => DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown), new FrameworkPropertyMetadata(typeof(NumericUpDown)));

        // Min property
        public static readonly DependencyProperty MinProperty = DependencyProperty.Register(
            nameof(Min), typeof(int), typeof(NumericUpDown),
            new PropertyMetadata(0, OnMinMaxChanged));

        public int Min
        {
            get => (int)GetValue(MinProperty);
            set => SetValue(MinProperty, value);
        }

        // Max property
        public static readonly DependencyProperty MaxProperty = DependencyProperty.Register(
            nameof(Max), typeof(int), typeof(NumericUpDown),
            new PropertyMetadata(100, OnMinMaxChanged));

        public int Max
        {
            get => (int)GetValue(MaxProperty);
            set => SetValue(MaxProperty, value);
        }

        private static void OnMinMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)d;
            control.CoerceValue(ValueProperty);
        }

        // Step property
        public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
            nameof(Step), typeof(int), typeof(NumericUpDown),
            new PropertyMetadata(1));

        public int Step
        {
            get => (int)GetValue(StepProperty);
            set => SetValue(StepProperty, value);
        }

        // Value property
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
            nameof(Value), typeof(int), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(0, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnValueChanged, CoerceValue),
            ValidateValueCallback);

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        private static bool ValidateValueCallback(object v) => v is int;

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            var control = (NumericUpDown)d;
            var val = (int)baseValue;
            if (val < control.Min) return control.Min;
            if (val > control.Max) return control.Max;
            return val;
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (NumericUpDown)d;
            control.RaiseValueChangedEvent((int)e.OldValue, (int)e.NewValue);
        }

        // RoutedEvent: Direct
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent(
            "ValueChanged", RoutingStrategy.Direct, typeof(RoutedPropertyChangedEventHandler<int>), typeof(NumericUpDown));

        public event RoutedPropertyChangedEventHandler<int> ValueChanged
        {
            add => AddHandler(ValueChangedEvent, value);
            remove => RemoveHandler(ValueChangedEvent, value);
        }

        void RaiseValueChangedEvent(int oldVal, int newVal)
        {
            var args = new RoutedPropertyChangedEventArgs<int>(oldVal, newVal) { RoutedEvent = ValueChangedEvent };
            RaiseEvent(args);
        }

        // Increment/Decrement commands
        public static RoutedUICommand IncreaseCommand = new RoutedUICommand("Increase", "Increase", typeof(NumericUpDown));
        public static RoutedUICommand DecreaseCommand = new RoutedUICommand("Decrease", "Decrease", typeof(NumericUpDown));

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            if (GetTemplateChild("PART_Increase") is Button inc)
                inc.Command = IncreaseCommand;
            if (GetTemplateChild("PART_Decrease") is Button dec)
                dec.Command = DecreaseCommand;

            CommandBindings.Add(new CommandBinding(IncreaseCommand, (s, e) => Value += Step, (s, e) => e.CanExecute = Value + Step <= Max));
            CommandBindings.Add(new CommandBinding(DecreaseCommand, (s, e) => Value -= Step, (s, e) => e.CanExecute = Value - Step >= Min));
        }
    }
}
