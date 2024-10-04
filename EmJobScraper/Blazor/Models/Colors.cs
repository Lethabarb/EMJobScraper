namespace Blazor.Models
{
    public enum Colors
    {
        WHITE,
        BLACK,
        GREEN,
        GREEN_LIGHT,
        GREEN_DARK,
        BLUE,
        BLUE_LIGHT,
        BLUE_DARK,
        GRAY
    }

    public static class ColorSelector
    {
        public static string ToHex(this Colors color)
        {
            return color switch
            {
                Colors.WHITE => "#F9F9F9",
                Colors.BLACK => "#131516",
                Colors.GREEN => "#1FB093",
                Colors.GREEN_LIGHT => "#BAF3E7",
                Colors.GREEN_DARK => "##0F5748",
                Colors.BLUE => "#025A82",
                Colors.BLUE_LIGHT => "#6092A9",
                Colors.BLUE_DARK => "#01293C",
                Colors.GRAY => "#1C2021",
            };
        }
    }
}
