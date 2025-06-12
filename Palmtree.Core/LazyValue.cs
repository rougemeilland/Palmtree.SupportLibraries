using System;

namespace Palmtree
{
    public static class LazyValue
    {
        private sealed class InternalLazyValue<VALUE_T>
            : IResettableLazyValue<VALUE_T>, ILazyValue<VALUE_T>
        {
            private readonly Func<VALUE_T> _factory;
            private readonly Action<VALUE_T>? _finalizer;
            private VALUE_T? _value;
            private Boolean _initialized;

            public InternalLazyValue(Func<VALUE_T> factory, Action<VALUE_T>? finalizer)
            {
                _factory = factory;
                _finalizer = finalizer;
            }

            public VALUE_T Value
            {
                get
                {
                    lock (this)
                    {
                        if (!_initialized)
                        {
                            try
                            {
                                _value = _factory();
                            }
                            catch (Exception)
                            {
                                Reset();
                                throw;
                            }

                            _initialized = true;
                        }

                        Validation.Assert(_value is not null);
                        return _value!;
                    }
                }
            }

            public void Reset()
            {
                if (_finalizer is not null)
                {
                    try
                    {
                        if (!_initialized)
                        {
                            Validation.Assert(_value is not null);
                            _finalizer(_value!);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        _initialized = false;
                        _value = default;
                    }
                }
            }
        }

        public static IResettableLazyValue<VALUE_T> CreateResettable<VALUE_T>(Func<VALUE_T> factory)
        {
            ArgumentNullException.ThrowIfNull(factory);

            return new InternalLazyValue<VALUE_T>(factory, null);
        }

        public static IResettableLazyValue<VALUE_T> CreateResettable<VALUE_T>(Func<VALUE_T> factory, Action<VALUE_T> finalizer)
        {
            ArgumentNullException.ThrowIfNull(factory);
            ArgumentNullException.ThrowIfNull(finalizer);

            return new InternalLazyValue<VALUE_T>(factory, finalizer);
        }

        public static ILazyValue<VALUE_T> Create<VALUE_T>(VALUE_T value)
            => new InternalLazyValue<VALUE_T>(() => value, null);

        public static ILazyValue<VALUE_T> Create<VALUE_T>(Func<VALUE_T> factory)
        {
            ArgumentNullException.ThrowIfNull(factory);

            return new InternalLazyValue<VALUE_T>(factory, null);
        }
    }
}
