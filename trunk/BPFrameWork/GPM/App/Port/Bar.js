 
 
function WinOpen(url, winName)
{
            newWindow=window.open( url , winName ,'width=600,top=200,left=150,height=400,scrollbars=yes,resizable=yes,toolbar=false,location=false');
            newWindow.focus();  
}
//true:��ǰ������չ״̬����ɫ��false:��ǰ������С״̬����ɫ
//���������ʱ��ӦΪ��С״̬
var  g_Extended=true;
//��¼Frame�Ĵ�С��
//var Top_Rows='75,*';
var Top_Rows='90,*';
//var Bottom_Cols='198,*';
var Bottom_Cols='100,*';
function shrinkForm()
{        
	window.parent.Top.rows=Top_Rows;
	window.parent.Bottom.cols=Bottom_Cols;
	switchUpPoint.innerText=5
	switchLeftPoint.innerText=3
}
function ExtendForm()
{
	//�˶δ����޷�ʵ��Ԥ���Ĺ��ܡ������ڵ�֡�Ĵ�С�ı�ʱ��
	//֡��rows��cols���Բ������ϸı䡣
	if(window.parent.Top.rows!='0,*') Top_Rows=window.parent.Top.rows;
	if(window.parent.Bottom.cols!='0,*') Bottom_Cols=window.parent.Bottom.cols;
	switchUpPoint.innerText=6
	switchLeftPoint.innerText=4
	window.parent.Top.rows='0,*';
	window.parent.Bottom.cols='0,*';
}

function switchUpBar(){
	if (switchUpPoint.innerText==5){
		switchUpPoint.innerText=6
		Top_Rows=window.parent.Top.rows;
		window.parent.Top.rows='0,*';
	}
	else{
		window.parent.Top.rows=Top_Rows;
		switchUpPoint.innerText=5
	}
	if(window.parent.Bottom.cols=='0,*' && window.parent.Top.rows=='0,*') 
		{
		document.all("Tip").innerText="��׼��";
		document.all("imgsrc").src="max.gif";
		}
	else
		{
		document.all("Tip").innerText="���";
		document.all("imgsrc").src="mix.gif";
	    }
}

function switchLeftBar(){
	if (switchLeftPoint.innerText==3){
		switchLeftPoint.innerText=4
		Bottom_Cols=window.parent.Bottom.cols;

		window.parent.Bottom.cols='0,*';
	}
	else{
		window.parent.Bottom.cols=Bottom_Cols;
		switchLeftPoint.innerText=3
	}

	if(window.parent.Bottom.cols=='0,*' && window.parent.Top.rows=='0,*') 
		{
		document.all("Tip").innerText="��׼��"
		document.all("imgsrc").src="max.gif";
		}
	else
		{
		document.all("Tip").innerText="���"
		document.all("imgsrc").src="mix.gif";
		}

}
function ShiftStatus()
{
//���������ʱ��ӦΪ��С״̬
//��ǰ״̬Ϊ��չ״̬��Ҫ�л�����С״̬��
  if (g_Extended==true)
  {
	document.all("Tip").innerText="���"
	document.all("imgsrc").src="mix.gif";
	shrinkForm()
   }
   else
   {
	document.all("Tip").innerText="��׼��"
	document.all("imgsrc").src="max.gif";
	ExtendForm();
   }
   
   g_Extended=!g_Extended;
   
}
 