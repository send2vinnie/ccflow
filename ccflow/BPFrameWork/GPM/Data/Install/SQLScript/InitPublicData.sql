DELETE GPM_Bar;
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('01','信息块测试','信息块测试1','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('02','信息块测试','信息块测试2','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('03','信息块测试','信息块测试3','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('04','信息块测试','信息块测试4','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('05','信息块测试','信息块测试5','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('06','信息块测试','信息块测试6','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('07','信息块测试','信息块测试7','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('08','信息块测试','信息块测试8','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('09','信息块测试','信息块测试9','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('10','信息块测试','信息块测试10','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');
INSERT INTO GPM_Bar(No,Name,Title,Tag1,Tag2) VALUES ('11','信息块测试','信息块测试11','SELECT No,Name FROM PORT_DEPT','http://ccflow.org');

DELETE GPM_STem;
INSERT INTO GPM_STem(No,Name,CtrlWay,AppModel,Url,MyFileName,MyFilePath,MyFileExt,WebPath) VALUES ('CCFlow','工作流程',0,0,'http://localhost/ccflow/WF/Login.aspx','admin.gif','D:\ccflow\BPFrameWork\GPM\\DataUser\BP.GPM.STem\','GIF','Img/workflow.png');
INSERT INTO GPM_STem(No,Name,CtrlWay,AppModel,Url,MyFileName,MyFilePath,MyFileExt,WebPath) VALUES ('CCIM','即时通讯',0,1,'http://localhost/ccflow/WF/Login.aspx','admin.gif','D:\ccflow\BPFrameWork\GPM\\DataUser\BP.GPM.STem\','GIF','Img/im.png');
INSERT INTO GPM_STem(No,Name,CtrlWay,AppModel,Url,MyFileName,MyFilePath,MyFileExt,WebPath) VALUES ('GGXXW','公共信息网',0,1,'http://172.20.2.100:8081/','db.gif','D:\ccflow\BPFrameWork\GPM\\DataUser\BP.GPM.STem\','GIF','Img/common.png');
INSERT INTO GPM_STem(No,Name,CtrlWay,AppModel,Url,MyFileName,MyFilePath,MyFileExt,WebPath) VALUES ('GPM','权限管理',0,1,'http://localhost/GPM/App/Port/Signin.aspx','MyWork.gif','D:\ccflow\BPFrameWork\GPM\\DataUser\BP.GPM.STem\','GIF','Img/auth.png');
INSERT INTO GPM_STem(No,Name,CtrlWay,AppModel,Url,MyFileName,MyFilePath,MyFileExt,WebPath) VALUES ('SSO','单点登陆',0,1,'http://127.0.0.1/GPM/SSO/Default.aspx','RptDir.gif','D:\ccflow\BPFrameWork\GPM\\DataUser\BP.GPM.STem\','GIF','Img/sso.png');
 
DELETE GPM_PerAlert;
INSERT INTO GPM_PerAlert(No,Name,Url,GetSQL,WebPath) VALUES ('Email','新邮件','http://mail.google.com','SELECT COUNT(*) FROM PORT_EMP','Img/email.png');
INSERT INTO GPM_PerAlert(No,Name,Url,GetSQL,WebPath) VALUES ('Msg','系统消息','http://mail.google.com','SELECT COUNT(*) FROM PORT_EMP','Img/msg.png');
INSERT INTO GPM_PerAlert(No,Name,Url,GetSQL,WebPath) VALUES ('MyWork','待办工作','http://localhost/ccflow/WF/EmpWorks.aspx','SELECT COUNT(*) FROM ZHONGQI.dbo.WF_EMPWORKS WHERE FK_EMP=@WebUser.No','/GPM/DataUser/BP.GPM.PerAlert/MyWork.gif');
INSERT INTO GPM_PerAlert(No,Name,Url,GetSQL,WebPath) VALUES ('News','未读新闻','http://sina.com.cn','SELECT COUNT(*) FROM PORT_EMP','/GPM/DataUser/BP.GPM.PerAlert/News.gif');
INSERT INTO GPM_PerAlert(No,Name,Url,GetSQL,WebPath) VALUES ('Notice','未读公告','http://sina.com.cn','SELECT COUNT(*) FROM PORT_EMP','/GPM/DataUser/BP.GPM.PerAlert/Notice.gif');
INSERT INTO GPM_PerAlert(No,Name,Url,GetSQL,WebPath) VALUES ('Ontheway','在途工作','http://sina.com.cn','SELECT COUNT(*) FROM PORT_EMP','/GPM/DataUser/BP.GPM.PerAlert/Ontheway.gif');