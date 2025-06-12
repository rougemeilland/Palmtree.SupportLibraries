namespace Palmtree
{
    public interface ILazyValue<VALUE_T>
    {
        VALUE_T Value { get; }
    }
}
