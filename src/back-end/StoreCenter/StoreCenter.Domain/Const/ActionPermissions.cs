using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreCenter.Domain.Const
{
    public static class ActionPermissions
    {
        // For Roles Controller
        public const string RolesViewAll = "Roles:ViewAll";
        public const string RolesViewById = "Roles:ViewById";
        public const string RolesCreate = "Roles:Create";
        public const string RolesUpdate = "Roles:Update";
        public const string RolesDelete = "Roles:Delete";

        public const string ViewReports = "ViewReports";
        public const string EditProfile = "EditProfile";
        public const string ManageUsers = "ManageUsers";

        //public const string View = "View";
        //public const string Create = "Create";
        //public const string Update = "Update";
        //public const string Delete = "Delete";

    }
}
