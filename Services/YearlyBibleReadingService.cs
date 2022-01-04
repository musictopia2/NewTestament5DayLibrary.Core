namespace NewTestament5DayLibrary.Core.Services;
public class YearlyBibleReadingService
{
    private readonly IDateOnlyPicker _date;
    public YearlyBibleReadingService(IDateOnlyPicker date)
    {
        _date = date;
    }
    private BasicList<DailyReaderModel> _list = new();
    private DateOnly _lastDate;
    public CurrentModel GetCurrentReadingOnly()
    {
        //will use the dateonlypicker interface.  does mean that you can do mock date.
        if (_list.Count == 0 || _lastDate != _date.GetCurrentDate)
        {
            _list = GetReadingSchedule(_date.GetCurrentDate.Year, false); //stick with current year.
            _lastDate = _date.GetCurrentDate; //so if you keep it open but later refresh, then will check up again in another date.
        }
        CurrentModel output = new();
        output.CurrentReading = _list.First(); //hopefully this simple.
        output.IsCurrentReading = _lastDate == output.CurrentReading.ReadDate.ToDateOnly();
        //this will know when the next reading is if its the weekend.

        return output;
    }
    //a choice between using a service to save or just get a list and do whatever else.  i like choices.
    public BasicList<DailyReaderModel> GetReadingSchedule(int year, bool startFromBeginning)
    {
        BasicList<int> chapters = rr.NewTestament5DayReadingChapters.GetTextList();
        BasicList<string> books = rr.NewTestament5DayReadingOrder.GetTextList();
        if (chapters.Count != books.Count)
        {
            throw new CustomBasicException("Books and chapters don't match");
        }
        //int year = _date.CurrentYear();
        BasicList<DateOnly> dates = _date.GetReadingList(chapters.Sum(), year);
        int x;
        BasicList<DailyReaderModel> readings = new();
        int z = 0;
        for (x = 0; x < chapters.Count; x++)
        {
            int totals = chapters[x];
            string book = books[x];
            totals.Times(c =>
            {
                DailyReaderModel item = new();
                item.Book = book;
                item.Chapter = c;
                item.ReadDate = dates[z].ToDateTime();
                if (startFromBeginning == true || dates[z] >= _date.GetCurrentDate)
                {
                    readings.Add(item);
                }
                z++;
            });
        }
        if (readings.Count == 0)
        {
            year++;
            return GetReadingSchedule(year, true); //to guarantee you will have at least one reading
        }
        return readings;
    }
}