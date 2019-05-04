namespace HuaweiARInternal
{
    using System;
    using UnityEngine;
    using System.Runtime.InteropServices;
    using HuaweiARUnitySDK;
    internal static class UtilConstants
    {
#if UNITY_EDITOR_WIN
        public const string HWAugmentedImageCliBinaryName = "ImageDatabaseTool";
#elif UNITY_EDITOR_LINUX
        public const string HWAugmentedImageCliBinaryName = "ImageDatabaseTool_linux";
#elif UNITY_EDITOR_OSX
        public const string HWAugmentedImageCliBinaryName = "NoToolInMac";
#endif

#if UNITY_EDITOR_OSX
        public const string ARCoreAugmentedImageCliBinaryName = "augmented_image_cli_osx";
#elif UNITY_EDITOR_WIN
        public const string ARCoreAugmentedImageCliBinaryName = "augmented_image_cli_win";
#elif UNITY_EDITOR_LINUX
        public const string ARCoreAugmentedImageCliBinaryName = "augmented_image_cli_linux";
#endif
    }
}