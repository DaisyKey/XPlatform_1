using System;  
using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  



namespace XPlatform.Models
{
    public class Author
    {
        public int Id { get; set; } // это поле не нужно пользователю, но нужно в БД
        public string Name { get; set; } // это поле можно вывести пользователю
        public List<Book> Books { get; set; } = new(); // Это поле нужно вывести пользователю

        public override bool Equals(object obj)
        {
            if (obj is Author otherAuthor)
                return this.Id == otherAuthor.Id && this.Name == otherAuthor.Name;
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name);
        }

        // Метод для добавления книги к автору
        public void AddBook(Book book)
        {
            if (!Books.Contains(book))
                Books.Add(book);
        }
    }
}
