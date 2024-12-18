namespace Flurl.Http.Spec;

using Fluid;
using HttpRequestHeaders = System.Collections.Immutable.ImmutableDictionary<string, Fluid.IFluidTemplate>;

public class HttpRequestExecutor(string verb, string endpoint)
{
    public string Verb { get; } = verb;

    public IFluidTemplate Endpoint { get; } = HttpRequestContext.Template.Parse(endpoint);

    public string Version { get; init; } = "1.1";

    public HttpRequestHeaders Headers { get; init; } = HttpRequestHeaders.Empty;

    public IFluidTemplate Body { get; init; } = HttpRequestContext.Template.Parse(string.Empty);

    public Action<IFlurlRequest>? Configurator { get; set; }

    public async Task<IFlurlResponse> ExecuteAsync(TemplateContext context)
    {
        // Render template variables in the url, headers & body
        var verb = this.Verb;
        var url = await this.Endpoint.RenderAsync(context).ConfigureAwait(false);
        var version = this.Version;
        var headers = this.Headers.Aggregate
        (
            new Dictionary<string, string>(),
            (map, kvp) =>
            {
                var key = kvp.Key;
                var value = kvp.Value.Render(context);
                map.Add(key, value);
                return map;
            }
        );
        var body = await this.Body.RenderAsync(context).ConfigureAwait(false);

        // Create a new FlurlRequest instance with customized configuration
        var request = url.WithSettings(settings =>
        {
            settings.HttpVersion = this.Version;
            settings.Timeout = TimeSpan.FromSeconds(10);
            settings.AllowedHttpStatusRange = "*";
            settings.Redirects.Enabled = true;
        }).WithHeaders(headers);
        this.Configurator?.Invoke(request);

        // Send request and get proper response asynchronously
        var method = new HttpMethod(verb);
        using var content = body != null ? new StringContent(body) : null;
        return await request.SendAsync(method, content).ConfigureAwait(false);
    }
}
