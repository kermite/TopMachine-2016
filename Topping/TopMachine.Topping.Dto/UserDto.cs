namespace TopMachine.Topping.Dto
{
    using System;
    using System.Collections.Generic;

    public class UserDto
    {
        private string _Email;
        public string _FirstName;
        public DateTime _LastLoginDate;
        public string _LastName;
        private string _UserName;
		private bool _isAdmin;

        public string Email
        {
            get
            {
                return this._Email;
            }
            set
            {
                this._Email = value;
            }
        }

        public string FirstName
        {
            get
            {
                return this._FirstName;
            }
            set
            {
                this._FirstName = value;
            }
        }

        public DateTime LastLoginDate
        {
            get
            {
                return this._LastLoginDate;
            }
            set
            {
                this._LastLoginDate = value;
            }
        }

        public string LastName
        {
            get
            {
                return this._LastName;
            }
            set
            {
                this._LastName = value;
            }
        }
		       
        public string UserName
        {
            get
            {
                return this._UserName;
            }
            set
            {
                this._UserName = value;
            }
        }

		public bool IsAdmin
		{
			get
			{
				return _isAdmin;
			}

			set
			{
				_isAdmin = value;
			}
		}
	}
}

