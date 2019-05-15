namespace MvcWeek1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using MvcWeek1.DataTypeAttributes;
    using System.Linq;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var repo = RepositoryHelper.Get客戶聯絡人Repository();
            var contacts = repo.All().Where(p => p.客戶Id == this.客戶Id && p.Id != this.Id);
            if (contacts.Any(p => p.Email.Trim().ToUpper() == this.Email.Trim().ToUpper()))
            {
                yield return new ValidationResult("同一個客戶下的聯絡人，其Email不能重複"
                    , new string[] { "客戶Id", "Email" });
            }
            
        }
    }
    
    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [Required]
        //[RegularExpression(@"^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z]+$", ErrorMessage ="Email格式不正確")]
        [EmailAddress(ErrorMessage = "Email格式不正確")]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [驗證手機號碼格式]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

        public bool IsDeleted { get; set; }

        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
