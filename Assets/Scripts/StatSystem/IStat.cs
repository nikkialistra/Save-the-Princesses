using System;

namespace StatSystem
{
    public interface IStat
    {
        event Action<float> ValueChange;

        float Value { get; }
        float BaseValue { get; }
    }
}
