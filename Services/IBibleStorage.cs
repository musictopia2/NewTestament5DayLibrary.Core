namespace NewTestament5DayLibrary.Core.Services;
public interface IBibleStorage
{
    Task SaveDailyReadingSchedule(BasicList<DailyReaderModel> list); //this takes the list and its up to whoever does it to decide how its going to save it.
}