//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;

namespace Domain.Models
{
    [Serializable()]
    public partial class Employee : BaseModel
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool? Gender { get; set; }
        public string JobTitle { get; set; }
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
