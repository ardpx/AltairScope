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
    
    public partial class Property_Evaluation
    {
        public System.Guid property_id { get; set; }
        public Nullable<decimal> return_rate_y1 { get; set; }
        public Nullable<decimal> return_rate_y2 { get; set; }
        public Nullable<decimal> return_rate_y3 { get; set; }
        public Nullable<decimal> return_rate_y4 { get; set; }
        public Nullable<decimal> return_rate_y5 { get; set; }
        public Nullable<decimal> return_rate_y6 { get; set; }
        public Nullable<decimal> return_rate_y7 { get; set; }
        public Nullable<decimal> return_rate_y8 { get; set; }
        public Nullable<decimal> return_rate_y9 { get; set; }
        public Nullable<decimal> return_rate_y10 { get; set; }
        public Nullable<int> cash_flow_y1 { get; set; }
        public Nullable<int> cash_flow_y2 { get; set; }
        public Nullable<int> cash_flow_y3 { get; set; }
        public Nullable<int> cash_flow_y4 { get; set; }
        public Nullable<int> cash_flow_y5 { get; set; }
        public Nullable<int> cash_flow_y6 { get; set; }
        public Nullable<int> cash_flow_y7 { get; set; }
        public Nullable<int> cash_flow_y8 { get; set; }
        public Nullable<int> cash_flow_y9 { get; set; }
        public Nullable<int> cash_flow_y10 { get; set; }
        public Nullable<decimal> appreciation_rate { get; set; }
        public Nullable<decimal> vacancy_ratio_first_year { get; set; }
        public Nullable<decimal> vacancy_ratio_subsequent_years { get; set; }
        public Nullable<int> initial_cash { get; set; }
    
        public virtual Property Property { get; set; }
    }
}
