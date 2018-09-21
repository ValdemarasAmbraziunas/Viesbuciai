using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITPPro.App_Start
{
    public class ContainerBuilder
    {
        protected Dictionary<Type, Type> types;

        public ContainerBuilder()
        {
            types = new Dictionary<Type, Type>();
        }

        public void RegisterType<TInterface, TImplementation>()
        {
            try
            {
                types.Add(typeof(TInterface), typeof(TImplementation));
            }
            catch (ArgumentException) { }
        }

        public Type Resolve(Type resolveFor)
        {
            foreach (var type in types)
            {
                if (resolveFor == type.Key)
                {
                    return type.Value;
                }
            }
            return null;
        }
    }
}
