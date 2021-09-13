using NUnit.Framework;
using Newtonsoft;
using System;
using APITestApp.Services;
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
            Assert.That(_singlePostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200") );

        }

        [Test]
        public void StatusIs200_Alt()
        {
            Assert.That(_singlePostcodeService.ResponseContent["status"].ToString(), Is.EqualTo("200"));

        }

        [Test]
        public void CorrectPostcodeIsReturned()
        {
            var result = _singlePostcodeService.ResponseContent["result"]["postcode"].ToString();
            Assert.That(result, Is.EqualTo("EC2Y 5AS"));
        }

        [Test]
        public void ObjectStatusIs200()
        {
            var result = _singlePostcodeService.ResponseObject.Status;
            Assert.That(result, Is.EqualTo(200));
        }

        [Test]
        public void AdminDisctrict_IsCityOfLondon()
        {
            var result = _singlePostcodeService.ResponseObject.result.admin_district;
            Assert.That(result, Is.EqualTo("City of London"));
        }


    }

}
