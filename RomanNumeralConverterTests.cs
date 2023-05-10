using Xunit;
using Microsoft.AspNetCore.Mvc;
using UKE_api.Controllers;
using UKE_api.Models;
using Microsoft.EntityFrameworkCore;
using UKE_api.Data;

namespace UKE_xunit
{
    public class RomanNumeralConverterTest
    {
        private readonly RomanNumeralController _controller;
        private readonly NumeralDbContext _context;

        public RomanNumeralConverterTest()
        {
            var options = new DbContextOptionsBuilder<NumeralDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase") // Unique name for each test class
                .Options;

            _context = new NumeralDbContext(options);
            _controller = new RomanNumeralController(_context);
        }

        [Fact]
        public void Convert_Empty_Numeral_Returns_BadRequest()
        {
            var model = new RomanNumeralModel { Numeral = string.Empty };

            var result = _controller.Convert(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Convert_Invalid_Numeral_Returns_BadRequest()
        {
            var model = new RomanNumeralModel { Numeral = "O" };

            var result = _controller.Convert(model);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Convert_Valid_Numeral_Returns_Ok()
        {
            var model = new RomanNumeralModel { Numeral = "III" };

            var result = _controller.Convert(model);

            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal(3, okResult.Value);
        }

        [Theory]
        [InlineData("XXIV", 24)]
        [InlineData("XVI", 16)]
        [InlineData("MLCXIV", 1144)] // Not a valid combination of numerals resulting in wrong result. "C" can only be subracted from "M" and "D", not "L" I THINK
        [InlineData("MCLXIV", 1164)]
        public void Convert_Valid_Numeral_Returns_Correct_Number(string numeral, int expectedNumber)
        {
            var model = new RomanNumeralModel { Numeral = numeral };

            var result = _controller.Convert(model);

            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.Equal(expectedNumber, okResult.Value);
        }
    }
}