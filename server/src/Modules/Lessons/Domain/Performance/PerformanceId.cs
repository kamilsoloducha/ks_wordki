using System;

namespace Lessons.Domain.Performance;

public readonly struct PerformanceId
{
    public Guid Value { get; }

    private PerformanceId(Guid value)
    {
        Value = value;
    }

    internal static PerformanceId Create()
        => new PerformanceId(Guid.NewGuid());

    public static PerformanceId Restore(Guid id)
        => new PerformanceId(id);

    public static bool operator ==(PerformanceId id1, PerformanceId id2) => id1.Value == id2.Value;
    public static bool operator !=(PerformanceId id1, PerformanceId id2) => id1.Value != id2.Value;

    public override bool Equals(object obj)
        => obj is PerformanceId id ? id == this : false;

    public override int GetHashCode()
        => Value.GetHashCode();
}