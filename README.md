# Specflow.StepInterceptor

Allows a collection of Actions to be invoked before or after a step type seamlessly. 

E.g. Invoke .build() on a builder just before the first When step is called. Removing the need for a filler step to build the object. 

[![Build status](https://ci.appveyor.com/api/projects/status/km22mx51vgpgq2u3/branch/master?svg=true)](https://ci.appveyor.com/project/robert-fahey/stepinterceptor/branch/master)

Usage

 - Build repo
 - Add Specflow.StepInterceptor reference to your specs project
 - Add library reference to your app.config

```xml
  <specFlow>
    <!-- For additional details on SpecFlow configuration options see http://go.specflow.org/doc-config -->
    <stepAssemblies>
      <stepAssembly assembly="Specflow.StepInterceptor" />
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

