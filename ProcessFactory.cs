using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Evesting
{
    public class ProcessFactory
    {
        Dictionary<string, Type> processors;

        public ProcessFactory()
        {
            LoadTypesICanReturn();
        }

        public Processor CreateInstance(string processorName)
        {

            Type t = GetTypeToCreate(processorName);
            Console.WriteLine(t.ToString());
            if (t == null)
            {
                Console.WriteLine("No Processor with that name");
                Environment.Exit(1);
            }
            //return NullProcessor();

            return Activator.CreateInstance(t) as Processor;

        
        
        }

        Type GetTypeToCreate(string processorName)
        {
            foreach (var process in processors)
            {
                //Console.WriteLine(process.Key);
                if(process.Key.Contains(processorName))
                {
                    return processors[process.Key];
                }
                
            }
            return null;
        
        }
        void LoadTypesICanReturn()
        {
            processors = new Dictionary<string, Type>();

            Type[] typesInThisAssembly = Assembly.GetExecutingAssembly().GetTypes();

            foreach (Type type in typesInThisAssembly)
            {
                //if (type.GetMember(typeof(Processor).ToString()) != null)
                if (type.IsClass && typeof(Processor).IsAssignableFrom(type) && type.GetConstructor(Type.EmptyTypes) != null)
                //if (type.IsClass && type.BaseType != null && type.BaseType.GetGenericTypeDefinition() == typeof(Processor))
                {
                    processors.Add(type.Name, type);
                    Console.WriteLine(type);
                }

            }
        
        
        }


    }
}
