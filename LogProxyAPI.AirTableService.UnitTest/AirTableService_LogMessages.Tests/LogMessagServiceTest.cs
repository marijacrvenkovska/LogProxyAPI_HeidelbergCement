using LogProxyAPI.AirTableService.AirTableService.LogMessages;
using Microsoft.Extensions.Options;
using Xunit;

namespace LogProxyAPI.AirTableService.UnitTest.AirTableService_LogMessages.Tests
{
    // There are not any unit tests to be made, since we don't have any logic in any of the methods.
    public class LogMessagServiceTest
    {

        public LogMessagServiceTest()
        {

        }

        // Example unit test for SendLogMessage with Result model.
        // Tested method makes a call to a webservice, which should either me mocked or not included in the unit test.
        [Fact]
        public void SendLogMessageWithResult_BadServerAddress_ShouldGetUnknownHostMessage()
        {
            //Arrange 
            var airTableApiSettings = Options.Create(new AirTableApiSettings { ApiKey = "someapikey", AirTableApiUrl = "http://notexistingserver.my/" });
            AirTableLogMessagesService airTableLogMessageService = new AirTableLogMessagesService(airTableApiSettings);

            //Act
            var response = airTableLogMessageService.SendLogMessageWithResult(new LogProxyAPI_HeidelbergCement.Models.LogMessage("test", "test")).Result;

            //Assert
            Assert.True(response.Message.Equals("No such host is known."));
            Assert.True(response.ErrorType == AirTableLogMessageResponse<GetAirTableLogMessage.Record>.ErrorTypeEnum.RequestFailToSend);
        }
    }
}
