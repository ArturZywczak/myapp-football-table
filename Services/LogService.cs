using FootballTableApp.Models;

namespace FootballTableApp.Services;

public class LogService {

    public LogData GetLogData() {

        // TEMPDATA
        var logData = new LogData();
        logData.Logs.Push(new LogEntry() { Message = "Gotowe!", PopAt = 13, Type = LogType.Success });
        logData.Logs.Push(new LogEntry() { Message = "Uwaga: wysokie obciążenie", PopAt = 10, Type = LogType.Warning });
        logData.Logs.Push(new LogEntry() { Message = "Załadowano użytkowników", PopAt = 6, Type = LogType.Info });
        logData.Logs.Push(new LogEntry() { Message = "Połączono z bazą danych", PopAt = 3, Type = LogType.Success });
        logData.Logs.Push(new LogEntry() { Message = "Aplikacja wystartowała", PopAt = 0, Type = LogType.Info });
        
        logData.StartTime = new DateTime(2026, 6, 19, 18, 0, 0);
        logData.EndTime = new DateTime(2026, 6, 19, 20, 0, 0);

        return logData;
    }
}