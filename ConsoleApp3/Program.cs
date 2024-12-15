namespace ConsoleApp3 {
    internal class Program {
        private readonly static string[] _descriptionMethodSearchBook = [
            "По названию",
            "По автору",
            "По году"
            ];

        private readonly static string[] _menuItemsDescription = [
            "Добавить книгу",
            "Удалить книгу",
            "Получить список книг",
            "Найти книгу"
            ];

        private readonly static Library _library = new();

        static void Main(string[] args) {
            do {
                Console.Clear();

                try {
                    MainMenuNavigation();
                }
                catch (Exception generalException) {
                    Console.WriteLine(generalException.Message);
                }
                finally {
                    Console.WriteLine("Для завершения работы программы нажмите клавишу \"Esc\" ...");
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        private static bool CheckValidYear(int year) {
            return year > 1000 &&
                year <= DateTime.Now.Year;
        }

        private static Author GetAuthorFromUser() {
            string lastNameAuthor = UserInput("Введите фамилию автора: ");
            string firstNameAuthor = UserInput("Введите первую букву или имя автора: ");
            string surNameAuthor = UserInput("Введите первую букву или отчество автора: ");
            DateTime dateBirthdAuthor = GetDateFromUser("Дата рождения автора: ");

            return new Author(lastNameAuthor, firstNameAuthor, surNameAuthor, dateBirthdAuthor);
        }

        private static DateTime GetDateFromUser(string message) {
            Console.WriteLine($"\n{message}");

            int day = ParseToInt(UserInput("Введите день: "));
            int month = ParseToInt(UserInput("Введите номер месяца: "));
            int year = ParseToInt(UserInput("Введите год: "));

            return new DateTime(year, month, day);
        }

        private static void MainMenuNavigation() {
            PrintMenuItems(_menuItemsDescription);
            BasicMenuItem basicMenuItem = SelectBasicMenuItem();

            switch (basicMenuItem) {
                case BasicMenuItem.AddBook:
                    string nameBook = UserInput("Введите название книги: ");
                    Author author = GetAuthorFromUser();
                    DateTime date = GetDateFromUser("Дата публикации книги");

                    var book = new Book(nameBook, author, date);

                    _ = _library.AddBook(book);
                    break;
                case BasicMenuItem.RemoveBook:
                    _ = _library.RemoveBook(UserInput("Название книги: "));
                    break;
                case BasicMenuItem.GetBooks:
                    string[] books = _library.GetBooksDescription();
                    PrintListBooks(books);
                    break;
                case BasicMenuItem.SearchBook:
                    SearchBookMenuNavigation();
                    break;
                default:
                    break;
            }
        }

        private static T ParseToEnum<T>(int valueToPrase) where T : Enum {
            if (!Enum.IsDefined(typeof(T), valueToPrase))
                throw new ArgumentException("Введеное значение не является частью меню");

            return (T)Enum.ToObject(typeof(T), valueToPrase);
        }

        private static void PrintBooks(List<Book> books) {
            for (int i = 0; i < books.Count; i++) {
                Console.WriteLine(books[i].GetDescription());
            }
        }

        private static void PrintListBooks(string[] booksDescription) {
            if (booksDescription.Length == 0) {
                Console.WriteLine("Список книг пуст");
                return;
            }

            for (int i = 0; i < booksDescription.Length; i++)
                Console.WriteLine(booksDescription[i]);
        }

        private static void PrintMenuItems(string[] menuItemDescriptions) {
            for (int i = 0; i < menuItemDescriptions.Length; i++)
                Console.WriteLine($"{i} - {menuItemDescriptions[i]}");
        }

        private static void SearchBookMenuNavigation() {
            PrintMenuItems(_descriptionMethodSearchBook);
            MethodSearchBook methodSearchBook = SelectMethodSearchBook();
            List<Book> books = new List<Book>();
            int yeaPublication = -1;

            switch (methodSearchBook) {
                case MethodSearchBook.ByNameBook:
                    string nameBook = UserInput("Введите имя книги: ");

                    books.Add(_library.GetBook(nameBook));
                    break;
                case MethodSearchBook.ByNameAuthor:

                    books = _library.GetBook(GetAuthorFromUser());
                    break;
                case MethodSearchBook.ByYearPublication:
                    yeaPublication = ParseToInt(UserInput("Введите год издания: "));

                    if (!CheckValidYear(yeaPublication))
                        throw new ArgumentException("Введен неверный год");

                    books = _library.GetBook(yeaPublication);
                    break;
                default:
                    break;
            }

            PrintBooks(books);
        }

        private static BasicMenuItem SelectBasicMenuItem() =>
            ParseToEnum<BasicMenuItem>(ParseToInt(UserInput()));

        private static MethodSearchBook SelectMethodSearchBook() =>
            ParseToEnum<MethodSearchBook>(ParseToInt(UserInput()));

        private static string UserInput() {
            return UserInput("Ввод: ");
        }

        private static string UserInput(string message) {
            Console.Write($"{message}");
            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput) || string.IsNullOrWhiteSpace(userInput))
                throw new ArgumentNullException("Введено пустое значение");

            return userInput;
        }

        private static int ParseToInt(string userInput) {
            int imputNumber = -1;

            if (!int.TryParse(userInput, out imputNumber))
                throw new ArgumentException("Введенное значение не явлется числом");

            return imputNumber;
        }
    }
}
