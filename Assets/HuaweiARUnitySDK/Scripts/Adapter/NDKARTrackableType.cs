
namespace HuaweiARInternal
{
    using System;

    public enum NDKARTrackableType
    {
        //Type of ARCore 
        Invalid = 0,
        BaseTrackable = 0x41520100,
        Plane = 0x41520101,
        Point = 0x41520102, //103???
        AugmentedImage = 0x41520104,
        //0x41520105????


        //Type of AREngine 

        Hand = 0x50000000,
        Body = 0x50000001,
        Face = 0x50000002,
    }
}
