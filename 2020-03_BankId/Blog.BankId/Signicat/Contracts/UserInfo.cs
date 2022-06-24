using Newtonsoft.Json;

namespace Blog.BankId.Signicat.Contracts
{
public class UserInfo
{
    [JsonProperty("sub")]
    public string Sub { get; set; }

    [JsonProperty("signicat.national_id")]
    public string SocialSecurityNumber { get; set; }
}
}
