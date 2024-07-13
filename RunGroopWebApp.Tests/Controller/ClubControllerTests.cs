using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RunGroopWebApp.Controllers;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RunGroopWebApp.Tests.Controller
{
    public class ClubControllerTests
    {

        private ClubController _clubController;
        private readonly IClubRepository _clubRepository;
        private readonly IPhotoService _photoService;
        private readonly HttpContextAccessor _httpContextAccessor;
        

        public ClubControllerTests()
        {
            //Dependencies
            _clubRepository = A.Fake<IClubRepository>();
            _photoService = A.Fake<IPhotoService>();
            _httpContextAccessor = A.Fake<HttpContextAccessor>();

            //SUT what we are actually executing on
            _clubController = new ClubController(_clubRepository,_photoService, _httpContextAccessor);

          
        }

        [Fact]
        public void ClubController_Index_ReturnsSuccess()
        {
            //arrage what we need to bring in
            var clubs = A.Fake<IEnumerable<Club>>();
            A.CallTo(() => _clubRepository.GetAll()).Returns(clubs);

            var result = _clubController.Index();

            result.Should().BeOfType<Task<IActionResult>>();


        }


        [Fact]
        public void ClubController_Detail_ReturnsSuccess()
        {
            var id = 1;
            var club = A.Fake<Club>();
            A.CallTo(() => _clubRepository.GetByIdAsync(id)).Returns(club);


            var result = _clubController.Detail(id);


            result.Should().BeOfType<Task<IActionResult>>();  

        }
    }


}
