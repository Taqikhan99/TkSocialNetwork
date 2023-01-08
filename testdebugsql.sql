





DECLARE @RC int
DECLARE @FirstName varchar(50)
DECLARE @LastName varchar(50)
DECLARE @Email varchar(50)
DECLARE @Phone varchar(50)
DECLARE @Password varchar(50)

-- TODO: Set parameter values here.

EXECUTE @RC = [dbo].[spRegisterUser] 
   @FirstName
  ,@LastName
  ,@Email
  ,@Phone
  ,@Password
GO