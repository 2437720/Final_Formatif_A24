using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Exceptions;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Tests;

[TestClass]
public class SeatsControllerTests
{
    [TestMethod]
    public void ReserveSeat()
    {

        Mock<SeatsService> mockSeatService = new Mock<SeatsService>();
        Mock<SeatsController> mockSeathController = new Mock<SeatsController>(mockSeatService.Object) { CallBase = true };
        mockSeathController.Setup(c => c.UserId).Returns("1111");
        mockSeatService.Setup(s => s.ReserveSeat("1111", 1)).Returns(new Seat { Number = 1});

        var actionResult = mockSeathController.Object.ReserveSeat(1);
        var result = actionResult.Result as OkObjectResult;

        Assert.IsNotNull(result);
        Seat? seatResult = (Seat?)result!.Value;

        int seatNumber = 1;

        Assert.AreEqual(seatNumber, seatResult!.Number);
    }

    [TestMethod]
    public void ReserveSeat_SeatAlreadyTakenWhitOtherUser()
    {
        Mock<SeatsService> mockSeatService = new Mock<SeatsService>();
        Mock<SeatsController> mockSeathController = new Mock<SeatsController>(mockSeatService.Object) { CallBase = true };
        mockSeathController.Setup(c => c.UserId).Returns("1111");
        mockSeatService.Setup(s => s.ReserveSeat("1111", 1)).Throws(new SeatAlreadyTakenException());


        ExamenUser user = new ExamenUser { Id = "2222" };
        Seat seat = new Seat { Number = 1, ExamenUserId = "2222", ExamenUser = user };

        var actionResult = mockSeathController.Object.ReserveSeat(1);
        var result = actionResult.Result as UnauthorizedResult;

        int codeResult = 401;

        Assert.IsNotNull(result);

        Assert.AreEqual(codeResult, result!.StatusCode);
    }


    [TestMethod]
    public void ReserveSeat_SeatBiggerThanAvailable()
    {
        Mock<SeatsService> mockSeatService = new Mock<SeatsService>();
        Mock<SeatsController> mockSeathController = new Mock<SeatsController>(mockSeatService.Object) { CallBase = true };
        mockSeathController.Setup(c => c.UserId).Returns("1111");
        mockSeatService.Setup(s => s.ReserveSeat("1111", 101)).Throws(new SeatOutOfBoundsException());

        

        var actionResult = mockSeathController.Object.ReserveSeat(101);
        var result = actionResult.Result as NotFoundObjectResult;


        Assert.IsNotNull(result);

        Assert.AreEqual("Could not find 101", result!.Value);
    }

    [TestMethod]
    public void ReserveSeat_SeatAlreadyTaken()
    {
        Mock<SeatsService> mockSeatService = new Mock<SeatsService>();
        Mock<SeatsController> mockSeathController = new Mock<SeatsController>(mockSeatService.Object) { CallBase = true };
        mockSeathController.Setup(c => c.UserId).Returns("1111");
        mockSeatService.Setup(s => s.ReserveSeat("1111", 59)).Throws(new UserAlreadySeatedException());

        ExamenUser user = new ExamenUser { Id = "1111" };
        Seat seat = new Seat { Number = 59, ExamenUserId = "1111", ExamenUser = user };

        var actionResult = mockSeathController.Object.ReserveSeat(59);
        var result = actionResult.Result as BadRequestResult;
        int badRequestCode = 400;   

        Assert.IsNotNull(result);

        Assert.AreEqual(badRequestCode, result!.StatusCode);
    }


}
