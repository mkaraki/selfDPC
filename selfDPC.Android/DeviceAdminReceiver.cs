using Android.Content;
using selfDPC.Android.Provision;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin = Android.App.Admin;

namespace selfDPC.Android
{
    internal class DeviceAdminReceiver : Admin.DeviceAdminReceiver
    {
        public override void OnProfileProvisioningComplete(Context context, Intent intent)
        {
            var postprovutl = new PostProvisioningUtils(context);
            base.OnProfileProvisioningComplete(context, intent);
        }

        public static ComponentName GetComponentName(Context context)
        {
            return new ComponentName(context.ApplicationContext, Java.Lang.Class.FromType(typeof(DeviceAdminReceiver)));
        }
    }
}
