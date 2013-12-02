//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AltairScope.DomainModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        public User()
        {
            this.Property_Change_Histories = new HashSet<Property_Change_History>();
            this.Property_Comments = new HashSet<Property_Comment>();
            this.Properties = new HashSet<Property>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string display_name { get; set; }
        public string email { get; set; }
        public Nullable<int> type { get; set; }
        public string password { get; set; }
        public bool is_activated { get; set; }
    
        public virtual ICollection<Property_Change_History> Property_Change_Histories { get; set; }
        public virtual ICollection<Property_Comment> Property_Comments { get; set; }
        public virtual ICollection<Property> Properties { get; set; }
    }
}
