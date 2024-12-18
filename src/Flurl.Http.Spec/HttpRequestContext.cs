namespace Flurl.Http.Spec;

using Fluid;

// TODO: Liquid template syntax
// TODO: Dynamic variables (system variables, environment variables, etc.)
public class HttpRequestContext
{
    public static readonly FluidParser Template = new();

    public TemplateContext Variables { get; } = new();

    public HttpRequestContext AddVariable(string name, string value)
    {
        // Support recursive variable references
        var template = Template.Parse(value);
        var replaced = template.Render(this.Variables);

        _ = this.Variables.SetValue(name, replaced);
        return this;
    }

    public TemplateContext WithTemplateContext(object model, bool allowModelMembers = true)
        => this.Variables.WithTemplateContext(model, allowModelMembers);
}
