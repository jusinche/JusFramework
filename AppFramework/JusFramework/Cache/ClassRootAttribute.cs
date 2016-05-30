using System;
using System.Collections.Generic;

namespace JusFramework.Cache
{
    public class ClassRootAttribute: Attribute
    {
        public List<Type> EditableRoot { get; private set; }


        public ClassRootAttribute(params Type[] types)
        {
             EditableRoot=new List<Type>();
            foreach (var type in types)
            {
                EditableRoot.Add(type);
            }
        }
    }
}
