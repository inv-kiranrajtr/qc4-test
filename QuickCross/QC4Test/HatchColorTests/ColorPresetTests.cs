using QC4Common.Classes.HatchColor;
using System.Drawing;
using Xunit;


namespace QC4Test.HatchColorTests
{
    public class ColorPresetTests
    {
        [Fact]
        public void DisplayName_UsesEnNameForGlobalModeStartingWithEn()
        {
            // Arrange
            var colorPreset = new ColorPreset
            {
                EnName = "EnglishName",
                JpName = "JapaneseName"
            };
            QC4Common.Common.Constants.GlobalMode = "en-US";

            // Act
            string displayName = colorPreset.DisplayName;

            // Assert
            Assert.Equal("EnglishName", displayName);
        }

        [Fact]
        public void DisplayName_UsesJpNameForGlobalModeStartingWithJa()
        {
            // Arrange
            var colorPreset = new ColorPreset
            {
                EnName = "EnglishName",
                JpName = "JapaneseName"
            };
            QC4Common.Common.Constants.GlobalMode = "ja-JP";

            // Act
            string displayName = colorPreset.DisplayName;

            // Assert
            Assert.Equal("JapaneseName", displayName);
        }

        [Fact]
        public void DisplayName_UsesEnNameByDefault()
        {
            // Arrange
            var colorPreset = new ColorPreset
            {
                EnName = "EnglishName",
                JpName = "JapaneseName"
            };
            QC4Common.Common.Constants.GlobalMode = "fr-FR"; // Not starting with "ja" or "en"

            // Act
            string displayName = colorPreset.DisplayName;

            // Assert
            Assert.Equal("EnglishName", displayName);
        }

        [Fact]
        public void Plus2Color_IsNotNullAfterInitialization()
        {
            // Arrange
            var colorPreset = new ColorPreset();

            // Act and Assert
            Assert.Equal(Color.Empty, colorPreset.Plus2Color);
        }
        [Fact]
        public void Plus1Color_IsNotNullAfterInitialization()
        {
            // Arrange
            var colorPreset = new ColorPreset();

            // Act and Assert
            Assert.Equal(Color.Empty, colorPreset.Plus1Color);
        }

        [Fact]
        public void Minus1Color_IsNotNullAfterInitialization()
        {
            // Arrange
            var colorPreset = new ColorPreset();

            // Act and Assert
            Assert.Equal(Color.Empty, colorPreset.Minus1Color);
        }

        [Fact]
        public void Minus2Color_IsNotNullAfterInitialization()
        {
            // Arrange
            var colorPreset = new ColorPreset();

            // Act and Assert
            Assert.Equal(Color.Empty, colorPreset.Minus2Color);
        }

        [Fact]
        public void Name_CanBeSetAndRetrieved()
        {
            // Arrange
            var colorPreset = new ColorPreset();
            string expectedName = "NewName";

            // Act
            colorPreset.Name = expectedName;
            string actualName = colorPreset.Name;

            // Assert
            Assert.Equal(expectedName, actualName);
        }

        [Fact]
        public void EnName_CanBeSetAndRetrieved()
        {
            // Arrange
            var colorPreset = new ColorPreset();
            string expectedEnName = "NewEnName";

            // Act
            colorPreset.EnName = expectedEnName;
            string actualEnName = colorPreset.EnName;

            // Assert
            Assert.Equal(expectedEnName, actualEnName);
        }

        [Fact]
        public void JpName_CanBeSetAndRetrieved()
        {
            // Arrange
            var colorPreset = new ColorPreset();
            string expectedJpName = "NewJpName";

            // Act
            colorPreset.JpName = expectedJpName;
            string actualJpName = colorPreset.JpName;

            // Assert
            Assert.Equal(expectedJpName, actualJpName);
        }

    }
}
