using NUnit.Framework;

namespace UITests;

public class PlatformSpecificSampleTest : BaseTest
{
	[Test]
	public void SampleTest()
	{
		App.GetScreenshot().SaveAsFile($"{nameof(SampleTest)}.png");
	}

	[Test]
	public void testRun()
	{
		Assert.Equals(true, false);
	}

    [Test]
    public void testWrite()
    {
		Console.WriteLine("hello");
    }
}