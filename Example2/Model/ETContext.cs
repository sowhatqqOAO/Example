using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Example2.Model
{
    /// <summary>
    /// 初始化：name=>WebConfig 資料庫連線字串名稱
    /// </summary>
    public class ETContext : DbContext
    {
        /// <summary>
        /// 初始化：name=>WebConfig 資料庫連線字串名稱
        /// </summary>
        public ETContext() : base("name=DefaultConnectionString") { }
        //public DbSet<Sys_Module> Sys_Module { get; set; }//模組

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //避免Code First建立Table時將Table Name加入複數名稱(s)
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}