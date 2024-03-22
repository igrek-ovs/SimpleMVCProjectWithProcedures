CREATE PROCEDURE GetBooks
AS
BEGIN
    SELECT Books.BookId, Books.Title, Authors.Name AS AuthorName
    FROM Books
    INNER JOIN Authors ON Books.AuthorId = Authors.AuthorId;
END;