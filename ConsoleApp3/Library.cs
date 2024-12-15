namespace ConsoleApp3 {
    public class Library {
        private readonly Dictionary<string, Book> _bookDictionary = [];

        public bool AddBook(Book addedBook) {
            if (ChecAvailableBook(addedBook)) return false;

            return _bookDictionary.TryAdd(addedBook.Name, addedBook);
        }

        public Book GetBook(string nameBook) {
            var foundBook = _bookDictionary.Values.FirstOrDefault(book => book.Name == nameBook);

            if (foundBook == null)
                throw new BookNotFoundException("Книга не найдена");

            return foundBook;
        }

        public List<Book> GetBook(Author searchAuthor) {
            var foundBooks = _bookDictionary.Values.Where(book => book.Author.Equals(searchAuthor)).ToList();

            if (foundBooks.Count == 0)
                throw new BookNotFoundException("Книги не найдены");

            return foundBooks;
        }

        public List<Book> GetBook(int yearPublication) {
            var foundBooks = _bookDictionary.Values.Where(book => book.DatePublication.Year == yearPublication).ToList();

            if (foundBooks.Count == 0)
                throw new BookNotFoundException("Книги не найдены");

            return foundBooks;
        }

        public string[] GetBooksDescription() {
            if (_bookDictionary.Count == 0) return Array.Empty<string>();

            var listBook = new string[_bookDictionary.Count];

            int i = 0;
            foreach (var book in _bookDictionary.Values) {
                if (i >= listBook.Length)
                    break;

                listBook[i++] = book.GetDescription();
            }

            return listBook;
        }

        public bool RemoveBook(string nameBook) {
            if (!ChecAvailableBook(nameBook))
                throw new BookNotFoundException("Книга не найдена");

            return _bookDictionary.Remove(nameBook);
        }

        private bool ChecAvailableBook(string nameBook) {
            if (_bookDictionary.Count == 0) return false;

            return _bookDictionary.ContainsKey(nameBook);
        }

        private bool ChecAvailableBook(Book searchBook) {
            if (_bookDictionary.Count == 0) return false;

            foreach (var book in _bookDictionary.Values)
                if (searchBook.Equals(book)) return true;

            return false;
        }
    }
}
