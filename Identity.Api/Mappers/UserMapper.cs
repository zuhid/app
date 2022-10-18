using Zuhid.ApiBase;
using Zuhid.Identity.Api.Entities;
using Zuhid.Identity.Api.Models;

namespace Zuhid.Identity.Api.Mappers;

public class UserMapper : IMapper<UserEntity, User> {
  public UserEntity GetEntity(User model) => new UserEntity {
    Id = model.Id,
    Updated = DateTime.UtcNow,
    // ConcurrencyStamp = null,
    Email = model.Email,
    PhoneNumber = model.PhoneNumber,
    FirstName = model.FirstName,
    LastName = model.LastName,
    TwoFactorEnabled = model.TwoFactorEnabled,
    LandingPage = model.LandingPage,
  };
}
