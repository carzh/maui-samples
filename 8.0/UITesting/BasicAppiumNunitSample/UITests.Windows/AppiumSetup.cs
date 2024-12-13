using NUnit.Framework;

using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System.Diagnostics;

namespace UITests;

[SetUpFixture]
public class AppiumSetup
{
	private static AppiumDriver? driver;

	public static AppiumDriver App => driver ?? throw new NullReferenceException("AppiumDriver is null");

	[OneTimeSetUp]
	public void RunBeforeAnyTests()
	{
		// If you started an Appium server manually, make sure to comment out the next line
		// This line starts a local Appium server for you as part of the test run
		AppiumServerHelper.StartAppiumLocalServer();

		var windowsOptions = new AppiumOptions
		{
			// Specify windows as the driver, typically don't need to change this
			AutomationName = "windows",
			// Always Windows for Windows
			PlatformName = "Windows",
			// The identifier of the deployed application to test
			App = "com.companyname.basicappiumsample_9zz4h110yvjzm!App",
		};

        // Note there are many more options that you can use to influence the app under test according to your needs

        //      var processes = Process.GetProcesses();
        //var WindowHandle = "";
        //var ApplicationPath = "C:\\Users\\carolinezhu\\Documents\\cowboy-coding\\BasicAppiumNunitSample\\8.0\\UITesting\\BasicAppiumNunitSample\\MauiApp\\bin\\Debug\\net8.0-windows10.0.19041.0\\win10-x64\\BasicAppiumNunitSample.dll";
        //      foreach (var process in processes)
        //      {

        //          string v = "";
        //          try
        //          {
        //              v = process.MainModule.FileName;
        //          }
        //          catch { }
        //          if (v.ToUpper() == ApplicationPath.ToUpper())
        //          {
        //              WindowHandle = process.MainWindowHandle.ToString("x");
        //              break;
        //          }
        //      }

        //      // TODO: in theory this should check for no windows or a null PlatformView and throw
        //      // https://github.com/microsoft/WinAppDriver/issues/1091#issuecomment-597437731
        //      windowsOptions.AddAdditionalAppiumOption("appTopLevelWindow", WindowHandle);

        IntPtr hwnd = ((MauiWinUIWindow)Microsoft.Maui.Controls.Application.Current.Windows[0].Handler.PlatformView).WindowHandle;
        windowsOptions.AddAdditionalAppiumOption("appTopLevelWindow", hwnd.ToString("x"));

        driver = new WindowsDriver(new Uri("http://127.0.0.1:4723/wd/hub"), windowsOptions);
	}

	[OneTimeTearDown]
	public void RunAfterAnyTests()
	{
		driver?.Quit();

		// If an Appium server was started locally above, make sure we clean it up here
		AppiumServerHelper.DisposeAppiumLocalServer();
	}
}