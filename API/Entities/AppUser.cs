using System;

namespace API.Entities;

public class AppUser
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    public required string DisplayName { get; set; } 
    public required string Email { get; set; } 
    public required byte[] PwdHash { get; set; }
    public required byte[] PwdSalt{ get; set; }
}
