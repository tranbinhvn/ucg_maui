namespace UCG.siteTRAXLite.Utils
{
    public static class PermisionChecking
    {
        public static async Task<bool> CheckPermissions<T>(T permission) where T : Permissions.BasePermission
        {
            var permissionStatus = await permission.CheckStatusAsync();
            bool request = false;
            if (permissionStatus == PermissionStatus.Denied)
            {
                if (DeviceInfo.Platform == DevicePlatform.iOS)
                {

                    var title = $"{permission} Permission";
                    var question = $"To use this plugin the {permission} permission is required. Please go into Settings and turn on {permission} for the app.";
                    var positive = "Settings";
                    var negative = "Maybe Later";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        AppInfo.Current.ShowSettingsUI();
                    }

                    return false;
                }

                request = true;

            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                var newStatus = await permission.RequestAsync();
                if (newStatus != PermissionStatus.Granted)
                {
                    var title = $"{permission} Permission";
                    var question = $"To use the plugin the {permission} permission is required.";
                    var positive = "Settings";
                    var negative = "Maybe Later";
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        AppInfo.Current.ShowSettingsUI();
                    }
                    return false;
                }
            }

            return true;
        }
    }
}
