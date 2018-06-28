using System;
using Sample.Domain.Abstract;

namespace Sample.Domain.BusinessObjects
{
    public class SampleBusinessObject : BusObjSuperclass 
    {
        private string sampleField = "Test";

        public string getSampleField()
        {
            return sanitizeField();
        }

        private string sanitizeField()
        {
            return sampleField[0].ToString(); // mocked sanitization operation
        }

        public SampleBusinessObject(string s)
        {
            sampleField = s;
        }

        public SampleBusinessObject()
        {

        }
    }
}
