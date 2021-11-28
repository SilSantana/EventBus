namespace Demo.Infrastructure.Ioc.Models
{
    public class MockSettings
    {
        public bool Booking { get; set; } = true;
        public DemoApiSettings BookingApi { get; set; } = new DemoApiSettings();

    }
}
