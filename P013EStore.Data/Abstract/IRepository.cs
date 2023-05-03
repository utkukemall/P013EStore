using System;
using System.Collections.Generic;
using System.Linq.Expressions; // Kendi lambda Expression(x=>x.) kullanabileceğimz metotları yazmamızı sağlayan kütüphane. 


namespace P013EStore.Data.Abstract
{
    public interface IRepository<T> where T : class // IRepository interface i dışarıdan alacağı T tipinde bir parametreyle çalışacak ve where şartı ile bu T nin veri tipi bir class olmalıdır dedik.
    {
    }
}
