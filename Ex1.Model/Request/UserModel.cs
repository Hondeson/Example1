using System;
using System.ComponentModel.DataAnnotations;

namespace Ex1.Model.Request
{
    public class UserModel
    {
        [Required]
        public long Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateOnly BornDate { get; set; }

        public string Gender { get; set; } = null!;

        public string EducationMaxReached { get; set; } = null!;

        public string? Interests { get; set; }
    }
}
