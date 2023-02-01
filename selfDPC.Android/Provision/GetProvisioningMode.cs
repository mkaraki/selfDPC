using Android.App.Admin;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace selfDPC.Android.Provision
{
    internal class GetProvisioningMode : Activity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var allowedMode = Intent?.GetIntegerArrayListExtra(DevicePolicyManager.ExtraProvisioningAllowedProvisioningModes);

            if (allowedMode == null || allowedMode.Count < 1)
            {
                // Not allowed any mode
                Toast.MakeText(this, "Unable to provisioning. Device didn't have approved mode.", ToastLength.Long)?
                    .Show();
                FinishWithCanceled();
                return;
            }

            if (allowedMode.Contains((Java.Lang.Integer)DevicePolicyManager.ProvisioningModeFullyManagedDevice))
            {
                Intent provisionIntent = new();
                provisionIntent.PutExtra(DevicePolicyManager.ExtraProvisioningMode, DevicePolicyManager.ProvisioningModeFullyManagedDevice);
                FinishWithIntent(provisionIntent);
                return;
            }
            else
            {
                Toast.MakeText(this, "Full managed device provisioning didn't allowed", ToastLength.Long)?.Show();
                FinishWithCanceled();
                return;
            }
        }

        public override void OnBackPressed()
        {
            FinishWithCanceled(false);
            base.OnBackPressed();
        }

        private void FinishWithCanceled(bool callFinish = true)
        {
            SetResult(Result.Canceled);
            if (callFinish) Finish();
        }

        private void FinishWithIntent(Intent intent)
        {
            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}
