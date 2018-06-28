using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Domain.Abstract
{
    public interface ICRUDWebApi
    {
        void Auth();
        void Create();
        void Read();
        void Update();
        void Delete();
    }
}
