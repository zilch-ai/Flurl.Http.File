namespace Flurl.Http.Spec.Tests;

using Fluid;

public class TemplateContextTest
{
    [Fact]
    public void TestContextOverwriting()
    {
        var content = @"Hello, {{ name }}! You are {{ age }} years old.";
        var template = new FluidParser().Parse(content);

        var model = new { name = "Alice", age = "30" };
        var context = new TemplateContext();
        context.SetValue("name", "Bob");
        context.SetValue("age", "20");
        context = context.WithTemplateContext(model);

        var result = template.Render(context);
        result.Should().Be("Hello, Alice! You are 30 years old.");
    }
}
