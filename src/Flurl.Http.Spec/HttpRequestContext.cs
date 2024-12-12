namespace Flurl.Http.Spec;

using Fluid;

// TODO: System variable references for dynamic rendering
// TODO: Performance optimization for static rendering
// TODO: Liquid template syntax
public class HttpRequestContext()
{
    private static readonly FluidParser Template = new();

    public TemplateContext Variables { get; } = new();

    public HttpRequestContext AddVariable(string name, string value)
    {
        value = this.Render(value);
        _ = this.Variables.SetValue(name, value);
        return this;
    }

    public async Task<string> RenderAsync(string content)
        => await Template.Parse(content).RenderAsync(this.Variables).ConfigureAwait(false);

    public string Render(string content)
        => Template.Parse(content).Render(this.Variables);
}
