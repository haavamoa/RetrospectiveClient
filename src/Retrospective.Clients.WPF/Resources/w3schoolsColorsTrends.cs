using System;
using System.Windows.Markup;
using System.Windows.Media;

namespace Retrospective.Clients.WPF.Resources
{
    public static class w3schoolsColorsTrends
    {
        public static SolidColorBrush CherryTomato => (SolidColorBrush)new BrushConverter().ConvertFrom("#E94B3C");
        public static SolidColorBrush Meadowlark => (SolidColorBrush)new BrushConverter().ConvertFrom("#ECDB54");
        public static SolidColorBrush LittleBlueBoy => (SolidColorBrush)new BrushConverter().ConvertFrom("#6F9FD8");
    }
}