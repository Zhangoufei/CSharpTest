namespace UploadScore
{
    using System;

    public enum EnumSubmitResult
    {
        CategoryHasInfo = -4,
        Failed = 0,
        HasSubCategory = -3,
        RepeatCode = -1,
        RepeatName = -2,
        Success = 1
    }
}

