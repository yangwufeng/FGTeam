using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.APIModel
{
  public   class SRMStatusUpdateRequest
    {

        [Key]
        public int? Id { get; set; }

        private string srmNo;

        public string SrmNo { get; set; }
        private int srmStatus;

        public int SrmStatus { get; set; }
        private string faultCode;

        public string FaultCode { get; set; }
        private string fault;

        public string Fault { get; set; }
    }
}
