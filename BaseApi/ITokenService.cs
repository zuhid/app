using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Zuhid.BaseApi;

public interface ITokenService {
  void Configure(JwtBearerOptions options);
  string Build(Guid id, IList<string> roles, IList<Claim> claims);
}
