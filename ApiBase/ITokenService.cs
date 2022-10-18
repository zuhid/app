using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Zuhid.ApiBase;

public interface ITokenService {
  void Configure(JwtBearerOptions options);
  string Build(Guid id, IList<Claim> claims, IList<string> roles);
}
