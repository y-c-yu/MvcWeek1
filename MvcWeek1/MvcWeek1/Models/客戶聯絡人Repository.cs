using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MvcWeek1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(p=>!p.IsDeleted);
        }
        public override void Delete(客戶聯絡人 entity)
        {
            entity.IsDeleted = true;
            base.UnitOfWork.Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            //base.Delete(entity);
        }
        public 客戶聯絡人 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<客戶聯絡人> Get客戶聯絡人ListBy姓名(string name)
        {
            return this.All().Where(p => p.姓名.Contains(name));
        }
        public IQueryable<客戶聯絡人> Get客戶聯絡人ListBy職稱(string name)
        {
            return this.All().Where(p => p.職稱.Contains(name));
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}