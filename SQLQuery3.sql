DROP PROCEDURE delete_User
GO
CREATE PROCEDURE [dbo].[delete_User]
--@shortName VARCHAR (3),
--@longName VARCHAR(50),
@ID int OUTPUT
AS
DELETE FROM Users WHERE Id = @ID