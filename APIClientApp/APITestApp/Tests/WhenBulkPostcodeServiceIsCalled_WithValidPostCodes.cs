using NUnit.Framework;
using Newtonsoft;
using System;
using APITestApp.Services;
using System.Threading.Tasks;
using System.Linq;

namespace APITestApp.Tests
{
    class WhenBulkPostcodeServiceIsCalled_WithValidPostCodes
    {
        private BulkPostcodeService _bulkPostcodeService;

        [OneTimeSetUp]
        public async Task OneTimeSetUpAsync()
        {
            _bulkPostcodeService = new BulkPostcodeService();
            await _bulkPostcodeService.MakeRequest(new string[]
            { "OX49 5NU", "M32 0JG", "NE30 1DP" });
        }

        [Test]
        public void StatusIs200()
        {
            Assert.That(_bulkPostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));

        }

        [Test]
        public void ObjectStatusIs200()
        {
            var result = _bulkPostcodeService.ResponseObject.status;
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void CorrectPostcodes_AreReturned()
        {
            string[] expectedResult = new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP" };
            var result = _bulkPostcodeService.ResponseObject.result;
            for (int i = 0; i < expectedResult.Length; i++)
            {
                var queryForPostcode = _bulkPostcodeService.ResponseObject.result.Where(r => r.query == expectedResult[i]).Select(r => r.query);
                Assert.That(queryForPostcode.FirstOrDefault(), Does.Contain(expectedResult[i]));
            }
        }
    }
}
