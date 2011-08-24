/*

hr_user      用户表
hr_employee  员工表
HR_BaseOrganization 部门表


hr_position 岗位表
HR_EmpPosition  员位-岗位关联表
HR_EmpOrgan 员表-部门关联表

SELECT * FROM  hr_user
SELECT * FROM  hr_employee
SELECT * FROM  HR_BaseOrganization
SELECT * FROM  hr_position
SELECT * FROM  HR_EmpPosition
SELECT * FROM  HR_EmpOrgan

SELECT * FROM  hr_user
SELECT * FROM  hr_employee
*/


/* 部门 */
GO
DROP VIEW Port_Dept
GO
CREATE VIEW Port_Dept
AS
SELECT TreeNo as No, OragnName as Name, OrganID 
FROM HR_BaseOrganization 
GO 
/* 岗位 */
GO
DROP VIEW Port_Station
GO
CREATE VIEW Port_Station
AS
SELECT PositionCode as No, PositionName as Name, 0 as StaGrade, PID
FROM hr_position
GO
  


/* 操作员 */
GO
DROP VIEW Port_EmpT
GO
CREATE VIEW Port_EmpT
AS
SELECT a.UName as No, b.EmpName as Name, a.UPassWord as Pass, a.EmpID
 FROM hr_user A, hr_employee B WHERE A.EmpID=B.EmpID
GO


/* 人员部门 */
GO
DROP VIEW Port_EmpDept
GO
CREATE VIEW Port_EmpDept
AS
SELECT D.No as FK_Emp,B.No as FK_Dept ,
 a.EmpID,a.OrganID,D.Name,D.Pass
  FROM  HR_EmpOrgan A, Port_Dept B , hr_user C, Port_EmpT D
WHERE A.OrganID=B.OrganID AND A.EmpID=D.EmpID
GO

/* 人员 */
GO
DROP VIEW Port_Emp
GO
CREATE VIEW Port_Emp
AS
SELECT Distinct FK_Emp as No, Name,Pass,FK_Dept FROM Port_EmpDept
GO

/* 人员岗位对应 */
GO
DROP VIEW Port_EmpStation
GO
CREATE VIEW Port_EmpStation
AS
SELECT C.No as FK_Emp,B.No as FK_Station  
  FROM  HR_EmpPosition A, Port_Station B , Port_EmpT C
WHERE A.PID=B.PID AND A.EmpID=C.EmpID
GO

 



