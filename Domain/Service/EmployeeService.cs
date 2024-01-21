using AutoMapper;
using Data.UnitofWork;
using Domain.Service.Generic.SyncService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Service
{
    public class EmployeeService<Tv, Te> : GenericService<Tv, Te>
                                        where Tv : Models.Employee
                                        where Te : Data.Models.Employee
    {
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper) : base (unitOfWork, mapper)
        {
        }

        //add here any custom service method or override generic service method
        public bool DoNothing()
        {
            return true;
        }
    }
}
