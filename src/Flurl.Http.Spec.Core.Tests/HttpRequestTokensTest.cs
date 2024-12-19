namespace Flurl.Http.Spec.Tests;

public class HttpRequestTokensTest
{
    [Theory]
    [InlineData("\r\nABC", "\r\n")]
    [InlineData("\nABC", "\n")]
    public void TestNewLine(string content, string expected)
    {
        var actual = HttpRequestTokens.NewLine.Parse(content).ToString();
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("\r")]
    [InlineData("ABC")]
    public void TestNewLineMismatched(string content)
    {
        var actual = HttpRequestTokens.NewLine.Parse(content).ToString();
        actual.Should().Be(null);
    }

    [Theory]
    [InlineData("#ABC", "ABC")]
    [InlineData("//ABC", "ABC")]
    [InlineData(" ", " ")]
    [InlineData("\t", "\t")]
    [InlineData("\n", "\n")]
    [InlineData("#ABC\n", "ABC")]
    [InlineData("//ABC\n", "ABC")]
    [InlineData(" \t\n", " \t\n")]
    public void TestComment(string content, string expected)
    {
        var actual = HttpRequestTokens.Comment.Parse(content).ToString();
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("###")]
    [InlineData("###ABC")]
    [InlineData("### ABC")]
    [InlineData("ABC")]
    public void TestCommentMismatched(string content)
    {
        var actual = HttpRequestTokens.Comment.Parse(content).ToString();
        actual.Should().BeNull();
    }

    [Theory]
    [InlineData("GET ", "GET")]
    [InlineData("POST ", "POST")]
    [InlineData("PUT ", "PUT")]
    [InlineData("DELETE ", "DELETE")]
    [InlineData("PATCH ", "PATCH")]
    public void TestVerb(string content, string expected)
    {
        var actual = HttpRequestTokens.Verb.Parse(content);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("Get ")]
    [InlineData(" GET ")]
    [InlineData("GETS ")]
    public void TestVerbMismatched(string content)
    {
        var actual = HttpRequestTokens.Verb.Parse(content);
        actual.Should().BeNull();
    }

    [Theory]
    [InlineData("https://www.example.com", "https://www.example.com")]
    [InlineData("Https://www.example.com", "Https://www.example.com")]
    [InlineData("HTTPS://www.example.com", "HTTPS://www.example.com")]
    [InlineData("https://www.example.com ", "https://www.example.com")]
    [InlineData("http://www.example.com", "http://www.example.com")]
    public void TestEndpoint(string content, string expected)
    {
        var actual = HttpRequestTokens.Endpoint.Parse(content);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("ftp://www.example.com")]
    [InlineData("www.example.com")]
    [InlineData(" www.example.com")]
    public void TestEndpointMismatched(string content)
    {
        var actual = HttpRequestTokens.Endpoint.Parse(content);
        actual.Should().BeNullOrEmpty();
    }

    [Theory]
    [InlineData("HTTP/1.0", "1.0")]
    [InlineData("HTTP/1.1", "1.1")]
    [InlineData("http/1.1", "1.1")]
    [InlineData("HTTP/2", "2.0")]
    [InlineData("HTTP/3", "3.0")]
    public void TestVersion(string content, string expected)
    {
        var actual = HttpRequestTokens.Version.Parse(content);
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("")]
    [InlineData("http/")]
    [InlineData("http/abc")]
    [InlineData("abc")]
    public void TestVersionMismatched(string content)
    {
        var actual = HttpRequestTokens.Version.Parse(content);
        actual.Should().BeNull();
    }
}
