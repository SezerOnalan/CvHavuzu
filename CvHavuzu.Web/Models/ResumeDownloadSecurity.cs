using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CvHavuzu.Web.Models
{
    public enum ResumeDownloadSecurity
    {
        FreeDownload = 1,
        NamePhoneEmailRequired = 2,
        MembershipRequired = 3
    }
}
