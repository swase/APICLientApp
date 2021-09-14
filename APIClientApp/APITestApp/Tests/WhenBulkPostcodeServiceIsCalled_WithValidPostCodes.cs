using NUnit.Framework;
using Newtonsoft;
using System;
using APITestApp.PostcodeIOSerice;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace APITestApp.Tests
{
    class WhenBulkPostcodeServiceIsCalled_WithValidPostCodes
    {
        private BulkPostcodeService _bulkPostcodeService;
        private string[] _postcodes = new string[] { "OX49 5NU", "M32 0JG", "NE30 1DP" };

        [OneTimeSetUp]
        public async Task OneTimeSetUpAsync()
        {
            _bulkPostcodeService = new BulkPostcodeService();
            await _bulkPostcodeService.MakeRequest(_postcodes);
        }

        [Test]
        public void StatusIs200()
        {
            Assert.That(_bulkPostcodeService.BulkPostcodeDTO.Response.status, Is.EqualTo(200));

        }

        [Test]
        public void CorrectPostcodes_AreReturned()
        {
            var result = _bulkPostcodeService.GetPostCodes();

            Assert.AreEqual(_postcodes, result);

        }

        [Test]
        public void AssertListOfRegionsIsCorrect()
        {
            var expectedListOfRegions = new List<string>() { "South East", "North West", "North East" };
            var result = _bulkPostcodeService.GetDisctricts();

            Assert.AreEqual(expectedListOfRegions, result);
        }

        public void AssertCountOfPostCodesIsCorret()
        {
            var result = _bulkPostcodeService.GetPostCodes();

            Assert.AreEqual(_postcodes.Length, result.Length);
        }
    }
}
