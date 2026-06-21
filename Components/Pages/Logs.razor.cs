using FootballTableApp.Models;
using FootballTableApp.Services;
using Microsoft.AspNetCore.Components;

namespace FootballTableApp.Components.Pages;

public partial class Logs : ComponentBase, IDisposable {
    [Inject] private LogService LogService { get; set; } = default!;

    public List<LogEntry> VisibleLogs => visibleStack.ToList();
    public int MatchdayLength { get; set; }
    public int Elapsed { get; private set; } = 0;
    public DateTime StartTime { get; private set; } 
    public DateTime EndTime { get; private set; }
    public bool IsRunning { get; private set; } = false;

    private Stack<LogEntry> visibleStack = new();
    private LogData matchdayData = new();
    private PeriodicTimer? timer;

    protected override void OnInitialized() {
        matchdayData = LogService.GetLogData();
        StartTime = matchdayData.StartTime;
        EndTime = matchdayData.EndTime;
        MatchdayLength = (int)(EndTime - StartTime).TotalSeconds;


        timer = new PeriodicTimer(TimeSpan.FromSeconds(1));
        _ = RunTimer();

        AdvanceStack();
    }

    private async Task RunTimer() {
        while (await timer!.WaitForNextTickAsync()) {
            if (!IsRunning) continue;

            Elapsed++;
            if (Elapsed > MatchdayLength) {
                Elapsed = MatchdayLength;
                IsRunning = false;
            }

            AdvanceStack();
            await InvokeAsync(StateHasChanged);
        }
    }

    private void AdvanceStack() {
        
        while (matchdayData.Logs.TryPeek(out var top) && top.PopAt <= Elapsed) {
            visibleStack.Push(matchdayData.Logs.Pop());
        }

        while (visibleStack.TryPeek(out var top) && top.PopAt > Elapsed) {

            matchdayData.Logs.Push(visibleStack.Pop());
        }
    }

    public void Play() => IsRunning = true;
    public void Pause() => IsRunning = false;

    public void Reset() {
        Elapsed = 0;
        IsRunning = false;
        
        while (visibleStack.Count > 0) {
            matchdayData.Logs.Push(visibleStack.Pop());
        }
    }

    private void OnSeek(ChangeEventArgs e) {
        Elapsed = int.Parse(e.Value!.ToString()!);
        AdvanceStack();
    }

    public void Dispose() => timer?.Dispose();
}