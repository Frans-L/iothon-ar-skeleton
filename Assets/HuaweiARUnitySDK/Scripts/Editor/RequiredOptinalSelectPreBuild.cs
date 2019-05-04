namespace HuaweiARInternal
{
    using UnityEditor.Build;
    using UnityEditor;
    using UnityEngine;
    internal class RequiredOptinalSelectPreBuild : IPreprocessBuild
    {
        public int callbackOrder
        {
            get
            {
                return 0;
            }
        }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            bool isHuaweiARReruired = HuaweiARProjectSettings.Instance.IsHuaweiARRequired;
            bool isARCoreRequired = HuaweiARProjectSettings.Instance.IsARCoreRequired;

            Debug.LogFormat("Building application with {0} HuaweiAR support, with {1} arcore support",
                isHuaweiARReruired ? "Required" : "Optional", isARCoreRequired ? "Required" : "Optional");
            PluginHelper.GetImporterByPluginName("HUAWEI AR Engine Plugin_Required.aar")
                .SetCompatibleWithPlatform(BuildTarget.Android, isHuaweiARReruired);
            PluginHelper.GetImporterByPluginName("HUAWEI AR Engine Plugin_Optional.aar")
                .SetCompatibleWithPlatform(BuildTarget.Android, !isHuaweiARReruired);
            PluginHelper.GetImporterByPluginName("google_ar_required.aar")
                .SetCompatibleWithPlatform(BuildTarget.Android, isARCoreRequired);
            PluginHelper.GetImporterByPluginName("google_ar_optional.aar")
                .SetCompatibleWithPlatform(BuildTarget.Android, !isARCoreRequired);
        }
    }
}
