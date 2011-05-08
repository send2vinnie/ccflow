DELETE Port_Dept
DELETE Port_Station
DELETE Port_Emp
DELETE Port_EmpStation
DELETE Port_EmpDept

-- Port_Dept
INSERT INTO Port_Dept (No,Name) VALUES('01','XXX市局')
INSERT INTO Port_Dept (No,Name) VALUES('0101','市征管科')
INSERT INTO Port_Dept (No,Name) VALUES('0102','市信息科')

INSERT INTO Port_Dept (No,Name) VALUES('0151','YYY区局')
INSERT INTO Port_Dept (No,Name) VALUES('015101','YYY区征管科')
INSERT INTO Port_Dept (No,Name) VALUES('015102','YYY区信息科')
INSERT INTO Port_Dept (No,Name) VALUES('015151','YYY区一分局')
INSERT INTO Port_Dept (No,Name) VALUES('015152','YYY区二分局')


INSERT INTO Port_Dept (No,Name) VALUES('0152','ZZZ区局')
INSERT INTO Port_Dept (No,Name) VALUES('015201','ZZZ区征管科')
INSERT INTO Port_Dept (No,Name) VALUES('015202','ZZZ区信息科')
INSERT INTO Port_Dept (No,Name) VALUES('015251','YYY区一分局')
INSERT INTO Port_Dept (No,Name) VALUES('015352','YYY区二分局')

-- Port_Station
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('01','市局长','1')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('02','市副局长','1')

INSERT INTO Port_Station (No,Name,StaGrade) VALUES('03','市征管科长','2')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('04','市征管科员','2')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('05','市信息科长','2')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('06','市信息科员','2')

INSERT INTO Port_Station (No,Name,StaGrade) VALUES('11','区县局长','2')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('12','区县副局长','2')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('13','区县管科长','2')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('14','区县管科员','2')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('15','区县息科长','2')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('16','区县息科员','2')

INSERT INTO Port_Station (No,Name,StaGrade) VALUES('21','分局局长','3')
INSERT INTO Port_Station (No,Name,StaGrade) VALUES('22','分局科员','3')
 

-- Port_Emp
-- 总经理部
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('admin','admin','pub','01')
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('zhoupeng','周朋','pub','01')

-- 市场部
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('zhanghaicheng','张海成','pub','0101')
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('guoxiangbin','郭祥宾','pub','0101')


-- 研发部
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('hewenxiang','何文祥','pub','0102')
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('zhoutianjiao','周天娇','pub','0102')

-- 技术服务部经理
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('liuxianchen','刘贤臣','pub','0103')
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('fuhui','福惠','pub','0103')

-- 财务部
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('yangyilei','杨依雷','pub','0104')
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('guobaogeng','郭宝庚','pub','0104')

-- 人力资源部
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('liping','李萍','pub','0105')
INSERT INTO Port_Emp (No,Name,Pass,FK_Dept) VALUES('liyan','李言','pub','0105')

 
-- Port_EmpDept 人员与部门的对应
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('zhoupeng','01')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('zhanghaicheng','0101')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('guoxiangbin','0101')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('hewenxiang','0102')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('zhoutianjiao','0102')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('liuxianchen','0103')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('fuhui','0103')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('yangyilei','0104')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('guobaogeng','0104')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('liping','0105')
INSERT INTO Port_EmpDept (FK_Emp,FK_Dept) VALUES('liyan','0105')

-- Port_EmpStation 人员与岗位的对应
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('zhoupeng','01')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('zhanghaicheng','02')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('guoxiangbin','07')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('hewenxiang','03')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('zhoutianjiao','08')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('liuxianchen','04')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('fuhui','09')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('yangyilei','05')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('guobaogeng','10')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('liping','06')
INSERT INTO Port_EmpStation (FK_Emp,FK_Station) VALUES('liyan','11')





