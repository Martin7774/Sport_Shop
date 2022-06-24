DROP PROCEDURE sp_userAdd
GO
CREATE PROCEDURE [dbo].[sp_userAdd]
@Name NVARCHAR (50),
@Password NVARCHAR(50),
@ProfessionId INT,
@Salt VARBINARY(16),
@ID int OUTPUT
AS
INSERT INTO Users(name, password, professionId, salt) VALUES (@Name, @Password, @ProfessionId, @Salt)
SET @ID = @@IDENTITY