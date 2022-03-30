using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDataBinding.Shared
{
    public  class C1GridStyle
    {
        public static String RowStyle { get; set; } = "background-color:#E5E6FA;color:#000";
        public static String AlternatingRowStyle { get; set; } = "background-color:#fff";
        public static String ColumnHeaderStyle { get; set; } = "background-color:#1565C0;color:#fff;font-size: 16px;";
        public static String GridStyle { get; set; } = "min-height:200px;width:100%;max-height:80vh";
    }

    
}
