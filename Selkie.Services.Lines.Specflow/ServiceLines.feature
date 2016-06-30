@LongRunningTest
Feature: ServiceLines

Scenario: Ping LineService
	Given Service is running
	And Did not receive ping response message
	When I send a ping message
	Then the result should be a ping response message

Scenario: Stop LineService
	Given Service is running
	And Did not receive ping response message
	When I send a stop message
	Then the result should be service not running

Scenario: Stopping LineService sends message
	Given Service is running
	When I send a stop message
	Then the result should be that I received a ServiceStoppedMessage

Scenario: Started LineService sends message
	Given Service is running
	Then the result should be that I received a ServiceStartedMessage

Scenario: TestLine message request and response
	Given Service is running
	And Did not a receive a TestLineResponseMessage
	When I send a TestLineRequestMessage
	Then the result should be that I received a TestLineResponseMessage

Scenario: LineValidation request and response
	Given Service is running
	And Did not a receive a LineValidationResponseMessage
	When I send a LineValidationRequestMessage
	Then the result should be that I received a LineValidationResponseMessage

Scenario: ImportGeoJsonText message request and response
	Given Service is running
	And Did not a receive a ImportGeoJsonTextResponseMessage
	When I send a ImportGeoJsonTextRequestMessage
	Then the result should be that I received a ImportGeoJsonTextResponseMessage