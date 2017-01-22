using System;
using System.Collections;

namespace ACTBase
{
    public static class GFunction
    {
        //利用反射 调用Inst对象的FunName方法
        public static Object Run(Object Inst, string FunName, Object obj1 = null, Object obj2 = null, Object obj3 = null)
        {
            var method = Inst.GetType().GetMethod(FunName);
            if (method == null)
            {
                return null;
            }

            Object[] obj = null;
            if (obj1 == null) { }
            else if (obj2 == null)
            {
                obj = new Object[1];
                obj[0] = obj1;
            }
            else if (obj3 == null)
            {
                obj = new Object[2];
                obj[0] = obj1;
                obj[1] = obj2;
            }
            else
            {
                obj = new Object[3];
                obj[0] = obj1;
                obj[1] = obj2;
                obj[2] = obj3;
            }

            return method.Invoke(Inst , obj);
        }
    }
}
