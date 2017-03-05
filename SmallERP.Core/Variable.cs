using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallERP.Core
{
    public class Variable
    {
        public const int DB_INSERT_SUCCESS = 1;
        public const int DB_UPDATE_SUCCESS = 2;
        public const int DB_QUERRY_SUCCESS = 3;
        public const int DB_INSERT_ERROR = -100;
        public const int DB_UPDATE_ERROR = -101;
        public const int DB_QUERRY_ERROR = -102;
    }
}
