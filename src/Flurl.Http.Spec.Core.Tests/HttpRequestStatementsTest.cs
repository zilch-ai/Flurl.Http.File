namespace Flurl.Http.Spec.Tests;

using Parlot;

public class HttpRequestStatementsTest
{
    [Theory]
    [InlineData("@id=value\n", "id", "value")]
    [InlineData("@_id=value\n", "_id", "value")]
    [InlineData("@id = value\n", "id", "value")]
    [InlineData("@id\t=\tvalue\n", "id", "value")]
    [InlineData("@id \t= \tvalue\n", "id", "value")]
    [InlineData("@id value\n", "id", "value")]
    [InlineData("@id=value", "id", "value")]
    public void TestVariable(string content, string id, string value)
    {
        var actual = HttpRequestStatements.Variable.Parse(content);
        actual.Item1.ToString().Should().Be(id);
        actual.Item2.ToString().Should().Be(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("@")]
    [InlineData("id=value")]
    [InlineData("@id")]
    [InlineData("@id=")]
    [InlineData("@123=456")]
    public void TestVariableMismatched(string content)
    {
        var actual = HttpRequestStatements.Variable.Parse(content);
        actual.Should().NotBeNull();
        actual.Item1.Should().BeNull();
        actual.Item2.Should().BeNull();
    }

    [Theory]
    [InlineData("GET https://www.example.com HTTP/1.0", "GET", "https://www.example.com", "1.0")]
    [InlineData("POST https://www.example.com HTTP/1.1", "POST", "https://www.example.com", "1.1")]
    [InlineData("PUT https://www.example.com HTTP/2", "PUT", "https://www.example.com", "2.0")]
    [InlineData("DELETE https://www.example.com HTTP/3", "DELETE", "https://www.example.com", "3.0")]
    [InlineData("PATCH https://www.example.com", "PATCH", "https://www.example.com", null)]
    public void TestCommand(string content, string verb, string endpoint, string? version)
    {
        var actual = HttpRequestStatements.Command.Parse(content);
        actual.Item1.Should().Be(verb);
        actual.Item2.Should().Be(endpoint);
        actual.Item3.Should().Be(version);
    }

    [Theory]
    [InlineData("###\n", "")]
    [InlineData("### \t\n", "")]
    [InlineData("### abc\n", "abc")]
    [InlineData("### \tabc\t \n", "abc")]
    public void TestRequestTag(string content, string tag)
    {
        var actual = HttpRequestStatements.RequestTag.Parse(content);
        actual.Should().Be(tag);
    }

    [Theory]
    [InlineData("")]
    [InlineData("###")]
    [InlineData("### abc")]
    [InlineData(" ###\n")]
    [InlineData("\t###\n")]
    [InlineData("anytext###\n")]
    public void TestRequestTagMismatched(string content)
    {
        var actual = HttpRequestStatements.RequestTag.Parse(content);
        actual.Should().BeNull();
    }

    [Theory]
    [InlineData("ContentType:json", "ContentType", "json")]
    [InlineData("ContentType:json\n", "ContentType", "json")]
    [InlineData("ContentType: json", "ContentType", "json")]
    [InlineData("ContentType:json ", "ContentType", "json")]
    [InlineData("ContentType: json ", "ContentType", "json")]
    public void TestHeader(string content, string expectedKey, string expectedValue)
    {
        var actual = HttpRequestStatements.Header.Parse(content);
        actual.Item1.ToString().Should().Be(expectedKey);
        actual.Item2.ToString()?.Trim().Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData("\n")]
    [InlineData("ABC")]
    [InlineData("ContentType:")]
    [InlineData("ContentType :json")]
    public void TestHeaderMismatched(string content)
    {
        var actual = HttpRequestStatements.Header.Parse(content);
        actual.Item1.Should().Be(new TextSpan());
        actual.Item2.Should().Be(new TextSpan());
    }

    [Theory]
    [InlineData("\nContentType:json", "ContentType", "json")]
    [InlineData("\nContentType: json", "ContentType", "json")]
    [InlineData("\nContentType:json ", "ContentType", "json")]
    [InlineData("\nContentType: json ", "ContentType", "json")]
    public void TestHeaders(string content, string expectedKey, string expectedValue)
    {
        var actual = HttpRequestStatements.Headers.Parse(content);
        actual![0].Item1.ToString().Should().Be(expectedKey);
        actual![0].Item2.ToString()?.Trim().Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("\nABC", "ABC")]
    [InlineData("\nABC\n###", "ABC")]
    [InlineData("\r\nABC\r\n###", "ABC")]
    [InlineData("\nABC\n### 123", "ABC")]
    public void TestBody(string content, string expected)
    {
        var actual = HttpRequestStatements.Body.Parse(content);
        actual.Should().Be(expected);
    }
}
