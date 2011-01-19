using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Tax666.SystemFramework
{
	/// <summary>
	/// 该类主要用于调试阶段。
	/// <remarks>
	///  开发各个逻辑层次时，在一些容易出错的关键操作位置，可以使用该类提供的方法输入一些条件进行检查；
	///  调试应用程序时，如果执行检查时发现条件没有被满足，就会向开发人员发出错误警告信息，
	///  并调用ApplicationLog类记录错误日志，以便开发人员能够快速了解并定位错误。
	/// </remarks>
	/// </summary>
    public class ApplicationAssert
    {
#if !DEBUG
		public const int LineNumber = 0;
#else
        public static int LineNumber
        {
            get
            {
                try
                {
                    return (new StackTrace(1, true)).GetFrame(0).GetFileLineNumber();
                }
                catch
                {
                }

                return 0;
            }
        }
#endif

        /// <summary>
        /// 检查给定的条件是否有效。
        /// </summary>
        /// <param name="condition">为True的被测试表达式</param>
        /// <param name="errorText">错误信息</param>
        /// <param name="lineNumber">错误行号</param>
        [ConditionalAttribute("DEBUG")]
        public static void Check(bool condition, String errorText, int lineNumber)
        {
            String currentTrace = String.Empty;
#if DEBUG
            StringBuilder message;
            String fileName;
            int currentLine;
            String sourceLine;
            StreamReader fileStream = null;
            bool openedFile = false;
            StackTrace curTrace;
            StackFrame curFrame;

            message = new StringBuilder();

            try
            {
                curTrace = new StackTrace(2, true);
                try
                {
                    curFrame = curTrace.GetFrame(0);

                    if ((String.Empty != (fileName = curFrame.GetFileName())) && (0 <= (currentLine = (lineNumber != 0) ? lineNumber : curFrame.GetFileLineNumber())))
                    {
                        message.Append(fileName).Append(", Line: ").Append(currentLine);

                        fileStream = new StreamReader(fileName);
                        openedFile = true;
                        do
                        {
                            sourceLine = fileStream.ReadLine();
                            --currentLine;
                        }
                        while (currentLine != 0);

                        message.Append("\r\n");

                        if (lineNumber != 0)
                        {
                            message.Append("Current executable line:");
                        }
                        else
                        {
                            message.Append("\r\n").Append("Next executable line:");
                        }

                        message.Append("\r\n").Append(sourceLine.Trim());
                    }
                }
                catch
                {
                }
                finally
                {
                    if (openedFile)
                        fileStream.Close();
                }
                currentTrace = message.ToString();
            }
            catch
            {
            }
#endif

            if (!condition)
            {
                //StringBuilder strBuilder;
                String detailMessage = String.Empty;
                detailMessage = currentTrace;
                StringBuilder strBuilder = new StringBuilder();
                strBuilder.Append("Assert: ").Append("\r\n").Append(errorText).Append("\r\n").Append(detailMessage);
                ApplicationLog.WriteWarning(strBuilder.ToString());
                System.Diagnostics.Debug.Fail(errorText, detailMessage);
            }
        }

        /// <summary>
        /// 验证所获取的请求条件。
        /// </summary>
        /// <param name="condition">为True的被测试表达式</param>
        /// <param name="errorText">错误信息</param>
        /// <param name="lineNumber">错误行号</param>
        /// <exception class="System.ApplicationException">检查条件失败</exception>
        public static void CheckCondition(bool condition, String errorText, int lineNumber)
        {
            if (!condition)
            {
                //调试时产生错误的详细信息；
                String currentTrace = String.Empty;
                StringBuilder message;
                String fileName;
                int currentLine;
                String sourceLine;
                StreamReader fileStream = null;
                bool openedFile = false;
                StackTrace curTrace;
                StackFrame curFrame;
                message = new StringBuilder();

                try
                {
                    curTrace = new StackTrace(2, true);
                    try
                    {
                        curFrame = curTrace.GetFrame(0);

                        if ((String.Empty != (fileName = curFrame.GetFileName())) && (0 <= (currentLine = (lineNumber != 0) ? lineNumber : curFrame.GetFileLineNumber())))
                        {
                            message.Append(fileName).Append(", Line: ").Append(currentLine);

                            fileStream = new StreamReader(fileName);
                            openedFile = true;
                            do
                            {
                                sourceLine = fileStream.ReadLine();
                                --currentLine;
                            }
                            while (currentLine != 0);

                            message.Append("\r\n");

                            if (lineNumber != 0)
                            {
                                message.Append("Current executable line:");
                            }
                            else
                            {
                                message.Append("\r\n").Append("Next executable line:");
                            }

                            message.Append("\r\n").Append(sourceLine.Trim());
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (openedFile)
                            fileStream.Close();
                    }
                    currentTrace = message.ToString();
                }
                catch
                {
                }

                String detailMessage = currentTrace;
                Debug.Fail(errorText, detailMessage);

                throw new ApplicationException(errorText);
            }
        }

    }
}