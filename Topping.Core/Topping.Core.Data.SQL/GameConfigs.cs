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
    
    public partial class GameConfigs
    {
        public GameConfigs()
        {
            this.GamesDetail = new HashSet<GamesDetail>();
        }
    
        public System.Guid Id { get; set; }
        public string XmlConfig { get; set; }
    
        public virtual ICollection<GamesDetail> GamesDetail { get; set; }
    }
}