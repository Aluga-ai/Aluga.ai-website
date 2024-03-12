    public class Building
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string HomeComplement { get; set; }
        public string Neighborhood { get; set; }
        public string District { get; set; }
        public string State { get; set; }
        public string Lat { get; set; }
        public string Long { get; set; }

        public ICollection<Student> StudentsLiked { get; set; } = new List<Student>();

}
