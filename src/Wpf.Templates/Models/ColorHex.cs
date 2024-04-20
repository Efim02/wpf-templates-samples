namespace Wpf.Templates.Models
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Структура цвета, для svg параметров.
    /// </summary>
    /// <remarks>
    /// ColorHex т.к. цвет хранится, в текстовом формате hex.
    /// Создаем собственный тип Color, что бы не ссылаться на другие сборки, в частности на PresentationCore.
    /// </remarks>
    public struct ColorHex
    {
        /// <summary>
        /// Красный.
        /// </summary>
        public byte R { get; private set; }

        /// <summary>
        /// Зеленый.
        /// </summary>
        public byte G { get; private set; }

        /// <summary>
        /// Синий.
        /// </summary>
        public byte B { get; private set; }

        /// <summary>
        /// Создать Color, указав RGB значения.
        /// </summary>
        /// <param name="r"> Красный. </param>
        /// <param name="g"> Зеленый. </param>
        /// <param name="b"> Синий. </param>
        private ColorHex(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
            _hex = $"#{ToHexString(r)}{ToHexString(g)}{ToHexString(b)}";
        }

        /// <summary>
        /// Текстовое Hex представление цвета.
        /// </summary>
        private string _hex;

        /// <summary>
        /// Получить текстовое hex представление.
        /// </summary>
        public string Hex
        {
            get => _hex;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception($"Значение {value} является null.");

                if (value.Length != 7)
                    throw new Exception($"Значение {value} имеет отличную длину от 7.");

                var regex = new Regex(@"\A#[0-9A-Fa-f]{6}");
                if (!regex.IsMatch(value))
                    throw new Exception($"Значение {value} не является Hex значением.");

                _hex = value;
            }
        }

        /// <summary>
        /// Установить значения R, G, B, Hex.
        /// </summary>
        /// <param name="hexCode"> HexCode. </param>
        private void SetValues(string hexCode)
        {
            if (!IsHex(hexCode))
                throw new Exception("HexCode, не требуемого вида. Пример: #111000.");

            Hex = hexCode;
            hexCode = hexCode.Substring(1);

            R = Convert.ToByte(hexCode.Substring(0, 2), 16);
            G = Convert.ToByte(hexCode.Substring(2, 2), 16);
            B = Convert.ToByte(hexCode.Substring(4, 2), 16);
        }

        /// <summary>
        /// Белый цвет.
        /// </summary>
        public static readonly ColorHex White = new ColorHex(255, 255, 255);

        /// <summary>
        /// Черный цвет.
        /// </summary>
        public static readonly ColorHex Black = new ColorHex(0, 0, 0);

        /// <summary>
        /// Цвет по умолчанию.
        /// </summary>
        public static readonly ColorHex Default = Black;

        /// <summary>
        /// Установить цвет.
        /// </summary>
        /// <param name="r"> Красный. </param>
        /// <param name="g"> Зеленый. </param>
        /// <param name="b"> Синий. </param>
        public static ColorHex FromRgb(byte r, byte g, byte b)
        {
            return new ColorHex(r, g, b);
        }

        /// <summary>
        /// Установить цвет.
        /// </summary>
        /// <param name="hexCode"> Текстовое hex представление. </param>
        /// <returns> </returns>
        public static ColorHex FromHex(string hexCode)
        {
            var colorHex = new ColorHex();
            colorHex.SetValues(hexCode);
            return colorHex;
        }

        /// <summary>
        /// Проверка на валидность hex кода.
        /// </summary>
        /// <param name="hexCode"> Код. </param>
        /// <returns> Возвращает true, если валиден. </returns>
        public static bool IsHex(string hexCode)
        {
            if (string.IsNullOrEmpty(hexCode))
                return false;

            if (hexCode.Length != 7)
                return false;

            var regex = new Regex(@"\A#[0-9A-Fa-f]{6}");
            return regex.IsMatch(hexCode);
        }

        /// <summary>
        /// Цвет в hex формате, хранит числа в 2 символах.
        /// </summary>
        private static string ToHexString(byte param)
        {
            var numbers = Convert.ToString(param, 16);
            return numbers.Length == 2 ? numbers : $"0{numbers}";
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ColorHex - {Hex}; R:{R} G:{G} B:{B}";
        }
    }
}