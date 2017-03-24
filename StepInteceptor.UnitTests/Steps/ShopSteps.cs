using System;
using FluentAssertions;
using StepInteceptor.UnitTests.Builders;
using StepInteceptor.UnitTests.Models;
using TechTalk.SpecFlow;

namespace StepInteceptor.UnitTests.Steps
{

    [Binding]
    public class ShopSteps
    {
        private Shop _shop;
        private ShopBuilder _shopBuilder;
        private readonly StepInterceptorBuilder _stepInterceptorBuilder;

        public ShopSteps(Shop shop, StepInterceptorBuilder stepInterceptorBuilder, ShopBuilder shopBuilder)
        {
            _shop = shop;
            _stepInterceptorBuilder = stepInterceptorBuilder;
            _shopBuilder = shopBuilder;

            _stepInterceptorBuilder.BeforeWhen(() =>
            {
                _shop = _shopBuilder.Build();
            });
        }

        [Given(@"I call my shop '(.*)' in the builder")]
        public void GivenTheReturnWasCreatedOn(string shopName)
        {
            _shopBuilder.WithName(shopName);
        }

        [When(@"The shop object is built by the inceptor")]
        public void WhenTheShopObjectIsBuiltByTheInceptor()
        {
            Console.Out.WriteLine("Go here!");
        }
        
        [Then(@"My shop object is populated with the name '(.*)'")]
        public void ThenMyShopObjectIsPopulatedWithTheName(string name)
        {
            _shop.Name.Should().Be(name);
        }

    }
}