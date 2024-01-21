using AutoMapper;
using Data;
using Data.UnitofWork;
using Domain.Service.Generic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Service.Generic.AsyncService
{
    public class GenericService<Tv, Te> : IServiceAsync<Tv, Te> where Tv : BaseModel where Te : Data.BaseModel
    {
        protected IUnitOfWork _unitOfWork;
        protected IMapper Mapper;
        public GenericService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public virtual async Task<IEnumerable<Tv>> GetAll()
        {
            var entities = await _unitOfWork.GetRepositoryAsync<Te>()
                .GetAll();
            return Mapper.Map<IEnumerable<Tv>>(source: entities);
        }

        public virtual async Task<Tv> GetOne(int id)
        {
            var entity = await _unitOfWork.Context.Set<Te>().FindAsync(id);
            return Mapper.Map<Tv>(source: entity);
        }

        public virtual async Task<Te> Add(Tv view)
        {
            var entity = Mapper.Map<Te>(source: view);
            await _unitOfWork.GetRepositoryAsync<Te>().Insert(entity);
            await _unitOfWork.SaveAsync();
            return entity;
        }

        public async Task<int> Update(Tv view)
        {
            await _unitOfWork.GetRepositoryAsync<Te>().Update(Mapper.Map<Te>(source: view));
            return await _unitOfWork.SaveAsync();
        }

        public virtual async Task<int> Remove(int idd)
        {
            Te entity = await _unitOfWork.Context.Set<Te>().FindAsync(idd);
            await _unitOfWork.GetRepositoryAsync<Te>().Delete(idd);
            return await _unitOfWork.SaveAsync();
        }

        public virtual async Task<IEnumerable<Tv>> Get(Expression<Func<Te, bool>> predicate)
        {
            var items = await _unitOfWork.GetRepositoryAsync<Te>()
                .Get(predicate: predicate);
            return Mapper.Map<IEnumerable<Tv>>(source: items);
        }
    }
}
