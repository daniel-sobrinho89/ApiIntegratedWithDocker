using DateTimeProviders;

namespace Tests.Common.Infrastructure;

public static class Clock
{
    private static IDateTimeProvider? _instance;
    private static readonly object _padlock = new();

    public static IDateTimeProvider Provider
    {
        get
        {
            if (_instance == null)
            {
                lock (_padlock)
                {
                    _instance ??= new UtcDateTimeProvider();
                }
            }

            return _instance;
        }
        set
        {
            lock (_padlock)
            {
                _instance = value;
            }
        }
    }

    public static DateTime UtcNow => Provider.Now.UtcDateTime;

    public static void FreezeCurrentDate() => Provider = new StaticDateTimeProvider(DateTimeProvider.UtcNow);
}
