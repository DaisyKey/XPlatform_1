using System; 
using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  

namespace XPlatform.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }  // Связь с автором
        public bool IsBorrowed { get; set; }  // Статус, взята ли книга
        public int? BorrowerId { get; set; }  // Кто взял книгу (если не null, то книга занята)
        public Visitor Borrower { get; set; }  // Навигационное свойство для связи с посетителем

        // Метод для проверки доступности книги
        public bool IsAvailable() => !IsBorrowed;

        // Метод для записи о том, что книга была взята
        public void BorrowBook(Visitor visitor)
        {
            if (!IsAvailable())
                throw new InvalidOperationException("The book is already borrowed.");

            BorrowerId = visitor.Id;
            Borrower = visitor;
            IsBorrowed = true;
        }

        // Метод для возврата книги
        public void ReturnBook()
        {
            BorrowerId = null;
            Borrower = null;
            IsBorrowed = false;
        }
    }
}
