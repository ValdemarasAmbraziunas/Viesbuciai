using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPPro.App_Start
{
    public class Singleton<T> where T : class
    {
        private static T instance;

        private Singleton() { }

        public static T Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = Activator.CreateInstance(typeof(T)) as T;
                }
                return instance;
            }
        }
    }
}