// Required to define request/response-based handlers in the MediatR pipeline
using MediatR;

// Required for managing users, passwords, claims, etc.
using Microsoft.AspNetCore.Identity;

// Imports your domain's custom user entity
using MyFootball.Domain.Entities;

namespace MyFootball.Application.Users
{
    // This class handles the user registration command.
    // It accepts a RegisterUserCommand and returns a string (new user ID).
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, string>
    {
        // Identity's user manager for creating, finding, validating users.
        private readonly UserManager<User> _userManager;

        // Constructor with dependency injection for the UserManager service.
        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        // Core logic: handles the RegisterUserCommand
        public async Task<string> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            // Create a new User entity from the command's data
            User user = new User
            {
                UserName = request.Email,         // Username set to the user's email
                Email = request.Email,            // User's email address
                BirthDate = request.BirthDate,    // User's date of birth
                FirstName = request.FirstName,    // First name
                LastName = request.LastName       // Last name
            };

            // Attempts to create the user with the given password
            var result = await _userManager.CreateAsync(user, request.Password);

            // If creation fails, gather all error messages and throw an exception
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                throw new ApplicationException($"User creation failed: {errors}");
            }

            // If creation succeeds, return the new user's ID
            return user.Id;
        }
    }
}

