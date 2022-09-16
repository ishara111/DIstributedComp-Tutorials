using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceProvider.Models
{
    public class ServiceMethods
    {
        int token, num1, num2, num3;
        Authenticate auth;

        public ServiceMethods(int token,int num1,int num2,int num3, Authenticate auth)
        {
            this.token = token;
            this.num1 = num1;
            this.num2 = num2;
            this.num3 = num3;
            this.auth = auth;
        }

        public object AddTwoNumbers()
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                return num1 + num2;
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }

        public object AddThreeNumbers()
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                return num1 + num2 + num3;
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }
        public object MulTwoNumbers()
        {
            if ((auth.authenticate.Validate(token)).Equals("Validated"))
            {
                int tot = num1 * num2;
                return tot;
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }
        public object MulThreeNumbers()
        {
            if (auth.authenticate.Validate(token).Equals("Validated"))
            {
                return num1 * num2 * num3;
            }
            else
            {
                Error error = new Error();
                return error;
            }
        }

    }
}