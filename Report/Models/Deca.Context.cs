﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Report.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DecaFinancialEntities : DbContext
    {
        public DecaFinancialEntities()
            : base("name=DecaFinancialEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VW_AccountingDocumentPrint> VW_AccountingDocumentPrint { get; set; }
    
        public virtual ObjectResult<GetColumnsOfTable_Result> GetColumnsOfTable(string tableName)
        {
            var tableNameParameter = tableName != null ?
                new ObjectParameter("TableName", tableName) :
                new ObjectParameter("TableName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetColumnsOfTable_Result>("GetColumnsOfTable", tableNameParameter);
        }
    
        public virtual ObjectResult<STP_GetCertainAccountBalance4Columns_Result> STP_GetCertainAccountBalance4Columns(Nullable<int> accoutingYear, Nullable<int> userID, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var accoutingYearParameter = accoutingYear.HasValue ?
                new ObjectParameter("AccoutingYear", accoutingYear) :
                new ObjectParameter("AccoutingYear", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<STP_GetCertainAccountBalance4Columns_Result>("STP_GetCertainAccountBalance4Columns", accoutingYearParameter, userIDParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<STP_GetDetailedAccountBalance4Columns_Result> STP_GetDetailedAccountBalance4Columns(Nullable<int> accoutingYear, Nullable<int> userID, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var accoutingYearParameter = accoutingYear.HasValue ?
                new ObjectParameter("AccoutingYear", accoutingYear) :
                new ObjectParameter("AccoutingYear", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<STP_GetDetailedAccountBalance4Columns_Result>("STP_GetDetailedAccountBalance4Columns", accoutingYearParameter, userIDParameter, startDateParameter, endDateParameter);
        }
    
        public virtual ObjectResult<STP_GetCertainAccountBalance4Columns_Result> STP_GetTotalAccountBalance4Columns(Nullable<int> accoutingYear, Nullable<int> userID, Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate)
        {
            var accoutingYearParameter = accoutingYear.HasValue ?
                new ObjectParameter("AccoutingYear", accoutingYear) :
                new ObjectParameter("AccoutingYear", typeof(int));
    
            var userIDParameter = userID.HasValue ?
                new ObjectParameter("UserID", userID) :
                new ObjectParameter("UserID", typeof(int));
    
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<STP_GetCertainAccountBalance4Columns_Result>("STP_GetTotalAccountBalance4Columns", accoutingYearParameter, userIDParameter, startDateParameter, endDateParameter);
        }
    }
}
