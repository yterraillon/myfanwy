using BlazorBootstrap;

namespace Web.Helpers;

public static class ChartHelper
{
    public static LineChartDataset CreateLineChartDataset(string label, List<double> data,int color = 0) =>
        new()
        {
            Label = label,
            Data = data,
            BackgroundColor = new List<string> {Colors[color]},
            BorderColor = new List<string> {Colors[color]},
            BorderWidth = new List<double> {2},
            HoverBorderWidth = new List<double> {4},
            PointBackgroundColor = [Colors[color]],
            PointRadius = [0], // hide points
            PointHoverRadius = [4]
        };

    public static LineChartOptions CreateDefaultLineChartOptions(string chartTitle, string xAxisTitle, string yAxisTitle)
    {
        var options = new LineChartOptions
        {
            Responsive = true,
            Interaction = new Interaction {Mode = InteractionMode.Index}
        };

        options.Scales.X!.Title = new ChartAxesTitle {Text = xAxisTitle, Display = true};
        options.Scales.Y!.Title = new ChartAxesTitle {Text = yAxisTitle, Display = true};
        
        options.Plugins.Title!.Text = chartTitle;
        options.Plugins.Title.Display = true;
        
        return options;
    }

    public static BarChartDataset CreateBarChartDataset(List<double> data, int color = 0) =>
        new()
        {
            Data = data,
            BackgroundColor = new List<string> { Colors[color] },
            BorderColor = new List<string> { Colors[color] },
            BorderWidth = new List<double> { 0 },
        };

    public static BarChartOptions CreateDefaultBarChartOptions(string chartTitle, string xAxisTitle, string yAxisTitle)
    {
        var options = new BarChartOptions
        {
            Responsive = true,
            Interaction = new Interaction { Mode = InteractionMode.Y },
            IndexAxis = "x"
        };
        
        options.Scales.X!.Title = new ChartAxesTitle {Text = xAxisTitle, Display = true};
        options.Scales.Y!.Title = new ChartAxesTitle {Text = yAxisTitle, Display = true};
        
        options.Plugins.Title!.Text = chartTitle;
        options.Plugins.Title.Display = true;
        
        options.Plugins.Legend.Display = false;

        return options;
    }
    
    public static class ColorsPalette
    {
        public const int Lightseagreen = 0;
        public const int Royalblue = 1;
        public const int Darkorange = 2;
        public const int Mediumvioletred = 3;
        public const int Mediumslateblue = 4;
        public const int Lightgreen = 5;
        public const int Dodgerblue = 6;
        public const int Blueviolet = 7;
        public const int Gold = 8;
        public const int Chocolate = 9;
        public const int Teal = 10;
        public const int Greenyellow = 11;
    }

    private static readonly string[] Colors = ColorUtility.CategoricalTwelveColors;
}