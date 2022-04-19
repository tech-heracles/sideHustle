using System;
using System.Diagnostics;
using System.Reflection;

namespace DbCore
{
    public class SpeedTest
    {
        string testImplement;
        object[] testParams;
        int repetitions;
        double average;
        string dllName, className;
        public SpeedTest(string dllName, string className) { 
         this.dllName = dllName;
        this.className = className;
        }
        public void SpeedTestMethod(string testImplement, object[] testParams, int repetitions)
        {
            this.testImplement = testImplement;
            this.testParams = testParams;
            this.repetitions = repetitions;
            this.average = 0;
        }
        public void SpeedTestMethod(string testImplement, object[] testParams)
        {
            this.testImplement = testImplement;
            this.testParams = testParams;
            this.repetitions = 10000;
            this.average = 0;
        }
        public void startTest()
        {
            try
            {
                Int64 sumTimes = 0;
                for (int i = 0, x = this.repetitions; i < x; i++)
                {
                    Stopwatch s1 = Stopwatch.StartNew();
                    this.TestMethod(this.testImplement, testParams);
                    s1.Stop();
                    sumTimes += s1.ElapsedMilliseconds;
                }
                this.average = sumTimes / this.repetitions;
                Debug.Write("Average execution across " + this.repetitions + ": " + this.average);
            }
            catch (MyException m)
            {
                Debug.Write(m.Message);
            }
            return;
        }
        public void TestMethod(string methodName, object[] testParams)
        {
            Assembly assembly = Assembly.Load(dllName);
            Type type = assembly.GetType(dllName+"."+className);
            if (type != null)
            {
                MethodInfo methodInfo = type.GetMethod(methodName);
                if (methodInfo != null)
                {
                    object result = null;
                    ParameterInfo[] parameters = methodInfo.GetParameters();
                    object classInstance = Activator.CreateInstance(type, null);
                    if (testParams.Length != parameters.Length)
                    {
                        throw new MyException("parameter number mismatch");
                    }
                    if (parameters.Length == 0)
                    {
                        //This works fine
                        result = methodInfo.Invoke(classInstance, null);
                    }
                    else
                    {
                        result = methodInfo.Invoke(classInstance, testParams);
                    }
                }
            }
        }
        //public void TestKonstruktor(string methodName, object[] testParams)
        //{
        //    Assembly assembly = Assembly.LoadFile("...Assembly1.dll");
        //    Type type = assembly.GetType("TestAssembly.Main");
        //    if (type != null)
        //    {
        //        MethodInfo methodInfo = type.GetConstructor(new[] { typeof(testParams) });
        //        if (methodInfo != null)
        //        {
        //            object result = null;
        //            ParameterInfo[] parameters = methodInfo.GetParameters();
        //            object classInstance = Activator.CreateInstance(type, null);
        //            if (testParams.Length != parameters.Length)
        //            {
        //                throw new MyException("parameter number mismatch");
        //            }
        //            if (parameters.Length == 0)
        //            {
        //                //This works fine
        //                result = methodInfo.Invoke(classInstance, null);
        //            }
        //            else
        //            {
        //                result = methodInfo.Invoke(methodInfo, testParams);
        //            }
        //        }
        //    }
        //}
    }
}
