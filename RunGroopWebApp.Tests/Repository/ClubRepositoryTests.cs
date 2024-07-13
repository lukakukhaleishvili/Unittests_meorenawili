using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RunGroopWebApp.Data;
using RunGroopWebApp.Data.Enum;
using RunGroopWebApp.Interfaces;
using RunGroopWebApp.Models;
using RunGroopWebApp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static System.Net.Mime.MediaTypeNames;

namespace RunGroopWebApp.Tests.Repository
{
    public class ClubRepositoryTests
    {
        private int id;

        public string Title { get; private set; }
        public string Image { get; private set; }
        public string Description { get; private set; }
        public ClubCategory ClubCategory { get; private set; }
        public Address Address { get; private set; }

        

        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            if(await databaseContext.Clubs.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {


                    databaseContext.Clubs.Add(
                        new Club()
                        {
                            Title = "Running club 1",
                            Image = "https://www.google.com/search?sca_esv=34431ff227bfa674&sca_upv=1&rlz=1C1GCEU_enGE1078GE1078&q=image&udm=2&fbs=AEQNm0DYVld7NGDZ8Pi819Yg8r6em07j6rW9d2jUMtr8MB7htoxbI0iAKNRPykigVf3e9aputkbr8jzmN5LYbANOqrq5HYnx4MjtyMxZ94LvgeHWmGBcuWUoydKfNaoB5JMdZlMtXmg2De2y5O7nn-eTbNdYHsRiT1RQ-pB6qp3ejXJ5VpdCk5NA1Jug5hVR16L7F-A1C1p-4xpfp7qj2HsGNaipPZQOiw&sa=X&ved=2ahUKEwj5xqrQkKWHAxUFQ_EDHXrzB68QtKgLegQIDRAB&biw=768&bih=746&dpr=1.25#vhid=tYmxDgFq4MrkJM&vssid=mosaic",
                            Description = "This is inmage",
                            ClubCategory = ClubCategory.City,
                            Address = new Address()
                            {
                                Street = "givi kartozia",
                                City = "New Deli",
                                State = "Africa"

                            }
                        });
                    await databaseContext.SaveChangesAsync();

                }
                
            }
            return databaseContext;

        }

        [Fact]
        public async void IClubRepository_Add_ReturnsBool()
        {

            //Arrange
            var club = new Club()
            {
                Title = "Running club 1",
                Image = "https://www.google.com/search?sca_esv=34431ff227bfa674&sca_upv=1&rlz=1C1GCEU_enGE1078GE1078&q=image&udm=2&fbs=AEQNm0DYVld7NGDZ8Pi819Yg8r6em07j6rW9d2jUMtr8MB7htoxbI0iAKNRPykigVf3e9aputkbr8jzmN5LYbANOqrq5HYnx4MjtyMxZ94LvgeHWmGBcuWUoydKfNaoB5JMdZlMtXmg2De2y5O7nn-eTbNdYHsRiT1RQ-pB6qp3ejXJ5VpdCk5NA1Jug5hVR16L7F-A1C1p-4xpfp7qj2HsGNaipPZQOiw&sa=X&ved=2ahUKEwj5xqrQkKWHAxUFQ_EDHXrzB68QtKgLegQIDRAB&biw=768&bih=746&dpr=1.25#vhid=tYmxDgFq4MrkJM&vssid=mosaic",
                Description = "This is inmage",
                ClubCategory = ClubCategory.City,
                Address = new Address()
                {
                    Street = "givi kartozia",
                    City = "New Deli",
                    State = "Africa"
                }
                   
                       
            };

            var dbContext = await GetDbContext();
           var clubRepository = new ClubRepository(dbContext);

            //Act
            var result = clubRepository.Add(club);


            //Assert
            result.Should().BeTrue();

        }


        [Fact]
        public async void ClubRepository_GetBYAsync_ReturnClub()
        {
            var dbContext = await GetDbContext();
            var clubRepository = new ClubRepository(dbContext);



            //act
            var result = clubRepository.GetByIdAsync(id);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Task<Club>>();
        }

        
    }
}
