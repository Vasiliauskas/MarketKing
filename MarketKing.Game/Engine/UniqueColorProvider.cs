using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketKing.Game
{
    public static class UniqueColorProvider
    {
        static string[] ColorValues = new string[] {
        "#FA6367", "#5FCEE4", "#FBF336", "#C5FC45", "#FC8836", "#D77BBA", "#75E0B9",
        "#FF3E45", "#5A94FC", "#383000", "#40BA02", "#A14402", "#C701B5", "#006C73",
        "#FF0009", "#04296E", "#B0B0B0", "#00800D", "#FC6700", "#3F00FF", "#0DB6BD",
        "#6B0003", "#065FFF", "#9C8300", "#00FF30", "#5C5C5C", "#7B119C", "#000D0D",
        "#205AC4", "#FFD200", "#064200", "#2B0B91", "#C2EEF7", "#86BA02", "#401A05",
        "#C67EF5", "#003B3D", "#B80C0F", "#FCD4AF", "#00FFFF",

        // low quality unique colors behind this line
        "#9DFF8E", "#FF948E", "#82AFFF", "#FFD470",  "#C15EFF", "#C7FF2D", "#FF75D7",
        "#FF904C", "#00FF21", "#7FFFFF", "#FF0000",  "#A17FFF", "#FF00EE", "#00870F",
        "#C0C0C0", "#D3B000", "#820006", "#632BFF",  "#4DB0C6", "#B7C60D", "#FFF460",
        "#404040", "#0026FF", "#000020", "#202000",  "#002020", "#002000", "#202020",
        "#C00000", "#C000C0", "#0000C0", "#C0C000",  "#00C0C0", "#00C000", "#C0C0C0",
        "#600000", "#600060", "#000060", "#606000",  "#006060", "#006000", "#606060",
        "#E00000", "#E000E0", "#0000E0", "#E0E000",  "#00E0E0", "#00E000", "#E0E0E0",
        "#A00000", "#A000A0", "#0000A0", "#A0A000",  "#00A0A0", "#00A000", "#A0A0A0"
    };
        public static string GetUniqueColor(int existingColorCount)
        {
            if (existingColorCount >= ColorValues.Length)
                return ColorValues.Last();
            return ColorValues[existingColorCount];
        }
    }
}
