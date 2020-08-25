using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Model
{
    [Table("wcsinterfacelog")]
    public class InterfaceLog : BaseModels
    {
        public string InterfaceName { get { return InterfaceName; } set { InterfaceName = value; HandlerPropertyChanged("InterfaceName"); } }
        public string Request { get { return Request; } set { Request = value; HandlerPropertyChanged("Request"); } }
        public string Response { get { return Response; } set { Response = value; HandlerPropertyChanged("Response"); } }
        public string Flag { get { return Flag; } set { Flag = value; HandlerPropertyChanged("Flag"); } }
        public string Content { get { return Content; } set { Content = value; HandlerPropertyChanged("Content"); } }
        public string Remark { get { return Remark; } set { Remark = value; HandlerPropertyChanged("Remark"); } }

    }
}
