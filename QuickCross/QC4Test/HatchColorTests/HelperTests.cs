using QC4Common.Classes.HatchColor;
using System.IO;
using System.Xml.Linq;
using Xunit;

namespace QC4Test.HatchColorTests
{
    public class HelperTests
    {
        [Fact]
        public void ParseXml_WithValidXml_ShouldUpdateColorPresetStore()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ColorPresets>
                  <DefaultPreset PresetName=""Preset1""/>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]赤 / [-]青"" en-US=""[+]Red / [-]Blue"">
                    <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                    <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus2Color"" r=""149"" g=""179"" b=""215""/>
                  </ColorPreset>
                  <ColorPreset PresetName=""Preset2"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                    <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                    <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus2Color"" r=""218"" g=""150"" b=""148""/>
                  </ColorPreset>
                </ColorPresets>";

            var xDocument = XDocument.Parse(xml);
            var parser = new Helper();

            // Act
            parser.ParseXml(xDocument);

            // Assert
            Assert.Equal(2, ColorPresetStore.ColorPresets.Count); // Check if color presets are updated
            Assert.Equal("Preset1", ColorPresetStore.DefaultColorPresetName); // Check if the default preset is set correctly
        }

        [Fact]
        public void ParseXml_SameNAme()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ColorPresets>
                  <DefaultPreset PresetName=""Preset1""/>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]赤 / [-]青"" en-US=""[+]Red / [-]Blue"">
                    <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                    <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus2Color"" r=""149"" g=""179"" b=""215""/>
                  </ColorPreset>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                    <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                    <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus2Color"" r=""218"" g=""150"" b=""148""/>
                  </ColorPreset>
                </ColorPresets>";

            // Assert
            Assert.Throws<ColorPresetException>(() => new Helper().ParseXml(XDocument.Parse(xml)));
        }


        [Fact]
        public void ParseXml_ColorElementNull()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ColorPresets>
                  <DefaultPreset PresetName=""Preset1""/>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]赤 / [-]青"" en-US=""[+]Red / [-]Blue"">
                    <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                    <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus2Color""/>
                  </ColorPreset>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                    <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                    <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus2Color""/>
                  </ColorPreset>
                </ColorPresets>";

            Assert.Throws<ColorPresetException>(() => new Helper().ParseXml(XDocument.Parse(xml)));
        }

        [Fact]
        public void ParseXml_ColorElementNameNull()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <ColorPresets>
                      <DefaultPreset PresetName=""Preset1""/>
                      <ColorPreset PresetName="""" ja-JP="""" en-US="""">
                        <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                        <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                        <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                        <Color name=""Minus2Color""/>
                      </ColorPreset>
                      <ColorPreset PresetName=""Preset1"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                        <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                        <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                        <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                        <Color name=""Minus2Color""/>
                      </ColorPreset>
                    </ColorPresets>";


            Assert.Throws<ColorPresetException>(() => new Helper().ParseXml(XDocument.Parse(xml)));
        }
        [Fact]
        public void ParseXml_ColorElementNull2()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <ColorPresets>
                      <DefaultPreset PresetName=""Preset1""/>
                      <ColorPreset PresetName=""Preset1"" ja-JP=""[+]赤 / [-]青"" en-US=""[+]Red / [-]Blue"">
                        <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                        <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                        <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                      </ColorPreset>
                      <ColorPreset PresetName=""Preset1"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                        <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                        <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                        <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                      </ColorPreset>
                    </ColorPresets>";


            Assert.Throws<ColorPresetException>(() => new Helper().ParseXml(XDocument.Parse(xml)));
        }
        [Fact]
        public void ParseXml_ColorPresetGIsNiull()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                    <ColorPresets>
                      <DefaultPreset PresetName=""Preset1""/>
                      <ColorPreset PresetName=""Preset1"" ja-JP=""[+]赤 / [-]青"" en-US=""[+]Red / [-]Blue"">
                        <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                        <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                        <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                        <Color name=""Minus2Color"" r=""218"" />
                      </ColorPreset>
                      <ColorPreset PresetName=""Preset1"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                        <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                        <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                        <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                        <Color name=""Minus2Color"" r=""218""/>
                      </ColorPreset>
                    </ColorPresets>";


            Assert.Throws<ColorPresetException>(() => new Helper().ParseXml(XDocument.Parse(xml)));
        }

        [Fact]
        public void ParseXml_ColorPresetBIsNiull()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ColorPresets>
                  <DefaultPreset PresetName=""Preset1""/>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]赤 / [-]青"" en-US=""[+]Red / [-]Blue"">
                    <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                    <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus2Color"" r=""218"" g=""150""/>
                  </ColorPreset>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                    <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                    <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus2Color"" r=""218"" g=""150""/>
                  </ColorPreset>
                </ColorPresets>";

            Assert.Throws<ColorPresetException>(() => new Helper().ParseXml(XDocument.Parse(xml)));
        }

        [Fact]
        public void ParseXml_WithValidXml_DefaultPresetIsNull()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ColorPresets>
                  <DefaultPreset PresetName=""""/>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]赤 / [-]青"" en-US=""[+]Red / [-]Blue"">
                    <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                    <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus2Color"" r=""149"" g=""179"" b=""215""/>
                  </ColorPreset>
                  <ColorPreset PresetName=""Preset2"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                    <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                    <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus2Color"" r=""218"" g=""150"" b=""148""/>
                  </ColorPreset>
                </ColorPresets>";

            var xDocument = XDocument.Parse(xml);
            var parser = new Helper();

            // Act
            parser.ParseXml(xDocument);

            // Assert
            Assert.Equal(2, ColorPresetStore.ColorPresets.Count); // Check if color presets are updated
            Assert.Equal("Preset1", ColorPresetStore.DefaultColorPresetName); // Check if the default preset is set correctly
        }

        [Fact]
        public void ParseColorPresetXml_SuccessfulParsing_LogsAndUpdatesStore()
        {
            // Arrange
            var helper = new Helper();
            var xmlContent =
                 @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ColorPresets>
                  <DefaultPreset PresetName=""Preset1""/>
                  <ColorPreset PresetName=""Preset1"" ja-JP=""[+]赤 / [-]青"" en-US=""[+]Red / [-]Blue"">
                    <Color name=""Plus2Color"" r=""218"" g=""150"" b=""148""/>
                    <Color name=""Plus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus2Color"" r=""149"" g=""179"" b=""215""/>
                  </ColorPreset>
                  <ColorPreset PresetName=""Preset2"" ja-JP=""[+]青 / [-]赤"" en-US=""[+]Blue / [-]Red"">
                    <Color name=""Plus2Color"" r=""149"" g=""179"" b=""215""/>
                    <Color name=""Plus1Color"" r=""220"" g=""230"" b=""241""/>
                    <Color name=""Minus1Color"" r=""242"" g=""220"" b=""219""/>
                    <Color name=""Minus2Color"" r=""218"" g=""150"" b=""148""/>
                  </ColorPreset>
                </ColorPresets>";

            string xmlFilePath = "path_to_existing_color_preset.xml";
            File.WriteAllText(xmlFilePath, xmlContent);

            try
            {
                // Act
                helper.ParseColorPresetXml(xmlFilePath);

                // Assert
                Assert.Equal(2, ColorPresetStore.ColorPresets.Count);
            }
            finally
            {
                // Clean up: Delete the temporary XML file.
                File.Delete(xmlFilePath);
            }
        }

        [Fact]
        public void ParseColorPresetXml_UnexpectedError()
        {
            // Arrange
            var helper = new Helper();

            string xmlFilePath = "invalid_path_to_existing_color_preset.xml";

            try
            {
                // Act
                helper.ParseColorPresetXml(xmlFilePath);

                // Assert
                Assert.Empty(ColorPresetStore.ColorPresets);
            }
            finally
            {
                // Clean up: Delete the temporary XML file.
                File.Delete(xmlFilePath);
            }
        }

        [Fact]
        public void ParseXml_ColorprsetElementNull()
        {
            // Arrange
            var xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ColorPresets>
 
                </ColorPresets>";

            Assert.Throws<ColorPresetException>(() => new Helper().ParseXml(XDocument.Parse(xml)));
        }

        [Fact]
        public void ParseColorPresetXml_ThrowsColorPresetException()
        {
            // Arrange
            var helper = new Helper();
            var xmlContent =
                 @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <ColorPresets>
                 
                </ColorPresets>";

            string xmlFilePath = "path_to_existing_color_preset.xml";
            File.WriteAllText(xmlFilePath, xmlContent);

            try
            {
                // Act
                helper.ParseColorPresetXml(xmlFilePath);

                // Assert 
                Assert.Empty(ColorPresetStore.ColorPresets);
            }
            finally
            {
                // Clean up: Delete the temporary XML file.
                File.Delete(xmlFilePath);
            }

        }
        [Theory]
        [InlineData(0x00FF00)] // Green color
        [InlineData(0x0000FF)] // Blue color
        [InlineData(0xFF0000)] // Red color
        [InlineData(0x000000)] // Red color
        [InlineData(0xFFFFFF)] // Red color
        public void GetColorByColorIndex_ReturnsCorrectColor(int colorIndex)
        {   
            // Arrange
            var expectedColor = System.Drawing.Color.FromArgb(
                colorIndex & 0xFF,         // Red component
                (colorIndex >> 8) & 0xFF,  // Green component
                (colorIndex >> 16) & 0xFF  // Blue component
            );

            // Act
            var result = Helper.GetColorByColorIndex(colorIndex);

            // Assert
            Assert.Equal(expectedColor, result);
        }

        [Fact]
        public void GetColorIndexByColor_ReturnsCorrectColorIndex()
        {
            // Arrange
            var color = System.Drawing.Color.FromArgb(255, 128, 64); // Example color
            int expectedColorIndex = (color.B << 16) | (color.G << 8) | color.R;

            // Act
            var result = Helper.GetColorIndexByColor(color);

            // Assert
            Assert.Equal(expectedColorIndex, result);
        }
    }
}