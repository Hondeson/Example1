using System.ComponentModel.DataAnnotations;

namespace Ex1.Model.Model;

public partial class User
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateOnly BornDate { get; set; }

    public int Gender { get; set; }

    public int EducationMaxReached { get; set; }

    public string? Interests { get; set; }
}
