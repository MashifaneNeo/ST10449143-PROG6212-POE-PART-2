using Xunit;
using PROG6212_ST10449143_POE_PART_1.Models;
using System.ComponentModel.DataAnnotations;

namespace PROG6212_ST10449143_POE_PART_1.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CalculateTotalAmount_ValidInputs_ReturnsCorrectTotal()
        {
            // Arrange
            var claim = new Claim
            {
                LecturerName = "Dr. John Smith",
                Month = "January",
                HoursWorked = 40.5m,
                HourlyRate = 250.75m
            };

            // Act
            var expectedTotal = 40.5m * 250.75m;
            var actualTotal = claim.TotalAmount;

            // Assert
            Assert.Equal(expectedTotal, actualTotal);
            Assert.Equal(10155.375m, actualTotal);
        }
       
        [Fact]
        public void AdditionalNotes_NullAndEmpty()
        {
            // Arrange & Act - Test constructor handles null
            var claim = new Claim
            {
                LecturerName = "Dr. Test User",
                Month = "January",
                HoursWorked = 40m,
                HourlyRate = 200m
            };
            
            // Assert - Constructor should initialize to empty string
            Assert.NotNull(claim.AdditionalNotes);
            Assert.Equal(string.Empty, claim.AdditionalNotes);

            // Test explicit empty string
            var claimWithEmpty = new Claim
            {
                LecturerName = "Dr. Test User",
                Month = "January",
                HoursWorked = 40m,
                HourlyRate = 200m,
                AdditionalNotes = ""
            };

            Assert.Equal(string.Empty, claimWithEmpty.AdditionalNotes);
        }

        [Fact]
        public void FileSupportingDocument_NullAndEmpty()
        {
            // Arrange & Act - Test constructor handles null
            var claim = new Claim
            {
                LecturerName = "Dr. Test User",
                Month = "January",
                HoursWorked = 40m,
                HourlyRate = 200m
            };
            // SupportingDocument is not set, should use constructor default
            // Assert - Constructor should initialize to empty string
            Assert.NotNull(claim.SupportingDocument);
            Assert.Equal(string.Empty, claim.SupportingDocument);
            // Test explicit empty string
            var claimWithEmpty = new Claim
            {
                LecturerName = "Dr. Test User",
                Month = "January",
                HoursWorked = 40m,
                HourlyRate = 200m,
                SupportingDocument = ""
            };
            Assert.Equal(string.Empty, claimWithEmpty.SupportingDocument);
        }
    }
}