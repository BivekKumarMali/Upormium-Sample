using System;

namespace Upormium.Util.StringConstants
{
    public interface IStringConstant
    {
        #region Model Validation
        string InvalidModelError { get; }
        string InvalidLoginError { get; }
        #endregion

        #region Roles

        string Admin { get; }
        #endregion
    }
}
