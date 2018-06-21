using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BeLifeRe
{
	/// <summary>
	/// Lógica de interacción para App.xaml
	/// </summary>
	public partial class App : Application
	{
        protected override void OnStartup(StartupEventArgs e)
    {
        // add custom accent and theme resource dictionaries to the ThemeManager
        // you should replace MahAppsMetroThemesSample with your application name
        // and correct place where your custom accent lives
        ThemeManager.AddAccent("CustomAccent1", new Uri(Environment.CurrentDirectory + "/Resources/CustomAccent.xaml"));

        // get the current app style (theme and accent) from the application
        Tuple<AppTheme, Accent> theme = ThemeManager.DetectAppStyle(Current);

        // now change app style to the custom accent and current theme
        ThemeManager.ChangeAppStyle(Current,
                                    ThemeManager.GetAccent("CustomAccent1"),
                                                ThemeManager.AppThemes.First(x => string.Equals(x.Name, "BaseLight")));

        base.OnStartup(e);
    }
	}
}
