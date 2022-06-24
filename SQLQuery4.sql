DROP PROCEDURE edit_User
GO
CREATE PROCEDURE [dbo].[edit_User]
@Name NVARCHAR (50),
@Password NVARCHAR(50),
@ProfessionId INT,
@Salt VARBINARY(16),
@ID int OUTPUT
AS
UPDATE Users SET name = @Name, password = @Password, professionId = @ProfessionId, salt = @Salt WHERE Id = @ID