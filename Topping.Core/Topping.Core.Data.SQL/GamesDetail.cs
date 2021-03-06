//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Topping.Core.Data.SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class GamesDetail
    {
        public System.Guid GameId { get; set; }
        public Nullable<System.Guid> UserId { get; set; }
        public string DetailXml { get; set; }
        public Nullable<int> Total { get; set; }
        public Nullable<int> Solos { get; set; }
        public Nullable<int> Warnings { get; set; }
        public Nullable<int> Time { get; set; }
        public Nullable<double> Percentage { get; set; }
        public Nullable<int> TopLost { get; set; }
        public Nullable<int> Top { get; set; }
        public Nullable<int> Negative { get; set; }
        public Nullable<int> Selection { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<int> Status { get; set; }
        public System.DateTime Date { get; set; }
        public Nullable<System.Guid> GameConfigId { get; set; }
    
        public virtual GameConfigs GameConfigs { get; set; }
    }
}
