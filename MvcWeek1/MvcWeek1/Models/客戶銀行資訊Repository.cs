using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MvcWeek1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Where(p => p.Id == id).FirstOrDefault();
        }

        public IQueryable<客戶銀行資訊> Get客戶銀行資訊ListBy帳戶名稱(string name)
        {
            return this.All().Where(p => p.帳戶名稱.Contains(name));
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}