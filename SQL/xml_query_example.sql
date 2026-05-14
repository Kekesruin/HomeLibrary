SET QUOTED_IDENTIFIER ON;
GO

SELECT title,
       content_xml.value('(//h1)[1]', 'NVARCHAR(MAX)') AS first_chapter
FROM books
WHERE content_html LIKE N'%<h1>%';


SELECT title,
       content_xml.value('count(//h2)', 'INT') AS chapter_count
FROM books;


SELECT title,
       content_xml.value('(//p)[1]', 'NVARCHAR(MAX)') AS first_paragraph
FROM books
WHERE content_html LIKE N'%<p>%';

GO
