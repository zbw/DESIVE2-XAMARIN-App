using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Desive2.Droid.DependencyServices;
using Desive2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionAndBuild_Android))]
namespace Desive2.Droid.DependencyServices
{
    public class VersionAndBuild_Android : IAppVersionAndBuild
    {
        PackageInfo _appInfo;
        public VersionAndBuild_Android()
        {
            var context = Android.App.Application.Context;
            _appInfo = context.PackageManager.GetPackageInfo(context.PackageName, 0);
        }
        public string GetVersionNumber()
        {
            return _appInfo.VersionName;
        }
        public string GetBuildNumber()
        {
            
            return _appInfo.VersionCode.ToString();
        }
    }
}