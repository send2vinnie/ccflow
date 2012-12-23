--2012.5����
CREATE TABLE [dbo].[UserRecentContact](
	[UserID] [varchar](50) NOT NULL,
	[ContactID] [varchar](50) NULL,
	[ContactType] [int] NULL,
	[ContactTime] [varchar](50) NULL,
	[OrderID] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0-User  1-Group' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'UserRecentContact', @level2type=N'COLUMN',@level2name=N'ContactType'

GO


--ֻ��ȡǰ20��
CREATE PROCEDURE [dbo].[UserGetRecentContactList] 
   @userId varchar(20)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT TOP 20 UserID,ContactID,ContactType,ContactTime,OrderID from UserRecentContact  where UserID=@userId ORDER BY CONVERT(datetime,ContactTime) DESC

END

GO

-- =============================================
-- Author:		Li Jian
-- Create date: 
-- Description:	�û����������ϵ���б�
-- =============================================
CREATE PROCEDURE [dbo].[UpdateRecentContact] 
	-- Add the parameters for the stored procedure here
	--@userID varchar(20),--�û�ID
	@userID nvarchar(20),--�û�ID
	@receiveID nvarchar(20)-- �����ϵ��ID
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
   SET NOCOUNT ON;
   
   DECLARE @existUserID int;
   DECLARE @cTime varchar(50);

   SET @cTime= CONVERT(varchar(4),year(getdate()))+'-'+CONVERT(varchar(4),month(getdate()))+'-'+CONVERT(varchar(2),day(getdate()))+' '+CONVERT(varchar(2),datepart(hh,getdate()))+':'+CONVERT(varchar(2),datepart(mi,getdate()))+':'+CONVERT(varchar(2),datepart(ss,getdate()))+'.'+CONVERT(varchar(10),datepart(ms,getdate()))

   SELECT @existUserID=COUNT(*) FROM UserRecentContact WHERE UserID=@userID AND ContactID=@receiveID 
   
   IF @existUserID=0--�б���û�и��û�������Ӽ�¼
    begin 
      INSERT INTO UserRecentContact(UserID,ContactID,ContactType,ContactTime,OrderID) VALUES(@userID,@receiveID,0,@cTime,0)
    end
   else --�б����Ѵ��ڣ������ʱ��
   begin
	  update UserRecentContact set ContactTime=@cTime where UserID=@userID AND ContactID=@receiveID
   end
END

GO
