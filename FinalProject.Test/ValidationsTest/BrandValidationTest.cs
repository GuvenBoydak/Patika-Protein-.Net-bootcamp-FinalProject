using FinalProject.Business;
using FinalProject.DTO;
using FluentAssertions;
using FluentValidation.Results;

namespace FinalProject.Test
{
    public class BrandValidationTest
    {
        [Theory]
        [InlineData("test123")]
        [InlineData("denem1")]
        public void WhenInvalidInputsAreGiven_BrandAddDtoValidator_SouldBeReturnErrors(string name)
        {
            BrandAddDto brandAddDto = new BrandAddDto()
            {
                Name = name
            };

            BrandAddDtoValidator validation = new BrandAddDtoValidator();
            ValidationResult result= validation.Validate(brandAddDto);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData("test")]
        [InlineData("denem")]
        public void WhenInvalidInputsAreGiven_BrandAddDtoValidator_SouldNotBeReturnError(string name)
        {
            BrandAddDto brandAddDto = new BrandAddDto()
            {
                Name = name
            };

            BrandAddDtoValidator validation = new BrandAddDtoValidator();
            ValidationResult result = validation.Validate(brandAddDto);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(-1,"test")]
        [InlineData(0,"denem")]
        public void WhenInvalidInputsAreGiven_BrandUpdateDtoValidator_SouldBeReturnError(int id,string name)
        {
            BrandUpdateDto brandAddDto = new BrandUpdateDto()
            {
                ID=id,
                Name = name
            };

            BrandUpdateDtoValidator validation = new BrandUpdateDtoValidator();
            ValidationResult result = validation.Validate(brandAddDto);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData(1, "test")]
        [InlineData(2, "denem")]
        public void WhenInvalidInputsAreGiven_BrandUpdateDtoValidator_SouldNotBeReturnError(int id, string name)
        {
            BrandUpdateDto brandAddDto = new BrandUpdateDto()
            {
                ID = id,
                Name = name
            };

            BrandUpdateDtoValidator validation = new BrandUpdateDtoValidator();
            ValidationResult result = validation.Validate(brandAddDto);

            result.Errors.Count.Should().Be(0);
        }
    }
}
