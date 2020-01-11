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
            

            return Activator.CreateInstance(t) as Processor;

        
        
        }

        Type GetTypeToCreate(string processorName)
        {
            foreach (var process in processors)
            {
                
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
                
                if (type.IsClass && typeof(Processor).IsAssignableFrom(type) && type.GetConstructor(Type.EmptyTypes) != null)
                {
                    processors.Add(type.Name, type);
                    
                }

            }
        
        
        }


    }
}
