using System;
using System.Collections.Generic;

namespace Specflow.StepInterceptor.UnitTests.Builders
{
    namespace Asos.Scdsl.DeliveryServicesRouter.Infrastructure
    {
        /// <summary>
        /// A generice builder for constructing and then mutating types
        /// </summary>
        /// <typeparam name="TBuilder">The builder type used for the fluent API</typeparam>
        /// <typeparam name="TItem">The type being constructed</typeparam>
        public abstract class Builder<TBuilder, TItem> where TBuilder : Builder<TBuilder, TItem>
        {
            private readonly List<Action<TItem>> _mutations = new List<Action<TItem>>();

            public static implicit operator TItem(Builder<TBuilder, TItem> builder)
            {
                return builder.Build();
            }

            private TItem MutateItem(TItem item)
            {
                _mutations.ForEach(action => action(item));
                return item;
            }

            public TBuilder With(Action<TItem> mutation)
            {
                _mutations.Add(mutation);
                return this as TBuilder;
            }

            public TItem Build()
            {
                return MutateItem(CreateItem());
            }

            protected abstract TItem CreateItem();

            protected TBuilder ClearMutations()
            {
                _mutations.Clear();
                return this as TBuilder;
            }

        }
    }
}