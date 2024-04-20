namespace Wpf.Templates.Elements
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    using Wpf.Templates.Models;

    /// <summary>
    /// ColorPicker VM для элемента, избиратель цвета.
    /// </summary>
    public class ColorPicker : Control
    {
        /// <summary>
        /// Свойство зависимости R.
        /// </summary>
        public static readonly DependencyProperty RValueDependency =
            DependencyProperty.Register(
                nameof(RValue),
                typeof(byte),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(
                    (byte)0,
                    RValue_Changed));

        /// <summary>
        /// Свойство зависимости G.
        /// </summary>
        public static readonly DependencyProperty GValueDependency =
            DependencyProperty.Register(
                nameof(GValue),
                typeof(byte),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(
                    (byte)0,
                    GValue_Changed));

        /// <summary>
        /// Свойство зависимости B.
        /// </summary>
        public static readonly DependencyProperty BValueDependency =
            DependencyProperty.Register(
                nameof(BValue),
                typeof(byte),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(
                    (byte)0,
                    BValue_Changed));

        /// <summary>
        /// Color по умолчанию.
        /// </summary>
        private static readonly Color DefaultColor = Color.FromRgb(0, 0, 0);

        /// <summary>
        /// Свойство зависимости Color.
        /// </summary>
        public static readonly DependencyProperty ColorDependency =
            DependencyProperty.Register(
                nameof(Color),
                typeof(Color),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(
                    DefaultColor));

        /// <summary>
        /// Свойство зависимости hex входа.
        /// </summary>
        public static readonly DependencyProperty HexInputDependency =
            DependencyProperty.Register(
                nameof(HexInput),
                typeof(string),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(DEFAULT_HEX, HexInput_Changed));

        /// <summary>
        /// Свойство зависимости ColorHex.
        /// </summary>
        public static readonly DependencyProperty ColorHexDependency =
            DependencyProperty.Register(
                nameof(ColorHex),
                typeof(ColorHex),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(ColorHex_Changed));

        /// <summary>
        /// Для Noesis при определении <see cref="FrameworkPropertyMetadata" /> нужно указывать const значение.
        /// </summary>
        private const string DEFAULT_HEX = "#000000";


        /// <summary>
        /// Проверка, изменяется ли сейчас ColorHex.
        /// </summary>
        /// <remarks> Избавляемся от бесконечного цикла, при установке свойств. </remarks>
        private bool _isMute;

        /// <summary>
        /// Свойство BValue.
        /// </summary>
        public byte BValue
        {
            get => (byte)GetValue(BValueDependency);
            set => SetValue(BValueDependency, value);
        }

        /// <summary>
        /// Свойство Color.
        /// </summary>
        public Color Color
        {
            get => (Color)GetValue(ColorDependency);
            set => SetValue(ColorDependency, value);
        }

        /// <summary>
        /// Свойство ColorHex.
        /// </summary>
        /// <remarks> Основное свойство для Binding цвета. </remarks>
        public ColorHex ColorHex
        {
            get => (ColorHex)GetValue(ColorHexDependency);
            set => SetValue(ColorHexDependency, value);
        }

        /// <summary>
        /// Свойство GValue.
        /// </summary>
        public byte GValue
        {
            get => (byte)GetValue(GValueDependency);
            set => SetValue(GValueDependency, value);
        }

        /// <summary>
        /// Свойство HexInput.
        /// </summary>
        public string HexInput
        {
            get => (string)GetValue(HexInputDependency);
            set => SetValue(HexInputDependency, value);
        }

        /// <summary>
        /// Свойство RValue.
        /// </summary>
        public byte RValue
        {
            get => (byte)GetValue(RValueDependency);
            set => SetValue(RValueDependency, value);
        }

        /// <summary>
        /// Действие при изменении BValue свойства зависимости.
        /// </summary>
        /// <param name="dependencyObject"> Свойство зависимости. </param>
        /// <param name="args"> Аргументы свойства зависимости. </param>
        private static void BValue_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (!(dependencyObject is ColorPicker colorPicker))
                return;

            RGB_Changed(colorPicker);
        }

        /// <summary>
        /// Действие при изменении ColorHex свойства зависимости.
        /// </summary>
        /// <param name="dependencyObject"> Свойство зависимости. </param>
        /// <param name="args"> Аргументы свойства зависимости. </param>
        private static void ColorHex_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (!(dependencyObject is ColorPicker colorPicker))
                return;

            if (colorPicker._isMute)
                return;

            colorPicker._isMute = true;

            var colorHex = (ColorHex)args.NewValue;

            colorPicker.RValue = colorHex.R;
            colorPicker.GValue = colorHex.G;
            colorPicker.BValue = colorHex.B;

            colorPicker.Color = Color.FromRgb(colorHex.R, colorHex.G, colorHex.B);
            colorPicker.HexInput = colorHex.Hex;

            colorPicker._isMute = false;
        }

        /// <summary>
        /// Действие при изменении GValue свойства зависимости.
        /// </summary>
        /// <param name="dependencyObject"> Свойство зависимости. </param>
        /// <param name="args"> Аргументы свойства зависимости. </param>
        private static void GValue_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (!(dependencyObject is ColorPicker colorPicker))
                return;

            RGB_Changed(colorPicker);
        }

        /// <summary>
        /// Действие при изменении HexInput свойства зависимости.
        /// </summary>
        /// <param name="dependencyObject"> Свойство зависимости. </param>
        /// <param name="args"> Аргументы свойства зависимости. </param>
        private static void HexInput_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (!(dependencyObject is ColorPicker colorPicker))
                return;

            if (colorPicker._isMute)
                return;

            var hexCode = (string)args.NewValue;

            if (ColorHex.IsHex(hexCode))
            {
                colorPicker.ColorHex = ColorHex.FromHex(hexCode);
                return;
            }

            colorPicker.HexInput = DEFAULT_HEX;
        }

        /// <summary>
        /// Действие при изменении R, G, B свойства зависимости.
        /// </summary>
        /// <param name="colorPicker"> ColorPicker, в котором нужно поменять значение свойств. </param>
        private static void RGB_Changed(ColorPicker colorPicker)
        {
            if (colorPicker._isMute)
                return;

            colorPicker.ColorHex = ColorHex.FromRgb(colorPicker.RValue, colorPicker.GValue, colorPicker.BValue);
        }

        /// <summary>
        /// Действие при изменении RValue свойства зависимости.
        /// </summary>
        /// <param name="dependencyObject"> Свойство зависимости. </param>
        /// <param name="args"> Аргументы свойства зависимости. </param>
        private static void RValue_Changed(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs args)
        {
            if (!(dependencyObject is ColorPicker colorPicker))
                return;

            RGB_Changed(colorPicker);
        }
    }
}