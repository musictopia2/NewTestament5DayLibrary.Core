namespace NewTestament5DayLibrary.Core.Services;
public class UpdateBibleReadingClass
{
    private readonly IBibleStorage _storage;
    private readonly IDateOnlyPicker _date;
    public UpdateBibleReadingClass(IBibleStorage storage,
        IDateOnlyPicker date)
    {
        _storage = storage;
        _date = date;
    }
    public async Task SaveNewReadingsAsync()
    {
        YearlyBibleReadingService service = new(_date);
        var list = service.GetReadingSchedule();
        await _storage.SaveDailyReadingSchedule(list);
    }
}