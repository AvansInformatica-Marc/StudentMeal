using Xunit;
using StudentMeal.Domain;

namespace StudentMeal.Tests {
    public class DomainTests {
        [Fact]
        public void StudentIsValidModelReturnsTrueWhenInputIsValid() {
            // Arrange
            var student = new Student {
                Name = "Foo Bar",
                Email = "foo.bar@example.com",
                PhoneNumber = "+31 (6) 12345678"
            };

            // Act
            var isValidModel = student.IsValidModel();

            // Assert
            Assert.True(isValidModel);
        }

        [Fact]
        public void StudentIsValidModelReturnsFalseWhenPhoneNumberIsInvalid() {
            // Arrange
            var student = new Student {
                Name = "Foo Bar",
                Email = "foo.bar@example.com",
                PhoneNumber = "blablabla"
            };

            // Act
            var isValidModel = student.IsValidModel();

            // Assert
            Assert.False(isValidModel);
        }

        [Fact]
        public void StudentIsValidModelReturnsFalseWhenEmailIsInvalid() {
            // Arrange
            var student = new Student {
                Name = "Foo Bar",
                Email = "blablabla",
                PhoneNumber = "+31 (6) 12345678"
            };

            // Act
            var isValidModel = student.IsValidModel();

            // Assert
            Assert.False(isValidModel);
        }
    }
}