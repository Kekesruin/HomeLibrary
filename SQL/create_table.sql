IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'HomeLibrary')
BEGIN
    CREATE DATABASE HomeLibrary;
END;
GO

USE HomeLibrary;
GO

CREATE TABLE books (
    id INT IDENTITY(1,1) PRIMARY KEY,
    title NVARCHAR(300) NOT NULL,
    author NVARCHAR(200) NOT NULL,
    year INT,
    genre NVARCHAR(100),
    publisher NVARCHAR(200),
    isbn NVARCHAR(20),
    content_html NVARCHAR(MAX)
);
GO

IF NOT EXISTS (SELECT * FROM sys.columns WHERE name = 'content_xml' AND object_id = OBJECT_ID('books'))
BEGIN
    ALTER TABLE books ADD content_xml AS CAST(content_html AS XML);
END;
GO

