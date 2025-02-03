﻿using System;  
using System.ComponentModel.DataAnnotations;  
using System.ComponentModel.DataAnnotations.Schema;  



namespace XPlatform.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; } = new();

        // Метод для добавления книги к автору
        public void AddBook(Book book)
        {
            if (!Books.Contains(book))
                Books.Add(book);
        }
    }
}
