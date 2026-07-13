using QC4Common.Classes.HatchColor;
using System.Collections.Generic;
using Xunit;

namespace QC4Test.HatchColorTests
{
    public class ColorPresetStoreTests
    {
        [Fact]
        public void GetColorPresetByName_ReturnsColorPreset_WhenPresetExists()
        {
            // Arrange
            var colorPreset1 = new ColorPreset { Name = "Preset1" };
            var colorPreset2 = new ColorPreset { Name = "Preset2" };
            ColorPresetStore.ColorPresets = new List<ColorPreset> { colorPreset1, colorPreset2 };

            // Act
            var result = ColorPresetStore.GetColorPresetByName("Preset1");

            // Assert
            Assert.Same(colorPreset1, result);
        }

        [Fact]
        public void GetColorPresetByName_ReturnsNull_WhenPresetDoesNotExist()
        {
            // Arrange
            ColorPresetStore.ColorPresets = new List<ColorPreset>();

            // Act
            var result = ColorPresetStore.GetColorPresetByName("NonExistentPreset");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetDefaultColorPreset_ReturnsDefaultPreset_WhenDefaultPresetExists()
        {
            // Arrange
            var defaultPreset = new ColorPreset { Name = "DefaultPreset" };
            ColorPresetStore.ColorPresets = new List<ColorPreset> { defaultPreset };
            ColorPresetStore.DefaultColorPresetName = "DefaultPreset";

            // Act
            var result = ColorPresetStore.GetDefaultColorPreset();

            // Assert
            Assert.Same(defaultPreset, result);
        }

        [Fact]
        public void GetDefaultColorPreset_ReturnsFirstPreset_WhenDefaultPresetDoesNotExist()
        {
            // Arrange
            var preset1 = new ColorPreset { Name = "Preset1" };
            var preset2 = new ColorPreset { Name = "Preset2" };
            ColorPresetStore.ColorPresets = new List<ColorPreset> { preset1, preset2 };
            ColorPresetStore.DefaultColorPresetName = "NonExistentPreset";

            // Act
            var result = ColorPresetStore.GetDefaultColorPreset();

            // Assert
            Assert.Same(preset1, result);
        }

        [Fact]
        public void ClearStore_RemovesAllPresetsAndResetsDefaultPreset()
        {
            // Arrange
            var colorPreset = new ColorPreset { Name = "Preset" };
            ColorPresetStore.ColorPresets = new List<ColorPreset> { colorPreset };
            ColorPresetStore.DefaultColorPresetName = "DefaultPreset";

            // Act
            ColorPresetStore.ClearStore();

            // Assert
            Assert.Empty(ColorPresetStore.ColorPresets);
            Assert.Equal(string.Empty, ColorPresetStore.DefaultColorPresetName);
        }
    }
}
