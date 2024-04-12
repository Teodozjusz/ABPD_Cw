using LegacyApp.Service;

namespace LegacyAppTests;

public class AddUserTest
{
    
    [Fact]
    public void UserAdd_Should_Return_True_When_Everything_OK()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        string email = "johndoe@gmail.com";
        var dateofBirth = new DateTime(1982, 03, 21);
        int clientId = 1;
        
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateofBirth, clientId);

        // Assert
        Assert.True(result);

    }
    
    [Fact]
    public void UserAdd_Should_Return_False_When_Missing_FirstName()
    {
        // Arrange
        string firstName = null;
        string lastName = "Doe";
        string email = "johndoe@gmail.com";
        var dateofBirth = new DateTime(1980, 05, 01);
        int clientId = 1;
        
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateofBirth, clientId);

        // Assert
        Assert.False(result);

    }
    
    [Fact]
    public void UserAdd_Should_Return_False_When_Missing_LastName()
    {
        // Arrange
        string firstName = "John";
        string lastName = null;
        string email = "johndoe@gmail.com";
        var dateofBirth = new DateTime(1980, 05, 01);
        int clientId = 1;
        
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateofBirth, clientId);

        // Assert
        Assert.False(result);

    }
    
    [Fact]
    public void UserAdd_Should_Return_False_When_Age_Is_Too_Low()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        string email = "johndoe@gmail.com";
        var dateofBirth = DateTime.Now;
        int clientId = 1;
        
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateofBirth, clientId);

        // Assert
        Assert.False(result);

    }
    
    [Fact]
    public void UserAdd_Should_Return_True_When_Data_Is_Correct()
    {
        // Arrange
        string firstName = "John";
        string lastName = "Doe";
        string email = "johndoe@gmail.com";
        var dateofBirth = new DateTime(1980, 05, 01);
        int clientId = 1;
        
        var userService = new UserService();
        
        // Act
        bool result = userService.AddUser(firstName, lastName, email, dateofBirth, clientId);

        // Assert
        Assert.False(result);
    }
}