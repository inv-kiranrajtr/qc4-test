using QC4Common.Classes.HatchColor;
using System.Linq;
using System.Windows.Controls;
using static Qc4Launcher.Logic.CrossSettingsReader;

namespace Qc4Launcher.Classes.HatchColor
{
    public static class HatchColorCommon
    {

        /// <summary>
        /// Updates the status of the 'Combo_Color_Settings' control based on the state of two checkboxes.
        /// </summary>
        /// <param name="Check_Summary_Rate_Difference1">The first checkbox.</param>
        /// <param name="Check_Summary_Rate_Difference2">The second checkbox.</param>
        /// <param name="Combo_Color_Settings">The combo box control to be updated.</param>
        public static void Combo_Color_Settings_Status_Update(CheckBox Check_Summary_Rate_Difference1, CheckBox Check_Summary_Rate_Difference2, ComboBox Combo_Color_Settings)
        {
            // Check if there are any color presets available.
            if (!ColorPresetStore.ColorPresets.Any())
            {
                // No color presets available, so there's nothing to update.
                return;
            }

            // Check if either 'Check_Summary_Rate_Difference1' or 'Check_Summary_Rate_Difference2' checkboxes are checked.
            if ((Check_Summary_Rate_Difference1.IsChecked ?? false) || (Check_Summary_Rate_Difference2.IsChecked ?? false))
            {
                // If either checkbox is checked, enable the 'Combo_Color_Settings' control.
                Combo_Color_Settings.IsEnabled = true;
            }
            else
            {
                // If neither checkbox is checked, disable the 'Combo_Color_Settings' control.
                Combo_Color_Settings.IsEnabled = false;
            }
        }


        /// <summary>
        /// Initializes the color presets and sets the default color preset if available.
        /// </summary>
        public static void InitialiseColorPreset(ComboBox Combo_Color_Settings)
        {
            // Check if there are no color presets available.
            if (!ColorPresetStore.ColorPresets.Any())
            {
                // Disable the Combo_Color_Settings ComboBox if no color presets are available.
                Combo_Color_Settings.IsEnabled = false;
                return; // Exit the method early if there are no color presets.
            }

            // Set the items source for a combo box to the list of color presets.
            Combo_Color_Settings.ItemsSource = ColorPresetStore.ColorPresets;

            // Find the default color preset and select it in the combo box, if available.
            ColorPreset defPreset = ColorPresetStore.GetDefaultColorPreset();
            if (defPreset != null)
                Combo_Color_Settings.SelectedValue = defPreset.Name;
        }

        /// <summary>
        /// Sets the hatch color preferences for a given CrossOptions object based on the specified preset name.
        /// If the preset name is null or empty, the default color preset is used.
        /// </summary>
        /// <param name="options">The Options object for which hatch colors need to be set.</param>
        /// <param name="presetName">The name of the color preset to be applied, or null/empty for the default preset.</param>
        internal static void SetHatchColorPreference(dynamic options, string presetName)
        {
            // Retrieve the color preset based on the specified presetName or use the default if it's null or empty.
            ColorPreset preset = string.IsNullOrEmpty(presetName)
                ? ColorPresetStore.GetDefaultColorPreset()
                : ColorPresetStore.GetColorPresetByName(presetName);

            // Check if a valid color preset was found.
            if (preset != null)
            {
                // Set the color index using the Corresponding Color from the color preset.
                options.Level2highcolorindex = Helper.GetColorIndexByColor(preset.Plus2Color);
                options.Level1highcolorindex = Helper.GetColorIndexByColor(preset.Plus1Color);
                options.Level1lowcolorindex = Helper.GetColorIndexByColor(preset.Minus1Color);
                options.Level2lowcolorindex = Helper.GetColorIndexByColor(preset.Minus2Color);
            }
        }
    }
}
