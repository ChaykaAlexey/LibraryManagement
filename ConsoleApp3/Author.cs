using System.Text;

namespace ConsoleApp3 {
    public class Author {
        public Author(string lastName) : this(lastName, string.Empty, string.Empty, DateTime.Now) { }

        public Author(string lastName, string firstName) : this(lastName, firstName, string.Empty, DateTime.Now) { }

        public Author(string lastName, string firstName, string surName) : this(lastName, firstName, surName, DateTime.Now) { }

        public Author(string lastName, string firstName, string surName, DateTime dateOfBirthd) {
            LastName = lastName;
            FirstName = firstName;
            SurName = surName;
            DateOfBirthd = dateOfBirthd;
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirthd { get; set; }

        public override bool Equals(object? obj) {
            if (obj is Author author) {
                return author.LastName == LastName &&
                    author.FirstName == FirstName &&
                    author.SurName == SurName;
            }

            return false;
        }

        public string GetDescription() {
            char[] chars = null;
            var description = new StringBuilder(LastName);
            description.Append(' ');
            description.Append(FirstName);
            description.Append(' ');
            description.Append(SurName);
            description.Append(", ");
            description.Append("Дата рождения: ");
            description.Append(DateOfBirthd.Day);
            description.Append('.');
            description.Append(DateOfBirthd.Month);
            description.Append('.');
            description.Append(DateOfBirthd.Year);

            return description.ToString();
        }
    }
}
