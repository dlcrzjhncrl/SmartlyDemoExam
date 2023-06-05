@_regression
Feature: BNZ Demo

Testing BNZ functionalities.

Background: 
	Given I open the BNZ Demo website

Scenario: 01 Validate Payees Page Navigation
	When I click the main Menu
		And click on Payees option
	Then Payees page should be loaded

Scenario: 02 Validate Add New Payee
	When I navigate to Payees page
	When I add new Payee
	| Name   | Bank | Branch | Account | Suffix |
	| Cleene | 03   | 0004   | 0000008 | 005    | 
	Then create success confirmation message should be displayed
		And new Payee should be created successfully

Scenario: 03 Validate Required Field
	When I navigate to Payees page
	When I add new Payee without entering Payee details
	Then error message for mandatory field should be displayed
	When I enter Payee Name
	| Name   |
	| Jigsaw |
	Then error message for mandatory field should not be displayed

Scenario: 04 Validate Payee List Order
	When I navigate to Payees page
	When I add new Payee
	| Name   | Bank | Branch | Account | Suffix |
	| Cleene | 03   | 0004   | 0000008 | 005    | 
	Then Payee list is sorted in ascending order
	When I click the Name header in Payee list
	Then Payee list is sorted in descending order

Scenario Outline: 05 Validate Transfer Payments
	When I navigate to Payments page
	When I transfer <Amount> from <Sender> Account to <Receiver> Account
	Then transfer success message should be displayed
		And balances after success transfer are correct for <Sender> and <Receiver>
Examples: 
	| Amount | Sender   | Receiver |
	| 500    | Everyday | Bills    |
