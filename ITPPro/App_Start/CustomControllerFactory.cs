using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ITPPro.App_Start
{
    public class CustomControllerFactory : DefaultControllerFactory
    {
        private readonly ContainerBuilder containerBuilder;

        public CustomControllerFactory()
        {
            containerBuilder = Singleton<ContainerBuilder>.Instance;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            IController controller = Instantiate(controllerType) as IController;
            return controller;
        }

        /// <summary>
        /// creates instance using constructor with most arguments
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private object Instantiate(Type type)
        {
            object instance;

            //rekursiškai atsidūrėm taške kai turi būt tuščias konstruktorius
            if (type.GetConstructors().Count() == 1 && type.GetConstructors().First().GetParameters().Count() == 0)
            {
                return Activator.CreateInstance(type);
            }
            else
            {
                var longestConstructor = type.GetConstructors()
                    .OrderByDescending(x => x.GetParameters().Length).FirstOrDefault();

                ParameterInfo[] parameters = longestConstructor.GetParameters();
                object[] instances = new object[parameters.Count()];
                int i = 0;
                foreach (var parameter in parameters)
                {
                    Type parameterType = containerBuilder.Resolve(parameter.ParameterType);
                    if (parameterType != null)
                    {
                        instances[i++] = Instantiate(parameterType);
                    }
                }
                instance = Activator.CreateInstance(type, instances);
            }

            return instance;
        }
    }
}