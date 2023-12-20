Feature: CartFeature

A short summary of the feature

@tag1
Scenario: cart 
	Given User will is on home page
	When User Type '<searchText>'
	And  click on serach button
	Then User will be move into product page
	When User click on add cart button for products
	And Click On goto cart button
	Then User Will Be On Cart Page
	When user removes products from cart
	Then products are removed
	Examples: 
		
		| searchText |
		| pedigree   |