namespace ConsoleApp3 {
    public class BookNotFoundException : Exception {
        public BookNotFoundException() : base() { }
        public BookNotFoundException(string message) : base(message) { }
    }
}
