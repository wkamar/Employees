using Data.Models;
using Data.Repositories.Interfaces;
using Data.Repository;
using Data.UnitofWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentRepository(IUnitOfWork unitOfWork) : base (unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //Override any generic method for your own custom implemention, add new repository methods to the IDepartmentRepository.cs file
    }

}
