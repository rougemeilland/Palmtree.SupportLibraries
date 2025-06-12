namespace Palmtree
{
    public interface IResettableLazyValue<VALUE_T>
    {
        VALUE_T Value { get; }
        void Reset();
    }
}
