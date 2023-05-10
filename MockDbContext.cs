using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKE_api.Data;
using UKE_api.Models;

namespace UKE_xunit
{
    public class MockDbContext
    {
        public Mock<NumeralDbContext> _mockContext;
        public Mock<DbSet<ConversionHistory>> _mockSet;

        public MockDbContext()
        {
            _mockSet = new Mock<DbSet<ConversionHistory>>();
            _mockContext = new Mock<NumeralDbContext>();
            _mockContext.Setup(c => c.ConversionHistory).Returns(_mockSet.Object);
        }
    }
}
