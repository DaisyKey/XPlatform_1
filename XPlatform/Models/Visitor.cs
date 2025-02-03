namespace XPlatform.Models
{
    public class Visitor
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        // Список всех книг, взятых посетителем
        public List<Book> BorrowedBooks { get; set; } = new();

        // Метод для взятия книги
        public void BorrowBook(Book book)
        {
            if (book.IsAvailable())
            {
                book.BorrowBook(this);
                BorrowedBooks.Add(book);
            }
            else
            {
                throw new InvalidOperationException("Book is already borrowed.");
            }
        }

        // Метод для возврата книги
        public void ReturnBook(Book book)
        {
            if (BorrowedBooks.Contains(book))
            {
                book.ReturnBook();
                BorrowedBooks.Remove(book);
            }
            else
            {
                throw new InvalidOperationException("This book is not borrowed by the visitor.");
            }
        }
    }
}
