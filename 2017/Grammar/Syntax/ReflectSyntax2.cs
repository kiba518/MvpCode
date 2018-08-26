using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Syntax
{
    public class Client
    {
        public void ExcuteGetNameCommand()
        {
            Proxy proxy = new Proxy();
            GetNameCommand cmd = new GetNameCommand();
            ResultBase rb = proxy.ExcuteCommand(cmd);
        } 
    } 
    public class Proxy
    {
        public ResultBase ExcuteCommand(CommandBase command)
        {
            var result = HandlerSwitcher.Excute(command);
            return result as ResultBase;
        }
    }
    public class HandlerSwitcher
    {
        private const string methodName = "Excute";//约定的方法名
        private const string classNamePostfix = "Handler";//约定的处理Command的类的名称的后缀 
        //获取命名空间的名称
        public static string GetNameSpace(CommandBase command)
        {
            Type commandType = command.GetType();//获取完全限定名
            string[] CommandTypeNames = commandType.ToString().Split('.');
            string nameSpace = "";
            for (int i = 0; i < CommandTypeNames.Length - 1; i++)
            {
                nameSpace += CommandTypeNames[i];
                if (i < CommandTypeNames.Length - 2)
                {
                    nameSpace += ".";
                }
            } 
            return nameSpace;
        }

        public static object Excute(CommandBase command)
        {
            string fullName = command.GetType().FullName;//完全限定名
            string nameSpace = GetNameSpace(command);//命名空间  
            Assembly assembly = Assembly.Load(nameSpace);
            Type handlerType = assembly.GetType(fullName + classNamePostfix, true, false);
            object obj = assembly.CreateInstance(fullName + classNamePostfix);
            MethodInfo handleMethod = handlerType.GetMethod(methodName);//获取函数基本信息
            object[] pmts = new object[] { command }; //传递一个参数command
            try
            {
                return handleMethod.Invoke(obj, pmts);
            }
            catch (TargetInvocationException tie)
            {
                throw tie.InnerException;
            }
        }
    }
    public class GetNameCommandHandler
    {
        public ResultBase Excute(CommandBase cmd)
        {
            GetNameCommand command = (GetNameCommand)cmd;
            ResultBase result = new ResultBase();
            result.Message = "I'm Kiba518";
            return result;
        }
    }
    public class GetNameCommand: CommandBase
    {  
    } 
    public class CommandBase
    { 
        public int UserId { get; set; } 
         
        public string UserName { get; set; } 
        
        public string ArgIP { get; set; } 
    }
    public class ResultBase
    { 
        public string Message { get; set; } 
    }
}

   
 
