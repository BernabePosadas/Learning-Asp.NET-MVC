using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserTask.Objects.Static
{
    public static class SingletonObjects
    {
        public static MySQLClass DB = new MySQLClass("server=149.129.34.148;port=3306;database=BernabeDB;uid=BernabePosadas;password=423#TzLFDXLdniM!E#jc;");
        public static SHA256Hasher Hasher = new SHA256Hasher();
    }
}