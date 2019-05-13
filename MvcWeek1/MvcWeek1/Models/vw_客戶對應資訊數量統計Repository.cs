using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MvcWeek1.Models
{   
	public  class vw_客戶對應資訊數量統計Repository : EFRepository<vw_客戶對應資訊數量統計>, Ivw_客戶對應資訊數量統計Repository
	{

	}

	public  interface Ivw_客戶對應資訊數量統計Repository : IRepository<vw_客戶對應資訊數量統計>
	{

	}
}