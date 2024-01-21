using Data.Models;
using Data.Repositories.Interfaces;
using Data.Repository;
using Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Override any generic method for your own custom implemention, add new repository methods to the IEmployeeRepository.cs file
    }
}
