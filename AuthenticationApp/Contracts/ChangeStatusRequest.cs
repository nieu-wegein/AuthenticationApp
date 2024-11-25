using AuthenticationApp.Domain.Enums;

namespace AuthenticationApp.Contracts
{
    public record ChangeStatusRequest(List<string> EmailList, UserStatus Status);
}
