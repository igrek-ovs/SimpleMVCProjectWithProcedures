CREATE PROCEDURE GetAllAuthors
AS
BEGIN
    SELECT AuthorId, Name
    FROM Authors;
END;