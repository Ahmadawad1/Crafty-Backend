namespace CentralLoggingService
{
    public class LogRecord
    {
        public string Level { get; set; } 
        public string Message { get; set; }
        public string ServiceName { get; set; } 
        public DateTime Timestamp { get; set; } 
        public string CorrelationId { get; set; } 
    }
}
