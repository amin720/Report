//------------------------------------------------------------------------------
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
    
    public partial class STP_GetDetailedAccountBalance4Columns_Result
    {
        public Nullable<int> Code { get; set; }
        public string Name { get; set; }
        public Nullable<long> TotalDebtor { get; set; }
        public Nullable<long> TotalCreditor { get; set; }
        public Nullable<long> RemainDebtor { get; set; }
        public Nullable<long> RemainCreditor { get; set; }
        public Nullable<bool> OperationType { get; set; }
    }
}
