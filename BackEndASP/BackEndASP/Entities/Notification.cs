
    public class Notification
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTimeOffset Moment = DateTimeOffset.Now;

        public bool Read { get; set; } = false;

        public IEnumerable<User> Users { get;} = Enumerable.Empty<User>();
    }

