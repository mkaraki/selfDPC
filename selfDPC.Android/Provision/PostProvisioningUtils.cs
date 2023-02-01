using Android.App.Admin;
using Android.Content;
using Android.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selfDPC.Android.Provision
{
    internal class PostProvisioningUtils
    {
        private Context context;
        private DevicePolicyManager devicePolicyManager;
        private ISharedPreferences sharedPreferences;

        private const string PREFS = "post-provisioning";
        private const string PREF_DONE = "done";

        public PostProvisioningUtils(Context _context)
        {
            context = _context;
            devicePolicyManager = context.GetSystemService(Context.DevicePolicyService) as DevicePolicyManager;
            sharedPreferences = context.GetSharedPreferences(PREFS, FileCreationMode.Private);
        }

        public void CompleteProvisioning()
        {
            if (IsDone)
                return;

            var componentName = DeviceAdminReceiver.GetComponentName(context);
            devicePolicyManager.SetProfileName(componentName, "selfDPC");

            devicePolicyManager.SetProfileEnabled(componentName);
        }

        public bool IsDone { get => sharedPreferences.GetBoolean(PREF_DONE, false); }
    }
}
