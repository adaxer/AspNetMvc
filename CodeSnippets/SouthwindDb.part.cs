using Southwind.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Southwind.DataAccess
{
    public partial class SouthwindDb : ISouthwindDb
    {
        public SouthwindDb(string connection, IDictionary<string,string> args)
            : base(connection)
        {
            if(args.ContainsKey("Mode"))
            {
                if(args["Mode"] == "Online")
                {
                    this.Configuration.LazyLoadingEnabled = false;
                    this.Configuration.ProxyCreationEnabled = false;
                }
            }
        }
    }
}
