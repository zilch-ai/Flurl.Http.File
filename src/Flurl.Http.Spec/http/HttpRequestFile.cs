namespace Flurl.Http.Spec;

public partial class HttpRequestFile : IHttpRequestSpec
{
    private HttpRequestExecutors executors = new();

    /// <inheritdoc/>
    public string Format => "http";

    /// <inheritdoc/>
    public string Spec { get; private set; } = string.Empty;

    public HttpRequestContext Context { get; private set; } = new();

    /// <inheritdoc/>
    public HttpRequestExecutor this[int index]
        => this.executors[index];

    /// <inheritdoc/>
    public HttpRequestExecutor this[string key]
        => string.IsNullOrEmpty(key) ? this.executors[0] : this.executors[key];

    public static HttpRequestFile LoadFromString(string content)
    {
        var (context, executors) = HttpRequestBlocks.All.Parse(content);

        return new()
        {
            Spec = content,
            Context = context,
            executors = executors!,
        };
    }

    public static HttpRequestFile LoadFromFile(string path)
    {
        _ = path.ThrowIfNull();
        var content = File.ReadAllText(path);
        return LoadFromString(content);
    }
}
