namespace NewTestament5DayLibrary.Core.Services;
public class YearlyBibleReadingService
{
    private readonly IDateOnlyPicker _date;
    public YearlyBibleReadingService(IDateOnlyPicker date)
    {
        _date = date;
    }
    //a choice between using a service to save or just get a list and do whatever else.  i like choices.
    public BasicList<DailyReaderModel> GetReadingSchedule()
    {
        BasicList<int> chapters = rr.NewTestament5DayReadingChapters.GetTextList();
        BasicList<string> books = rr.NewTestament5DayReadingOrder.GetTextList();
        if (chapters.Count != books.Count)
        {
            throw new CustomBasicException("Books and chapters don't match");
        }
        BasicList<DateOnly> dates = _date.GetReadingList(chapters.Sum());
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
                readings.Add(item);
                z++;
            });
        }
        return readings;
    }
}