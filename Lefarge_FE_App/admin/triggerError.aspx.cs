﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Lefarge_FE_App
{
    public partial class triggerError : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            throw new InvalidOperationException("An InvalidOperationException " +
   "occurred in the Page_Load handler on the Default.aspx page.");
        }
    }
}