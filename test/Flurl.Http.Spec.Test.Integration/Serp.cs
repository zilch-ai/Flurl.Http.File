namespace Flurl.Http.Spec.Test.Integration;

using Fluid;

public class Serp
{
    [Theory]
    [InlineData("http/serp/google.serp.http", "google.com")]
    [InlineData("http/serp/baidu.serp.http", "baidu.com")]
    [InlineData("http/serp/bing.serp.http", "bing.com")]
    public async Task Test(string file, string host)
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

    [Fact]
    public void TestFluid()
    {
        var content = @"Hello, {{ name }}! You are {{ age }} years old.";
        var template = new FluidParser().Parse(content);

        var model = new { name = "Alice", age = "30" };
        var context = new TemplateContext();
        context.SetValue("name", "Bob");
        context.SetValue("age", "20");
        context = context.WithTemplateContext(model);

        var result = template.Render(context);
        result.Should().Be("Hello, Alice! You are 30 years old.");
    }
}
