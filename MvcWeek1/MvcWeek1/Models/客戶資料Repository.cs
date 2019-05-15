using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcWeek1.Models
{
    public class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
    {
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => !p.IsDeleted);
        }
        public override void Delete(客戶資料 entity)
        {
            entity.IsDeleted = true;
            base.UnitOfWork.Context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            //base.Delete(entity);
        }
        public 客戶資料 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<客戶資料> Get客戶資料ListBy客戶名稱(string name)
        {
            return this.All().Where(p => p.客戶名稱.Contains(name));
        }

        public IQueryable<客戶資料> Get客戶資料ListBy客戶分類(string name)
        {
            return this.All().Where(p => p.客戶分類 == name);
        }

        public IQueryable<string> Get客戶分類List()
        {
            return (from ss in this.All()
                    where !(ss.客戶分類 == null || ss.客戶分類.Trim() == string.Empty)
                    select ss.客戶分類).Distinct();
        }
    }

    public interface I客戶資料Repository : IRepository<客戶資料>
    {

    }
}