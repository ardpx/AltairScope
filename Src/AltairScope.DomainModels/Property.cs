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
    
    public partial class Property
    {
        public Property()
        {
            this.Property_Change_Histories = new HashSet<Property_Change_History>();
            this.Property_Comments = new HashSet<Property_Comment>();
        }
    
        public System.Guid id { get; set; }
        public string mls { get; set; }
        public string address { get; set; }
        public Nullable<int> square_foot { get; set; }
        public Nullable<int> taxable_additions { get; set; }
        public Nullable<int> taxable_total { get; set; }
        public Nullable<int> taxable_tax { get; set; }
        public int hoa { get; set; }
        public Nullable<int> fmv_zestimate { get; set; }
        public Nullable<int> fmv_smartzip { get; set; }
        public Nullable<int> fmv_eappraisal { get; set; }
        public Nullable<int> fmv_mean { get; set; }
        public Nullable<int> rental_zrent { get; set; }
        public Nullable<int> rental_rentometer { get; set; }
        public Nullable<int> rental_zilpy { get; set; }
        public Nullable<int> rental_mean { get; set; }
        public Nullable<System.Guid> neighbourhood_id { get; set; }
        public Nullable<decimal> tax_rate { get; set; }
        public Nullable<decimal> zillow_growth_rate { get; set; }
        public Nullable<System.DateTime> create_date { get; set; }
        public Nullable<int> created_by { get; set; }
        public Nullable<System.Guid> last_update_id { get; set; }
        public Nullable<int> bed { get; set; }
        public Nullable<decimal> addition_total_ratio { get; set; }
    
        public virtual Property_Sale Property_Sale { get; set; }
        public virtual ICollection<Property_Change_History> Property_Change_Histories { get; set; }
        public virtual ICollection<Property_Comment> Property_Comments { get; set; }
        public virtual Property_Evaluation Property_Evaluation { get; set; }
        public virtual Neighbourhood Neighbourhood { get; set; }
        public virtual Property_Change_History Last_Property_Change { get; set; }
        public virtual User Creator { get; set; }
    }
}
