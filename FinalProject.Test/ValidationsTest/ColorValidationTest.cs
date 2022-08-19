using FinalProject.Business;
using FinalProject.DTO;
using FluentAssertions;
using FluentValidation.Results;

namespace FinalProject.Test
{
    public class ColorValidationTest
    {
        [Theory]
        [InlineData("sarı123")]
        [InlineData("mor1")]
        public void WhenInvalidInputsAreGiven_ColorAddDtoValidator_SouldBeReturnErrors(string name)
        {
            ColorAddDto colorAddDto = new ColorAddDto()
            {
                Name = name
            };

            ColorAddDtoValidator validation = new ColorAddDtoValidator();
            ValidationResult result = validation.Validate(colorAddDto);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData("turuncu")]
        [InlineData("mor")]
        public void WhenInvalidInputsAreGiven_ColorAddDtoValidator_SouldNotBeReturnError(string name)
        {
            ColorAddDto colorAddDto = new ColorAddDto()
            {
                Name = name
            };

            ColorAddDtoValidator validation = new ColorAddDtoValidator();
            ValidationResult result = validation.Validate(colorAddDto);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(-1, "")]
        [InlineData(0, "gri")]
        public void WhenInvalidInputsAreGiven_ColorUpdateDtoValidator_SouldBeReturnError(int id, string name)
        {
            ColorUpdateDto colorUpdateDto = new ColorUpdateDto()
            {
                ID = id,
                Name = name
            };

            ColorUpdateDtoValidator validation = new ColorUpdateDtoValidator();
            ValidationResult result = validation.Validate(colorUpdateDto);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData(1, "sarı")]
        [InlineData(2, "mor")]
        public void WhenInvalidInputsAreGiven_ColorUpdateDtoValidator_SouldNotBeReturnError(int id, string name)
        {
            ColorUpdateDto colorUpdateDto = new ColorUpdateDto()
            {
                ID = id,
                Name = name
            };

            ColorUpdateDtoValidator validation = new ColorUpdateDtoValidator();
            ValidationResult result = validation.Validate(colorUpdateDto);

            result.Errors.Count.Should().Be(0);
        }
    }
}
