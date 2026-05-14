USE HomeLibrary;
GO

CREATE PROCEDURE GetAllBooks
AS
BEGIN
    SELECT id, title, author, year, genre, publisher, isbn, content_html
    FROM books
    ORDER BY title;
END;
GO

CREATE PROCEDURE GetBookById
    @BookId INT
AS
BEGIN
    SELECT id, title, author, year, genre, publisher, isbn, content_html
    FROM books
    WHERE id = @BookId;
END;
GO

CREATE PROCEDURE InsertBook
    @Title NVARCHAR(300),
    @Author NVARCHAR(200),
    @Year INT,
    @Genre NVARCHAR(100),
    @Publisher NVARCHAR(200),
    @ISBN NVARCHAR(20),
    @ContentHtml NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO books (title, author, year, genre, publisher, isbn, content_html)
    VALUES (@Title, @Author, @Year, @Genre, @Publisher, @ISBN, @ContentHtml);
    
    SELECT SCOPE_IDENTITY() AS NewId;
END;
GO

CREATE PROCEDURE UpdateBook
    @BookId INT,
    @Title NVARCHAR(300),
    @Author NVARCHAR(200),
    @Year INT,
    @Genre NVARCHAR(100),
    @Publisher NVARCHAR(200),
    @ISBN NVARCHAR(20),
    @ContentHtml NVARCHAR(MAX)
AS
BEGIN
    UPDATE books
    SET title = @Title,
        author = @Author,
        year = @Year,
        genre = @Genre,
        publisher = @Publisher,
        isbn = @ISBN,
        content_html = @ContentHtml
    WHERE id = @BookId;
END;
GO

CREATE PROCEDURE DeleteBook
    @BookId INT
AS
BEGIN
    DELETE FROM books WHERE id = @BookId;
END;
GO

EXIT
