DELETE Sys_SFTable WHERE No='BP.CN.Citys';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.CN.Citys','城市','FK_City','2','中国的市级城市','','1');

DELETE Sys_SFTable WHERE No='BP.CN.PQs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.CN.PQs','地区','FK_DQ','2','华北、西北、西南。。。','','1');

DELETE Sys_SFTable WHERE No='BP.CN.SFs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.CN.SFs','省份','FK_SF','2','中国的省份。','','1');

DELETE Sys_SFTable WHERE No='BP.Port.Depts';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Port.Depts','部门','FK_Dept','2','部门','','1');

DELETE Sys_SFTable WHERE No='BP.Port.Emps';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Port.Emps','人员','FK_Emp','2','系统中的操作员','','1');


DELETE Sys_SFTable WHERE No='BP.Port.Stations';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Port.Stations','岗位','FK_Station','2','工作岗位','','1');

DELETE Sys_SFTable WHERE No='BP.Pub.Days';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Pub.Days','日','FK_Day','0','1-31日','','1');


DELETE Sys_SFTable WHERE No='BP.Pub.YFs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Pub.YFs','月','FK_NY','0','1-12月','','1');

DELETE Sys_SFTable WHERE No='BP.Pub.NYs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Pub.NYs','年月','FK_NY','0','年度与月份','','1');

DELETE Sys_SFTable WHERE No='BP.Pub.NDs';
INSERT INTO Sys_SFTable (No,Name,FK_Val,SFTableType,TableDesc,DefVal,IsEdit)
VALUES ('BP.Pub.NDs','年度','FK_ND','0','年度','','1');

