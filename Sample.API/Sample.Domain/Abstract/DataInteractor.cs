using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Domain.Abstract
{
    public interface IDataInteractor
    {
        void rePullData(BusObjSuperclass s);
        void fetchData(BusObjSuperclass s);
        void abandonRequest(BusObjSuperclass s);

    }
}
