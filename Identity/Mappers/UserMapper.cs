using Zuhid.BaseApi;
using Zuhid.Identity.Entities;
using Zuhid.Identity.Models;

namespace Zuhid.Identity.Mappers;

public class UserMapper : IMapper<UserEntity, User> {
  public UserEntity GetEntity(User model) => new UserEntity {
    Id = model.Id,
    Updated = DateTime.UtcNow,
    Email = model.Email,
    PhoneNumber = model.PhoneNumber,
    FirstName = model.FirstName,
    LastName = model.LastName,
    TwoFactorEnabled = model.TwoFactorEnabled,
    LandingPage = model.LandingPage,
  };
}
