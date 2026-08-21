using Ghaem.Web.Models.Enums;
using Ghaem.Web.Models.ViewModels;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Ghaem.Tests
{
    public class PropertyValidationTests
    {
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Fact]
        public void Property_Sale_RequiresSalePrice()
        {
            // Arrange
            var model = new PropertyFormViewModel
            {
                Title = "Test Property",
                Description = "Description",
                Area = 100,
                TransactionType = TransactionType.Sale,
                Address = "Test Address",
                Slug = "test-property"
                // SalePrice is deliberately left null
            };

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.Contains(results, v => v.MemberNames.Contains(nameof(model.SalePrice)));
        }

        [Fact]
        public void Property_Sale_WithSalePrice_IsValid()
        {
            // Arrange
            var model = new PropertyFormViewModel
            {
                Title = "Test Property",
                Description = "Description",
                Area = 100,
                TransactionType = TransactionType.Sale,
                SalePrice = 1000000000,
                Address = "Test Address",
                Slug = "test-property"
            };

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.DoesNotContain(results, v => v.MemberNames.Contains(nameof(model.SalePrice)));
            Assert.Empty(results); // Should have no validation errors at all
        }

        [Fact]
        public void Property_Rent_RequiresDepositAndMonthlyRent()
        {
            // Arrange
            var model = new PropertyFormViewModel
            {
                Title = "Test Property",
                Description = "Description",
                Area = 100,
                TransactionType = TransactionType.Rent,
                Address = "Test Address",
                Slug = "test-property"
                // Deposit and MonthlyRent left null
            };

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.Contains(results, v => v.MemberNames.Contains(nameof(model.Deposit)));
            Assert.Contains(results, v => v.MemberNames.Contains(nameof(model.MonthlyRent)));
        }
        
        [Fact]
        public void Property_Rent_WithDepositAndRent_IsValid()
        {
            // Arrange
            var model = new PropertyFormViewModel
            {
                Title = "Test Property",
                Description = "Description",
                Area = 100,
                TransactionType = TransactionType.Rent,
                Deposit = 50000000,
                MonthlyRent = 5000000,
                Address = "Test Address",
                Slug = "test-property"
            };

            // Act
            var results = ValidateModel(model);

            // Assert
            Assert.Empty(results);
        }
    }
}
