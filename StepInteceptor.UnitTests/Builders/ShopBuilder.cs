using StepInteceptor.UnitTests.Builders.Asos.Scdsl.DeliveryServicesRouter.Infrastructure;
using StepInteceptor.UnitTests.Models;

namespace StepInteceptor.UnitTests.Builders
{
    public class ShopBuilder : Builder<ShopBuilder, Shop>
    {
        public ShopBuilder WithCurrency(string value)
        {
            return With(x =>
            {
                x.Currency = value;
            }
            );
        }

        public ShopBuilder WithLanguage(string value)
        {
            return With(x =>
            {
                x.Lang = value;
            }
            );
        }

        public ShopBuilder WithName(string value)
        {
            return With(x =>
            {
                x.Name = value;
            }
            );
        }

        protected override Shop CreateItem()
        {
            return new Shop();
        }
    }
}