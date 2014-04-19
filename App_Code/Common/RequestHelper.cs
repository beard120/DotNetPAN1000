namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Web;

    public class RequestHelper
    {
        public static T Get<T>(params string[] @params)
        {
            PropertyInfo[] propertys = typeof(T).GetProperties();

            HttpRequest Request = HttpContext.Current.Request;

            T ins = Activator.CreateInstance<T>();
            for (int i = 0; i < propertys.Length; i++)
            {
                PropertyInfo pi = propertys[i];

                if (@params.Contains(pi.Name))
                {
                    Type pp = pi.PropertyType;
                    string value = Request[pi.Name];

                    if (pp.IsValueType)
                    {
                        if (value != null)
                        {
                            object co = Convert.ChangeType(value, pp);
                            pi.SetValue(ins, co, null);
                        }
                    }
                    else
                    {

                        object co = Convert.ChangeType(value, pp);
                        pi.SetValue(ins, co, null);
                    }
                }
            }
            return ins;
        }



        public static T GetWithout<T>(params string[] @params)
        {
            PropertyInfo[] propertys = typeof(T).GetProperties();

            HttpRequest Request = HttpContext.Current.Request;

            T ins = Activator.CreateInstance<T>();
            for (int i = 0; i < propertys.Length; i++)
            {
                PropertyInfo pi = propertys[i];

                if (!@params.Contains(pi.Name))
                {
                    Type pp = pi.PropertyType;
                    string value = Request[pi.Name];

                    if (pp.IsValueType)
                    {
                        if (value != null)
                        {
                            object co = Convert.ChangeType(value, pp);
                            pi.SetValue(ins, co, null);
                        }
                    }
                    else
                    {

                        object co = Convert.ChangeType(value, pp);
                        pi.SetValue(ins, co, null);
                    }
                }


            }

            return ins;
        }


        public static T Get<T>()
        {
            PropertyInfo[] propertys = typeof(T).GetProperties();

            HttpRequest Request = HttpContext.Current.Request;

            T ins = Activator.CreateInstance<T>();
            for (int i = 0; i < propertys.Length; i++)
            {
                PropertyInfo pi = propertys[i];

                Type pp = pi.PropertyType;

                string value = Request[pi.Name];

                if (pp.IsValueType)
                {
                    if (value != null)
                    {

                        object co = Convert.ChangeType(value, pp);
                        pi.SetValue(ins, co, null);
                    }
                }
                else
                {
                    object co = Convert.ChangeType(value, pp);
                    pi.SetValue(ins, co, null);
                }
            }
            return ins;
        }
    }




}