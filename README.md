# StepInterceptor

A Specflow plugin that allows a collection of Actions to be invoked before or after any step seamlessly - Great for builders 

Usage

Build repo
Add StepInterceptor reference to your specs project
Add library refernce to your app.config

<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="specFlow" type="TechTalk.SpecFlow.Configuration.ConfigurationSectionHandler, TechTalk.SpecFlow" />
  </configSections>
  <specFlow>
    <!-- For additional details on SpecFlow configuration options see http://go.specflow.org/doc-config -->
    <stepAssemblies>
      <stepAssembly assembly="StepInteceptor" />
    </stepAssemblies>
  </specFlow>
</configuration>

--------
Example
--------


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
