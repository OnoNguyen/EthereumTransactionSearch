using System;

namespace EthereumTransactionSearch.ValueObjects
{
    /// <summary>
    /// Sets up basic behaviour for simple value objects.
    /// </summary>
    public abstract class ValueObject<TValue>
    {
        protected TValue Value { get; }

        protected ValueObject(TValue value)
        {
            Value = value;
        }

        protected ValueObject(TValue value, Action predicate)
        {
            predicate();
            Value = value;
        }

        public static bool operator ==(ValueObject<TValue> value1, ValueObject<TValue> value2) => value1.Equals(value2);

        public static bool operator !=(ValueObject<TValue> value1, ValueObject<TValue> value2) =>
            !value1.Equals(value2);

        public override string ToString() => Value.ToString();

        public override bool Equals(object obj)
            => obj is ValueObject<TValue> vObj
               && vObj.Value.Equals(Value);

        public override int GetHashCode() => Value.GetHashCode();
    }
}