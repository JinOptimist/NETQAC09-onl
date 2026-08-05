using System;
using System.Collections.Generic;
using System.Text;

namespace MiniAutomationToolkit.Core.Models
{
    public record UserDto
    {
        public string Name { get; init; }
        public string Email { get; init; }

        public UserDto(string name, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException($"Invalid email: {email}");
            }

            if (!email.Contains("@") || email.Contains(" "))
            {
                throw new ArgumentException($"Invalid email: {email}");
            }

            Name = name;
            Email = email;
        }
    }
}
