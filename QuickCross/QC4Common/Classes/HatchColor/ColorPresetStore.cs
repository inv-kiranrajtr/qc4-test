using System.Collections.Generic;
using System.Linq;

namespace QC4Common.Classes.HatchColor
{
    /// <summary>
    /// A static class responsible for managing and storing color presets in the application.
    /// </summary>
    public static class ColorPresetStore
    {
        /// <summary>
        /// A list of color presets available in the application.
        /// </summary>
        public static List<ColorPreset> ColorPresets { get; set; } = new List<ColorPreset>();

        /// <summary>
        /// The name of the default color preset.
        /// </summary>
        public static string DefaultColorPresetName { get; set; } = string.Empty;

        /// <summary>
        /// Retrieves a color preset with the specified name.
        /// </summary>
        /// <param name="colorPresetName">The name of the color preset to retrieve.</param>
        /// <returns>The ColorPreset object with the specified name, or null if not found.</returns>
        public static ColorPreset GetColorPresetByName(string colorPresetName) =>
            ColorPresets.SingleOrDefault(colorPreset => colorPreset.Name.Equals(colorPresetName));

        /// <summary>
        /// Retrieves the default color preset from the list of available color presets.
        /// </summary>
        /// <returns>
        /// The ColorPreset object representing the default color preset if found,
        /// or the first preset in the list is returned (or null if the list is empty).
        /// </returns>
        public static ColorPreset GetDefaultColorPreset() =>
            ColorPresets.SingleOrDefault(colorPreset => colorPreset.Name.Equals(DefaultColorPresetName)) ?? ColorPresets.FirstOrDefault();

        /// <summary>
        /// Clears the list of color presets and resets the default color preset name.
        /// </summary>
        public static void ClearStore()
        {
            ColorPresets.Clear();
            DefaultColorPresetName = string.Empty;
        }
    }
}