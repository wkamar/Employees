using Data.Models;
using Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories.Interfaces
{
    public interface IDepartmentRepository : IRepository<Department>
    {
        //Add any additional repository methods other than the generic ones (GetAll, GetById, Delete, Add)
    }
}
