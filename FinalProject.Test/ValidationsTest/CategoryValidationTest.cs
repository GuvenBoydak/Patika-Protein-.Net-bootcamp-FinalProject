using FinalProject.Business;
using FinalProject.DTO;
using FluentAssertions;
using FluentValidation.Results;

namespace FinalProject.Test
{
    public class CategoryValidationTest
    {

        [Theory]
        [InlineData("test1","")]
        [InlineData("","deneme21")]
        public void WhenInvalidInputsAreGiven_CategoryAddDtoValidator_SouldBeReturnErrors(string name,string description)
        {
            CategoryAddDto categoryAddDto = new CategoryAddDto()
            { Name = name, Description = description };

            CategoryAddDtoValidator validation = new CategoryAddDtoValidator();
            ValidationResult result = validation.Validate(categoryAddDto);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData("test", "deneme")]
        [InlineData("deneme", "test")]
        public void WhenInvalidInputsAreGiven_CategoryAddDtoValidator_SouldNotBeReturnErrors(string name, string description)
        {
            CategoryAddDto categoryAddDto = new CategoryAddDto()
            { Name = name, Description = description };

            CategoryAddDtoValidator validation = new CategoryAddDtoValidator();
            ValidationResult result = validation.Validate(categoryAddDto);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(1,"test", "deneme")]
        [InlineData(2,"deneme", "test")]
        public void WhenInvalidInputsAreGiven_CategoryUpdateDtoValidator_SouldNotBeReturnErrors(int id,string name, string description)
        {
            CategoryUpdateDto categoryUpdate = new CategoryUpdateDto()
            {ID=id,Name = name, Description = description };

            CategoryUpdateDtoValidator validation = new CategoryUpdateDtoValidator();
            ValidationResult result = validation.Validate(categoryUpdate);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData(-1, "test1", "")]
        [InlineData(0, "deneme1", "tes2t")]
        public void WhenInvalidInputsAreGiven_CategoryUpdateDtoValidator_SouldBeReturnErrors(int id, string name, string description)
        {
            CategoryUpdateDto categoryUpdate = new CategoryUpdateDto()
            { ID = id, Name = name, Description = description };

            CategoryUpdateDtoValidator validation = new CategoryUpdateDtoValidator();
            ValidationResult result = validation.Validate(categoryUpdate);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
