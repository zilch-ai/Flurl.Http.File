namespace Flurl.Http.Spec.Test.Integration;

using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.Text.Unicode;

public class Serp
{
    [Theory]
    [InlineData("http/serp/google.serp.http", "google.com")]
    [InlineData("http/serp/baidu.serp.http", "baidu.com")]
    [InlineData("http/serp/bing.serp.http", "bing.com")]
    public async Task Test(string file, string host)
    {
        var spec = HttpRequestFile.LoadFromFile(file);
        var executor = spec[0];
        var response = await executor.ExecuteAsync(spec.Context);
        response.StatusCode.Should().Be(200);
        response.ResponseMessage.Content.Headers.ContentType!.ToString().Should().StartWith("text/html");
        response.ResponseMessage!.RequestMessage!.RequestUri!.ToString().Should().Contain(host);
    }
}
