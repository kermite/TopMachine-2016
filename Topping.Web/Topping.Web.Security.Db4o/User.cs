/// Copyright (c) 2008-2011 Brad Williams
/// 
/// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
/// associated documentation files (the "Software"), to deal in the Software without restriction,
/// including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense,
/// and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so,
/// subject to the following conditions:
/// 
/// The above copyright notice and this permission notice shall be included in all copies or substantial
/// portions of the Software.
/// 
/// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
/// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
/// IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
/// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
/// SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Configuration.Provider;
using System.Web.Security;
using Db4objects.Db4o.Config.Attributes;
using db4oProviders.Common;

namespace db4oProviders
{
    public class User : DataContainer
    {
        [Indexed]
        private Guid _PKID;

        public Guid PKID
        {
            get { return _PKID; }
            set { _PKID = value; }
        }

        [Indexed]
        private string _ApplicationName;

        public string ApplicationName
        {
            get { return _ApplicationName; }
            set { _ApplicationName = value; }
        }

        private string _Comment;

        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
        }
        private DateTime  _CreationDate;

        public DateTime  CreationDate 
        {
            get { return _CreationDate; }
            set { _CreationDate = value; }
        }
        [Indexed]
        private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        public int  FailedPasswordAnswerAttemptCount;
        public DateTime FailedPasswordAnswerAttemptWindowStart;
        public int  FailedPasswordAttemptCount;
        public DateTime  FailedPasswordAttemptWindowStart;
        private bool _IsApproved;

        public bool IsApproved
        {
            get { return _IsApproved; }
            set { _IsApproved = value; }
        }
        private bool _IsLockedOut;

        public bool IsLockedOut
        {
            get { return _IsLockedOut; }
            set { _IsLockedOut = value; }
        }
        private bool _IsOnLine;

        public bool IsOnLine
        {
            get { return _IsOnLine; }
            set { _IsOnLine = value; }
        }

        [Indexed]
        public DateTime _LastActivityDate;

        public DateTime LastActivityDate
        {
            get { return _LastActivityDate; }
            set { _LastActivityDate = value; }
        }
        public DateTime LastLockedOutDate;

        public DateTime LastLoginDate;
        public DateTime LastPasswordChangedDate;
        private string _Password;

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string PasswordAnswer;
        public string PasswordQuestion;
        [Indexed]
        public string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        public string _FirstName;

        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        public string _LastName;

        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }

        public User() 
        { 
        
        }

        public User(Guid pkid,
                    string username,
                    string firstname,
                    string lastname,
                    string password,
                    string email,
                    string passwordQuestion,
                    string passwordAnswer,
                    bool isApproved,
                    string comment,
                    DateTime creationDate,
                    DateTime lastPasswordChangedDate,
                    DateTime lastActivityDate,
                    string applicationName,
                    bool isLockedOut,
                    DateTime lastLockedOutDate,
                    int failedPasswordAttemptCount,
                    DateTime failedPasswordAttemptWindowStart,
                    int failedPasswordAnswerAttemptCount,
                    DateTime failedPasswordAnswerAttemptWindowStart)
        {
            PKID = pkid;
            UserName = username;
            FirstName = firstname; 
            LastName = lastname; 
            Password = password;
            Email = email;
            PasswordQuestion = passwordQuestion;
            PasswordAnswer = passwordAnswer;
            IsApproved = isApproved;
            Comment = comment;
            CreationDate = creationDate;
            LastPasswordChangedDate = lastPasswordChangedDate;
            LastActivityDate = lastActivityDate;
            ApplicationName = applicationName;
            IsLockedOut = isLockedOut;
            LastLockedOutDate = lastLockedOutDate;
            FailedPasswordAttemptCount = failedPasswordAttemptCount;
            FailedPasswordAttemptWindowStart = failedPasswordAttemptWindowStart;
            FailedPasswordAnswerAttemptCount = failedPasswordAnswerAttemptCount;
            FailedPasswordAnswerAttemptWindowStart = failedPasswordAnswerAttemptWindowStart;
        }

        public override string ToString()
        {
            return string.Format("User:{0}:{1}",
                                 UserName,
                                 ApplicationName);
        }

        public void UpdateFailureCount(string failureType, MembershipProvider provider)
        {
            DateTime windowStart;
            int  failureCount;

            if (failureType == "password")
            {
                windowStart = FailedPasswordAttemptWindowStart;
                failureCount = FailedPasswordAttemptCount;
            }
            else if (failureType == "passwordAnswer")
            {
                windowStart = FailedPasswordAnswerAttemptWindowStart;
                failureCount = FailedPasswordAnswerAttemptCount;
            }
            else
                throw new ProviderException("Invalid failure type");

            DateTime windowEnd = windowStart.AddMinutes(provider.PasswordAttemptWindow);

            if (failureCount == 0 || DateTime.Now > windowEnd)
            {
                // First password failure or outside of PasswordAttemptWindow. 
                // Start a new password failure count from 1 and a new window starting now.

                if (failureType == "password")
                {
                    FailedPasswordAttemptCount = 1;
                    FailedPasswordAttemptWindowStart = DateTime.Now;
                }
                else if (failureType == "passwordAnswer")
                {
                    FailedPasswordAnswerAttemptCount = 1;
                    FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
                }
            }
            else
            {
                if (failureCount++ >= provider.MaxInvalidPasswordAttempts)
                {
                    // Password attempts have exceeded the failure threshold. Lock out
                    // the user.

                    IsLockedOut = true;
                    LastLockedOutDate = DateTime.Now;
                }
                else
                {
                    // Password attempts have not exceeded the failure threshold. Update
                    // the failure counts. Leave the window the same.

                    if (failureType == "password")
                    {
                        FailedPasswordAttemptCount = failureCount;
                    }
                    else if (failureType == "passwordAnswer")
                    {
                        FailedPasswordAnswerAttemptCount = failureCount;
                    }
                }
            }
        }
    }
}
