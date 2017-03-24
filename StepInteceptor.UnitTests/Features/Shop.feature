Feature: Shop

Scenario: Name a shop
	Given I call my shop 'foo' in the builder
	When The shop object is built by the inceptor
	Then My shop object is populated with the name 'foo'