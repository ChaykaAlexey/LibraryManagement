using System.Text;

namespace ConsoleApp3 {
    public class Book {
        public Book(string name, Author author, DateTime datePublication) {
            Name = name;
            Author = author;
            DatePublication = datePublication;
        }

        public Author Author { get; set; }
        public DateTime DatePublication { get; set; }
        public string Name { get; set; }

        public override bool Equals(object? obj) {
            if (obj is Book book)
                return book.Author.Equals(Author) &&
                    book.Name == Name &&
                    book.DatePublication == DatePublication;

            return false;
        }

        public string GetDescription() {
            var descriptionBook = new StringBuilder("Название книги: ");
            descriptionBook.Append(Name);
            descriptionBook.Append(", ");
            descriptionBook.Append("Автор: ");
            descriptionBook.Append(Author.GetDescription());
            descriptionBook.Append(", ");
            descriptionBook.Append("Дата публикации: ");
            descriptionBook.Append(DatePublication.Day);
            descriptionBook.Append('.');
            descriptionBook.Append(DatePublication.Month);
            descriptionBook.Append('.');
            descriptionBook.Append(DatePublication.Year);

            return descriptionBook.ToString();
        }
    }
}
