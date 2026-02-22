namespace OOPWorkShop.Backend;

public class Time
{
    #region Fields

    private int _hour;
    private int _minute;
    private int _second;
    private int _millisecond;

    #endregion

    #region Constructors (4 overloads + default)

    public Time() : this(0, 0, 0, 0) { }

    public Time(int hour) : this(hour, 0, 0, 0) { }

    public Time(int hour, int minute) : this(hour, minute, 0, 0) { }

    public Time(int hour, int minute, int second) : this(hour, minute, second, 0) { }

    public Time(int hour, int minute, int second, int millisecond)
    {
        Hour = hour;
        Minute = minute;
        Second = second;
        Millisecond = millisecond;
    }

    #endregion

    #region Properties

    public int Hour
    {
        get => _hour;
        set => _hour = ValidHour(value);
    }

    public int Minute
    {
        get => _minute;
        set => _minute = ValidMinute(value);
    }

    public int Second
    {
        get => _second;
        set => _second = ValidSecond(value);
    }

    public int Millisecond
    {
        get => _millisecond;
        set => _millisecond = ValidMillisecond(value);
    }

    #endregion

    #region Validation Methods

    private int ValidHour(int value)
    {
        if (value < 0 || value > 23)
            throw new ArgumentOutOfRangeException($"The hour: {value}, is not valid.");
        return value;
    }

    private int ValidMinute(int value)
    {
        if (value < 0 || value > 59)
            throw new ArgumentOutOfRangeException($"The minute: {value}, is not valid.");
        return value;
    }

    private int ValidSecond(int value)
    {
        if (value < 0 || value > 59)
            throw new ArgumentOutOfRangeException($"The second: {value}, is not valid.");
        return value;
    }

    private int ValidMillisecond(int value)
    {
        if (value < 0 || value > 999)
            throw new ArgumentOutOfRangeException($"The millisecond: {value}, is not valid.");
        return value;
    }

    #endregion

    #region Methods

    public override string ToString()
    {
        int displayHour = _hour % 12;
        if (displayHour == 0)
            displayHour = 12;

        string period = _hour >= 12 ? "PM" : "AM";

        return $"{displayHour:00}:{_minute:00}:{_second:00}.{_millisecond:000} {period}";
    }

    public int ToMilliseconds()
    {
        return (_hour * 3600000) +
               (_minute * 60000) +
               (_second * 1000) +
               _millisecond;
    }

    public int ToSeconds()
    {
        return (_hour * 3600) +
               (_minute * 60) +
               _second;
    }

    public int ToMinutes()
    {
        return (_hour * 60) + _minute;
    }

    public bool IsOtherDay(Time other)
    {
        return (this.ToMilliseconds() + other.ToMilliseconds()) >= 86400000;
    }

    public Time Add(Time other)
    {
        int ms = _millisecond + other._millisecond;
        int secCarry = ms / 1000;
        ms %= 1000;

        int sec = _second + other._second + secCarry;
        int minCarry = sec / 60;
        sec %= 60;

        int min = _minute + other._minute + minCarry;
        int hourCarry = min / 60;
        min %= 60;

        int hour = _hour + other._hour + hourCarry;
        hour %= 24;

        return new Time(hour, min, sec, ms);
    }

    #endregion
}
