using System.Reflection;
using System.Runtime.CompilerServices;

//
// �йس��򼯵ĳ�����Ϣ��ͨ������
// ���Լ����Ƶģ�������Щ����ֵ���޸�����򼯡�
// ��������Ϣ��
//

[assembly: AssemblyTitle("ccflow")]
[assembly: AssemblyDescription("ccflow4.5.0.0")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("���ϳ۳���Ϣ�������޹�˾")]
[assembly: AssemblyProduct("ccflow4.5")]
[assembly: AssemblyCopyright("�۳ҹ�����2003-2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]	
 
//
// ���򼯵İ汾��Ϣ������ 4 ��ֵ���:
//
//      ���汾
//      �ΰ汾 
//      �ڲ��汾��
//      �޶���
//
// ������ָ��������Щֵ��Ҳ����ʹ�á��޶��š��͡��ڲ��汾�š���Ĭ��ֵ�������ǰ�
// ������ʾʹ�� '*':

[assembly: AssemblyVersion("2003.06.7.2")]

//
// Ҫ�Գ��򼯽���ǩ��������ָ��Ҫʹ�õ���Կ���йس���ǩ���ĸ�����Ϣ����ο�
// Microsoft .NET Framework �ĵ���
//
// ʹ����������Կ�������ǩ������Կ��
//
// ע��: 
//   (*) ���δָ����Կ������򼯲��ᱻǩ����
//   (*) KeyName ��ָ�Ѿ���װ��
//       ������ϵļ��ܷ����ṩ����(CSP)�е���Կ��KeyFile ��ָ����
//       ��Կ���ļ���
//   (*) ��� KeyFile �� KeyName ֵ����ָ������
//       ��������Ĵ���: 
//       (1) ����� CSP �п����ҵ� KeyName����ʹ�ø���Կ��
//       (2) ��� KeyName �����ڶ� KeyFile ���ڣ��� 
//           KeyFile �е���Կ��װ�� CSP �в���ʹ�ø���Կ��
//   (*) Ҫ���� KeyFile������ʹ�� sn.exe(ǿ����)ʵ�ù��ߡ�
//        ��ָ�� KeyFile ʱ��KeyFile ��λ��Ӧ��
//        ����ڡ���Ŀ���Ŀ¼������Ŀ���
//        Ŀ¼��λ��ȡ����������ʹ�ñ�����Ŀ���� Web ��Ŀ��
//        ���ڱ�����Ŀ����Ŀ���Ŀ¼����Ϊ
//       <Project Directory>\obj\<Configuration>�����磬��� KeyFile λ�ڸ�
//       ��ĿĿ¼�У�Ӧ�� AssemblyKeyFile 
//       ����ָ��Ϊ [assembly: AssemblyKeyFile("..\\..\\mykey.snk")]
//        ���� Web ��Ŀ����Ŀ���Ŀ¼����Ϊ.
//       %HOMEPATH%\VSWebCache\<Machine Name>\<Project Directory>\obj\<Configuration>��
//   (*) ���ӳ�ǩ������һ���߼�ѡ�� - �й����ĸ�����Ϣ������� Microsoft .NET Framework
//       �ĵ���
//

[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]
[assembly: AssemblyVersion("2003.06.03.1")]

