<%@ control language="C#" autoeventwireup="true" inherits="Comm_UC_UIEn, App_Web_tbnyxtyf" %>
	<%@ Register src="UCEn.ascx" tagname="UCEn" tagprefix="uc1" %>
<%@ Register src="ToolBar.ascx" tagname="ToolBar" tagprefix="uc2" %>

	<TABLE  width="95%"  cellSpacing="1" cellPadding="1" border="0">
					<TR>
						<TD  class="ToolBar" >
                            <uc2:ToolBar ID="ToolBar1" runat="server" />
                        </TD>
					</TR>
					
					<TR valign="top">
						<TD valign="top" height="0">
                            <uc1:UCEn ID="UCEn1" runat="server" />
                        </TD>
					</TR>
</TABLE>
				