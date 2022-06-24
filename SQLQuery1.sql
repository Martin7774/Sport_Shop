CREATE PROCEDURE [dbo].[sp_addUser]
@Name NVARCHAR (50),
@Password NVARCHAR(50),
@ProfessionId INT,
@Salt VARBINARY(16),
@ID int OUTPUT
AS
INSERT INTO Users (name, password, professionId) VALUES (@Name, @Password, @ProfessionId, @Salt)
SET @ID = @@IDENTITY