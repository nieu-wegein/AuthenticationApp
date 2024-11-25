using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AuthenticationApp.Contracts
{
    public record LoginRequest(
        [BindRequired] string Email,
        [BindRequired] string Password);
}
