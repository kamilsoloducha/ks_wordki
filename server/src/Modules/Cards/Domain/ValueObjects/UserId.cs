using System;
using Domain;

namespace Cards.Domain.ValueObjects;

public readonly struct UserId
{
    public Guid Value { get; } = Guid.NewGuid();

    public UserId(Guid id)
    {
        if (id == Guid.Empty) throw new BuissnessArgumentException(nameof(id), id);

        Value = id;
    }

    public static UserId Restore(Guid id)
    {
        if (id == Guid.Empty) throw new BuissnessArgumentException(nameof(id), id);

        return new UserId(id);
    }

    public static bool operator ==(UserId id1, UserId id2) => id1.Value == id2.Value;
    public static bool operator !=(UserId id1, UserId id2) => id1.Value != id2.Value;

    public override bool Equals(object obj) => GetHashCode() == obj?.GetHashCode();

    public override int GetHashCode() => Value.GetHashCode();
}