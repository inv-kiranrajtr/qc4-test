using Qc4Launcher.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Drawing; 
using NSubstitute;

namespace QC4Test.CombineBannersTests
{
    public class HexToColorTests
    {
        [Fact]
        public void HexToColor_ConvertsValidHexToColor()
        {

            // Arrange
            string argbHex = "#FF0000FF"; // ARGB for opaque red

            // Act
           Color result = CombineBanners.HexToColor(argbHex);

            // Assert
            Assert.Equal(Color.FromArgb(255, 0, 0, 255), result);
        }

        [Fact]
        public void HexToColor_ThrowsExceptionForInvalidHex()
        {
            // Arrange
            string argbHex = "12345678dewd"; // Invalid format

            // Act and Assert
            Assert.Throws<ArgumentException>(() => CombineBanners.HexToColor(argbHex));
        }

        [Fact]
        public void HexToColor_ThrowsExceptionForShortHex()
        {
            // Arrange
            string argbHex = "#FF00"; // Too short (6 characters)

            // Act and Assert
            Assert.Throws<ArgumentException>(() => CombineBanners.HexToColor(argbHex));
        }

        [Fact]
        public void HexToColor_ThrowsExceptionForLongHex()
        {
            // Arrange
            string argbHex = "#FF0000FFFF"; // Too long (10 characters)

            // Act and Assert
            Assert.Throws<ArgumentException>(() => CombineBanners.HexToColor(argbHex));
        }
    }
}