using System;
using Roslyn.Scripting;
using Roslyn.Scripting.CSharp;

namespace RoslynSamples
{
    class Program
    {

        public static string FileName = "Program.cs";
        static void Main(string[] args)
        {

            //Executing a simple C# statement passed as a string.
            {
                string statement = @"System.Console.WriteLine("" Hello World!!!"")";
                var engine = new ScriptEngine();
                engine.Execute(statement);
            }


            //Previous example does not use any execution context. So there is no sharing of execution context when multiple statements are executed. 
            //In other words, one statement does not have access to other statement(variable/functions etc).
            //To share the variable/functions etc declared in one statement , with other statements, a session is used which acts as an execution context.

            {
                var engine = new ScriptEngine();
                var session = Session.Create();
                engine.Execute(@"var statement = ""Hello World!!!"";", session);
                engine.Execute(@"System.Console.WriteLine(statement);", session);
            }
            //Session object worsks as a container and holds the object declared. If you do not pass session object as a second parameter in the Execute(), this code will throw an exception that statement 
            //does not exist in the current context. 

            //Interaction with the host object -
            //Interaction with the host application means, accessing the value from hosting application which is executing the script.
            //For this, you need an host obejct which expose any thing which need to be passed to the script, by a public property/Function.

            {
                hostObj hostObj = new hostObj();
                var engine = new ScriptEngine(new string[]{ hostObj.GetType().Assembly.Location });
                var session = Session.Create(hostObj);
                engine.Execute(@"System.Console.WriteLine(""File name is ""+GetFileName())",session);
            }
            //In this example, script is accessing a variable calles 'FileName' from the host Application through host object.

            //Executing a Script from a file.
            {
                var engine = new ScriptEngine(new string[] { "System" });
                engine.ExecuteFile(@"Script1.csx");
            }

            Console.ReadLine();

        }
    }

    public class hostObj
    {
        public string GetFileName()
        {
            return Program.FileName;
        }
    }
}
