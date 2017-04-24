# StepInterceptor

A Specflow plugin that allows a collection of Actions to be invoked before or after any step seamlessly - Great for builders 

[![Build status](https://ci.appveyor.com/api/projects/status/km22mx51vgpgq2u3/branch/master?svg=true)](https://ci.appveyor.com/project/robert-fahey/stepinterceptor/branch/master)

Usage

 - Build repo
 - Add StepInterceptor reference to your specs project
 - Add library refernce to your app.config

```xml
  <specFlow>
    <!-- For additional details on SpecFlow configuration options see http://go.specflow.org/doc-config -->
    <stepAssemblies>
      <stepAssembly assembly="StepInteceptor" />
    </stepAssemblies>
  </specFlow>
```

--------
Example
--------

```csharp
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
            Console.Out.WriteLine("Builder invoked automatically");
        }
        
        [Then(@"My shop object is populated with the name '(.*)'")]
        public void ThenMyShopObjectIsPopulatedWithTheName(string name)
        {
            _shop.Name.Should().Be(name);
        }

    }
```

