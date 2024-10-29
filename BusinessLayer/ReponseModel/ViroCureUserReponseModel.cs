using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ReponseModel
{
    public class ViroCureUserReponseModel
    {
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public int Role { get; set; }
    }
    public class LoginReponseModel
    {
        public TokenModel Token { get; set; }
        public UserReponseModel User { get; set; }
        

    }

    public class UserReponseModel{
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public int Role { get; set; }
    }

}
