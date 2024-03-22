CREATE PROCEDURE AddBook
    @Title NVARCHAR(100),
    @AuthorId INT
AS
BEGIN
    INSERT INTO Books (Title, AuthorId) VALUES (@Title, @AuthorId);
END;