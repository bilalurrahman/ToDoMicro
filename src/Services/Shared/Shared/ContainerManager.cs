using System;
using System.Collections.Concurrent;


namespace SharedKernal
{
    public class ContainerManager
    {        
        public static IServiceProvider Container { get; set; }
        private static ConcurrentDictionary<int, Type> services = new ConcurrentDictionary<int, Type>();
       
        public static void AddKeyToService(int key, Type Service)
        { 
            services.TryAdd(key, Service);
        }

        public static void AddKeyToService(int[] keys, Type Service)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                services.TryAdd(keys[i], Service);
            }
        }

        public static object GetInstanceByKey(int key)
        {
            return Container.GetService(services[key]);
        }
    }
}
