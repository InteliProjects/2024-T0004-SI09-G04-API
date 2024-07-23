using NUnit.Framework;
using Kombi.Dashboard.Repository;
using Kombi.Dashboard.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kombi.Dashboard.Tests
{
    public class Tests
    {
        private Mock<ICidsService> _cidsServiceMock;

        [SetUp]
        public void Setup()
        {
            
            _cidsServiceMock = new Mock<ICidsService>();
        }

        [Test]
        public async Task TestMethodToTest()
        {
            
            var cidsModel = new CidsModel();

          
            _cidsServiceMock.Setup(x => x.MethodToTest(cidsModel)).ReturnsAsync(42);

         
            var result = await _cidsServiceMock.Object.MethodToTest(cidsModel);

         
          
        }

    }
}

