using AuthenticationApp.Domain.Enums;

namespace AuthenticationApp.ViewModels
{
    public record UserViewModel(
        Guid Id,
        string Email,
        string FirstName,
        string LastName,
        DateTime LastOnline,
        UserStatus Status) : IComparable<UserViewModel>
    {
        public int CompareTo(UserViewModel? u2)
        {
            return LastOnline < u2.LastOnline ? 1 : -1;
        }

        public string GetLastOnlineString()
        {
            string online;
            int daysInMonth = DateTime.DaysInMonth(DateTime.UtcNow.Year, DateTime.UtcNow.Month - 1);
            TimeSpan fromNow = DateTime.UtcNow - LastOnline;
            int totalDays = (int)fromNow.TotalDays;
            int totalHours = (int)fromNow.TotalHours;
            int totalMinutes = (int)fromNow.TotalMinutes;


            if (totalDays > 365)
                online = $"{totalDays / 365} {(totalDays / 365 == 1 ? "year" : "years")} ago";
            else if (totalDays > daysInMonth)
                online = $"{totalDays / daysInMonth} {(totalDays / daysInMonth == 1 ? "month" : "months")} ago";
            else if (totalDays > 0)
                online = $"{totalDays} {(totalDays == 1 ? "day" : "days")} ago";
            else if (totalHours > 0)
                online = $"{totalHours} {(totalHours == 1 ? "hour" : "hours")} ago";
            else if (totalMinutes > 0)
                online = $"{totalMinutes} {(totalMinutes == 1 ? "minute" : "minutes")} ago";
            else
                online = $"less then a minute ago";

            return online;
        }
    }
}
