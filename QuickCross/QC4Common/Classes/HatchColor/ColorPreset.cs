using System.Drawing;

namespace QC4Common.Classes.HatchColor
{
    /// <summary>
    /// Represents a color preset with various properties for different color variations.
    /// </summary>
    public class ColorPreset
    {
        /// <summary>
        /// The name of the color preset.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The English name of the color preset.
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        /// The Japanese name of the color preset.
        /// </summary>
        public string JpName { get; set; }

        /// <summary>
        /// Gets the display name of the color preset based on the current global mode.
        /// </summary>
        public string DisplayName => Common.Constants.GlobalMode.StartsWith("ja") ? JpName : EnName;

        /// <summary>
        /// The color for a plus 2 variation of the preset.
        /// </summary>
        public Color Plus2Color { get; set; }

        /// <summary>
        /// The color for a plus 1 variation of the preset.
        /// </summary>
        public Color Plus1Color { get; set; }

        /// <summary>
        /// The color for a minus 1 variation of the preset.
        /// </summary>
        public Color Minus1Color { get; set; }

        /// <summary>
        /// The color for a minus 2 variation of the preset.
        /// </summary>
        public Color Minus2Color { get; set; }
    }
}