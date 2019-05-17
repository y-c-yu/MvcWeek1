using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcWeek1.ViewModels
{
    public class Customer
    {
        public int Id { get; set; }
        public string 客戶名稱 { get; set; }
        public string 統一編號 { get; set; }
        public string 電話 { get; set; }
        public string 傳真 { get; set; }
        public string 地址 { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
        public string 客戶分類 { get; set; }
    }
}