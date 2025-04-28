using CommandLine;

namespace CheckingAvailability.Services
{
    [Verb("arguments", isDefault: true)]
    public class ArgumentService
    {
        [Option(shortName: 'o', longName: "old-port", Required = false, HelpText = "The old local port that the service checking availability listens on")]
        public int OldPortCheckingAvailability { get; set; }

        [Option(shortName: 'n', longName: "new-port", Required = false, HelpText = "The new local port that the service checking availability listens on")]
        public int NewPortCheckingAvailability { get; set; }
    }
}
