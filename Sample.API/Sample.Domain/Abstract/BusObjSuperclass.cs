using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Domain.Abstract
{
    public abstract class BusObjSuperclass
    {
        // attributes with getters/setters


        // todo: 
        // expose a dictionary of public members for data load, or leave that up to reflection.
        public void PreSave()
        {
            // finalize changes?
        }


        public void PostLoad()
        {
            // load from database
        }
               
    }
}
