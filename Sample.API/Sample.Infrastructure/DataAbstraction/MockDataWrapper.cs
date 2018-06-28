using System;
using System.Collections.Generic;
using System.Text;
using Sample.Domain.Abstract;

namespace Sample.Infrastructure
{

    /* functionality similar to EntityFrameowork's DBset; commands are issued and later executed 
     * against the dbset, ideally returning only when iterated over.
     */
    public class MockDataWrapper : IDataInteractor
    {

        public string ExportedValue = "";

        public void abandonRequest(Sample.Domain.Abstract.BusObjSuperclass s)
        { 
            throw new NotImplementedException();
        }



        private String queryLookup(BusObjSuperclass lookupObject)
        {
            // lookup type of lookupObject via reflection, map to pre-set sql load queries

            return "Select blah from ...";
            
        }

        public void rePullData(BusObjSuperclass s)
        {
            throw new NotImplementedException();
        }


        public void fetchData(BusObjSuperclass s)
        {
            String query = queryLookup(s);
            MockDataClass DS = new MockDataClass();
            String results = DS.ProduceDataSynchronous(query);
            //todo:  load object via publically available dictionary of loadable member fields or reflection on annotated members.
            ExportedValue = results;
        }
    }
}
