using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.BankId.Signicat.Contracts;
using Refit;

namespace Blog.BankId.Signicat
{
[Headers("Authorization")]
public interface ISignicatApi
{
    [Post("/oidc/token")]
    Task<TokenResponse> GetToken([Header("Authorization")] string authorization, [Body(BodySerializationMethod.UrlEncoded)] Dictionary<string, object> data);

    [Get("/oidc/userinfo")]
    Task<UserInfo> GetUserInfo([Header("Authorization")] string authorization);
}
}
