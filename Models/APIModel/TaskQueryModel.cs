using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.APIModel
{
    public class TaskQueryModel
    {
        public List<string> TaskNos { get; set; }
    }

    public class TaskSelectModel
    {
        public string TaskNo { get; set; }
    }
}
