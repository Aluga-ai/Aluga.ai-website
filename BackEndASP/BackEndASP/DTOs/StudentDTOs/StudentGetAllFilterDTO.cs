namespace BackEndASP.DTOs.StudentDTOs
{
    public class StudentGetAllFilterDTO
    {

        public string Name { get; set; }
        public int Age { get; set; }
        public string College { get; set; }
        public List<HOBBIE> Hobbies { get; set; } = new List<HOBBIE>();
        public List<PERSONALITY> Personalitys { get; set; } = new List<PERSONALITY>();



        public StudentGetAllFilterDTO()
        {

        }


        public StudentGetAllFilterDTO(Student entity)
        {
            this.Name = entity.UserName.ToUpper();
            this.Age = CalcAge(entity.BirthDate, DateTimeOffset.Now);
            this.College = entity.College.Name ?? "";
            this.Hobbies = entity.Hobbies.ToList();
            this.Personalitys = entity.Personalitys.ToList();
        }



        private int CalcAge(DateTimeOffset dataNascimento, DateTimeOffset dataAtual)
        {
            int idade = dataAtual.Year - dataNascimento.Year;

            if (dataAtual < dataNascimento.AddYears(idade))
            {
                idade--;
            }

            return idade;
        }

    }
}
