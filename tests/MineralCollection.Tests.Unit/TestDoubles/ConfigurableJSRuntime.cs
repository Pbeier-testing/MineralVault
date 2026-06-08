using Microsoft.JSInterop;

namespace MineralCollection.Tests.Unit.TestDoubles;

internal sealed class ConfigurableJSRuntime : IJSRuntime
{
    public bool ConfirmResult { get; set; }

    public List<string> Invocations { get; } = new();

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, object?[]? args)
    {
        Invocations.Add(identifier);

        if (identifier == "confirm" && typeof(TValue) == typeof(bool))
        {
            return ValueTask.FromResult((TValue)(object)ConfirmResult);
        }

        return ValueTask.FromResult(default(TValue)!);
    }

    public ValueTask<TValue> InvokeAsync<TValue>(string identifier, CancellationToken cancellationToken, object?[]? args)
    {
        return InvokeAsync<TValue>(identifier, args);
    }
}
