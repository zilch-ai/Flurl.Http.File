namespace Flurl.Http.Spec;

using Scriban;
using HttpRequestContext = System.Collections.Immutable.ImmutableDictionary<string, string>;
using HttpRequestHeaders = System.Collections.Immutable.ImmutableDictionary<string, string>;

public class HttpRequestExecutor(string verb, string endpoint, HttpRequestContext? context = null)
{
    public HttpRequestContext Context { get; } = context ?? HttpRequestContext.Empty;

    public string Verb { get; } = verb;

    public string Endpoint { get; } = endpoint;

    public string Version { get; init; } = "HTTP/1.1";

    public HttpRequestHeaders Headers { get; init; } = HttpRequestHeaders.Empty;

    public string? Body { get; init; }

    public Action<IFlurlRequest>? Configurator { get; set; }

    public async Task<IFlurlResponse> ExecuteAsync()
    {
        // String template replacement in the url, headers & body
        var verb = this.Verb;
        var url = await Template.Parse(this.Endpoint).RenderAsync(this.Context).ConfigureAwait(false);
        var version = this.Version[5..];
        var headers = this.Headers.ToDictionary(pair => pair.Key, pair => Template.Parse(pair.Value).Render(this.Context));
        var body = await Template.Parse(this.Body).RenderAsync(this.Context).ConfigureAwait(false);

        // Create a new FlurlRequest instance with customized configuration
        var request = url
            .WithSettings(settings =>
            {
                settings.HttpVersion = version;
                settings.Timeout = TimeSpan.FromSeconds(10);
                settings.AllowedHttpStatusRange = "*";
                settings.Redirects.Enabled = true;
            })
            .WithHeaders(headers);
        this.Configurator?.Invoke(request);

        // Send request and get proper response asynchronously
        var method = new HttpMethod(verb);
        using var content = body != null ? new StringContent(body) : null;
        return await request.SendAsync(method, content).ConfigureAwait(false);
    }
}
