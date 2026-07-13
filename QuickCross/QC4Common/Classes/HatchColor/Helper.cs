using log4net;
using QC4Common.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace QC4Common.Classes.HatchColor
{
    /// <summary>
    /// A helper class for reading and parsing color presets from an XML configuration file.
    /// </summary>
    public class Helper
    {
        private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Reads the default preset name from the provided XElement.
        /// </summary>
        /// <param name="defaultPresetElement">The XElement containing the default preset information.</param>
        /// <returns>The default preset name.</returns>
        private string ReadDefaultPresetName(XElement defaultPresetElement)
        {
            string defaultPresetName = defaultPresetElement?.Attribute("PresetName")?.Value?.Trim();

            if (string.IsNullOrEmpty(defaultPresetName))
            {
                _log.Warn("ColorPreset default PresetName is not present");
            }

            return defaultPresetName;
        }

        /// <summary>
        /// Parses and validates an individual color component (R, G, or B) from a string.
        /// </summary>
        /// <param name="colorValue">The string representation of the color component.</param>
        /// <param name="color">The name of the color component (e.g., "R", "G", or "B").</param>
        /// <param name="colorName">The name of the color being processed (for error reporting).</param>
        /// <returns>The parsed and validated color component as an integer.</returns>
        /// <exception cref="ColorPresetException">Thrown when the input string cannot be parsed into a valid color component.</exception>
        private int ParseColorValue(string colorValue, string color, string colorName)
        {
            // Attempt to parse the provided string 'colorValue' into an integer.
            // If successful and the parsed value is within the valid RGB range (0 to 255), return it.
            if (int.TryParse(colorValue, out int result) && result >= 0 && result <= 255)
            {
                return result;
            }

            throw new ColorPresetException(ExceptionMessages.InvalidColorValue, color, colorName);
        }

        /// <summary>
        /// Creates a Color instance from an XElement for the specified color name.
        /// </summary>
        /// <param name="element">The XElement containing color information.</param>
        /// <param name="colorName">The name of the color to create.</param>
        /// <returns>A Color instance representing the specified color.</returns>
        private Color CreateColor(XElement element, string colorName)
        {
            var colorElement = element.Elements("Color").SingleOrDefault(e => e.Attribute("name")?.Value?.Trim() == colorName);

            if (colorElement == null)
            {
                throw new ColorPresetException(ExceptionMessages.ReadingColorError, colorName);
            }

            int r = ParseColorValue(colorElement.Attribute("r")?.Value, "R", colorName);
            int g = ParseColorValue(colorElement.Attribute("g")?.Value, "G", colorName);
            int b = ParseColorValue(colorElement.Attribute("b")?.Value, "B", colorName);

            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// Parses a string value representing a name and ensures that it is not null, empty.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="errorMessage">The error message to be used in case the value is null, empty.</param>
        /// <returns>The parsed and validated name as a non-empty string.</returns>
        /// <exception cref="ColorPresetException">Thrown when the provided value is null, empty.</exception>
        private string ParseName(string value, string errorMessage)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ColorPresetException(errorMessage);
            }
            return value;
        }

        /// <summary>
        /// Parses a string value representing a name and ensures that it is not null, empty.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="errorMessage">The error message to be used in case the value is null, empty.</param>
        /// <returns>The parsed and validated name as a non-empty string.</returns>
        /// <exception cref="ColorPresetException">Thrown when the provided value is null or empty.</exception>
        /// <exception cref="ColorPresetException">Thrown when the length of the provided value exceeds the maximum allowed length.</exception>
        private string ParsePresetName(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ColorPresetException(ExceptionMessages.InvalidPresetName);

            if (value.Length > Constants.ColorPresetNameMaxLength)
                throw new ColorPresetException(ExceptionMessages.PresetNameMaxLengthExceeded, Constants.ColorPresetNameMaxLength);

            return value;
        }

        /// <summary>
        /// Reads and constructs a list of ColorPreset instances from a collection of XElement elements.
        /// </summary>
        /// <param name="colorPresetElements">The collection of XElement elements representing color presets.</param>
        /// <returns>A list of ColorPreset instances.</returns>
        private List<ColorPreset> ReadColorPresets(IEnumerable<XElement> colorPresetElements)
        {
            // Check if the input collection is null or empty  and throw an exception if it is.
            if (colorPresetElements == null || !colorPresetElements.Any())
            {
                throw new ColorPresetException(ExceptionMessages.InvalidColorPresetElements);
            }

            // Create a list to store the ColorPreset instances.
            List<ColorPreset> result = colorPresetElements.Select(element => new ColorPreset
            {
                Name = ParsePresetName(element.Attribute("PresetName")?.Value?.Trim()),
                JpName = ParseName(element.Attribute("ja-JP")?.Value?.Trim(), ExceptionMessages.InvalidJaJPDescription),
                EnName = ParseName(element.Attribute("en-US")?.Value?.Trim(), ExceptionMessages.InvalidEnUSDescription),
                Plus2Color = CreateColor(element, "Plus2Color"),
                Plus1Color = CreateColor(element, "Plus1Color"),
                Minus1Color = CreateColor(element, "Minus1Color"),
                Minus2Color = CreateColor(element, "Minus2Color")
            }).ToList();

            // Check for duplicate ColorPreset names and create a list of non-unique groups.
            List<IGrouping<string, ColorPreset>> nonUniqueGroups = result
                                    .GroupBy(item => item.Name)
                                    .Where(group => group.Count() > 1).ToList();
            // If non-unique ColorPreset names are found, throw an exception.
            if (nonUniqueGroups.Any())
            {
                string nonUniqueNames = string.Join(", ", nonUniqueGroups.Select(group => group.Key).ToArray());
                throw new ColorPresetException(ExceptionMessages.UniqueColorPreset, nonUniqueNames);
            }

            return result;
        }

        /// <summary>
        /// Parses the provided XML document and updates the ColorPresetStore with the parsed data.
        /// </summary>
        /// <param name="xDocument">The XML document containing color preset information.</param>
        public void ParseXml(XDocument xDocument)
        {
            List<ColorPreset> colorPresets = ReadColorPresets(xDocument.Root.Elements("ColorPreset"));

            if (colorPresets.Any())
            {
                ColorPresetStore.ColorPresets = colorPresets;

                string defaultColorPresetName = ReadDefaultPresetName(xDocument.Root.Element("DefaultPreset"));

                // Check if the default preset name exists in the list of color presets.
                if (!ColorPresetStore.ColorPresets.Exists(preset => preset.Name.Equals(defaultColorPresetName)))
                {
                    // If the default preset name is not found, log an error and use the first preset as a fallback.
                    _log.WarnFormat("Invalid Default PresetName: {0}, Continuing with the first preset as the default", defaultColorPresetName);
                    defaultColorPresetName = ColorPresetStore.ColorPresets.FirstOrDefault()?.Name;
                }

                ColorPresetStore.DefaultColorPresetName = defaultColorPresetName;

                _log.InfoFormat("ColorSettings Default PresetName: {0}", defaultColorPresetName);
            }
        }

        /// <summary>
        /// Parses a color preset XML file and updates the color preset store based on its content.
        /// </summary>
        /// <param name="colorPresetXmlPath">The path to the color preset XML file to be parsed.</param>>
        public void ParseColorPresetXml(string colorPresetXmlPath)
        {
            try
            {
                // Load the XML document from the specified file path.
                XDocument xml = XDocument.Load(colorPresetXmlPath);

                ParseXml(xml);

                _log.InfoFormat("Successfully parsed {0}.", Common.Constants.ColorPresetXmlFileName);
            }
            catch (ColorPresetException e)
            {
                // Handle exceptions related to parsing the color preset XML.
                _log.ErrorFormat("Error Occured while parsing {0}: {1}", Common.Constants.ColorPresetXmlFileName, e.Message);
                ColorPresetStore.ClearStore();
            }
            catch (Exception e)
            {
                // Handle unexpected errors that may occur during XML parsing.
                _log.ErrorFormat("Unexpected Error Occured while parsing {0}: {1}", Common.Constants.ColorPresetXmlFileName, e.Message);
                ColorPresetStore.ClearStore();
            }
        }

        /// <summary>
        /// Retrieves an integer color index representing the specified <see cref="Color"/>.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> object to convert into a color index.</param>
        /// <returns>
        /// An <see cref="int"/> value that represents the color index.
        /// </returns>
        public static int GetColorIndexByColor(Color color)
        {
            // Calculate the color index by combining the Red, Green, and Blue components.
            int colorIndex = (color.B << 16) | (color.G << 8) | color.R;

            return colorIndex;
        }

        /// <summary>
        /// Retrieves a <see cref="Color"/> object from an integer color index.
        /// </summary>
        /// <param name="colorIndex">The integer color index to convert into a <see cref="Color"/>.</param>
        /// <returns>
        /// A <see cref="Color"/> object representing the color components extracted from the color index.
        /// </returns>
        public static Color GetColorByColorIndex(int colorIndex)
        {
            // Extract the Red, Green, and Blue components from the integer color index.
            int red = colorIndex & 0xFF;
            int green = (colorIndex >> 8) & 0xFF;
            int blue = (colorIndex >> 16) & 0xFF;

            // Create and return a new Color object.
            return Color.FromArgb(red, green, blue);
        }
    }
}