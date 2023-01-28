using TariffComparison.Core.Domain.Base;
using TariffComparison.Core.Domain.Base.Exceptions;

namespace TariffComparison.Core.Domain.ValueObjects
{
    public class Money : BaseValueObject<Money>
    {
        public decimal Value { get; }

        public static Money Zero
        {
            get
            {
                return new Money(0);
            }
        }


        public Money(decimal value)
        {
            if(value < 0)
            {
                throw new InvalidValueObjectStateException("The amount cannot be negative");
            }

            Value = value;
        }

        public Money Increase(Money value)
        {
            return new Money(Value + value.Value);
        } 
        public Money Decrease(Money value)
        {
            return new Money(Value - value.Value);
        }

        public static Money operator +(Money left, Money right)
        {
            return new Money(left.Value + right.Value);
        }

        public static Money operator -(Money left, Money right)
        {
            return new Money(left.Value - right.Value);
        }

        public static bool operator <(Money t1, Money t2)
        {
            return t1.Value < t2.Value;
        }
        
        public static bool operator >(Money t1, Money t2)
        {
            return t1.Value > t2.Value;
        }
        
        public static bool operator <=(Money t1, Money t2)
        {
            return t1.Value <= t2.Value;
        }
        
        public static bool operator >=(Money t1, Money t2)
        {
            return t1.Value >= t2.Value;
        }


        public override bool ObjectIsEqual(Money otherObject)
        {
            return otherObject.Value == Value;
        }

        public override int ObjectGetHashCode()
        {
            return Convert.ToInt32(Value);
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static Money CreateIfNotEmpty(decimal? val)
        {
            if (val.HasValue == false)
            {
                return null;
            }

            return new Money(val.Value);
        }

    }
}