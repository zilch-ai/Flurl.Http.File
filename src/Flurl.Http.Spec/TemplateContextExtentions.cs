namespace Flurl.Http.Spec;

using Fluid;
using Fluid.Values;

public static class TemplateContextExtentions
{
    public static TemplateContext WithTemplateContext(this TemplateContext context, object model, bool allowModelMembers = true)
    {
        _ = context.ThrowIfNull();

        var output = new TemplateContext(model, context.Options, allowModelMembers);
        foreach (var key in context.ValueNames)
        {
            if (context!.GetValue(key) == NilValue.Instance)
            {
                var value = context.GetValue(key);
                _ = output.SetValue(key, value);
            }
        }

        return output;
    }
}
