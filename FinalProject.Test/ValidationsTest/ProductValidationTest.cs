using FinalProject.Business;
using FinalProject.DTO;
using FluentAssertions;
using FluentValidation.Results;

namespace FinalProject.Test
{
    public class ProductValidationTest
    {

        [Theory]
        [InlineData("test","test",22,1,1)]
        public void WhenInvalidInputsAreGiven_ProductAddDtoValidator_SouldNotBeReturnErrors(string name,string description,decimal unitprice,int unitsInStock,int categoryID)
        {
            ProductAddDto productAddDto = new ProductAddDto()
            {
                Name = name,
                Description = description,
                UnitPrice = unitprice,
                UnitsInStock = unitsInStock,
                CategoryID = categoryID,
                UsageStatus=Entities.UsageStatus.LittleUsed
            };

            ProductAddDtoValidator validation = new ProductAddDtoValidator();
            ValidationResult result=validation.Validate(productAddDto);

            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("t2est","", -22, 0, 1)]
        public void WhenInvalidInputsAreGiven_ProductAddDtoValidator_SouldBeReturnErrors(string name, string description, decimal unitprice, int unitsInStock, int categoryID)
        {
            ProductAddDto productAddDto = new ProductAddDto()
            {
                Name = name,
                Description = description,
                UnitPrice = unitprice,
                UnitsInStock = unitsInStock,
                CategoryID = categoryID
            };

            ProductAddDtoValidator validation = new ProductAddDtoValidator();
            ValidationResult result = validation.Validate(productAddDto);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData(-7,"t2est", "t1est", 0, 0, 1)]
        public void WhenInvalidInputsAreGiven_ProductUpdateDtoValidator_SouldBeReturnErrors(int id,string name, string description, decimal unitprice, int unitsInStock, int categoryID)
        {
            ProductUpdateDto productUpdateDto = new ProductUpdateDto()
            {
                Name = name,
                Description = description,
                UnitPrice = unitprice,
                UnitsInStock = unitsInStock,
                CategoryID = categoryID
            };

            ProductUpdateDtoValidator validation = new ProductUpdateDtoValidator();
            ValidationResult result = validation.Validate(productUpdateDto);

            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Theory]
        [InlineData(1,"test", "test", 12, 1, 1)]
        public void WhenInvalidInputsAreGiven_ProductUpdateDtoValidator_SouldNotBeReturnErrors(int id,string name, string description, decimal unitprice, int unitsInStock, int categoryID)
        {
            ProductUpdateDto productUpdateDto = new ProductUpdateDto()
            {
                ID=id,
                Name = name,
                Description = description,
                UnitPrice = unitprice,
                UnitsInStock = unitsInStock,
                CategoryID = categoryID
            };

            ProductUpdateDtoValidator validation = new ProductUpdateDtoValidator();
            ValidationResult result = validation.Validate(productUpdateDto);

            result.Errors.Count.Should().Be(0);
        }
    }
}
