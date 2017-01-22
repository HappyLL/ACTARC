using System;
using System.Collections.Generic;

namespace ACTBase
{
    //创建的对象池是针对于某一类型的
    public class ObjectPool<T> where T : class , new()
    {
        //表示当前已经被回收的T对象的缓存列表
        private static List<T> m_list = new List<T>();
        private static int m_outCount = 0; //外部使用这个类的个数

        //向外部环境提供这个类的空对象
        public static T Checkout()
        {
            int count = m_list.Count - 1;
            m_outCount = m_outCount + 1;
            if (count > 0)
            {
                var ret = m_list[count];
                m_list.RemoveAt(count);

                return ret;
            }
            return new T();
        }

        //为一组数据提供对应的对象集合
        public static void CheckOutList<R>(List<T> list, List<R> datas)
        {
            foreach (var data in datas)
            {
                var t = Checkout();
                GFunction.Run(t, "Init" , data);
                list.Add(t);
            }
        }

        //为一组数据提供对应的对象映射
        public static void CheckOutList<R>(Dictionary<int , T> dic, List<R> datas)
        {
            foreach (var data in datas)
            {
                var t = Checkout();
                GFunction.Run(t , "Init" , data);

                int id = (int)GFunction.Run(t , "GetId");
                dic.Add(id , t);
            }
        }

        public static void CheckIn(T t)
        {
            m_outCount--;

            m_list.Add(t);
        }

        //已链表形式回收
        public static void CheckInList(List<T> list)
        {
            foreach (var item in list)
            {
                GFunction.Run(item , "Destroy");
                CheckIn(item);
            }

            list.Clear();
        }

        //已字典形式回收
        public static void CheckInList(Dictionary<int , T> dic)
        {
            foreach (var item in dic)
            {
                GFunction.Run(item.Value , "Destroy");

                CheckIn(item.Value);
            }

            dic.Clear();
        }

        //只是代表当前外部有对象没有回收(在资源清除时)
        public static bool MemoryLeak()
        {
            return m_outCount != 0;
        }

    }
}
