using Moq;
using StudentMeal.AppLogic;
using StudentMeal.DataAccess;
using StudentMeal.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace StudentMeal.Tests {
    public class AppLogicTests {
        [Fact]
        public void AddStudentCallsSaveChanges() {
            // Arange
            var student = new Student {
                Name = "Test",
                Email = "test@example.com",
                PhoneNumber = "+31 (6) 12345678"
            };
            var repositoryMock = new Mock<IRepository>();
            var manager = new StudentMealManager(repositoryMock.Object);

            // Act
            manager.AddStudent(student);

            // Assert
            repositoryMock.Verify(m => m.SaveChanges());
        }

        [Fact]
        public void MealCanStudentEnterAsGuestReturnsFalseWhenStudentIsCook() {
            // Arrange
            var student = new Student {
                Name = "Test",
                Email = "test@example.com",
                PhoneNumber = "+31 (6) 12345678"
            };

            var meal = new Meal {
                Name = "Test",
                Description = "Test",
                DateTime = DateTime.Now,
                MaxGuests = 4,
                Price = 1.40F,
                Cook = student
            };

            // Act
            var result = meal.CanStudentEnterAsGuest(student.Email);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void MealCanStudentEnterAsGuestReturnsFalseWhenStudentIsAlreadyAGuest() {
            // Arrange
            var student = new Student {
                Name = "Test",
                Email = "test@example.com",
                PhoneNumber = "+31 (6) 12345678"
            };

            var meal = new Meal {
                Name = "Test",
                Description = "Test",
                DateTime = DateTime.Now,
                MaxGuests = 4,
                Price = 1.40F,
                Cook = new Student {
                    Name = "test1",
                    Email = "test1@example.com",
                    PhoneNumber = "+31 (6) 12345678"
                }
            };

            meal.GuestsList.Add(new MealStudent {
                Meal = meal,
                Student = student
            });

            // Act
            var result = meal.CanStudentEnterAsGuest(student.Email);

            // Assert
            Assert.False(result);
        }
    }
}
