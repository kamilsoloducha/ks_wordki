using System;
using Domain;

namespace Cards.Domain.ValueObjects;

public readonly struct OwnerId
{
    public Guid Value { get; }

    private OwnerId(Guid id)
    {
        Value = id;
    }

    private static OwnerId New() => Restore(Guid.NewGuid());
    public static OwnerId Restore(Guid id)
    {
        if (id == Guid.Empty) throw new BuissnessArgumentException(nameof(id), id);

        return new OwnerId(id);
    }

    public static bool operator ==(OwnerId id1, OwnerId id2) => id1.Value == id2.Value;
    public static bool operator !=(OwnerId id1, OwnerId id2) => id1.Value != id2.Value;

    public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

    public override int GetHashCode() => Value.GetHashCode();

}