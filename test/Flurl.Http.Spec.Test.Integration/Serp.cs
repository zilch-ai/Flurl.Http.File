namespace Flurl.Http.Spec.Test.Integration;

public class Serp
{
    [Theory]
    [InlineData("http/serp/serp.bing.http", "bing.com")]
    [InlineData("http/serp/serp.baidu.http", "baidu.com")]
    [InlineData("http/serp/serp.google.http", "google.com")]
    public async Task TestIndividual(string file, string host)
    {
        var spec = HttpRequestFile.LoadFromFile(file);
        var response = await spec.ExecuteAsync
        (
            key: string.Empty,
            model: new { Query = "github" }
        );
        response.StatusCode.Should().Be(200);
        response.ResponseMessage.Content.Headers.ContentType!.ToString().Should().StartWith("text/html");
        response.ResponseMessage!.RequestMessage!.RequestUri!.ToString().Should().Contain(host);
        response.ResponseMessage!.RequestMessage!.RequestUri!.ToString().Should().Contain("github");
        response.ResponseMessage!.RequestMessage!.RequestUri!.ToString().Should().NotContain("openai");
    }

    [Theory]
    [InlineData("http/serp/serp.http", "", "bing.com")]
    [InlineData("http/serp/serp.http", "baidu", "baidu.com")]
    [InlineData("http/serp/serp.http", "google", "google.com")]
    public async Task TestMixed(string file, string key, string host)
    {
        var spec = HttpRequestFile.LoadFromFile(file);
        var response = await spec.ExecuteAsync
        (
            key: key,
            model: new { Query = "github" }
        );
        response.StatusCode.Should().Be(200);
        response.ResponseMessage.Content.Headers.ContentType!.ToString().Should().StartWith("text/html");
        response.ResponseMessage!.RequestMessage!.RequestUri!.ToString().Should().Contain(host);
        response.ResponseMessage!.RequestMessage!.RequestUri!.ToString().Should().Contain("github");
        response.ResponseMessage!.RequestMessage!.RequestUri!.ToString().Should().NotContain("openai");
    }
}
