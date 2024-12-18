namespace Flurl.Http.Spec;

public partial class HttpRequestFile : IHttpRequestSpec
{
    private HttpRequestExecutors executors = new();

    /// <inheritdoc/>
    public string Format => "http";

    /// <inheritdoc/>
    public string Spec { get; private set; } = string.Empty;

    /// <inheritdoc/>
    public HttpRequestContext Context { get; private set; } = new();

    /// <inheritdoc/>
    public HttpRequestExecutor this[int index]
        => this.executors[index];

    /// <inheritdoc/>
    public HttpRequestExecutor this[string? key]
        => string.IsNullOrEmpty(key) ? this.executors[0] : this.executors[key];

    /// <inheritdoc/>
    public async Task<IFlurlResponse> ExecuteAsync(string? key = null, object? model = null)
    {
        var executor = this[key];
        var context = model != null ? this.Context.WithTemplateContext(model, true) : this.Context.Variables;
        return await executor.ExecuteAsync(context).ConfigureAwait(false);
    }
}
