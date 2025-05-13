using CheckingAvailability.Interface;

namespace CheckingAvailability.Services
{
    public class SettingsService : ISettingsService
    {
        public int OldPortCheckingAvailability { get; set; }
        public int NewPortCheckingAvailability { get; set; }
    }
}
