namespace Flurl.Http.Spec.Test.Integration;

using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Nodes;

public class HttpBin
{
    private sealed class JsonResponse
    {
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> Args { get; set; } = new Dictionary<string, string>();

        public string Origin { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public string? Data { get; set; }

        public JsonNode? Json { get; set; }

        public Dictionary<string, string> Form { get; set; } = new Dictionary<string, string>();

        public Dictionary<string, string> Files { get; set; } = new Dictionary<string, string>();
    }

    #region Get

    [Theory]
    [InlineData("http/httpbin/httpbin.get.http", "1.1")]
    [InlineData("http/httpbin/httpbin.get#v1.http", "1.0")]
    [InlineData("http/httpbin/httpbin.get#v2.http", "2.0")]
    [InlineData("http/httpbin/httpbin.get#v3.http", "3.0")]
    public async Task TestHttpGet(string file, string expected)
    {
        var content = File.ReadAllText(file);
        var executor = HttpRequestFile.LoadFromString(content)[0];
        var response = await executor!.ExecuteAsync();

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Get);
        request.RequestUri.Should().Be("https://httpbin.org/get?arg=argv");
        request.Version.ToString().Should().Be(expected);

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Url.Should().Be("https://httpbin.org/get?arg=argv");
    }

    [Theory]
    [InlineData("http/httpbin/httpbin.get#headers.http")]
    public async Task TestHttpGetWithHeaders(string file)
    {
        var content = File.ReadAllText(file);
        var executor = HttpRequestFile.LoadFromString(content)[0];
        var response = await executor!.ExecuteAsync();

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Get);
        request.RequestUri.Should().Be("https://httpbin.org/get?arg=argv");
        request.Version.ToString().Should().Be("1.1");

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Headers.Should().Contain("Test-Header-Key1", "test-header-value1");
        json.Headers.Should().Contain("Test-Header-Key2", "test-header-value2");
        json.Url.Should().Be("https://httpbin.org/get?arg=argv");
    }

    #endregion

    #region Post

    [Theory]
    [InlineData("http/httpbin/httpbin.post.http")]
    public async Task TestHttpPost(string file)
    {
        var content = File.ReadAllText(file);
        var executor = HttpRequestFile.LoadFromString(content)[0];
        var response = await executor!.ExecuteAsync();

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().Be("https://httpbin.org/post?arg=argv");
        request.Version.ToString().Should().Be("1.1");

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Headers.Should().Contain("Test-Header-Key1", "test-header-value1");
        json.Headers.Should().Contain("Test-Header-Key2", "test-header-value2");
        json.Url.Should().Be("https://httpbin.org/post?arg=argv");
    }

    [Theory]
    [InlineData("http/httpbin/httpbin.post.data.http")]
    public async Task TestHttpPostData(string file)
    {
        var content = File.ReadAllText(file);
        var executor = HttpRequestFile.LoadFromString(content)[0];
        var response = await executor!.ExecuteAsync();

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().Be("https://httpbin.org/post?arg=argv");
        request.Version.ToString().Should().Be("1.1");

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Headers.Should().Contain("Content-Type", "text/plain");
        json.Headers.Should().Contain("Test-Header-Key1", "test-header-value1");
        json.Headers.Should().Contain("Test-Header-Key2", "test-header-value2");
        json.Url.Should().Be("https://httpbin.org/post?arg=argv");
        json.Data.Should().Be("body-test-data");
    }

    [Theory]
    [InlineData("http/httpbin/httpbin.post.json.http")]
    public async Task TestHttpPostJson(string file)
    {
        var content = File.ReadAllText(file);
        var executor = HttpRequestFile.LoadFromString(content)[0];
        var response = await executor!.ExecuteAsync();

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().Be("https://httpbin.org/post?arg=argv");
        request.Version.ToString().Should().Be("1.1");

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Headers.Should().Contain("Content-Type", "application/json");
        json.Headers.Should().Contain("Test-Header-Key1", "test-header-value1");
        json.Headers.Should().Contain("Test-Header-Key2", "test-header-value2");
        json.Url.Should().Be("https://httpbin.org/post?arg=argv");

        // TODO: Better mechanism to parse json as dynamic object and verify it
        json.Json.Should().NotBeNull();
        dynamic? dyn = JsonSerializer.Deserialize<ExpandoObject>(json.Json!.ToString());
        ((string)dyn!.TestJsonKey1.GetString()).Should().Be("TestJsonValue1");
        ((string)dyn!.TestJsonKey2.GetString()).Should().Be("TestJsonValue2");
    }

    [Theory]
    [InlineData("http/httpbin/httpbin.post.form.http")]
    public async Task TestHttpPostForm(string file)
    {
        var content = File.ReadAllText(file);
        var executor = HttpRequestFile.LoadFromString(content)[0];
        var response = await executor!.ExecuteAsync();

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().Be("https://httpbin.org/post?arg=argv");
        request.Version.ToString().Should().Be("1.1");

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Headers.Should().Contain("Content-Type", "application/x-www-form-urlencoded");
        json.Headers.Should().Contain("Test-Header-Key1", "test-header-value1");
        json.Headers.Should().Contain("Test-Header-Key2", "test-header-value2");
        json.Url.Should().Be("https://httpbin.org/post?arg=argv");
        json.Form.Should().Contain("test-form-key1", "test-form-value1");
        json.Form.Should().Contain("test-form-key2", "test-form-value2");
    }

    [Theory]
    [InlineData("http/httpbin/httpbin.post.files.http")]
    public async Task TestHttpPostFiles(string file)
    {
        var content = File.ReadAllText(file);
        var executor = HttpRequestFile.LoadFromString(content)[0];
        var response = await executor!.ExecuteAsync();

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().Be("https://httpbin.org/post?arg=argv");
        request.Version.ToString().Should().Be("1.1");

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Headers.Should().Contain("Content-Type", @"multipart/form-data; boundary=MIME_BOUNDARY");
        json.Headers.Should().Contain("Test-Header-Key1", "test-header-value1");
        json.Headers.Should().Contain("Test-Header-Key2", "test-header-value2");
        json.Url.Should().Be("https://httpbin.org/post?arg=argv");
        json.Files.Should().Contain("testfile1.txt", "testfile1 content here");
        json.Files.Should().Contain("testfile2.txt", "testfile2 content here");
    }

    [Theory]
    [InlineData("http/httpbin/httpbin.post#variables.http")]
    [InlineData("http/httpbin/httpbin.post#comments.http")]
    public async Task TestHttpPostWithVariables(string file)
    {
        // TODO: Refactor detailed design to manage template context
        var content = File.ReadAllText(file);
        var executors = HttpRequestFile.LoadFromString(content);
        var response = await executors[0]!.ExecuteAsync(executors.Context);

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Post);
        request.RequestUri.Should().Be("https://httpbin.org/post?arg=argv");
        request.Version.ToString().Should().Be("1.1");

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Headers.Should().Contain("Content-Type", @"text/plain");
        json.Headers.Should().Contain("Test-Header-Key1", "test-header-value1");
        json.Headers.Should().Contain("Test-Header-Key2", "test-header-value2");
        json.Url.Should().Be("https://httpbin.org/post?arg=argv");
        json.Data.Should().Be("body-test-data");
    }

    #endregion

    #region Put, Patch & Delete

    [Theory]
    [InlineData("http/httpbin/httpbin.put.http", "put")]
    [InlineData("http/httpbin/httpbin.patch.http", "patch")]
    [InlineData("http/httpbin/httpbin.delete.http", "delete")]
    public async Task TestHttpOthers(string file, string verb)
    {
        var content = File.ReadAllText(file);
        var executor = HttpRequestFile.LoadFromString(content)[0];
        var response = await executor!.ExecuteAsync();

        // Verify Http Request
        var request = response.ResponseMessage.RequestMessage!;
        request.Method.Should().Be(HttpMethod.Parse(verb));
        request.RequestUri.Should().Be($"https://httpbin.org/{verb}?arg=argv");
        request.Version.ToString().Should().Be("1.1");

        // Verify Json Response
        response.StatusCode.Should().Be(200);
        var json = await response.GetJsonAsync<JsonResponse>();
        json.Should().NotBeNull();
        json.Args.Should().Contain("arg", "argv");
        json.Headers.Should().Contain("Host", "httpbin.org");
        json.Headers.Should().Contain("Content-Type", "text/plain");
        json.Headers.Should().Contain("Test-Header-Key1", "test-header-value1");
        json.Headers.Should().Contain("Test-Header-Key2", "test-header-value2");
        json.Url.Should().Be($"https://httpbin.org/{verb}?arg=argv");
        json.Data.Should().Be("body-test-data");
    }

    #endregion
}
