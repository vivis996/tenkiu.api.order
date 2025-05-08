namespace tenkiu.api.order.Models.Common;

public class DateTimePeriod
{
  public DateTime Start { get; set; }
  public DateTime End { get; set; }

  public DateTimePeriod()
  {
  }

  public DateTimePeriod(DateTime start, DateTime end) : base()
  {
    Start = start;
    End = end;
  }
}
