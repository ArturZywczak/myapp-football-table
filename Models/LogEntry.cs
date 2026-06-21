namespace FootballTableApp.Models;

public enum LogType {
    Info,
    Warning,
    Error,
    Success
}

public class LogEntry {
    public string Message { get; set; } = "";
    public LogType Type { get; set; }
    public int PopAt { get; set; } 
}

public class LogData {
    public Stack<LogEntry> Logs { get; set; } = new();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}