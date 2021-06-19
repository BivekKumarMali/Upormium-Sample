namespace Upormium.Util.StringConstants
{
    public class StringConstant : IStringConstant
    {

        #region Model Validation
        public string InvalidModelError
        {
            get
            {
                return "You have entered an invalid data.";
            }
        }
        public string InvalidLoginError
        {
            get
            {
                return "User or password is invalid";
            }
        }
        #endregion


        #region Roles

        public string Admin
        {
            get
            {
                return "Admin";
            }
        }
        #endregion
    }
}
