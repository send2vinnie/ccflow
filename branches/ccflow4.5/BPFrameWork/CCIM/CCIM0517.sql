--2012.5.17
ALTER TABLE Users ADD SID varchar(20) null

GO

-- =============================================
-- Author:		Name
-- Create date: 
-- Description:	���û���¼�ɹ��󣬸����û�������Ϣ
-- =============================================
ALTER PROCEDURE [dbo].[UserUpdateOnlineInfo] 
	-- Add the parameters for the stored procedure here
	--@userID varchar(20),--�û�ID 
	@userID nvarchar(20),--�û�ID 
	--@IP varchar(15) ,--�û�IP
	@IP nvarchar(15) ,--�û�IP
    @Port int,--�˿�
    @SID varchar(20), --SID
    @NetClass tinyint--��������
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--update systemUsers set lastIp=@IP,lastPort=@Port,lastNetClass=@NetClass,lastDate=GetDate(),onlineState=1 where userID=@userID
	update Users set lastIp=@IP,lastPort=@Port,lastNetClass=@NetClass,lastDate=GetDate(),[SID]=@SID,onlineState=1 where userID=@userID
END


GO







