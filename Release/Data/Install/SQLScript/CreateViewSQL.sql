/****** 对象:  View WF_EmpWorks    脚本日期: 03/12/2011 21:42:50 ******/
GO

/*  WF_EmpWorks  */
CREATE VIEW WF_EmpWorks
AS
SELECT     A.WorkID, A.Rec AS Starter, A.FK_Flow, C.FlowName,
 C.NodeID, C.Name AS NodeName, A.Title, A.RDT, B.RDT AS ADT, 
B.SDT, B.FK_Emp,B.FID ,C.FK_FlowSort
FROM         dbo.WF_GenerWorkFlow AS A INNER JOIN
                      dbo.WF_GenerWorkerList AS B ON A.WorkID = B.WorkID AND A.FK_Node = B.FK_Node INNER JOIN
                      dbo.WF_Node AS C ON B.FK_Node = C.NodeID
WHERE     (B.IsEnable = 1) AND (B.IsPass = 0) 
GO


/*  WF_GenerEmpWorks  */
GO
CREATE VIEW WF_GenerEmpWorks
AS
SELECT FK_Flow, FK_Emp, COUNT(*) AS NUM  FROM WF_GenerWorkerList
 WHERE IsEnable=1 AND IsPass=0 GROUP BY FK_FLOW, FK_Emp
GO

/*  WF_NodeExt  */
CREATE VIEW WF_NodeExt
AS
SELECT NODEID AS NO , NAME AS NAME FROM WF_Node
GO
 

/*  创建抄送存储过程  */
GO
CREATE PROCEDURE CCstaff
(
	@Sender nvarchar(100),
	@Receivers nvarchar(2000),
	@Title nvarchar(100),
	@Context nvarchar(max)
)	
AS
BEGIN
	 /* 编写您的业务逻辑在这里. */
	SET NOCOUNT ON;
--	declare @SenderId int
--	declare @CCs varbinary(max)
--	declare @BCCs varbinary(max)
--	declare @SendTime datetime
--	declare @Importance nchar(2)
--	declare @Size int
--	declare @HasAffixture bit
--	declare @Affixture nvarchar(4000)
END