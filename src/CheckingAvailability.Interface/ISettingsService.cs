namespace CheckingAvailability.Interface
{
    public interface ISettingsService
    {
        public int OldPortCheckingAvailability { get; set; }
        public int NewPortCheckingAvailability { get; set; }
    }
}
