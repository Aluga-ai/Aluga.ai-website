
    public class Image
    {
        public int Id { get; set; }
        public Byte[] ImageData { get; set; }


        public int BuildingId { get; set; }
        public Building Building { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }

