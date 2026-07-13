using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QC4Common.Classes.HatchColor
{
    /// <summary>
    /// Contains constant error messages for exceptions related to color presets.
    /// </summary>
    public static class ExceptionMessages
    {
        public const string InvalidColorPresetElements = "Invalid ColorPresetElements";

        public const string InvalidPresetName = "Invalid PresetName Found";
        public const string InvalidJaJPDescription = "Invalid ja-JP Description Found";
        public const string InvalidEnUSDescription = "Invalid en-US Description Found";

        public const string UniqueColorPreset = "ColorPreset must be unique: Non-unique Name values: {0}";

        public const string ReadingColorError = "Reading {0} error";

        public const string InvalidColorValue = "Invalid {0} Value for {1}: RGB values should be whole numbers within the range of 0 to 255";

        public const string PresetNameMaxLengthExceeded = "PresetName should be less than {0} characters";
    }
}