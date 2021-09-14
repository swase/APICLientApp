using NUnit.Framework;
using Newtonsoft;
using System;
using APITestApp.PostcodeIOSerice;
using System.Threading.Tasks;

namespace APITestApp.Tests
{
    public class WhenTheSinglePoscodeServiceIsCalled_WithValidPostcode
    {
        private SinglePostcodeService _singlePostcodeService;

        [OneTimeSetUp]
        public async Task OneTimeSetUpAsync()
        {
            _singlePostcodeService = new SinglePostcodeService();
            await _singlePostcodeService.MakeRequest("EC2Y 5AS");
        }

        [Test]
        public void StatusIs200()
        {
            Assert.That(_singlePostcodeService.SinglePostcodeDTO.Response.Status, Is.EqualTo(200));

        }

        [Test]
        public void CorrectPostcodeIsReturned()
        {
            var result = _singlePostcodeService.Json_Response["result"]["postcode"].ToString();
            Assert.That(result, Is.EqualTo("EC2Y 5AS"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            var result = _singlePostcodeService.SinglePostcodeDTO.Response.Status;
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void AdminDisctrict_IsCityOfLondon()
        {
            var result = _singlePostcodeService.SinglePostcodeDTO.Response.result.admin_district;
            Assert.That(result, Is.EqualTo("City of London"));
        }

        [Test]
        public void NumberOfCodes_IsCorrect()
        {
            Assert.That(_singlePostcodeService.CodeCount(), Is.EqualTo(12));
        }

    }

}
