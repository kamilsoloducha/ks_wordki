public abstract class ValueObject
{
    protected abstract object GetAtomicValue { get; }
    public static bool operator ==(ValueObject id1, ValueObject id2) => id1.GetAtomicValue == id2.GetAtomicValue;
    public static bool operator !=(ValueObject id1, ValueObject id2) => id1.GetAtomicValue != id2.GetAtomicValue;

    public override bool Equals(object obj)
        => obj is ValueObject valueObject ? valueObject.GetAtomicValue == GetAtomicValue : false;

    public override int GetHashCode()
        => GetAtomicValue.GetHashCode();
}