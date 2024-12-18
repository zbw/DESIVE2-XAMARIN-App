﻿using Desive2.iOS.Services;
using Desive2.Services;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(VersionAndBuild_iOS))]
namespace Desive2.iOS.Services
{
    public class VersionAndBuild_iOS : IAppVersionAndBuild
    {
        public string GetVersionNumber()
        {
            //var VersionNumber = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleShortVersionString")).ToString();   
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public string GetBuildNumber()
        {
            //var BuildNumber = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleVersion")).ToString();   
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }
    }
}