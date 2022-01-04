namespace NewTestament5DayLibrary.Core.Services;
public static class Extensions
{
    public static int CurrentYear(this IDateOnlyPicker date)
    {
        DateOnly used = date.GetCurrentDate;
        if (used.Month == 12)
        {
            return used.Year + 1;
        }
        return used.Year;
    }
    //eventually this can be in another library but not for a while.
    internal static BasicList<DateOnly> GetReadingList(this IDateOnlyPicker date, int year, int totalReadings)
    {
        //int year = date.CurrentYear();
        //starting on first monday of the year seemed to make the most sense.
        DateOnly start = new(year, 1, 1);
        DateOnly ends = new(year, 12, 31);
        DateOnly current = start;
        BasicList<DateOnly> output = new();
        do
        {
            if (current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday)
            {
                current = current.AddDays(1); //do again.
                continue;
            }
            output.Add(current);
            current = current.AddDays(1);
            if (current == ends)
            {
                break;
            }
            if (output.Count == totalReadings)
            {
                break;
            }
        } while (true);
        if (output.Count != totalReadings)
        {
            throw new CustomBasicException($"There should have been {totalReadings} but there was {output.Count} readings");
        }
        return output;
    }
}