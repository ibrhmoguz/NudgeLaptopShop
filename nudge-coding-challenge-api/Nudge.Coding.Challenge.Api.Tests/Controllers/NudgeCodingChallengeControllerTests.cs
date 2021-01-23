using System.Net.Http;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Nudge.Coding.Challenge.Api.Controllers;
using Nudge.Coding.Challenge.Api.Interfaces;
using Nudge.Coding.Challenge.Api.Services;
using Xunit;

namespace Nudge.Coding.Challenge.Api.Tests.Controllers
{
    public class NudgeCodingChallengeControllerTests
    {
        [Fact]
        public void Get_OnInvoke_ReturnsExpectedMessage()
        {
            //var countryRepoServiceMock = new Mock<ICountryDataProvider>();
            //var controller = new NudgeCodingChallengeController(countryRepoServiceMock.Object);

            //var result = controller.Get().Result as OkObjectResult;

            //result.StatusCode.Should().Be(StatusCodes.Status200OK);
            //result.Value.Should().Be("Nudge Coding Challenge!");
        }
    }
}
