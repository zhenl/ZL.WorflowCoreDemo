using System;
using System.Collections.Generic;
using System.Text;

namespace ZL.WorflowCoreDemo.InputDictionary
{
    public class ManualWorkflowData
    {
        public Dictionary<string, object> MyDic;

        public string Name { get; set; }

        public ManualWorkflowData()
        {
            MyDic = new Dictionary<string, object>();
        }
    }
}
