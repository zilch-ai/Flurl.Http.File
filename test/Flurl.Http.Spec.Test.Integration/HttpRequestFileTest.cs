namespace Flurl.Http.Spec.Test.Integration;

public class HttpRequestFileTest
{
    [Theory]
    [InlineData("http/serp/baidu.serp.http")]
    public async Task Test(string file)
    {
        var spec = HttpRequestFile.LoadFromFile(file);
        var executor = spec[0];
        var response = await executor.ExecuteAsync();
        response.StatusCode.Should().Be(200);
    }
}
