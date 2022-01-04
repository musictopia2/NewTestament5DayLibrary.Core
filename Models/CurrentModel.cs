using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewTestament5DayLibrary.Core.Models;

public class CurrentModel
{
    public DailyReaderModel CurrentReading { get; set; } = new();
    public bool IsCurrentReading { get; set; } //this is new information needed.  so if its not a current reading, can figure out what day is the next reading.
}
